using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Xamarin.Forms;

using FlightPlanner.Models;
using FlightPlanner.Services;
using ReactiveUI.Fody.Helpers;

namespace FlightPlanner.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            RefreshToken();
        }
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public IAircraftService AircraftService => DependencyService.Get<IAircraftService>();

        [Reactive] public bool IsBusy { get; set; }

        [Reactive] public string Title { get; set; }

        protected CancellationTokenSource CancellationToken;

        public void CancelToken()
        {
            CancellationToken.Cancel();
        }
        
        public void RefreshToken()
        {
            CancellationToken?.Dispose();
            CancellationToken = new CancellationTokenSource();
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
