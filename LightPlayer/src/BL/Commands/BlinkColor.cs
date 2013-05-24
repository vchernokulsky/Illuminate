using System.Runtime.InteropServices;
using System.Windows.Media;

namespace Intems.LightPlayer.BL.Commands
{
    public class BlinkColor : Command
    {
        private const byte Func = (byte)CmdEnum.Blink;

        private Color _color;
        private int _frequency;

        public BlinkColor(byte channel) : base(channel, Func)
        {
        }

        public BlinkColor(byte channel, Color color, int freq) : base(channel, Func)
        {
            _color = color;
            _frequency = freq;
        }

        protected override byte[] GetParams()
        {
            var param = new[] { (byte)0x00, _color.R, (byte)0x00, _color.G, (byte)0x00, _color.B };
            return param;
        }
    }
}
