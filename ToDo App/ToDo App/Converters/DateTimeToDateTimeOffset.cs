using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Converters
{
    public class DateTimeToDateTimeOffset : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime date = (DateTime)value;
            return new DateTimeOffset(date < DateTime.Now ? DateTime.Now : date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset dto = (DateTimeOffset)value;
            return dto.DateTime < DateTime.Now ? DateTime.Now : dto.DateTime;
        }
    }

}
