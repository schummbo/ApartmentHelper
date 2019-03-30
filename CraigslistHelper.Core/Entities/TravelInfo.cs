using System.Collections.Generic;

namespace CraigslistHelper.Core.Entities
{
    public class TravelInfo
    {
        public int? TravelTimeSeconds { get; set; }

        public int NumberOfBuses { get; set; }

        public List<int> WalkingTimes { get; set; }
    }
}
