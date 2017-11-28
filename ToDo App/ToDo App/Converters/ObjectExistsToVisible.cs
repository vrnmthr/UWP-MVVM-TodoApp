using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Converters
{
    /// <summary>
    /// Converts objects that exist into XAML Visibilities
    /// </summary>
    class ObjectExistsToVisible : IValueConverter
    {
        /// <summary>
        /// Returns Collapsed if passed null and visible if passed a reference
        /// to some object
        /// </summary>
        /// <param name="value">Object to convert</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns>Visibility representing whether the object is null or not</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value == null) ? Windows.UI.Xaml.Visibility.Collapsed
                : Windows.UI.Xaml.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
