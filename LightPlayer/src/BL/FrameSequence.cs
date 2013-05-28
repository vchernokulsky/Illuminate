using System;
using System.Collections.Generic;

namespace Intems.LightPlayer.BL
{
    public class FrameSequence
    {
        private readonly object _locker;
        private readonly List<Frame> _commands;

        public FrameSequence()
        {
            _locker = new object();

            _commands = new List<Frame>();
        }

        private int _curIndex = -1;
        public Frame FrameByTime(TimeSpan time)
        {
            Frame result = null;

            lock (_locker)
            {
                var nextIndex = _curIndex + 1;
                if (nextIndex < _commands.Count)
                {
                    if (_commands[nextIndex].IsStartRequired(time))
                    {
                        _curIndex++;
                        result = _commands[nextIndex];
                    }
                }
            }
            return result;
        }

        public void Push(Frame frame)
        {
            lock (_locker)
            {
                _commands.Add(frame);
                frame.FrameChanged += OnFrameChanged;
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                _commands.Clear();
            }
        }

        private void OnFrameChanged(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

    }
}
