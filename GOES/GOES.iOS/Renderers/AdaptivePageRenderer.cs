using System;
using GOES.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(Label), typeof(AdaptiveLabelRenderer))]
[assembly: ExportRenderer(typeof(ContentPage), typeof(AdaptivePageRenderer))]
[assembly: ExportRenderer(typeof(Picker), typeof(AdaptivePickerRenderer))]
[assembly: ExportRenderer(typeof(Button), typeof(AdaptiveButtonRenderer))]
[assembly: ExportRenderer(typeof(NavigationPage), typeof(AdaptiveNavPageRenderer))]
namespace GOES.iOS.Renderers
{
    public class AdaptiveLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
                Control.TextColor = UIColor.LabelColor;
        }
    }

    public class AdaptivePageRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            NativeView.BackgroundColor = UIColor.SystemBackgroundColor;

            //ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
        }
    }

    public class AdaptivePickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
                Control.BackgroundColor = UIColor.TertiarySystemBackgroundColor;
        }

    }

    public class AdaptiveButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.SystemFillColor;
                Control.SetTitleColor(UIColor.SystemBlueColor, UIControlState.Normal);
            }
        }
    }

    public class AdaptiveNavPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            //NavigationBar.BarTintColor = UIColor.SystemRedColor;

            //ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationBar.BarTintColor = UIColor.TertiarySystemBackgroundColor;
            NavigationBar.TintColor = UIColor.SystemBlueColor;

            //ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
        }
    }
}
