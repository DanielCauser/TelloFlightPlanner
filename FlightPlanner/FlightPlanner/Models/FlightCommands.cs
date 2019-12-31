using FlightPlanner.Services;

namespace FlightPlanner.Models
{
    public class FlightCommands
    {
        public InFlightCommandEnum Action { get; set; }
        public int Value { get; set; }
    }
}