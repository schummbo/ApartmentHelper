using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public abstract class BaseEvaluator
    {
        private const int Perfect = 10;
        private const int Acceptable = 5;
        private const int NotGreat = 1;
        private const int NoGo = 0;

        protected Range PerfectRange;
        protected Range AcceptableRange;

        public bool Disabled { get; set; }

        protected BaseEvaluator(Range perfectRange, Range acceptableRange)
        {
            PerfectRange = perfectRange;
            AcceptableRange = acceptableRange;
        }

        public abstract double Evaluate(ApartmentListing listing);

        protected double EvaluateInternal(double? value)
        {
            if (!value.HasValue)
            {
                return NoGo;
            }

            if (PerfectRange.IsBetweenInclusive(value.Value))
            {
                return Perfect;
            }

            if (AcceptableRange.IsBetweenInclusive(value.Value))
            {
                return Acceptable;
            }

            return NotGreat;
        }
    }
}
