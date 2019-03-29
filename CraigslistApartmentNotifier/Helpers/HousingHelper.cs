using System;
using System.Text.RegularExpressions;
using CraigslistApartmentNotifier.Entities;

namespace CraigslistApartmentNotifier.Helpers
{
    public class HousingHelper
    {
        public HousingInfo GetHousingInfo(Title listing)
        {
            var housingInfo = new HousingInfo();

            if (string.IsNullOrWhiteSpace(listing.Housing))
            {
                return housingInfo;
            }

            try
            {
                var regex = new Regex("(([0-9]*)br)?\\s*-*\\s*(([0-9]*)ft2)?");

                var match = regex.Match(listing.Housing.Trim());



                if (int.TryParse(match.Groups[2].Value, out int bedrooms))
                {
                    housingInfo.Bedrooms = bedrooms;
                }

                if (int.TryParse(match.Groups[4].Value, out int sqft))
                {
                    housingInfo.SqFt = sqft;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return housingInfo;
        }
    }
}
