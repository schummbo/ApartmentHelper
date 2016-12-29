namespace CraigslistApartmentNotifier.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Web;
    using Entities;
    using Newtonsoft.Json.Linq;

    public class RouteHelper
    {
        public TravelInfo GetTravelInfo(ApartmentListing listing)
        {
            TravelInfo travelInfo = new TravelInfo();

            JObject googleResponse = GetGoogleDirectionsResponse(listing);

            travelInfo.TravelTimeSeconds = GetTravelDuration(googleResponse);

            travelInfo.NumberOfBuses = GetNumberOfBuses(googleResponse);

            return travelInfo;
        }

        private JObject GetGoogleDirectionsResponse(ApartmentListing listing)
        {
            string apiKey = ConfigurationManager.AppSettings["GoogleDirectionsApiKey"];
            string destination = ConfigurationManager.AppSettings["Destination"];

            string encodedDestination = HttpUtility.UrlEncode(destination);
            string encodedOrigin = HttpUtility.UrlEncode(listing.Origin);

            // need to make sure this isn't a weekend
            DateTime tomorrow = DateTime.Now.AddDays(1);
            DateTime next9 = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 9, 0, 0);
            
            int tomorrowAt9 = (int)ConvertToUnixTimestamp(next9);

            string url =
                $"https://maps.googleapis.com/maps/api/directions/json?origin={encodedOrigin}&destination={encodedDestination}&mode=transit&arrival_time={tomorrowAt9}&transit_routing_preference=fewer_transfers&key={apiKey}";

            WebClient client = new WebClient();
            string json = client.DownloadString(url);

            return JObject.Parse(json);
        }

        private static int GetNumberOfBuses(JObject googleResponse)
        {
            dynamic resp = googleResponse;

            IEnumerable<dynamic> steps = ((IEnumerable) resp.routes[0].legs[0].steps).Cast<dynamic>();

            IEnumerable<dynamic> transitSteps = steps.Where(x => x.travel_mode == "TRANSIT");

            return transitSteps.Count();
        }

        private static int? GetTravelDuration(JObject googleResponse)
        {
            dynamic resp = googleResponse as dynamic;

            return resp.routes[0].legs[0].duration.value;
        }

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}
