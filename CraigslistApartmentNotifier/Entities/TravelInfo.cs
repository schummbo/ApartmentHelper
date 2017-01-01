namespace CraigslistApartmentNotifier.Entities
{
    using System.Collections.Generic;

    public class TravelInfo
    {
        public int? TravelTimeSeconds { get; set; }

        public int NumberOfBuses { get; set; }

        public List<int> WalkingTimes { get; set; }
    }
}
