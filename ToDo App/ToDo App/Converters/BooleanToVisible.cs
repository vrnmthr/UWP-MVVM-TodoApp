using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Converters
{
    /// <summary>
    /// Class converting booleans to XAML visibility options
    /// </summary>
    class BooleanToVisible : IValueConverter
    {
        /// <summary>
        /// Converts "true" to Visible and "false" to Collapsed when passed in as the
        /// value object.
        /// </summary>
        /// <param name="value">Boolean to convert</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns>XAML visibility object</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value == null || !(bool)(value) ) ? Windows.UI.Xaml.Visibility.Collapsed
                : Windows.UI.Xaml.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
