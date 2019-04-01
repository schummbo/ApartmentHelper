using System.Collections.Generic;
using System.Net;
using System.Web;
using CraigslistHelper.Core.Entities;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace CraigslistHelper.Core.Processors
{
    public class CityProcessor : BaseProcessor
    {
        private readonly string _geocodeApiKey;

        public CityProcessor(Settings config) : base(config)
        {
            _geocodeApiKey = config.googleGeocodeApiKey;
        }

        public string GetCityName(ApartmentListing listing)
        {
            if (listing.Origin == null)
            {
                return null;
            }

            string encodedOrigin = HttpUtility.UrlEncode(listing.Origin);

            string url =
                $"https://maps.googleapis.com/maps/api/geocode/json?latlng={encodedOrigin}&key={_geocodeApiKey}";

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

        public override void Parse(HtmlNode node, ApartmentListing listing)
        {
            var cityName = GetCityName(listing);
            listing.CityName = cityName;
        }
    }
}
