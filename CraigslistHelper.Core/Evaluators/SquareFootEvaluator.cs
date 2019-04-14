using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public class SquareFootEvaluator : BaseEvaluator
    {
        public SquareFootEvaluator(Range perfectRange, Range acceptableRange, Settings settings) : base(perfectRange, acceptableRange, settings)
        {
        }

        public override double Evaluate(ApartmentListing listing)
        {
            var score = EvaluateInternal(listing.Housing?.SqFt);

            listing.ScoreReasons.Add($"Square Foot Score: {score}");

            return score;
        }
    }
}
