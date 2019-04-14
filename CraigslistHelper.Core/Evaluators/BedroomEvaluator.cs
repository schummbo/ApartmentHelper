using System;
using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public class BedroomEvaluator : BaseEvaluator
    {
        public BedroomEvaluator(Range perfectRange, Range acceptableRange, Settings setting) : base(perfectRange, acceptableRange, setting)
        {
        }

        public override double Evaluate(ApartmentListing listing)
        {
            var score =  EvaluateInternal(listing.Housing?.Bedrooms);

            listing.ScoreReasons.Add($"Bedroom Score: {score}");

            return score;
        }
    }
}
