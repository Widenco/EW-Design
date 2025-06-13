using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace EWDesign.Core
{
    class ColorToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                //Devuelve valor Hexadecimal del color solido
                return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                //Devuelve Color solido del valor Hexadecimal
                return (SolidColorBrush)(new BrushConverter().ConvertFrom(value));
            }
            catch
            {
                //Devuelve negro como color por defecto en caso de error
                return Brushes.Black;
            }
        }
    }
}
