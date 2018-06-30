using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BindingWithValueConverters.Converters
{
    class ToHigh: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var hourly = (ObservableCollection<TimeForecast>)value;

            return hourly.OrderByDescending(p => p.Temperature).FirstOrDefault().Temperature;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
