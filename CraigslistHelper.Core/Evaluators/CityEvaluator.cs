using System.Linq;
using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public class CityEvaluator : BaseEvaluator
    {
        public CityEvaluator(Range perfectRange, Range acceptableRange, Settings settings) : base(perfectRange, acceptableRange, settings)
        {
        }

        public override double Evaluate(ApartmentListing listing)
        {
            double score;

            score = this.Settings.keyCities.Contains(listing.CityName) 
                                            ? Perfect 
                                            : NoGo;

            listing.ScoreReasons.Add($"City Score: {score}");
            return score;
        }
    }
}
