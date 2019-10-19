using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GOES.Models;
using GOES.ViewModels;
using System.Diagnostics;
using Newtonsoft.Json;

namespace GOES.Views
{
    public partial class SliderOptionsPage : ContentPage
    {
        WebView WebContainer;
        SliderOptionsViewModel ViewModel => BindingContext as SliderOptionsViewModel;
        bool loading = true;

        public SliderOptionsPage(WebView container)
        {
            InitializeComponent();

            WebContainer = container;
            BindingContext = new SliderOptionsViewModel();

            LoadInitialData();
        }

        async void LoadInitialData()
        {
            loading = true;

            await WebContainer.EvaluateJavaScriptAsync(@"$('#satelliteSelectorChange option[disabled=""disabled""]').remove();");
            var test = await WebContainer.EvaluateJavaScriptAsync(@"$(""#satelliteSelectorChange>option"").map(function() { return $(this).val(); }).get();");
            Debug.WriteLine(test);

            var selected = await WebContainer.EvaluateJavaScriptAsync(@"$('#satelliteSelectorChange option[selected=""true""]').val();");
            Debug.WriteLine($"*** {selected} **");

            var label = ViewModel.AllSatellites[selected];
            var index = ViewModel.Satellites.IndexOf(label);
            ViewModel.SatelliteIndex = index;


            //Get Sectors
            await WebContainer.EvaluateJavaScriptAsync(@"$('#sectorSelectorChange option[disabled=""disabled""]').remove();");
            var sectorJson = await WebContainer.EvaluateJavaScriptAsync(@"$(""#sectorSelectorChange>option"").map(function() { return $(this).val(); }).get();");
            sectorJson.Replace('[', '{');
            sectorJson.Replace(']', '}');

            var sectors = JsonConvert.DeserializeObject<List<string>>(sectorJson);
            ViewModel.Sectors = sectors;

            loading = false;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            await WebContainer.EvaluateJavaScriptAsync("location.reload();");
            await Navigation.PopModalAsync();
        }

        async void Picker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if (loading)
                return;

            var select = ViewModel.Satellites[ViewModel.SatelliteIndex];
            var sat = ViewModel.AllSatellites.First(s => s.Value == select).Key;

            await WebContainer.EvaluateJavaScriptAsync($@"url_parameters.sat = ""{sat}"";");
            await WebContainer.EvaluateJavaScriptAsync(@"updateURL(1);");
            await WebContainer.EvaluateJavaScriptAsync(@"$('#sectorSelectorChange option').remove();");
            await WebContainer.EvaluateJavaScriptAsync(@"$.each(json.satellites[url_parameters.sat].sectors, function(e, t) {e != url_parameters.sec ? $(""#sectorSelectorChange"").append(""<option value='"" + e + ""'>"" + t.sector_title + ""</option>"") : $(""#sectorSelectorChange"").append("" <option selected='true' value='"" + e + ""'>"" + t.sector_title + ""</option>"")});");
            var test = await WebContainer.EvaluateJavaScriptAsync(@"$(""#sectorSelectorChange>option"").map(function() { return $(this).val(); }).get();");
            Debug.WriteLine(test);

            test.Replace('[','{');
            test.Replace(']', '}');

            var sats = JsonConvert.DeserializeObject<List<string>>(test);
            ViewModel.Sectors = sats;
        }
    }
}
