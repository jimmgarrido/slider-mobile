using System;
using SliderMobile.Data;

namespace SliderMobile.ViewModels
{
    public class SliderPageViewModel : BaseViewModel
    {
        bool isLoaded = false;
        public bool IsLoaded
        {
            get => isLoaded;
            set
            {
                isLoaded = value;
                OnPropertyChanged();
            }
        }

        public SliderPageViewModel()
        {
        }

        public void LoadSatelliteData(string json)
        {
            SatelliteData.LoadSatelliteData(json);
            IsLoaded = true;
        }
    }
}
