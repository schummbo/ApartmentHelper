using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public class PriceEvaluator : BaseEvaluator
    {
        public PriceEvaluator(Range perfectRange, Range acceptableRange, Settings settings) : base(perfectRange, acceptableRange, settings)
        {
        }

        public override double Evaluate(ApartmentListing listing)
        {
            var score = EvaluateInternal(listing.Price);

            listing.ScoreReasons.Add($"Price Score: {score}");

            return score;
        }
    }
}
