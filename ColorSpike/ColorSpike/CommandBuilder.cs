using System;
using System.Collections.Generic;
using System.Drawing;

namespace Intems.Illuminate.HardwareTester
{
    enum CmdEnum
    {
        None,
        SetColor,
        Fade,
        Blink,
        TurnOn,
        TurnOff
    }

    internal class CommandBuilder
    {
        private static byte[] CreateFadeParam(Color startColor, Color stopColor, string time)
        {
            var fadeParam = new List<byte>();
            fadeParam.AddRange(new[] { (byte)0x00, startColor.R, (byte)0x00, startColor.G, (byte)0x00, startColor.B });
            fadeParam.AddRange(new[] { (byte)0x00, stopColor.R, (byte)0x00, stopColor.G, (byte)0x00, stopColor.B });
            var ticks = new byte[2];
            int second = Int32.Parse(time);
            ticks[0] = (byte)(second * 10 >> 8);
            ticks[1] = (byte)(second * 10);
            fadeParam.AddRange(ticks);

            return fadeParam.ToArray();
        }

        public byte[] CreateCommonParams(CmdEnum cmd, Color startColor, Color stopColor, string time, string freq)
        {
            var param = new byte[] { };
            switch (cmd)
            {
                case CmdEnum.SetColor:
                    param = new[] { (byte)0x00, startColor.R, (byte)0x00, startColor.G, (byte)0x00, startColor.B };
                    break;
                case CmdEnum.Fade:
                    param = CreateFadeParam(startColor, stopColor, time);
                    break;
                case CmdEnum.Blink:
                    int freqs = Int32.Parse(freq);
                    param = new[]
                    {
                        (byte) 0x00, startColor.R, (byte) 0x00, startColor.G, (byte) 0x00, startColor.B, (byte) (freqs >> 8),
                        (byte) freqs
                    };
                    break;
                case CmdEnum.TurnOn:
                    param = new byte[] { };
                    break;
                case CmdEnum.TurnOff:
                    param = new byte[] { };
                    break;
            }
            return param;
        }

        public Package CreateCommand(CmdEnum cmd, byte[] param)
        {
            Package result = null;
            switch (cmd)
            {
                case CmdEnum.SetColor:
                    result = new Package(1, (int)CmdEnum.SetColor, param);
                    break;
            }
            return result;
        }
    }
}
