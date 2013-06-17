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
                    cmd = new SetColor(Command.DefaultChannel, Colors.Black);
                    break;
                case CmdEnum.Fade:
                    cmd = new FadeColor(Command.DefaultChannel, Colors.Black, Colors.Black, (short)(DefaultLength.TotalSeconds * TicksPerSec));
                    break;
                case CmdEnum.Blink:
                    cmd = new BlinkColor(Command.DefaultChannel, Colors.Black, 50);
                    break;
            }
            if(cmd != null)
                result = new Frame(startTime, DefaultLength) { Command = cmd };

            return result;
        }

    }
}
