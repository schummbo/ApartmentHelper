namespace CraigslistHelper.Core.Evaluators
{
    public class Range
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public bool IsBetweenInclusive(double value)
        {
            return value >= Min && value <= Max;
        }
    }
}
