using System;
using System.Collections.Generic;
using System.Linq;

namespace Intems.LightPlayer.BL
{
    [Serializable]
    public class FrameSequence
    {
        private readonly object _locker;
        private int _curIndex = -1;

        private List<Frame> _frames;

        public FrameSequence()
        {
            _locker = new object();
            _frames = new List<Frame>();
        }

        public event EventHandler SequenceChanged;

        public List<Frame> Frames
        {
            get { return _frames; }
            set { _frames = value; }
        }

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

        public void Rewind()
        {
            lock (_locker)
            {
                _curIndex = -1;
            }
        }

        public void UpdateFrom(Frame frame)
        {
            var idx = _frames.IndexOf(frame);
            for (int i = idx + 1; i < _frames.Count; i++)
                _frames[i].StartTime = _frames[i - 1].StartTime + _frames[i-1].Length;
            RaiseSequenceChanged();
        }

        public void UpdateAll()
        {
            UpdateFrom(_frames.First());
        }

        public void Push(Frame frame)
        {
            lock (_locker)
            {
                _frames.Add(frame);
                frame.FrameChanged += OnFrameChanged;
            }
        }

        public void ChangeFrame(Frame target, Frame newFrame)
        {
            lock (_locker)
            {
                var idx = _frames.IndexOf(target);
                if (idx >= 0)
                {
                    _frames[idx] = newFrame;
                    newFrame.FrameChanged += OnFrameChanged;
                }
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                _frames.Clear();
            }
        }

        //PRIVATE METHODS
        private void OnFrameChanged(object sender, FrameEventArgs eventArgs)
        {
            var frame = sender as Frame;
            if (frame != null)
            {
                var idx = _frames.IndexOf(frame);
                for (int i = idx+1; i < _frames.Count; i++)
                {
                    var delta = eventArgs.Delta;
                    _frames[i].StartTime += delta;
                }
                RaiseSequenceChanged();
            }
        }

        public void RaiseSequenceChanged()
        {
            var handler = SequenceChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
