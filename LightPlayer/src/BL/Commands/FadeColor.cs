using System;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace Intems.LightPlayer.BL.Commands
{
    public class FadeColor : Command
    {
        private Color _startColor;
        private Color _stopColor;

        public FadeColor(TimeSpan startTime, TimeSpan length, Color startColor, Color stopColor) : base(1, (byte) CmdEnum.Fade)
        {
            _startColor = startColor;
            _stopColor = stopColor;
        }

        public void ChangeColor(Color start, Color stop)
        {
            _startColor = start;
            _stopColor = stop;
        }

        protected override byte[] GetParams()
        {
            throw new NotImplementedException();
        }
    }
}
