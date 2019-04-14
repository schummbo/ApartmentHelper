using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Web;
using CraigslistHelper.Core.Entities;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
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

            Thread.Sleep(250);

            var coords = listing.Origin.Split(',');

            string url =
                $"https://us1.locationiq.com/v1/reverse.php?key={_geocodeApiKey}&lat={coords[0]}&lon={coords[1]}&format=json";

            WebClient webClient = new WebClient();
            string json = webClient.DownloadString(url);

            dynamic origin = JObject.Parse(json);

            var city = origin?.address?.city;

            return city;
        }

        public override void Parse(HtmlNode node, ApartmentListing listing)
        {
            var cityName = GetCityName(listing);
            if (!string.IsNullOrWhiteSpace(cityName))
                listing.CityName = cityName;
        }
    }
}
