using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public class TravelTimeEvaluator : BaseEvaluator
    {
        public TravelTimeEvaluator(Range perfectRange, Range acceptableRange) : base(perfectRange, acceptableRange)
        {
        }

        public override double Evaluate(ApartmentListing listing)
        {
            return EvaluateInternal(listing.TravelInfo.TravelTimeSeconds);
        }
    }
}
