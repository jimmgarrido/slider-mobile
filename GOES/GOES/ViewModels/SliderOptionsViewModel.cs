using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Forms;

namespace GOES.ViewModels
{
    internal class SliderOptionsViewModel : BaseViewModel
    {
        List<string> satellites;
        public List<string> Satellites
        {
            get {
                return satellites;
            }
            set {
                satellites = value;
            }
        }

        int satelliteIndex;
        public int SatelliteIndex
        {
            get
            {
                return satelliteIndex;
            }
            set
            {
                satelliteIndex = value;
            }
        }

        HttpClient client;

        public SliderOptionsViewModel()
        {
            Satellites = new List<string> { "GOES-16", "GOES-17", "Himawari-8", "JPSS" };
            SatelliteIndex = 1;

            client = new HttpClient();
            LoadData();
        }

        async void LoadData()
        {
            var page = await client.GetStringAsync("http://rammb-slider.cira.colostate.edu/?sat=goes-17&z=3&im=12&ts=1&st=0&et=0&speed=130&motion=loop&map=0&lat=0&opacity%5B0%5D=1&hidden%5B0%5D=0&pause=0&slider=-1&hide_controls=1&mouse_draw=0&follow_feature=0&follow_hide=0&s=rammb-slider&sec=conus&p%5B0%5D=geocolor&x=7448.6796875&y=3745");

            Debug.WriteLine(page);
        }
    }
}
