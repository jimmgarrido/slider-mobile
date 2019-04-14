using System;
using System.Collections.Generic;
using System.Windows.Input;

using Xamarin.Forms;

namespace GOES.ViewModels
{
    public class SliderViewModel : BaseViewModel
    {
        Dictionary<string, string> replacements;

        public SliderViewModel()
        {
            Title = "Slider";

            replacements = new Dictionary<string, string>();
        }



        public ICommand OpenWebCommand { get; }
    }
}