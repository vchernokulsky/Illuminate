using System;
using System.Windows.Media;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightPlayer.BL
{
    public class FrameBuilder
    {
        const int TicksPerSec = 10;

        protected readonly TimeSpan DefaultLength;

        public FrameBuilder()
        {
            DefaultLength = TimeSpan.FromSeconds(2);
        }

        public Frame CreateFrameByCmdEnum(CmdEnum cmdEnum, TimeSpan startTime)
        {
            Frame result = null;
            Command cmd = null;
            switch (cmdEnum)
            {
                case CmdEnum.SetColor:
                    cmd = new SetColor(Colors.Black){Channel = Command.DefaultChannel};
                    break;
                case CmdEnum.Fade:
                    var length = (short)(DefaultLength.TotalSeconds * TicksPerSec);
                    cmd = new FadeColor(Colors.Black, Colors.Black, length){Channel = Command.DefaultChannel};
                    break;
                case CmdEnum.Blink:
                    cmd = new BlinkColor(Colors.Black, 50){Channel = Command.DefaultChannel};
                    break;
            }
            if(cmd != null)
                result = new Frame(startTime, DefaultLength) { Command = cmd };

            return result;
        }

    }
}
