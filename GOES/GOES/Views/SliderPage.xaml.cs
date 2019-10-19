using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GOES.Views
{
    public partial class SliderPage : ContentPage
    {
        int zoomLevel = 2;

        public SliderPage()
        {
            InitializeComponent();

            WebContainer.Source = "http://rammb-slider.cira.colostate.edu/?sat=goes-17&z=3&im=12&ts=1&st=0&et=0&speed=130&motion=loop&map=0&lat=0&opacity%5B0%5D=1&hidden%5B0%5D=0&pause=0&slider=-1&hide_controls=1&mouse_draw=0&follow_feature=0&follow_hide=0&s=rammb-slider&sec=conus&p%5B0%5D=geocolor&x=7448.6796875&y=3745"; 
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            WebContainer.Reload();
        }

        private async void OptionsClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new SliderOptionsPage(WebContainer)));
        }

        async void ZoomOutClicked(object sender, EventArgs e)
        {
            //if(zoomLevel - 1 < 0)
            //{
            //    await DisplayAlert("", "Cannot zoom out further", "Okay");
            //}
            //else
            //{
            //    zoomLevel--;
            //    await WebContainer.EvaluateJavaScriptAsync("zoomOut($(window).width() / 2, $(window).height() / 2)");
            //}
            await WebContainer.EvaluateJavaScriptAsync("zoomOut($(window).width() / 2, $(window).height() / 2)");

        }

        async void ZoomInClicked(object sender, EventArgs e)
        {
            //if (zoomLevel + 1 > 2)
            //{
            //    await DisplayAlert("", "Cannot zoom in more", "Okay");
            //}
            //else
            //{
            //    zoomLevel++;
            //    await WebContainer.EvaluateJavaScriptAsync("zoomIn($(window).width() / 2, $(window).height() / 2)");
            //}
            await WebContainer.EvaluateJavaScriptAsync("zoomIn($(window).width() / 2, $(window).height() / 2)");
        }

        async void PlayPausedClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            await WebContainer.EvaluateJavaScriptAsync(@"$(""#playPause"").button().click()");

            if (btn.Text == "Pause")
                btn.Text = "Play";
            else
                btn.Text = "Pause";
        }
    }
}