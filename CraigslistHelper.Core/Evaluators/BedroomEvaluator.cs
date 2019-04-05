using System;
using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public class BedroomEvaluator : BaseEvaluator
    {
        public BedroomEvaluator(Range perfectRange, Range acceptableRange) : base(perfectRange, acceptableRange)
        {
        }

        public override double Evaluate(ApartmentListing listing)
        {
            return EvaluateInternal(listing.Housing?.Bedrooms);
        }
    }
}
