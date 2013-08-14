using System;
using System.Windows.Media;
using Intems.LightPlayer.BL.Helpers;

namespace Intems.LightPlayer.BL.Commands
{
    [Serializable]
    public class SetColor : Command
    {
        private CustomColor _color;

        public SetColor()
        {
            _color = Color.FromRgb(255, 255, 255);
        }

        public SetColor(Color color)
        {
            _color = color;
        }

        public override byte Channel { get; set; }

        public override byte Function
        {
            get { return (byte)CmdEnum.SetColor; }
            set {}
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        protected override byte[] GetParams()
        {
            return new byte[] {0x00, _color.R, 0x00, _color.G, 0x00, _color.B};
        }
    }
}
