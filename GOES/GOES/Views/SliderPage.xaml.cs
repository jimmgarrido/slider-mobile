using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GOES.Models;
using GOES.ViewModels;
using Xamarin.Forms;
using GOES.Services;

namespace GOES.Views
{
    public partial class SliderPage : ContentPage
    {
        bool firstLoad = true;

        public SliderPage()
        {
            InitializeComponent();

            WebContainer.Source = "http://rammb-slider.cira.colostate.edu/?sat=goes-17&z=3&im=12&ts=1&st=0&et=0&speed=130&motion=loop&map=0&lat=0&opacity%5B0%5D=1&hidden%5B0%5D=0&pause=0&slider=-1&hide_controls=1&mouse_draw=0&follow_feature=0&follow_hide=0&s=rammb-slider&sec=conus&p%5B0%5D=geocolor&x=7448.6796875&y=3745";
            WebContainer.Navigated += WebContainer_Navigated;

            MessagingCenter.Subscribe<SliderOptionsPage>(this, "OptionsChanged", (sender) => OptionsChanged());
            MessagingCenter.Subscribe<SliderOptionsViewModel, string>(this, "SatelliteChanged", (sender, arg) => SatelliteChanged(arg));
            MessagingCenter.Subscribe<SliderOptionsViewModel, string>(this, "SectorChanged", (sender, arg) => SectorChanged(arg));
            MessagingCenter.Subscribe<SliderOptionsViewModel, string>(this, "ProductChanged", (sender, arg) => ProductChanged(arg));
            MessagingCenter.Subscribe<SliderOptionsViewModel, bool>(this, "MapToggled", (sender, arg) => MapToggled(arg));
            MessagingCenter.Subscribe<SliderOptionsViewModel, int>(this, "NumImagesChanged", (sender, arg) => NumImagesChanged(arg));
            MessagingCenter.Subscribe<SliderOptionsViewModel, int>(this, "TimeStepChanged", (sender, arg) => TimeStepChanged(arg));
        }

        private async void WebContainer_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (!firstLoad)
                return;

            var productJson = await WebContainer.EvaluateJavaScriptAsync("json");

            SatelliteData.LoadSatelliteData(productJson);

            firstLoad = false;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            WebContainer.Reload();
        }

        private async void OptionsClicked(object sender, EventArgs e)
        {
            var options = await LoadOptions();

            await Navigation.PushModalAsync(new SliderOptionsPage(options));
        }

        async void ZoomOutClicked(object sender, EventArgs e)
        {
            //if(zoomLevel - 1 < 0)
            //{
            //    await DisplayAlert("", "Cannot zoom out further", "Okay");
            //}
            //else
            //{
            //    zoomLevel--;
            //    await WebContainer.EvaluateJavaScriptAsync("zoomOut($(window).width() / 2, $(window).height() / 2)");
            //}
            await WebContainer.EvaluateJavaScriptAsync("zoomOut($(window).width() / 2, $(window).height() / 2)");

        }

        async void ZoomInClicked(object sender, EventArgs e)
        {
            //if (zoomLevel + 1 > 2)
            //{
            //    await DisplayAlert("", "Cannot zoom in more", "Okay");
            //}
            //else
            //{
            //    zoomLevel++;
            //    await WebContainer.EvaluateJavaScriptAsync("zoomIn($(window).width() / 2, $(window).height() / 2)");
            //}
            await WebContainer.EvaluateJavaScriptAsync("zoomIn($(window).width() / 2, $(window).height() / 2)");
        }

        async void PlayPausedClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            await WebContainer.EvaluateJavaScriptAsync(@"$(""#playPause"").button().click()");

            if (btn.Text == "Pause")
                btn.Text = "Play";
            else
                btn.Text = "Pause";
        }

        async void PrevClicked(object sender, EventArgs e)
        {
            await WebContainer.EvaluateJavaScriptAsync(@"$(""#previous"").button().click()");
        }

        async void NextClicked(object sender, EventArgs e)
        {
            await WebContainer.EvaluateJavaScriptAsync(@"$(""#next"").button().click()");
        }

        async void OptionsChanged()
        {
            await WebContainer.EvaluateJavaScriptAsync("location.reload()");
        }

        async void SatelliteChanged(string satellite)
        {
            await WebContainer.EvaluateJavaScriptAsync($@"url_parameters.sat = ""{satellite}""");
            await WebContainer.EvaluateJavaScriptAsync(@"updateURL(1);");
        }

        async void SectorChanged(string sector)
        {
            await WebContainer.EvaluateJavaScriptAsync($@"url_parameters.sec = ""{sector}""");
            await WebContainer.EvaluateJavaScriptAsync(@"updateURL(0,1);");
        }

        async void ProductChanged(string product)
        {
            await WebContainer.EvaluateJavaScriptAsync($@"url_parameters.p = {{0:""{product}""}};");
            await WebContainer.EvaluateJavaScriptAsync(@"updateURL(0,0,1);");
        }

        async void NumImagesChanged(int num)
        {
            await WebContainer.EvaluateJavaScriptAsync($@"url_parameters.im = {num};");
            await WebContainer.EvaluateJavaScriptAsync(@"updateURL();");
        }

        async void TimeStepChanged(int step)
        {
            await WebContainer.EvaluateJavaScriptAsync($@"url_parameters.ts = {step};");
            await WebContainer.EvaluateJavaScriptAsync(@"updateURL();");
        }

        async void MapToggled(bool isEnabled)
        {
            await WebContainer.EvaluateJavaScriptAsync($@"$(""#mapHideShow"").click()");
        }

        async Task<SliderOptions> LoadOptions()
        {
            var selectedSatellite = await WebContainer.EvaluateJavaScriptAsync(@"$('#satelliteSelectorChange option[selected=""true""]').val();");
            var selectedSector = await WebContainer.EvaluateJavaScriptAsync(@"$('#sectorSelectorChange option[selected=""true""]').val();");
            var selectedProduct = await WebContainer.EvaluateJavaScriptAsync(@"$('#productSelectorChange option[selected=""true""]').val();");
            var isMapToggled = await WebContainer.EvaluateJavaScriptAsync(@"$('#mapHideShow').prop('checked')");
            var numImages = await WebContainer.EvaluateJavaScriptAsync(@"$('#numberOfImagesSelectorChange option[selected=""true""]').val();");
            var timeStep = await WebContainer.EvaluateJavaScriptAsync(@"$('#timeStepSelectorChange option[selected=""true""]').val();");

            return new SliderOptions
            {
                Satellite = selectedSatellite,
                Sector = selectedSector,
                Product = selectedProduct,
                IsMapToggled = bool.Parse(isMapToggled),
                NumImages = int.Parse(numImages),
                TimeStep = int.Parse(timeStep)
            };
        }
    }
}