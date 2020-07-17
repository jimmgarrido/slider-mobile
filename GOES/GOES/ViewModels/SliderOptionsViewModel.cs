using GOES.Models;
using GOES.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GOES.ViewModels
{
    public class SliderOptionsViewModel : BaseViewModel
    {
        public List<Satellite> Satellites
        {
            get => SatelliteData.Satellites;
        }

        Satellite currentSatellite;
        public Satellite CurrentSatellite
        {
            get => currentSatellite;
            set
            {
                if (value == currentSatellite)
                    return;

                currentSatellite = value;
                OnPropertyChanged();
                SectorIndex = CurrentSatellite.Sectors.FindIndex(s => s.Id == CurrentSatellite.DefaultSector); ;

                if (!isLoading)
                {
                    HasChanged = true;
                    MessagingCenter.Send(this, "SatelliteChanged", currentSatellite.Id);
                }
            }
        }

        int sectorIndex = -1;
        public int SectorIndex
        {
            get => sectorIndex;
            set
            {
                if (value == sectorIndex)
                    return;

                sectorIndex = value;
                OnPropertyChanged();

                if (sectorIndex >= 0)
                {
                    var missingProducts = CurrentSatellite.Sectors[sectorIndex].MissingProducts;

                    ValidProducts = missingProducts != null ?
                        CurrentSatellite.Products.Where(p => !missingProducts.Contains(p.Id)).ToList() :
                        CurrentSatellite.Products;

                    ProductIndex = ValidProducts.FindIndex(p => p.Id == CurrentSatellite.Sectors[sectorIndex].DefaultProduct);

                    var tempIndex = TimeStepIndex;
                    TimeSteps = SatelliteData.TimeSteps.Select(t => $"{t * CurrentSatellite.Sectors[sectorIndex].TimestepMultiplier} min").ToList();
                    TimeStepIndex = tempIndex;


                    if (!isLoading)
                    {
                        MessagingCenter.Send(this, "SectorChanged", currentSatellite.Sectors[sectorIndex].Id);
                        HasChanged = true;
                    }
                }
            }
        }

        int productIndex = -1;
        public int ProductIndex
        {
            get => productIndex;
            set
            {
                if (value == productIndex)
                    return;

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
                return validProducts;
            }
            set
            {
                if (validProducts == value)
                    return;

                validProducts = value;
                OnPropertyChanged();
            }
        }

        public List<int> NumOfImages {
            get => SatelliteData.NumOfImages;
        }

        int numOfImagesIndex = -1;
        public int NumOfImagesIndex
        {
            get => numOfImagesIndex;
            set
            {
                numOfImagesIndex = value;
                OnPropertyChanged();

                if (!isLoading && numOfImagesIndex >= 0)
                {
                    MessagingCenter.Send(this, "NumImagesChanged", NumOfImages[NumOfImagesIndex]);
                    HasChanged = true;
                }
            }
        }

        List<string> timeSteps;
        public List<string> TimeSteps
        {
            get => timeSteps;
            set
            {
                if (value == timeSteps)
                    return;

                timeSteps = value;
                OnPropertyChanged();
            }
        }

        int timeStepIndex = -1;
        public int TimeStepIndex
        {
            get => timeStepIndex;
            set
            {
                if (timeStepIndex == value)
                    return;

                timeStepIndex = value;
                OnPropertyChanged();

                if (!isLoading && TimeStepIndex >= 0)
                {
                    MessagingCenter.Send(this, "TimeStepChanged", SatelliteData.TimeSteps[TimeStepIndex]);
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

        public SliderOptionsViewModel() { }

        public void Init(SliderOptions currentOptions)
        {
            isLoading = true;

            CurrentSatellite = Satellites.Find(s => s.Id == currentOptions.Satellite);
            SectorIndex = CurrentSatellite.Sectors.IndexOf(CurrentSatellite.Sectors.Find(s => s.Id == currentOptions.Sector));
            ProductIndex = CurrentSatellite.Products.IndexOf(CurrentSatellite.Products.Find(s => s.Id == currentOptions.Product));
            NumOfImagesIndex = NumOfImages.IndexOf(currentOptions.NumImages);
            TimeStepIndex = SatelliteData.TimeSteps.IndexOf(currentOptions.TimeStep);
            IsMapEnabled = currentOptions.IsMapToggled;

            isLoading = false;
            HasChanged = false;
        }

    }
}
