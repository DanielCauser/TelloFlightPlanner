using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FlightPlanner.Models;

namespace FlightPlanner.Services
{
    public interface IAircraftService
    {
        Task ExecuteFlightPlan(Item flightCommands, CancellationToken cancellationToken);
        Task SendCommand(FlightCommands flightCommand);
        void Shutdown();
    }
}