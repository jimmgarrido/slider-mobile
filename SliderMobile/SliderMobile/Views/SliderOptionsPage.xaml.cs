using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using SliderMobile.Models;
using SliderMobile.ViewModels;

namespace SliderMobile.Views
{
    public partial class SliderOptionsPage : ContentPage
    {
        SliderOptionsViewModel ViewModel => BindingContext as SliderOptionsViewModel;

        public SliderOptionsPage(SliderOptions options)
        {
            InitializeComponent();

            ViewModel.Init(options);
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;

            if (DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Landscape)
            {
                LandscapeLayout.IsVisible = true;
                PortraitLayout.IsVisible = false;
            }
            else
            {
                LandscapeLayout.IsVisible = false;
                PortraitLayout.IsVisible = true;
            }
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            if (e.DisplayInfo.Orientation == DisplayOrientation.Landscape)
            {
                LandscapeLayout.IsVisible = true;
                PortraitLayout.IsVisible = false;
            }
            else
            {
                LandscapeLayout.IsVisible = false;
                PortraitLayout.IsVisible = true;
            }
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.HasChanged)
                MessagingCenter.Send(this, "OptionsChanged");
            //await webContainer.EvaluateJavaScriptAsync("location.reload();");

            await Navigation.PopModalAsync();
        }
    }
}
