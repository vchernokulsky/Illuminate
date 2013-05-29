using System.Windows.Media;

namespace Intems.LightPlayer.BL.Commands
{
    public class SetColor : Command
    {
        private Color _color;

        public SetColor() : base(1, (byte)CmdEnum.SetColor)
        {
            
        }

        public SetColor(Color color) : base(1, (byte) CmdEnum.SetColor)
        {
            _color = color;
        }

        public SetColor(byte red, byte green, byte blue) : this(Color.FromRgb(red, green, blue))
        {
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
