using System.Collections.Generic;

namespace CraigslistHelper.Core.Entities
{
    public class ApartmentListing
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public TravelInfo TravelInfo { get; set; }

        public string Origin { get; set; }

        public int? Price { get; set; }

        public HousingInfo Housing { get; set; }

        public string CityName { get; set; }

        public double Score { get; set; }

        public List<string> ScoreReasons { get; set; } = new List<string>();

        public string Body { get; set; }
    }
}
