using System;
using System.Collections.Generic;
using System.Windows.Media;
using Intems.LightDesigner.GUI.ViewModels;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightDesigner.GUI.Common
{
    public class FrameBuilder
    {
        const int TicksPerSec = 10;

        protected readonly TimeSpan DefaultLength;

        public FrameBuilder()
        {
            _channelMap = new Dictionary<SequenceViewModel, int>();
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

        public Frame CreateFrame(CmdEnum cmdEnum, SequenceViewModel targetSequence)
        {
            int channelNum = _channelMap.ContainsKey(targetSequence)
                ? _channelMap[targetSequence]
                : Command.DefaultChannel;

            Command cmd = null;
            switch (cmdEnum)
            {
                case CmdEnum.SetColor:
                    cmd = new SetColor(Colors.Black) { Channel = (byte) channelNum };
                    break;
                case CmdEnum.Fade:
                    var length = (short)(DefaultLength.TotalSeconds * TicksPerSec);
                    cmd = new FadeColor(Colors.Black, Colors.Black, length) { Channel = (byte) channelNum };
                    break;
                case CmdEnum.Blink:
                    cmd = new BlinkColor(Colors.Black, 50) { Channel = (byte) channelNum };
                    break;
            }
            Frame result = null;
            if (cmd != null)
                result = new Frame(TimeSpan.FromSeconds(0.0), DefaultLength) { Command = cmd };
            return result;
        }

        public void Clear()
        {
            _channelMap.Clear();
        }

        private readonly Dictionary<SequenceViewModel, int> _channelMap;
        public void RegisterSequence(int channelNum, SequenceViewModel sequenceViewModel)
        {
            _channelMap.Add(sequenceViewModel, channelNum);
        }
    }
}
