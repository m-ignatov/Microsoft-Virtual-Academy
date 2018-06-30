using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BindingWithValueConverters.Converters
{
    class DateToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //throw new NotImplementedException();
            DateTime date = (DateTime)value;

            return String.Format("{0:dddd} - {0:d}", date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
