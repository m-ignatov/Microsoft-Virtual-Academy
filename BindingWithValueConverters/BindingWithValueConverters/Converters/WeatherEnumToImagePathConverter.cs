using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BindingWithValueConverters.Converters
{
    class WeatherEnumToImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //throw new NotImplementedException();
            var weather = (Weather)value;
            var path = String.Empty;

            switch (weather)
            {
                case Weather.Sunny:
                    path = "assets/sunny.png";
                    break;
                case Weather.Cloudy:
                    path = "assets/cloudy.png";
                    break;
                case Weather.Rainy:
                    path = "assets/rainy.png";
                    break;
                case Weather.Snowy:
                    path = "assets/snowy.png";
                    break;
                default:
                    break;
            }
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
