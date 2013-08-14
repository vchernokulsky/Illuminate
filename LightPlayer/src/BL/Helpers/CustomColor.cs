using System;
using System.Windows.Media;

namespace Intems.LightPlayer.BL.Helpers
{
    [Serializable]
    public struct CustomColor
    {
        public byte A;
        public byte R;
        public byte G;
        public byte B;

        public CustomColor(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public CustomColor(Color color)
            : this(color.A, color.R, color.G, color.B)
        {
        }

        public static implicit operator CustomColor(Color color)
        {
            return new CustomColor(color);
        }

        public static implicit operator Color(CustomColor colour)
        {
            return Color.FromArgb(colour.A, colour.R, colour.G, colour.B);
        }
    }
}
