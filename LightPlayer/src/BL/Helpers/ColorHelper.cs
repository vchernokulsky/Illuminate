using System;
using System.Windows.Media;

namespace Intems.LightPlayer.BL.Helpers
{
    public static class ColorHelper
    {
        public static byte[] ToBytes(this Color color)
        {
            return new[] {color.R, color.G, color.B};
        }

        public static Color ToColor(this byte[] bytes)
        {
            if (bytes.Length == 3)
                return Color.FromRgb(bytes[0], bytes[1], bytes[2]);

            throw new ArgumentException(String.Format("Can't convert array with length = {0} to RGB color", bytes.Length));
        }
    }
}
