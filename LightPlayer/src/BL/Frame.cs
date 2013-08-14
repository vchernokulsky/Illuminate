using System;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightPlayer.BL
{
    public class FrameEventArgs : EventArgs
    {
        public TimeSpan Delta { get; set; }
    }

    [Serializable]
    public class Frame : ICloneable
    {
        private TimeSpan _length;
        private TimeSpan _startTime;

        public Frame()
        {
        }

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

        /// <summary>
        /// Время начала фрейма (временной интервал от начала композиции)
        /// </summary>
        public TimeSpan StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        /// <summary>
        /// Длина фрейма (временной интервал)
        /// </summary>
        public TimeSpan Length
        {
            get { return _length; }
            set
            {
                UpdateFrameLength(value);
                _length = value;
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

        private void UpdateFrameLength(TimeSpan newFrameLength)
        {
            //меняем длину фэйда
            var fadeCmd = Command as FadeColor;
            if (fadeCmd != null)
                fadeCmd.Length = (short)newFrameLength.TotalSeconds;

            var delta = newFrameLength - _length;
            RaiseFrameChanged(delta);
        }

        private void RaiseFrameChanged(TimeSpan delta)
        {
            var handler = FrameChanged;
            if (handler != null) handler(this, new FrameEventArgs {Delta = delta});
        }

        public object Clone()
        {
            var clone = new Frame(StartTime, Length, Command);
            return clone;
        }
    }
}
