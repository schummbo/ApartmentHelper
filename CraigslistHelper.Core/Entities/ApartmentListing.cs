namespace CraigslistHelper.Core.Entities
{
    public class ApartmentListing
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public TravelInfo TravelInfo { get; set; }

        public string Origin { get; set; }

        public ConfidenceLevel ConfidenceLevel { get; set; }

        public int? Price { get; set; }

        public HousingInfo Housing { get; set; }

        public string CityName { get; set; }
    }
}
