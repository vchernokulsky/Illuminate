using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Intems.LightDesigner.GUI.Converters
{
    public class BoolToSelectionBackground : IValueConverter
    {
        public Color Selected { get; set; }
        public Color Unselected { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (bool) value;
            return new SolidColorBrush(val ? Selected : Unselected);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
