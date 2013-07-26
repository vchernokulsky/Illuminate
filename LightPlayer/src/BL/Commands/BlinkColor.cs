using System;
using System.Windows.Media;
using Intems.LightPlayer.BL.Helpers;

namespace Intems.LightPlayer.BL.Commands
{
    [Serializable]
    public class BlinkColor : Command
    {
        private ColorSerializable _color;
        private short _frequency;

        public BlinkColor()
        {
        }

        public BlinkColor(Color color, short freq)
        {
            _color = color;
            _frequency = freq;
        }

        public override byte Channel { get; set; }

        public override byte Function
        {
            get { return (byte)CmdEnum.Blink; }
            set {}
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public short Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        protected override byte[] GetParams()
        {
            var colorBytes = new[] { (byte)0x00, _color.R, (byte)0x00, _color.G, (byte)0x00, _color.B };
            var freqBytes = _frequency.ToBytes();

            var param = new byte[colorBytes.Length + freqBytes.Length];
            Array.Copy(colorBytes, param, colorBytes.Length);
            Array.Copy(freqBytes, 0, param, colorBytes.Length, freqBytes.Length);

            return param;
        }
    }
}
