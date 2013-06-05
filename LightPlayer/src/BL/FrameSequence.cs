using System;
using System.Collections.Generic;

namespace Intems.LightPlayer.BL
{
    public class FrameSequence
    {
        private readonly object _locker;

        public FrameSequence()
        {
            _locker = new object();
            _frames = new List<Frame>();
        }

        private List<Frame> _frames;
        public List<Frame> Frames {get { return _frames; } set { _frames = value; }}


        private int _curIndex = -1;
        public  Frame FrameByTime(TimeSpan time)
        {
            Frame result = null;

            lock (_locker)
            {
                var nextIndex = _curIndex + 1;
                if (nextIndex < _frames.Count)
                {
                    if (_frames[nextIndex].IsStartRequired(time))
                    {
                        _curIndex++;
                        result = _frames[nextIndex];
                    }
                }
            }
            return result;
        }

        public void Push(Frame frame)
        {
            lock (_locker)
            {
                _frames.Add(frame);
                frame.FrameChanged += OnFrameChanged;
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                _frames.Clear();
            }
        }

        private void OnFrameChanged(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

    }
}
