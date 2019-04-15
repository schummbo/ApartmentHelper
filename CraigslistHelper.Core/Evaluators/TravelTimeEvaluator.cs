using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public class TravelTimeEvaluator : BaseEvaluator
    {
        public TravelTimeEvaluator(Range perfectRange, Range acceptableRange, Settings settings) : base(perfectRange, acceptableRange, settings)
        {
        }

        public override double Evaluate(ApartmentListing listing)
        {
            var score = EvaluateInternal(listing.TravelInfo?.TravelTimeSeconds);

            listing.ScoreReasons.Add($"Travel Time Score: {score}");

            return score;
        }
    }
}
