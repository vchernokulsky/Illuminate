using System;
using System.Globalization;
using System.Windows.Data;

namespace Intems.LightDesigner.GUI.Converters
{
    class TimeSpanToShortTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (TimeSpan) value;
            var sMin = String.Format("{00:0}", val.Minutes);
            var sSec = String.Format("{00:0}", val.Seconds);
            sMin = sMin.Length < 2 ? "0" + sMin : sMin;
            sSec = sSec.Length < 2 ? "0" + sSec : sSec;

            return String.Format("{0}:{1}", sMin, sSec);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (string)value;
            var str = val.Split(':');
            var min = double.Parse(str[0]);
            var sec = double.Parse(str[1]);

            return TimeSpan.FromSeconds(min*60 + sec);
        }
    }
}
