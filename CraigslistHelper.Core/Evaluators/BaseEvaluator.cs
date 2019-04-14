using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public abstract class BaseEvaluator
    {
        protected const int Perfect = 10;
        protected const int Acceptable = 5;
        protected const int NotGreat = 1;
        protected const int NoGo = 0;

        protected Range PerfectRange { get; set; }
        protected Range AcceptableRange { get; set; }

        protected Settings Settings { get; set; }

        public bool Disabled { get; set; }

        protected BaseEvaluator(Range perfectRange, Range acceptableRange, Settings setting)
        {
            PerfectRange = perfectRange;
            AcceptableRange = acceptableRange;
            Settings = setting;
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
