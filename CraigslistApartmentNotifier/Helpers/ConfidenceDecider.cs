﻿namespace CraigslistApartmentNotifier.Helpers
{
    using Entities;

    public class ConfidenceDecider
    {
        public ConfidenceLevel GetConfidenceLevel(ApartmentListing listing)
        {
            ConfidenceLevel level = ConfidenceLevel.Low;

            if (!string.IsNullOrWhiteSpace(listing.Origin))
            {
                level = ConfidenceLevel.Medium;
            }

            if (listing.TravelInfo.TravelTimeSeconds.HasValue && listing.TravelInfo?.TravelTimeSeconds.Value <= 3600)
            {
                level = ConfidenceLevel.High;
            }

            return level;
        }
    }
}
