    namespace CraigslistApartmentNotifier.Helpers
{
    using System;
    using Entities;

    public class ConfidenceDecider
    {
        public ConfidenceLevel GetConfidenceLevel(ApartmentListing listing)
        {
            ConfidenceLevel level = ConfidenceLevel.Unknown;

            if (!string.IsNullOrWhiteSpace(listing.Origin))
            {
                level = ConfidenceLevel.Low;
            }

            if (listing.TravelInfo.TravelTimeSeconds.HasValue && listing.TravelInfo?.TravelTimeSeconds.Value <= 3600)
            {
                level = ConfidenceLevel.Medium;

                if (string.Equals(listing.CityName, "seattle", StringComparison.InvariantCultureIgnoreCase))
                {
                    level = ConfidenceLevel.High;

                    if (listing.TravelInfo.NumberOfBuses == 1)
                    {
                        level = ConfidenceLevel.Perfect;
                    }
                }
            }

            return level;
        }
    }
}
