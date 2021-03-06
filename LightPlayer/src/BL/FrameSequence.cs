﻿using System;
using System.Collections.Generic;

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

        public void InsertAfter(Frame target, Frame newFrame)
        {
            lock (_locker)
            {
                var idx = _frames.IndexOf(target);
                if (idx >= 0)
                {
                    _frames.Insert(idx + 1, newFrame);
                    newFrame.FrameChanged += OnFrameChanged;
                }
            }
        }

        public void InserBefore(Frame target, Frame newFrame)
        {
            lock (_locker)
            {
                var idx = _frames.IndexOf(target);
                if (idx >= 0)
                {
                    _frames.Insert(idx, newFrame);
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
