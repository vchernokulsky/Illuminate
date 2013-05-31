using System.Windows.Media;

namespace Intems.LightPlayer.BL.Commands
{
    public class SetColor : Command
    {
        private const byte DefauleChannel = 1;

        private Color _color;

        public SetColor() : base(DefauleChannel)
        {
            _color = Color.FromRgb(255, 255, 255);
        }

        public SetColor(byte channel, Color color) : base(channel)
        {
            _color = color;
        }

        public override byte Function
        {
            get { return (byte) CmdEnum.SetColor; }
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
