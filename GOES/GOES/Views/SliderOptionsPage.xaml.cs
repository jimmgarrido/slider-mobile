using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using GOES.Models;
using GOES.ViewModels;
using System.Diagnostics;
using Newtonsoft.Json;

namespace GOES.Views
{
    public partial class SliderOptionsPage : ContentPage
    {
        SliderOptionsViewModel ViewModel
        {
            get => BindingContext as SliderOptionsViewModel;
            set => BindingContext = value;
        }

        WebView webContainer;
        bool loading = true;

        public SliderOptionsPage(SliderOptions options)
        {
            InitializeComponent();

            ViewModel = new SliderOptionsViewModel();
            ViewModel.LoadInitialData(options);

            MessagingCenter.Subscribe<SliderOptionsViewModel>(this, "SatelliteChanged", (sender) => SatelliteChanged());
            MessagingCenter.Subscribe<SliderOptionsViewModel>(this, "SectorChanged", (sender) => SectorChanged());
            MessagingCenter.Subscribe<SliderOptionsViewModel>(this, "ProductChanged", (sender => ProductChanged()));
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.HasChanged)
                await webContainer.EvaluateJavaScriptAsync("location.reload();");

            await Navigation.PopModalAsync();
        }

        async void SatelliteChanged()
        {
            await webContainer.EvaluateJavaScriptAsync($@"url_parameters.sat = ""{ViewModel.CurrentSatellite.Id}"";");
            await webContainer.EvaluateJavaScriptAsync(@"updateURL(1);");
        }

        async void SectorChanged()
        {
            await webContainer.EvaluateJavaScriptAsync($@"url_parameters.sec = ""{ViewModel.CurrentSatellite.Sectors[ViewModel.SectorIndex].Id}"";");
            await webContainer.EvaluateJavaScriptAsync(@"updateURL(0,1);");
        }

        async void ProductChanged()
        {
            var test = $@"url_parameters.p = {{0:{ViewModel.CurrentSatellite.Products[ViewModel.ProductIndex].Id}}}"";";
            Debug.WriteLine(test);
            await webContainer.EvaluateJavaScriptAsync($@"url_parameters.p = {{0:""{ViewModel.CurrentSatellite.Products[ViewModel.ProductIndex].Id}""}};");
            await webContainer.EvaluateJavaScriptAsync(@"updateURL(0,0,1);");
        }

        async void CloseBtnClicked(System.Object sender, System.EventArgs e)
        {
            if(ViewModel.HasChanged)
                await webContainer.EvaluateJavaScriptAsync("location.reload();");

            await Navigation.PopModalAsync();
        }
    }
}
