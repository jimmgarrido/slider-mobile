using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GOES.Models;
using GOES.ViewModels;

namespace GOES.Views
{
    public partial class SliderOptionsPage : ContentPage
    {
        public SliderOptionsPage()
        {
            InitializeComponent();

            BindingContext = new SliderOptionsViewModel();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}