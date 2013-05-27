using System;
using System.Windows.Media;
using Intems.LightPlayer.BL.Helpers;

namespace Intems.LightPlayer.BL.Commands
{
    public class FadeColor : Command
    {
        private Color _startColor;
        private Color _stopColor;
        private short _length;

        public FadeColor(Color startColor, Color stopColor, short length) : base(1, (byte) CmdEnum.Fade)
        {
            _startColor = startColor;
            _stopColor = stopColor;
            _length = length;
        }

        public void ChangeColor(Color start, Color stop)
        {
            _startColor = start;
            _stopColor = stop;
        }

        protected override byte[] GetParams()
        {
            var colorBytes1 = new[] { (byte)0x00, _startColor.R, (byte)0x00, _startColor.G, (byte)0x00, _startColor.B };
            var colorBytes2 = new[] { (byte)0x00, _stopColor.R, (byte)0x00, _stopColor.G, (byte)0x00, _stopColor.B };
            var lengthBytes = _length.ToBytes();

            var param = new byte[colorBytes1.Length + colorBytes2.Length + lengthBytes.Length];
            Array.Copy(colorBytes1, param, colorBytes1.Length);
            Array.Copy(colorBytes2, 0, param, colorBytes1.Length, colorBytes2.Length);
            Array.Copy(lengthBytes, 0, param, colorBytes1.Length + colorBytes2.Length, lengthBytes.Length);

            return param;
        }
    }
}
