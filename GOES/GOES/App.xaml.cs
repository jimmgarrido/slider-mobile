using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GOES.Views;

namespace GOES
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.UWP)
                MainPage = new MainPage();
            else
                MainPage = new AppShell();
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
