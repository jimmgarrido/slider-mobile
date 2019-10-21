using System;
using System.Collections.Generic;
using System.Linq;

namespace GOES.Models
{
    public class Satellite
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<Sector> Sectors { get; set; }

        public List<Product> Products { get; set; }

        public Satellite()
        {

        }
    }

    public class Sector
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

    public class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
