﻿using System;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightPlayer.BL
{
    public class Frame
    {
        public Frame(TimeSpan startTime, TimeSpan length)
        {
            StartTime = startTime;
            Length = length;
        }

        public Frame(TimeSpan startTime, TimeSpan length, Command cmd) : this(startTime, length)
        {
            Command = cmd;
        }

        public TimeSpan StartTime { get; set; }

        public TimeSpan Length { get; set; }

        public Command Command { get; set; }

        public event EventHandler FrameChanged;

        /// <summary>
        /// Устанавливает временные характеристики команды
        /// </summary>
        /// <param name="time">Временной интервал от начала композиции</param>
        /// <param name="length">Продолжительность команды</param>
        public void SetTime(TimeSpan time, TimeSpan length)
        {
            StartTime = time;
            Length = length;
            RaiseFrameChanged();
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

        protected virtual void RaiseFrameChanged()
        {
            var handler = FrameChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
