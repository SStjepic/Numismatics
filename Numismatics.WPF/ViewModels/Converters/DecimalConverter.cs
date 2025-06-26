using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Numismatics.WPF.ViewModels.Converters
{
    public class DecimalConverter : IValueConverter
    {
        private readonly CultureInfo culture = new CultureInfo("sr-SR");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double number;

                if (value is double d)
                    number = d;
                else if (value is string s && double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed))
                    number = parsed;
                else
                    return value?.ToString();

                if(number < 1000)
                {
                    return number;
                }
                else
                {
                    return number.ToString("N0", this.culture);
                }
            }
            catch
            {
                return value?.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

}
