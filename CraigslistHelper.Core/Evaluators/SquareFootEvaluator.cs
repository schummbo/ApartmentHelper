using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public class SquareFootEvaluator : BaseEvaluator
    {
        public SquareFootEvaluator(Range perfectRange, Range acceptableRange) : base(perfectRange, acceptableRange)
        {
        }

        public override double Evaluate(ApartmentListing listing)
        {
            return EvaluateInternal(listing.Housing?.SqFt);
        }
    }
}
