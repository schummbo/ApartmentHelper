using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public class PriceEvaluator
    {
        private readonly Range _perfectRange;
        private readonly Range _acceptableRange;

        private const int Perfect = 10;
        private const int Acceptable = 5;
        private const int NotGreat = 1;
        private const int NoGo = 0;

        public PriceEvaluator(Range perfectRange, Range acceptableRange)
        {
            _perfectRange = perfectRange;
            _acceptableRange = acceptableRange;
        }

        public double Evaluate(ApartmentListing listing)
        {
            if (!listing.Price.HasValue)
            {
                return NoGo;
            }

            if (_perfectRange.IsBetweenInclusive(listing.Price.Value))
            {
                return Perfect;
            }

            if (_acceptableRange.IsBetweenInclusive(listing.Price.Value))
            {
                return Acceptable;
            }

            return NotGreat;
        }
    }
}
