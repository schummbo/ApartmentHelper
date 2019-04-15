using CraigslistHelper.Core.Evaluators;

namespace CraigslistHelper.Core.Entities
{
    public class EvaluatorDefinition
    {
        public string Type { get; set; }
        public Range PerfectRange { get; set; }
        public Range AcceptableRange { get; set; }
        public bool Disabled { get; set; }
    }
}
