using GOES.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace GOES.ViewModels
{
    internal class SliderOptionsViewModel : BaseViewModel
    {
        public List<Satellite> AllSatellites { get; set; }

        public List<string> SatelliteNames
        {
            get
            {
                return AllSatellites.Select(s => s.Name).ToList();
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
                    MessagingCenter.Send(this, "ProductChanged", currentSatellite.Products[productIndex].Id);
                    HasChanged = true;
                }
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

        public bool HasChanged { get; set; }

        bool isLoading = true;

        public SliderOptionsViewModel()
        {
            ImageCount = new List<int> { 6, 12, 14, 18, 24, 28, 30, 36, 42, 48, 54, 56, 60 };

            AllSatellites = new List<Satellite>
            {
                new Satellite
                {
                    Id = "goes-16",
                    Name = "GOES-16",
                    Sectors = new List<Sector>
                    {
                        new Sector {Id = "full_disk", Name = "Full Disk" },
                        new Sector {Id = "conus", Name = "CONUS" },
                        new Sector {Id = "mesoscale_01", Name = "Mesoscale 1" },
                        new Sector {Id = "mesoscale_02", Name = "Mesoscale 2" }
                    },
                    Products = new List<Product>
                    {
                        new Product { Id = "band_01", Name = @"Band 1 (Blue)" },
                        new Product { Id = "band_02", Name = @"Band 2 (Red)" },
                        new Product { Id = "band_03", Name = @"Band 3 (Veggie)" },
                        new Product { Id = "band_04", Name = @"Band 4 (Cirrus)" },
                        new Product { Id = "band_05", Name = @"Band 5 (Snow/Ice)" },
                        new Product { Id = "band_06", Name = @"Band 6 (Cloud Particle Size)" },
                        new Product { Id = "band_07", Name = @"Band 7 (Shortwave Window)" },
                        new Product { Id = "band_08", Name = @"Band 8 (Upper Level Tropospheric Water Vapor)" },
                        new Product { Id = "band_09", Name = @"Band 9 (Mid-Level Tropospheric Water Vapor)" },
                        new Product { Id = "band_10", Name = @"Band 10 (Lower-Level Tropospheric Water Vapor)" },
                        new Product { Id = "band_11", Name = @"Band 11 (Cloud-Top Phase)" },
                        new Product { Id = "band_12", Name = @"Band 12 (Ozone)" },
                        new Product { Id = "band_13", Name = @"Band 13 (""Clean"" IR Longwave Window)" },
                        new Product { Id = "band_14", Name = @"Band 14 (IR Longwave Window)" },
                        new Product { Id = "band_15", Name = @"Band 15 (""Dirty"" Longwave Window)" },
                        new Product { Id = "band_16", Name = @"Band 16 (""CO2"" Longwave Window)" },
                        new Product { Id = "geocolor", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "shortwave_albedo_cira", Name = @"Shortwave Albedo (CIRA)" },
                        new Product { Id = "cira_snow-cloud_discriminator_rgb", Name = @"Snow/Cloud (CIRA)" },
                        new Product { Id = "natural_color", Name = @"Snow/Cloud-Layers (CIRA)" },
                        new Product { Id = "rgb_air_mass", Name = @"Airmass (EUMETSAT)" },
                        new Product { Id = "awips_dust", Name = @"Dust (EUMETSAT)" },
                        new Product { Id = "fire_temperature", Name = @"Fire Temperature (CIRA)" },
                        new Product { Id = "cloud_geometric_thickness_cira_clavr-x", Name = @"Cloud Geometric Thickness (CIRA/NOAA)" },
                        new Product { Id = "cloud_layers_cira_clavr-x", Name = @"Cloud Layers (CIRA/NOAA)" },
                        new Product { Id = "cloud_top_height_cira_clavr-x", Name = @"Cloud-Top Height (NOAA)" },
                        new Product { Id = "cira_natural_fire_color", Name = @"Natural Color-Fire (CIRA)" },
                        new Product { Id = "eumetsat_ash", Name = @"Ash (EUMETSAT)" },
                        new Product { Id = "cloud_optical_thickness_cira_clavr-x", Name = @"Cloud Optical Depth (NOAA)" },
                        new Product { Id = "cloud_effective_radius_cira_clavr-x", Name = @"Cloud-Top Effective Particle Size (NOAA)" },
                        new Product { Id = "jma_day_cloud_phase_distinction_rgb", Name = @"Day Cloud Phase Distinction (JMA)" },
                        new Product { Id = "cira_glm_l2_group_energy", Name = @"Group Energy Density (CIRA)" },
                        new Product { Id = "cira_glm_l2_group_counts", Name = @"Group Flash Count Density (CIRA)" },
                        new Product { Id = "cira_cloud_snow_discriminator", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_high_low_cloud_and_snow", Name = @"GeoColor (CIRA)" }
                    }
                },
                new Satellite
                {
                    Id = "goes-17",
                    Name = "GOES-17",
                    Sectors = new List<Sector>
                    {
                        new Sector {Id = "full_disk", Name = "Full Disk" },
                        new Sector {Id = "conus", Name = "CONUS" },
                        new Sector {Id = "mesoscale_01", Name = "Mesoscale 1" },
                        new Sector {Id = "mesoscale_02", Name = "Mesoscale 2" }
                    },
                    Products = new List<Product>
                    {
                        new Product { Id = "band_01", Name = @"Band 1 (Blue)" },
                        new Product { Id = "band_02", Name = @"Band 2 (Red)" },
                        new Product { Id = "band_03", Name = @"Band 3 (Veggie)" },
                        new Product { Id = "band_04", Name = @"Band 4 (Cirrus)" },
                        new Product { Id = "band_05", Name = @"Band 5 (Snow/Ice)" },
                        new Product { Id = "band_06", Name = @"Band 6 (Cloud Particle Size)" },
                        new Product { Id = "band_07", Name = @"Band 7 (Shortwave Window)" },
                        new Product { Id = "band_08", Name = @"Band 8 (Upper Level Tropospheric Water Vapor)" },
                        new Product { Id = "band_09", Name = @"Band 9 (Mid-Level Tropospheric Water Vapor)" },
                        new Product { Id = "band_10", Name = @"Band 10 (Lower-Level Tropospheric Water Vapor)" },
                        new Product { Id = "band_11", Name = @"Band 11 (Cloud-Top Phase)" },
                        new Product { Id = "band_12", Name = @"Band 12 (Ozone)" },
                        new Product { Id = "band_13", Name = @"Band 13 (""Clean"" IR Longwave Window)" },
                        new Product { Id = "band_14", Name = @"Band 14 (IR Longwave Window)" },
                        new Product { Id = "band_15", Name = @"Band 15 (""Dirty"" Longwave Window)" },
                        new Product { Id = "band_16", Name = @"Band 16 (""CO2"" Longwave Window)" },
                        new Product { Id = "geocolor", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "shortwave_albedo_cira", Name = @"Shortwave Albedo (CIRA)" },
                        new Product { Id = "cira_snow-cloud_discriminator_rgb", Name = @"Snow/Cloud (CIRA)" },
                        new Product { Id = "natural_color", Name = @"Snow/Cloud-Layers (CIRA)" },
                        new Product { Id = "rgb_air_mass", Name = @"Airmass (EUMETSAT)" },
                        new Product { Id = "awips_dust", Name = @"Dust (EUMETSAT)" },
                        new Product { Id = "fire_temperature", Name = @"Fire Temperature (CIRA)" },
                        new Product { Id = "cloud_geometric_thickness_cira_clavr-x", Name = @"Cloud Geometric Thickness (CIRA/NOAA)" },
                        new Product { Id = "cloud_layers_cira_clavr-x", Name = @"Cloud Layers (CIRA/NOAA)" },
                        new Product { Id = "cloud_top_height_cira_clavr-x", Name = @"Cloud-Top Height (NOAA)" },
                        new Product { Id = "cira_natural_fire_color", Name = @"Natural Color-Fire (CIRA)" },
                        new Product { Id = "eumetsat_ash", Name = @"Ash (EUMETSAT)" },
                        new Product { Id = "cloud_optical_thickness_cira_clavr-x", Name = @"Cloud Optical Depth (NOAA)" },
                        new Product { Id = "cloud_effective_radius_cira_clavr-x", Name = @"Cloud-Top Effective Particle Size (NOAA)" },
                        new Product { Id = "jma_day_cloud_phase_distinction_rgb", Name = @"Day Cloud Phase Distinction (JMA)" },
                        new Product { Id = "cira_glm_l2_group_energy", Name = @"Group Energy Density (CIRA)" },
                        new Product { Id = "cira_glm_l2_group_counts", Name = @"Group Flash Count Density (CIRA)" },
                        new Product { Id = "cira_cloud_snow_discriminator", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_high_low_cloud_and_snow", Name = @"GeoColor (CIRA)" }
                    }
                },
                new Satellite
                {
                    Id = "himawari",
                    Name = "Himawari-8",
                    Sectors = new List<Sector>
                    {
                        new Sector {Id = "full_disk", Name = "Full Disk" },
                        new Sector {Id = "japan", Name = "Japan" },
                        new Sector {Id = "mesoscale_01", Name = "Mesoscale 1" }
                    },
                    Products = new List<Product>
                    {
                        new Product { Id = "band_01", Name = @"Band 1 (Blue)" },
                        new Product { Id = "band_02", Name = @"Band 2 (Red)" },
                        new Product { Id = "band_03", Name = @"Band 3 (Veggie)" },
                        new Product { Id = "band_04", Name = @"Band 4 (Cirrus)" },
                        new Product { Id = "band_05", Name = @"Band 5 (Snow/Ice)" },
                        new Product { Id = "band_06", Name = @"Band 6 (Cloud Particle Size)" },
                        new Product { Id = "band_07", Name = @"Band 7 (Shortwave Window)" },
                        new Product { Id = "band_08", Name = @"Band 8 (Upper Level Tropospheric Water Vapor)" },
                        new Product { Id = "band_09", Name = @"Band 9 (Mid-Level Tropospheric Water Vapor)" },
                        new Product { Id = "band_10", Name = @"Band 10 (Lower-Level Tropospheric Water Vapor)" },
                        new Product { Id = "band_11", Name = @"Band 11 (Cloud-Top Phase)" },
                        new Product { Id = "band_12", Name = @"Band 12 (Ozone)" },
                        new Product { Id = "band_13", Name = @"Band 13 (""Clean"" IR Longwave Window)" },
                        new Product { Id = "band_14", Name = @"Band 14 (IR Longwave Window)" },
                        new Product { Id = "band_15", Name = @"Band 15 (""Dirty"" Longwave Window)" },
                        new Product { Id = "band_16", Name = @"Band 16 (""CO2"" Longwave Window)" },
                        new Product { Id = "geocolor", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "shortwave_albedo_cira", Name = @"Shortwave Albedo (CIRA)" },
                        new Product { Id = "cira_snow-cloud_discriminator_rgb", Name = @"Snow/Cloud (CIRA)" },
                        new Product { Id = "natural_color", Name = @"Snow/Cloud-Layers (CIRA)" },
                        new Product { Id = "rgb_air_mass", Name = @"Airmass (EUMETSAT)" },
                        new Product { Id = "awips_dust", Name = @"Dust (EUMETSAT)" },
                        new Product { Id = "fire_temperature", Name = @"Fire Temperature (CIRA)" },
                        new Product { Id = "cloud_geometric_thickness_cira_clavr-x", Name = @"Cloud Geometric Thickness (CIRA/NOAA)" },
                        new Product { Id = "cloud_layers_cira_clavr-x", Name = @"Cloud Layers (CIRA/NOAA)" },
                        new Product { Id = "cloud_top_height_cira_clavr-x", Name = @"Cloud-Top Height (NOAA)" },
                        new Product { Id = "cira_natural_fire_color", Name = @"Natural Color-Fire (CIRA)" },
                        new Product { Id = "eumetsat_ash", Name = @"Ash (EUMETSAT)" },
                        new Product { Id = "cloud_optical_thickness_cira_clavr-x", Name = @"Cloud Optical Depth (NOAA)" },
                        new Product { Id = "cloud_effective_radius_cira_clavr-x", Name = @"Cloud-Top Effective Particle Size (NOAA)" },
                        new Product { Id = "jma_day_cloud_phase_distinction_rgb", Name = @"Day Cloud Phase Distinction (JMA)" },
                        new Product { Id = "cira_glm_l2_group_energy", Name = @"Group Energy Density (CIRA)" },
                        new Product { Id = "cira_glm_l2_group_counts", Name = @"Group Flash Count Density (CIRA)" },
                        new Product { Id = "cira_cloud_snow_discriminator", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_high_low_cloud_and_snow", Name = @"GeoColor (CIRA)" }
                    }
                },
                new Satellite
                {
                    Id = "jpss",
                    Name = "JPSS",
                    Sectors = new List<Sector>
                    {
                        new Sector {Id = "northern_hemisphere", Name = "Northern Hemisphere" },
                        new Sector {Id = "southern_hemisphere", Name = "Southern Hemisphere" }
                    },
                    Products = new List<Product>
                    {
                        new Product { Id = "band_01", Name = @"Band 1 (Blue)" },
                        new Product { Id = "band_02", Name = @"Band 2 (Red)" },
                        new Product { Id = "band_03", Name = @"Band 3 (Veggie)" },
                        new Product { Id = "band_04", Name = @"Band 4 (Cirrus)" },
                        new Product { Id = "band_05", Name = @"Band 5 (Snow/Ice)" },
                        new Product { Id = "band_06", Name = @"Band 6 (Cloud Particle Size)" },
                        new Product { Id = "band_07", Name = @"Band 7 (Shortwave Window)" },
                        new Product { Id = "band_08", Name = @"Band 8 (Upper Level Tropospheric Water Vapor)" },
                        new Product { Id = "band_09", Name = @"Band 9 (Mid-Level Tropospheric Water Vapor)" },
                        new Product { Id = "band_10", Name = @"Band 10 (Lower-Level Tropospheric Water Vapor)" },
                        new Product { Id = "band_11", Name = @"Band 11 (Cloud-Top Phase)" },
                        new Product { Id = "band_12", Name = @"Band 12 (Ozone)" },
                        new Product { Id = "band_13", Name = @"Band 13 (""Clean"" IR Longwave Window)" },
                        new Product { Id = "band_14", Name = @"Band 14 (IR Longwave Window)" },
                        new Product { Id = "band_15", Name = @"Band 15 (""Dirty"" Longwave Window)" },
                        new Product { Id = "band_16", Name = @"Band 16 (""CO2"" Longwave Window)" },
                        new Product { Id = "geocolor", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "shortwave_albedo_cira", Name = @"Shortwave Albedo (CIRA)" },
                        new Product { Id = "cira_snow-cloud_discriminator_rgb", Name = @"Snow/Cloud (CIRA)" },
                        new Product { Id = "natural_color", Name = @"Snow/Cloud-Layers (CIRA)" },
                        new Product { Id = "rgb_air_mass", Name = @"Airmass (EUMETSAT)" },
                        new Product { Id = "awips_dust", Name = @"Dust (EUMETSAT)" },
                        new Product { Id = "fire_temperature", Name = @"Fire Temperature (CIRA)" },
                        new Product { Id = "cloud_geometric_thickness_cira_clavr-x", Name = @"Cloud Geometric Thickness (CIRA/NOAA)" },
                        new Product { Id = "cloud_layers_cira_clavr-x", Name = @"Cloud Layers (CIRA/NOAA)" },
                        new Product { Id = "cloud_top_height_cira_clavr-x", Name = @"Cloud-Top Height (NOAA)" },
                        new Product { Id = "cira_natural_fire_color", Name = @"Natural Color-Fire (CIRA)" },
                        new Product { Id = "eumetsat_ash", Name = @"Ash (EUMETSAT)" },
                        new Product { Id = "cloud_optical_thickness_cira_clavr-x", Name = @"Cloud Optical Depth (NOAA)" },
                        new Product { Id = "cloud_effective_radius_cira_clavr-x", Name = @"Cloud-Top Effective Particle Size (NOAA)" },
                        new Product { Id = "jma_day_cloud_phase_distinction_rgb", Name = @"Day Cloud Phase Distinction (JMA)" },
                        new Product { Id = "cira_glm_l2_group_energy", Name = @"Group Energy Density (CIRA)" },
                        new Product { Id = "cira_glm_l2_group_counts", Name = @"Group Flash Count Density (CIRA)" },
                        new Product { Id = "cira_cloud_snow_discriminator", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_high_low_cloud_and_snow", Name = @"GeoColor (CIRA)" }
                    }
                }
            };

            HasChanged = false;
        }

        public void LoadInitialData(SliderOptions options)
        {
            isLoading = true;

            CurrentSatellite = AllSatellites.Find(s => s.Id == options.Satellite);

            SectorIndex = CurrentSatellite.Sectors.IndexOf(CurrentSatellite.Sectors.Find(s => s.Id == options.Sector));

            ProductIndex = CurrentSatellite.Products.IndexOf(CurrentSatellite.Products.Find(s => s.Id == options.Product));

            isLoading = false;
        }

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
