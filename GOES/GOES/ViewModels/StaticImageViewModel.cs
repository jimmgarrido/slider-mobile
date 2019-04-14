using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GOES.ViewModels
{
    public class StaticImageViewModel : BaseViewModel
    {
        DateTimeOffset? modifiedDate;
        public DateTimeOffset? ModifiedDate
        {
            get {
                return modifiedDate;
            }
            set
            {
                modifiedDate = value;
                OnPropertyChanged();
            }
        }
    }

    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var dateTime = (DateTimeOffset)value;
                return dateTime.ToLocalTime().ToString("MMMM dd HH:mm");
            }
            else
                return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
