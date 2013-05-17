using System;
using System.Collections.Generic;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightPlayer.BL
{
    public class CommandSequence
    {
        private readonly object _locker;
        private readonly List<Command> _commands;

        public CommandSequence()
        {
            _locker = new object();

            _commands = new List<Command>();
        }

        private int _curIndex = -1;
        public Command CommandByTime(TimeSpan time)
        {
            Command result = null;

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

        public void PushCommand(Command cmd)
        {
            lock (_locker)
            {
                _commands.Add(cmd);
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                _commands.Clear();
            }
        }
    }
}
