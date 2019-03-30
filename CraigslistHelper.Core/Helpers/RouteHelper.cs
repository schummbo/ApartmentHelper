using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using CraigslistHelper.Core.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace CraigslistHelper.Core.Helpers
{
    public class RouteHelper
    {
        private readonly Settings _config;

        public RouteHelper(Settings config)
        {
            _config = config;
        }

        public TravelInfo GetTravelInfo(ApartmentListing listing)
        {
            TravelInfo travelInfo = new TravelInfo();

            if (listing.Origin == null || string.IsNullOrWhiteSpace(_config.destination))
            {
                return travelInfo;
            }

            JObject googleResponse = GetGoogleDirectionsResponse(listing);

            travelInfo.TravelTimeSeconds = GetTravelDuration(googleResponse);

            travelInfo.NumberOfBuses = GetNumberOfBuses(googleResponse);

            travelInfo.WalkingTimes = GetTotalWalkingTimes(googleResponse);

            return travelInfo;
        }

        private List<int> GetTotalWalkingTimes(JObject googleResponse)
        {
            dynamic resp = googleResponse;

            IEnumerable<dynamic> steps = ((IEnumerable)resp.routes[0].legs[0].steps).Cast<dynamic>();

            IEnumerable<dynamic> walkingSteps = steps.Where(x => x.travel_mode == "WALKING");

            return walkingSteps.Select(ws => Convert.ToInt32(ws.duration.value))
                                .Cast<int>()
                                .ToList();
        }

        private JObject GetGoogleDirectionsResponse(ApartmentListing listing)
        {
            string encodedDestination = HttpUtility.UrlEncode(_config.destination);
            string encodedOrigin = HttpUtility.UrlEncode(listing.Origin);

            DateTime today = DateTime.Today;

            int daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextMonday = today.AddDays(daysUntilNextMonday);
            DateTime nextMondayAt9 = new DateTime(nextMonday.Year, nextMonday.Month, nextMonday.Day, 9, 0, 0);
            
            int mondayAt9Unix = (int)ConvertToUnixTimestamp(nextMondayAt9);

            string url =
                $"https://maps.googleapis.com/maps/api/directions/json?origin={encodedOrigin}&destination={encodedDestination}&mode=transit&arrival_time={mondayAt9Unix}&transit_routing_preference=fewer_transfers&key={_config.googleDirectionsApiKey}";

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
