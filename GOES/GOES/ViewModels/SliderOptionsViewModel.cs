using GOES.Models;
using GOES.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace GOES.ViewModels
{
    public class SliderOptionsViewModel : BaseViewModel
    {
        public List<Satellite> Satellites
        {
            get
            {
                return SatelliteData.Satellites;
            }
        }

        Satellite currentSatellite;
        public Satellite CurrentSatellite
        {
            get
            {
                return currentSatellite;
            }
            set
            {
                if (value == currentSatellite)
                    return;

                Debug.WriteLine($"****** {value} *****");
                currentSatellite = value;
                OnPropertyChanged();

                if (!isLoading)
                {
                    MessagingCenter.Send(this, "SatelliteChanged", currentSatellite.Id);
                    SectorIndex = -1;
                    HasChanged = true;
                }
            }
        }

        int sectorIndex;
        public int SectorIndex
        {
            get
            {
                return sectorIndex;
            }
            set
            {
                if (value == sectorIndex)
                    return;

                sectorIndex = value;
                OnPropertyChanged();

                if (!isLoading && sectorIndex >= 0)
                {
                    MessagingCenter.Send(this, "SectorChanged", currentSatellite.Sectors[sectorIndex].Id);
                    //ProductIndex = -1;
                    HasChanged = true;
                }
            }
        }

        int productIndex;
        public int ProductIndex
        {
            get
            {
                return productIndex;
            }
            set
            {
                productIndex = value;
                OnPropertyChanged();

                if (!isLoading && productIndex >= 0)
                {
                    MessagingCenter.Send(this, "ProductChanged", ValidProducts[productIndex].Id);
                    HasChanged = true;
                }
            }
        }

        List<Product> validProducts;
        public List<Product> ValidProducts
        {
            get
            {
                return CurrentSatellite.Products.Where(p => !CurrentSatellite.Sectors[sectorIndex].MissingProducts.Contains(p.Id)).ToList();
            }
        }

        List<int> imageCount;
        public List<int> ImageCount { get; set; }

        int imageCountIndex;
        public int ImageCountIndex
        {
            get => imageCountIndex;
            set
            {
                imageCountIndex = value;
                OnPropertyChanged();

                if (!isLoading && imageCountIndex >= 0)
                {
                    MessagingCenter.Send(this, "ImageCountChanged");
                    HasChanged = true;
                }
            }
        }

        bool isMapEnabled;
        public bool IsMapEnabled
        {
            get => isMapEnabled;
            set
            {
                isMapEnabled = value;
                OnPropertyChanged();

                if(!isLoading)
                    MessagingCenter.Send(this, "MapToggled", isMapEnabled);
            }
        }

        public bool HasChanged { get; set; }

        bool isLoading = true;

        public SliderOptionsViewModel(SliderOptions currentOptions)
        {

            isLoading = true;

            CurrentSatellite = Satellites.Find(s => s.Id == currentOptions.Satellite);
            SectorIndex = CurrentSatellite.Sectors.IndexOf(CurrentSatellite.Sectors.Find(s => s.Id == currentOptions.Sector));
            ProductIndex = CurrentSatellite.Products.IndexOf(CurrentSatellite.Products.Find(s => s.Id == currentOptions.Product));
            IsMapEnabled = currentOptions.IsMapToggled;

            isLoading = false;
            HasChanged = false;
        }

        //public void LoadInitialData(SliderOptions options)
        //{
        //    isLoading = true;

        //    CurrentSatellite = SatelliteData.Find(s => s.Id == options.Satellite);

        //    SectorIndex = CurrentSatellite.Sectors.IndexOf(CurrentSatellite.Sectors.Find(s => s.Id == options.Sector));

        //    ProductIndex = CurrentSatellite.Products.IndexOf(CurrentSatellite.Products.Find(s => s.Id == options.Product));

        //    IsMapEnabled = options.IsMapToggled;

        //    isLoading = false;
        //}

        void SatelliteChanged()
        {
            //var select = ViewModel.Satellites[ViewModel.SatelliteIndex];
            //var sat = ViewModel.AllSatellites.First(s => s.Value == select).Key;

            //await WebContainer.EvaluateJavaScriptAsync($@"url_parameters.sat = ""{sat}"";");
            //await WebContainer.EvaluateJavaScriptAsync(@"updateURL(1);");
            //await WebContainer.EvaluateJavaScriptAsync(@"$('#sectorSelectorChange option').remove();");
            //await WebContainer.EvaluateJavaScriptAsync(@"$.each(json.satellites[url_parameters.sat].sectors, function(e, t) {e != url_parameters.sec ? $(""#sectorSelectorChange"").append(""<option value='"" + e + ""'>"" + t.sector_title + ""</option>"") : $(""#sectorSelectorChange"").append("" <option selected='true' value='"" + e + ""'>"" + t.sector_title + ""</option>"")});");
            //var test = await WebContainer.EvaluateJavaScriptAsync(@"$(""#sectorSelectorChange>option"").map(function() { return $(this).val(); }).get();");
            //Debug.WriteLine(test);

            //test.Replace('[', '{');
            //test.Replace(']', '}');

            //var sats = JsonConvert.DeserializeObject<List<string>>(test);
            //ViewModel.Sectors = sats;
        }

    }
}
