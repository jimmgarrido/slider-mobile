﻿using System;

namespace GOES.Models
{
    public class SliderOptions
    {
        public string Satellite { get; set; }

        public string Sector { get; set; }

        public string Product { get; set; }

        public bool IsMapToggled { get; set; }

        public int NumImages { get; set; }

        public SliderOptions()
        {
        }
    }
}
