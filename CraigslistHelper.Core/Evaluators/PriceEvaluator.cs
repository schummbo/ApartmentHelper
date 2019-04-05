using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public class PriceEvaluator : BaseEvaluator
    {
        public PriceEvaluator(Range perfectRange, Range acceptableRange) : base(perfectRange, acceptableRange)
        {
        }

        public override double Evaluate(ApartmentListing listing)
        {
            return EvaluateInternal(listing.Price);
        }
    }
}
