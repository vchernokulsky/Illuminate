using System;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightPlayer.BL
{
    public class FrameEventArgs : EventArgs
    {
        public TimeSpan Delta { get; set; }
    }

    public class Frame
    {
        private TimeSpan _length;

        public Frame(TimeSpan startTime, TimeSpan length)
        {
            StartTime = startTime;
            Length = length;
        }

        public Frame(TimeSpan startTime, TimeSpan length, Command cmd) : this(startTime, length)
        {
            Command = cmd;
        }

        public Command Command { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan Length
        {
            get { return _length; }
            set
            {
                _length = value;
                RaiseFrameChanged(value - _length);
            }
        }

        public event EventHandler<FrameEventArgs> FrameChanged;

        /// <summary>
        /// Устанавливает временные характеристики команды
        /// </summary>
        /// <param name="time">Временной интервал от начала композиции</param>
        /// <param name="length">Продолжительность команды</param>
        public void SetTime(TimeSpan time, TimeSpan length)
        {
            var delta = length - Length;

            StartTime = time;
            Length = length;
            RaiseFrameChanged(delta);
        }

        /// <summary>
        /// Проверка на начало команды
        /// </summary>
        /// <param name="time">Временной интервал от начала композиции</param>
        /// <returns></returns>
        public bool IsStartRequired(TimeSpan time)
        {
            return (time >= StartTime) && (time < (StartTime + Length));
        }

        protected virtual void RaiseFrameChanged(TimeSpan delta)
        {
            var handler = FrameChanged;
            if (handler != null) handler(this, new FrameEventArgs {Delta = delta});
        }
    }
}
