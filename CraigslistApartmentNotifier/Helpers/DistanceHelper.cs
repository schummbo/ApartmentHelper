namespace CraigslistApartmentNotifier.Helpers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web;
    using Newtonsoft.Json.Linq;
    using PageEntities;

    public class DistanceHelper
    {
        public TravelInfo GetTravelInfo(ApartmentListing listing)
        {
            TravelInfo travelInfo = new TravelInfo();

            travelInfo.TravelTimeSeconds = GetTravelDuration(listing);

            travelInfo.NumberOfTransfers = GetNumberOfTransfers(listing);

            return travelInfo;
        }

        private static int GetNumberOfTransfers(ApartmentListing listing)
        {
            return 1;
        }

        private static int? GetTravelDuration(ApartmentListing listing)
        {
            if (listing.Origin == null)
            {
                return null;
            }

            string encodedOrigin = HttpUtility.UrlEncode(listing.Origin);

            // need to make sure this isn't a weekend
            DateTime tomorrow = DateTime.Now.AddDays(1);
            DateTime next9 = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 9, 0, 0);


            int tomorrowAt9 = (int) ConvertToUnixTimestamp(next9);

            string url =
                $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={encodedOrigin}&destinations=redacted&mode=transit&units=imperial&arrival_time={tomorrowAt9}&traffic_model=pessimistic&transit_routing_preference=fewer_transfers&key=AIzaSyDZZuzZB3Sx4ptSHuKGGQGWpC1cNLM6OCQ";


            WebClient webClient = new WebClient();
            string json = webClient.DownloadString(url);

            JObject j = JObject.Parse(json);

            JToken rows = j["rows"];

            int travelDuration = 0;

            if (rows.Any())
            {
                JToken elements = j["rows"][0]["elements"];

                if (elements.Any())
                {
                    JToken firstElement = elements[0];
                    JToken duration = firstElement["duration"];

                    if (duration != null)
                    {
                        travelDuration = duration["value"].ToObject<int>();
                    }
                }
            }

            if (travelDuration == 0)
                return null;

            return travelDuration;
        }

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}
