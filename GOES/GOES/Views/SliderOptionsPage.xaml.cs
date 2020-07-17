using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using GOES.Models;
using GOES.ViewModels;

namespace GOES.Views
{
    public partial class SliderOptionsPage : ContentPage
    {
        SliderOptionsViewModel ViewModel => BindingContext as SliderOptionsViewModel;

        public SliderOptionsPage(SliderOptions options)
        {
            InitializeComponent();

            ViewModel.Init(options);
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.HasChanged)
                MessagingCenter.Send(this, "OptionsChanged");
            //await webContainer.EvaluateJavaScriptAsync("location.reload();");

            await Navigation.PopModalAsync();
        }
    }
}
