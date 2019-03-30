namespace CraigslistHelper.Core.Settings
{
    public class Settings
    {
        public string googleGeocodeApiKey { get; set; }
        public string googleDirectionsApiKey { get; set; }
        public string destination { get; set; }
        public Craigslistoptions craigslistOptions { get; set; }
    }

    public class Craigslistoptions
    {
        public bool hasImage { get; set; }
        public bool postedToday { get; set; }
        public bool bundleDuplicates { get; set; }
        public bool includeNearby { get; set; }
        public Milesfromzip milesFromZip { get; set; }
        public Price price { get; set; }
        public Bedrooms bedrooms { get; set; }
        public Sqft sqFt { get; set; }
        public bool catsOk { get; set; }
        public bool dogsOk { get; set; }
        public bool furnished { get; set; }
        public bool noSmoking { get; set; }
        public bool wheelchairAccess { get; set; }
        public object[] housingTypes { get; set; }
    }

    public class Milesfromzip
    {
        public int miles { get; set; }
        public int zip { get; set; }
    }

    public class Price
    {
        public int min { get; set; }
        public int max { get; set; }
    }

    public class Bedrooms
    {
        public int min { get; set; }
        public int max { get; set; }
    }

    public class Sqft
    {
        public int min { get; set; }
        public int max { get; set; }
    }

}
