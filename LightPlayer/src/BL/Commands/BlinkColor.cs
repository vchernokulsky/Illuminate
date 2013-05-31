using System;
using System.Windows.Media;
using Intems.LightPlayer.BL.Helpers;

namespace Intems.LightPlayer.BL.Commands
{
    public class BlinkColor : Command
    {
        private Color _color;
        private readonly short _frequency;

        public BlinkColor(byte channel) : base(channel)
        {
        }

        public BlinkColor(byte channel, Color color, short freq) : base(channel)
        {
            _color = color;
            _frequency = freq;
        }

        public override byte Function
        {
            get { return (byte) CmdEnum.Blink; }
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
