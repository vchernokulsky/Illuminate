using System;

namespace Intems.LightPlayer.BL
{
    public class CommandFrame
    {
        private TimeSpan _startTime;
        private TimeSpan _length;

        public CommandFrame(TimeSpan startTime, TimeSpan length)
        {
            _startTime = startTime;
            _length = length;
        }

        public TimeSpan StartTime
        {
            get { return _startTime; }
        }

        public TimeSpan Length
        {
            get { return _length; }
        }

        /// <summary>
        /// Устанавливает временные характеристики команды
        /// </summary>
        /// <param name="time">Временной интервал от начала композиции</param>
        /// <param name="length">Продолжительность команды</param>
        public void SetTime(TimeSpan time, TimeSpan length)
        {
            _startTime = time;
            _length = length;
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

    }
}
