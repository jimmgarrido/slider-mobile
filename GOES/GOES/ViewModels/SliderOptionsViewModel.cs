using GOES.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;

namespace GOES.ViewModels
{
    internal class SliderOptionsViewModel : BaseViewModel
    {
        public Dictionary<string, string> AllSatellites = new Dictionary<string, string>
        {
            {"goes-16","GOES-16" },
            {"goes-17","GOES-17" },
            {"himawari","Himawari-8" },
            {"jpss", "JPSS" }
        };

        public Dictionary<string, string> AllSectors = new Dictionary<string, string>
        {
            {"full_disk","Full Disk" },
            {"conus","CONUS" },
            {"mesoscale_01","Mesoscale 1" },
            {"mesoscale_02", "Mesoscale 2" },
            {"japan", "Japan" }
        };

        public List<string> Satellites
        {
            get
            {
                return AllSatellites.Values.ToList();
            }
        }

        int satelliteIndex;
        public int SatelliteIndex {
            get
            {
                return satelliteIndex;
            }
            set
            {
                if (satelliteIndex == value)
                    return;

                satelliteIndex = value;
                OnPropertyChanged();
            }
        }

        List<string> sectors;
        public List<string> Sectors
        {
            get => sectors;
            set
            {
                sectors = value;
                OnPropertyChanged();
                SectorIndex = 0;
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
                sectorIndex = value;
                OnPropertyChanged();
            }
        }

        List<string> products;
        public List<string> Products
        {
            get => products;
            set => products = value;
        }

        HttpClient client;

        public SliderOptionsViewModel()
        {
            //SatelliteIndex = 0;
            //Satellites = new List<Satellite>
            //{
            //    new Satellite {Index = "goes-16", Label = "GOES-16" },
            //    new Satellite {Index = "goes-17", Label = "GOES-17" },
            //    new Satellite {Index = "himawari", Label = "Himawari-8" },
            //    new Satellite {Index = "jpss", Label = "JPSS" }
            //};

            //Sectors = new List<string> { "Disk", "CONUS", "Mesoscale 1", "Mesoscale 2" };

            Products = new List<string> { "Band 1", "Band 2"};


            client = new HttpClient();
            //LoadData();
            //OnPropertyChanged("Satellites");
        }

        
    }
}
