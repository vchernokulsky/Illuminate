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
        private Color _startColor;
        private Color _stopColor;

        private readonly byte[] _ticksInBytes = new byte[2];
        private readonly byte[] _blinkFreqInBytes = new byte[2];
        private readonly byte[] _colorParams = new byte[6];

        public CommandBuilder(Color startColor, Color stopColor, string time, string freq)
        {
            _startColor = startColor;
            _stopColor = stopColor;

            int seconds = !String.IsNullOrEmpty(time) ? Int32.Parse(time) : 0;
            _ticksInBytes[0] = (byte)(seconds * 10 >> 8);
            _ticksInBytes[1] = (byte)(seconds * 10);

            int freqs = !String.IsNullOrEmpty(freq) ? Int32.Parse(freq) : 0;
            _blinkFreqInBytes[0] = (byte) (freqs >> 8);
            _blinkFreqInBytes[1] = (byte) (freqs);
        }

        private byte[] CreateColorByteArray(Color color)
        {
            var result = new[] { (byte)0x00, color.R, (byte)0x00, color.G, (byte)0x00, color.B };
            return result;
        }

        public byte[] CreateCommonParams(CmdEnum cmd)
        {
            var param = new byte[] { };
            switch (cmd)
            {
                case CmdEnum.SetColor:
                    param = CreateColorByteArray(_startColor);
                    break;
                case CmdEnum.Fade:
                    {
                        var fadeParam = new List<byte>();
                        fadeParam.AddRange(CreateColorByteArray(_startColor));
                        fadeParam.AddRange(CreateColorByteArray(_stopColor));
                        fadeParam.AddRange(_ticksInBytes);
                        param = fadeParam.ToArray();
                    }
                    break;
                case CmdEnum.Blink:
                    var blinkParam = new List<byte>();
                    blinkParam.AddRange(CreateColorByteArray(_startColor));
                    blinkParam.AddRange(_blinkFreqInBytes);
                    param = blinkParam.ToArray();
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
