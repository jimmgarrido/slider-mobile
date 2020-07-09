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

                        if (sectorElement.Value.TryGetProperty("missing_products", out JsonElement missingProductsElement))
                        {
                            sector.MissingProducts = missingProductsElement.EnumerateArray().Select(x => x.GetString()).ToList();
                        }

                        sectors.Add(sector);
                    }

                    foreach (var product in productElement.EnumerateObject())
                    {
                        products.Add(new Product
                        {
                            Id = product.Name,
                            Name = WebUtility.HtmlDecode(product.Value.GetProperty("product_title").GetString())
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
