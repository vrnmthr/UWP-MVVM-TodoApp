using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Converters
{
    /// <summary>
    /// Converts DateTime to DateTimeOffsets
    /// </summary>
    public class DateTimeToDateTimeOffset : IValueConverter
    {
        /// <summary>
        /// Converts DateTime into DateTimeOffset. If the DateTime is before the current value in time
        /// it is converted to be 5 minutes in the future.
        /// </summary>
        /// <param name="value">DateTime to convert</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns>DateTimeOffset representing DateTime</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime date = (DateTime)value;
            return new DateTimeOffset(date < DateTime.Now ? DateTime.Now.AddMinutes(5) : date);
        }

        /// <summary>
        /// Converts DateTimeOffset into DateTime. If the DateTimeOffset is before the current value in time
        /// it is converted to be 5 minutes in the future.
        /// </summary>
        /// <param name="value">DateTimeOffset to convert</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns>DateTime object representing DateTimeOffset</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset dto = (DateTimeOffset)value;
            return dto.DateTime < DateTime.Now ? DateTime.Now.AddMinutes(5) : dto.DateTime;
        }
    }

}
