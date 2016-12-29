namespace CraigslistApartmentNotifier.Helpers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Web;
    using Entities;
    using Newtonsoft.Json.Linq;

    public class CityHelper
    {
        public string GetCityName(ApartmentListing listing)
        {
            if (listing.Origin == null)
            {
                return null;
            }

            string apiKey = ConfigurationManager.AppSettings["GoogleGeocodeApiKey"];

            string encodedOrigin = HttpUtility.UrlEncode(listing.Origin);

            string url =
                $"https://maps.googleapis.com/maps/api/geocode/json?latlng={encodedOrigin}&key={apiKey}";

            WebClient webClient = new WebClient();
            string json = webClient.DownloadString(url);

            dynamic origin = JObject.Parse(json);

            IEnumerable<dynamic> results = origin.results;

            foreach (dynamic result in results)
            {
                foreach (dynamic addressComponent in result.address_components)
                {
                    foreach (dynamic type in addressComponent.types)
                    {
                        if (type.ToString() == "locality")
                        {
                            return addressComponent.long_name;
                        }
                    }
                }
            }

            return null;
        }
    }
}
