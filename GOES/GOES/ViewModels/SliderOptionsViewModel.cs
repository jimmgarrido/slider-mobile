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
                    MessagingCenter.Send(this, "SatelliteChanged");
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
                    MessagingCenter.Send(this, "SectorChanged");
                    ProductIndex = -1;
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
                    MessagingCenter.Send(this, "ProductChanged");
                    HasChanged = true;
                }
            }
        }

        public bool HasChanged { get; set; }

        bool isLoading = true;

        public SliderOptionsViewModel()
        {
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
                        new Product { Id = "band_01", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_02", Name = @"Band 1 (""Red"")" },
                        new Product { Id = "band_03", Name = @"Band 1 (""Veggie"")" },
                        new Product { Id = "band_04", Name = @"Band 1 (""Cirrus"")" },
                        new Product { Id = "band_05", Name = @"Band 1 (""Snow/Ice"")" },
                        new Product { Id = "band_06", Name = @"Band 1 (""Cloud Particle Size"")" },
                        new Product { Id = "band_07", Name = @"Band 1 (""Shortwave Window"")" },
                        new Product { Id = "band_08", Name = @"Band 1 (""Upper Level Tropospheric Water Vapor"")" },
                        new Product { Id = "band_09", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_10", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_11", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_12", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_13", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_14", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_15", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_16", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "geocolor", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "natural_color", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "rgb_air_mass", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "awips_dust", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "fire_temperature", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "shortwave_albedo_cira", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_geometric_thickness_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_layers_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_top_height_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_snow-cloud_discriminator_rgb", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_natural_fire_color", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "eumetsat_ash", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_optical_thickness_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_effective_radius_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "jma_day_cloud_phase_distinction_rgb", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_glm_l2_group_energy", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_glm_l2_group_counts", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_cloud_snow_discriminator", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_high_low_cloud_and_snow", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "split_window_difference_10_3-12_3", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_dust_debra_original_cloud_mask", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "mrms_merged_base_reflectivity_qc", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "mrms_reflectivity_at_lowest_altitude", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "mrms_radar_precipitation_accumulation_01-hour", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "mrms_lightning_probability_0-30-min_nldn", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "mrms_cg_lightning_density_5-min_nldn", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "mrms_precip_flag", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "mrms_radar_precipitation_rate", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_dust_debra_cloud_cleared_background", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_dust_debra_clavr-x_cloud_mask", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "eumetsat_dust", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_proxy_visible", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_low_cloud_night", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_low_cloud_night_cloud_cleared_background", Name = @"GeoColor (CIRA)" }
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
                        new Product { Id = "band_01", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_02", Name = @"Band 1 (""Red"")" },
                        new Product { Id = "band_03", Name = @"Band 1 (""Veggie"")" },
                        new Product { Id = "band_04", Name = @"Band 1 (""Cirrus"")" },
                        new Product { Id = "band_05", Name = @"Band 1 (""Snow/Ice"")" },
                        new Product { Id = "band_06", Name = @"Band 1 (""Cloud Particle Size"")" },
                        new Product { Id = "band_07", Name = @"Band 1 (""Shortwave Window"")" },
                        new Product { Id = "band_08", Name = @"Band 1 (""Upper Level Tropospheric Water Vapor"")" },
                        new Product { Id = "band_09", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_10", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_11", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_12", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_13", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_14", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_15", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_16", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "geocolor", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "natural_color", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "rgb_air_mass", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "awips_dust", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "fire_temperature", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "shortwave_albedo_cira", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_geometric_thickness_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_layers_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_top_height_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_snow-cloud_discriminator_rgb", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_natural_fire_color", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "eumetsat_ash", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_optical_thickness_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_effective_radius_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "jma_day_cloud_phase_distinction_rgb", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_glm_l2_group_energy", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_glm_l2_group_counts", Name = @"GeoColor (CIRA)" },
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
                        new Product { Id = "band_01", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_02", Name = @"Band 1 (""Red"")" },
                        new Product { Id = "band_03", Name = @"Band 1 (""Veggie"")" },
                        new Product { Id = "band_04", Name = @"Band 1 (""Cirrus"")" },
                        new Product { Id = "band_05", Name = @"Band 1 (""Snow/Ice"")" },
                        new Product { Id = "band_06", Name = @"Band 1 (""Cloud Particle Size"")" },
                        new Product { Id = "band_07", Name = @"Band 1 (""Shortwave Window"")" },
                        new Product { Id = "band_08", Name = @"Band 1 (""Upper Level Tropospheric Water Vapor"")" },
                        new Product { Id = "band_09", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_10", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_11", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_12", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_13", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_14", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_15", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_16", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "geocolor", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "natural_color", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "rgb_air_mass", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "awips_dust", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "fire_temperature", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "shortwave_albedo_cira", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_geometric_thickness_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_layers_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_top_height_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_snow-cloud_discriminator_rgb", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_natural_fire_color", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "eumetsat_ash", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_optical_thickness_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_effective_radius_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "jma_day_cloud_phase_distinction_rgb", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_glm_l2_group_energy", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_glm_l2_group_counts", Name = @"GeoColor (CIRA)" },
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
                        new Product { Id = "band_01", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_02", Name = @"Band 1 (""Red"")" },
                        new Product { Id = "band_03", Name = @"Band 1 (""Veggie"")" },
                        new Product { Id = "band_04", Name = @"Band 1 (""Cirrus"")" },
                        new Product { Id = "band_05", Name = @"Band 1 (""Snow/Ice"")" },
                        new Product { Id = "band_06", Name = @"Band 1 (""Cloud Particle Size"")" },
                        new Product { Id = "band_07", Name = @"Band 1 (""Shortwave Window"")" },
                        new Product { Id = "band_08", Name = @"Band 1 (""Upper Level Tropospheric Water Vapor"")" },
                        new Product { Id = "band_09", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_10", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_11", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_12", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_13", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_14", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_15", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "band_16", Name = @"Band 1 (""Blue"")" },
                        new Product { Id = "geocolor", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "natural_color", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "rgb_air_mass", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "awips_dust", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "fire_temperature", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "shortwave_albedo_cira", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_geometric_thickness_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_layers_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_top_height_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_snow-cloud_discriminator_rgb", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_natural_fire_color", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "eumetsat_ash", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_optical_thickness_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cloud_effective_radius_cira_clavr-x", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "jma_day_cloud_phase_distinction_rgb", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_glm_l2_group_energy", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_glm_l2_group_counts", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_cloud_snow_discriminator", Name = @"GeoColor (CIRA)" },
                        new Product { Id = "cira_high_low_cloud_and_snow", Name = @"GeoColor (CIRA)" }
                    }
                }
            };

            HasChanged = false;

        }

        public void LoadInitialData(string satellite, string sector, string product)
        {
            isLoading = true;

            CurrentSatellite = AllSatellites.Find(s => s.Id == satellite);

            SectorIndex = CurrentSatellite.Sectors.IndexOf(CurrentSatellite.Sectors.Find(s => s.Id == sector));

            ProductIndex = CurrentSatellite.Products.IndexOf(CurrentSatellite.Products.Find(s => s.Id == product));

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
