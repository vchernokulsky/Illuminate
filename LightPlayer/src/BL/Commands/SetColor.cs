using System.Windows.Media;

namespace Intems.LightPlayer.BL.Commands
{
    public class SetColor
    {
        private Color _color;

        public SetColor(byte red, byte green, byte blue)
        {
            _color = new Color {R = red, G = green, B = blue};
        }
    }
}
