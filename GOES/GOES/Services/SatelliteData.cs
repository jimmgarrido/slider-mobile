using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using GOES.Models;

namespace GOES.Services
{
    public class SatelliteData
    {
        public static List<Satellite> Satellites { get; private set; }

        public static List<int> NumOfImages = new List<int> { 6, 12, 14, 18, 24, 28, 30, 36, 42, 48, 54, 56, 60 };

        public static List<int> TimeSteps = new List<int> { 1, 2, 3, 4, 6, 8, 12, 24, 48, 96 };

        public static void LoadSatelliteData(string dataJson)
        {
            Satellites = new List<Satellite>();

            using (JsonDocument document = JsonDocument.Parse(dataJson))
            {
                var satelliteElement = document.RootElement.GetProperty("satellites");

                foreach (var element in satelliteElement.EnumerateObject())
                {
                    var sectorsElement = element.Value.GetProperty("sectors");
                    var productElement = element.Value.GetProperty("products");

                    var sectors = new List<Sector>();
                    var products = new List<Product>();

                    foreach (var sectorElement in sectorsElement.EnumerateObject())
                    {
                        var sector = new Sector
                        {
                            Id = sectorElement.Name,
                            Name = sectorElement.Value.GetProperty("sector_title").GetString(),
                            DefaultProduct = sectorElement.Value.GetProperty("default_product").GetString()
                        };

                        //Workaround for a bug in the site's JSON where the default proeprty is mnissing from GOES-17 Meso-2 sector
                        if (sectorElement.Value.TryGetProperty("defaults", out JsonElement sectorDefaultsElement))
                        {
                            sector.TimestepMultiplier = sectorDefaultsElement.GetProperty("minutes_between_images").GetDouble();
                        }
                        else
                        {
                            sector.TimestepMultiplier = 1.0;
                        }

                        if (sectorElement.Value.TryGetProperty("missing_products", out JsonElement missingProductsElement))
                        {
                            sector.MissingProducts = missingProductsElement.EnumerateArray().Select(x => x.GetString()).ToList();
                        }

                        sectors.Add(sector);
                    }

                    foreach (var product in productElement.EnumerateObject())
                    {
                        var title = product.Value.GetProperty("product_title").GetString();

                        if(!title.StartsWith("-"))
                            products.Add(new Product
                            {
                                Id = product.Name,
                                Name = WebUtility.HtmlDecode(title)
                            });
                    }

                    var sat = new Satellite()
                    {
                        Id = element.Name,
                        Name = element.Value.GetProperty("satellite_title").GetString(),
                        DefaultSector = element.Value.GetProperty("default_sector").GetString(),
                        Sectors = sectors,
                        Products = products
                    };

                    Satellites.Add(sat);
                }
            }
        }
    }
}
