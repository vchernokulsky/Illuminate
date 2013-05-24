using System;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightPlayer.BL
{
    public class CommandFrame
    {
        public CommandFrame(TimeSpan startTime, TimeSpan length)
        {
            StartTime = startTime;
            Length = length;
        }

        public CommandFrame(TimeSpan startTime, TimeSpan length, Command cmd) : this(startTime, length)
        {
            Command = cmd;
        }

        public TimeSpan StartTime { get; private set; }

        public TimeSpan Length { get; private set; }

        public Command Command { get; set; }

        /// <summary>
        /// Устанавливает временные характеристики команды
        /// </summary>
        /// <param name="time">Временной интервал от начала композиции</param>
        /// <param name="length">Продолжительность команды</param>
        public void SetTime(TimeSpan time, TimeSpan length)
        {
            StartTime = time;
            Length = length;
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
