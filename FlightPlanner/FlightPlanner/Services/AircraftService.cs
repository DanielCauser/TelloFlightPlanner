using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightPlanner.Models;

namespace FlightPlanner.Services
{
    public class AircraftService : IAircraftService
    {
        private readonly Socket _sock;
        private readonly IPEndPoint _endPoint;
        private int _defaultAirCraftSpeed;

        public AircraftService()
        {
            _sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _endPoint = new IPEndPoint(IPAddress.Parse("192.168.10.1"), 8889);
            _defaultAirCraftSpeed = 40;
        }

        public async Task ExecuteFlightPlan(Item flightCommands, CancellationToken cancellationToken)
        {
            ExecuteCommand(nameof(FlightControlCommandEnum.command));
            ExecuteCommand($"{nameof(FlightControlCommandEnum.speed)} {_defaultAirCraftSpeed}");
            ExecuteCommand(nameof(FlightControlCommandEnum.takeoff));

            foreach (var command in flightCommands.FlightCommands)
            {
                if (cancellationToken.IsCancellationRequested)
                    throw new TaskCanceledException();

                await SendCommand(command);
            }

            ExecuteCommand(nameof(FlightControlCommandEnum.land));
        }

        public async Task SendCommand(FlightCommands flightCommand)
        {
            // We need to calculate how long we want to wait 
            // for a given command to finish execution.
            await Task.Delay(TimeSpan.FromSeconds(6));
            var command = $"{flightCommand.Action} {flightCommand.Value}";
            ExecuteCommand(command);
        }

        public void Shutdown()
        {
            ExecuteCommand(nameof(FlightControlCommandEnum.land));
        }

        private void ExecuteCommand(string command)
        {
            var sendBuffer = Encoding.ASCII.GetBytes(command);
            var result = _sock.SendTo(sendBuffer, _endPoint);
            Debug.WriteLine($"R: {result} Executed command: {command}");
        }

        private void TestFlight()
        {
            string text = "command";
            byte[] sendBuffer = Encoding.ASCII.GetBytes(text);
            _sock.SendTo(sendBuffer, _endPoint);

            text = "takeoff";
            sendBuffer = Encoding.ASCII.GetBytes(text);
            _sock.SendTo(sendBuffer, _endPoint);

            text = "land";
            sendBuffer = Encoding.ASCII.GetBytes(text);
            _sock.SendTo(sendBuffer, _endPoint);
        }
    }
}