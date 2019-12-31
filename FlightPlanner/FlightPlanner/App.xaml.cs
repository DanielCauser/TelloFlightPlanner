using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FlightPlanner.Services;
using FlightPlanner.Views;
using ReactiveUI;

namespace FlightPlanner
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<AircraftService>();
            RxApp.DefaultExceptionHandler = new GlobalExceptionHandler();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
