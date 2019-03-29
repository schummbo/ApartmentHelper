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

            if (listing.Housing.Bedrooms.HasValue || listing.Housing.SqFt.HasValue)
            {
                level = ConfidenceLevel.Medium;
            }

            if (listing.Housing.Bedrooms.HasValue && listing.Housing.SqFt.HasValue)
            {
                level = ConfidenceLevel.High;
            }



            return level;
        }
    }
}
