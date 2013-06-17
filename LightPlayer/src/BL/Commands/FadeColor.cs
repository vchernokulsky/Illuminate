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

        public FadeColor() : base(DefaultChannel)
        {
            _startColor = Colors.Black;
            _startColor = Colors.Black;
            _length = 0;
        }

        public FadeColor(Color startColor, Color stopColor, short length) : base(DefaultChannel)
        {
            _startColor = startColor;
            _stopColor = stopColor;
            _length = length;
        }

        public FadeColor(byte channel, Color startColor, Color stopColor, short length) : base(channel)
        {
            _startColor = startColor;
            _stopColor = stopColor;
            _length = length;
        }

        public Color StartColor
        {
            get { return _startColor; }
            set { _startColor = value; }
        }

        public Color StopColor
        {
            get { return _stopColor; }
            set { _stopColor = value; }
        }

        public short Length
        {
            get { return _length; }
            set { _length = value; }
        }

        public void ChangeColor(Color start, Color stop)
        {
            _startColor = start;
            _stopColor = stop;
        }

        public override byte Function
        {
            get { return (byte) CmdEnum.Fade; }
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
