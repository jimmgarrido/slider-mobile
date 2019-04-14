using GOES.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GOES.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StaticImagePage : ContentPage
    {
        string url = "https://cdn.star.nesdis.noaa.gov/GOES17/ABI/SECTOR/psw/GEOCOLOR/2400x2400.jpg";
        bool isFirstLaunch = true;
        StaticImageViewModel viewModel;

        public StaticImagePage()
        {
            BindingContext = viewModel = new StaticImageViewModel();
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (isFirstLaunch)
            {
                viewModel.IsBusy = true;
                var stream = await GetImageAsync();
                viewModel.IsBusy = false;
                ImageView.Source = ImageSource.FromStream(() => stream);
                isFirstLaunch = false;
            }
                
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            viewModel.ModifiedDate = null;
            ImageView.Source = null;
            viewModel.IsBusy = true;
            var stream = await GetImageAsync();
            viewModel.IsBusy = false;
            ImageView.Source = ImageSource.FromStream(() => stream);
        }

        async Task<MemoryStream> GetImageAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var imgStream = await response.Content.ReadAsByteArrayAsync();
                viewModel.ModifiedDate = response.Content.Headers.LastModified;

                return new MemoryStream(imgStream);
            }
        }
    }
}