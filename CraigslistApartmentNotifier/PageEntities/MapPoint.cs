namespace CraigslistApartmentNotifier.PageEntities
{
    public class MapPoint
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public static implicit operator string(MapPoint point)
        {
            if (point == null)
            {
                return null;
            }

            return $"{point.Latitude}, {point.Longitude}";
        }
    }
}
