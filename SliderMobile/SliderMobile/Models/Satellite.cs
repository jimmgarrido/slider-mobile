﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SliderMobile.Models
{
    public class Satellite
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string DefaultSector { get; set; }

        public List<Sector> Sectors { get; set; }

        public List<Product> Products { get; set; }

        public Satellite()
        {
            Debug.WriteLine("");
        }
    }

    public class Sector
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<string> MissingProducts { get; set; }

        public string DefaultProduct { get; set; }

        public double TimestepMultiplier { get; set; }
    }

    public class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
