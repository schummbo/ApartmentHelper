namespace CraigslistHelper.Core.Evaluators
{
    public class Range
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public bool IsBetweenInclusive(int value)
        {
            return value >= Min && value <= Max;
        }
    }
}
