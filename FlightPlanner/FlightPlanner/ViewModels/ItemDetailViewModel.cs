using System;
using System.Threading;
using System.Windows.Input;
using FlightPlanner.Models;
using ReactiveUI;

namespace FlightPlanner.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
            
            this.StartFlightCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                await AircraftService.ExecuteFlightPlan(Item, CancellationToken.Token);
            });
            
            this.EmergencyShutdownCommand = ReactiveCommand.Create( () =>
            {
                CancelToken();
                AircraftService.Shutdown();
                RefreshToken();
            });
        }

        public ICommand StartFlightCommand { get; }
        public ICommand EmergencyShutdownCommand { get; }
    }
}
