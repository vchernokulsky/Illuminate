using System;
using System.Windows.Media;
using Intems.LightPlayer.BL.Helpers;

namespace Intems.LightPlayer.BL.Commands
{
    [Serializable]
    public class FadeColor : Command
    {
        private CustomColor _startColor;
        private CustomColor _stopColor;
        private short _length;

        public FadeColor()
        {
            _startColor = Colors.Black;
            _startColor = Colors.Black;
            _length = 0;
        }

        public FadeColor(Color startColor, Color stopColor, short length)
        {
            _startColor = startColor;
            _stopColor = stopColor;
            _length = length;
        }

        public override sealed byte Channel { get; set; }

        public override byte Function
        {
            get { return (byte)CmdEnum.Fade; }
            set {}
        }

        /// <summary>
        /// Начальный цвет fade'а
        /// </summary>
        public Color StartColor
        {
            get { return _startColor; }
            set { _startColor = value; }
        }

        /// <summary>
        /// Конечный цвет fade'а
        /// </summary>
        public Color StopColor
        {
            get { return _stopColor; }
            set { _stopColor = value; }
        }

        /// <summary>
        /// Длительность fade'а в секундах
        /// </summary>
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

        protected override byte[] GetParams()
        {
            var colorBytes1 = new[] { (byte)0x00, _startColor.R, (byte)0x00, _startColor.G, (byte)0x00, _startColor.B };
            var colorBytes2 = new[] { (byte)0x00, _stopColor.R, (byte)0x00, _stopColor.G, (byte)0x00, _stopColor.B };
            var lengthBytes = ((short)(_length * 10)).ToBytes();

            var param = new byte[colorBytes1.Length + colorBytes2.Length + lengthBytes.Length];
            Array.Copy(colorBytes1, param, colorBytes1.Length);
            Array.Copy(colorBytes2, 0, param, colorBytes1.Length, colorBytes2.Length);
            Array.Copy(lengthBytes, 0, param, colorBytes1.Length + colorBytes2.Length, lengthBytes.Length);

            return param;
        }
    }
}
