using System;
using System.Collections.Generic;

namespace Intems.LightPlayer.BL
{
    public class CommandSequence
    {
        private readonly object _locker;
        private List<Command> _commands;

        public CommandSequence()
        {
            _locker = new object();

            _commands = new List<Command>();
        }

        private int _curIndex = -1;
        public Command GetCommand(TimeSpan time)
        {
            Command result = null;

            lock (_locker)
            {
                var nextIndex = _curIndex + 1;
                if (nextIndex < _commands.Count)
                {
                    if (_commands[nextIndex].IsStarteRequired(time))
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
            _commands.Add(cmd);
        }
    }
}
