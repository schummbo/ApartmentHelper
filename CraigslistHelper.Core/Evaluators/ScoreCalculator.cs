using System.Collections.Generic;
using System.Linq;
using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Evaluators
{
    public class ScoreCalculator
    {
        private readonly List<BaseEvaluator> _evaluators;

        public ScoreCalculator(List<BaseEvaluator> evaluators)
        {
            _evaluators = evaluators;
        }

        public double GetApartmentScore(ApartmentListing listing)
        {
            double score = 0;

            foreach (var evaluator in _evaluators.Where(e => !e.Disabled))
            {
                score += evaluator.Evaluate(listing);
            }

            return score;
        }
    }
}
