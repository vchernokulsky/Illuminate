using System;
using System.Windows.Media;

namespace Intems.LightPlayer.BL.Helpers
{
    [Serializable]
    public struct ColorSerializable
    {
        public byte A;
        public byte R;
        public byte G;
        public byte B;

        public ColorSerializable(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public ColorSerializable(Color color)
            : this(color.A, color.R, color.G, color.B)
        {
        }

        public static implicit operator ColorSerializable(Color color)
        {
            return new ColorSerializable(color);
        }

        public static implicit operator Color(ColorSerializable colour)
        {
            return Color.FromArgb(colour.A, colour.R, colour.G, colour.B);
        }
    }
}
