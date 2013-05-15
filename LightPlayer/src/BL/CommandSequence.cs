using System;
using System.Collections.Generic;

namespace Intems.LightPlayer.BL
{
    public class Command  
    {
        public bool IsStarted(TimeSpan time)
        {
            return true;
        }
    }

    public class CommandSequence
    {
        private readonly object _locker;
        private List<Command> _commands;

        public CommandSequence()
        {
            _locker = new object();

            _commands = new List<Command>();
        }

        private int _curIndex = 0;
        public Command GetCommand(TimeSpan time)
        {
            Command result = null;

            lock (_locker)
            {
                var nextIndex = _curIndex + 1;
                if (_commands[nextIndex].IsStarted(time))
                {
                    _curIndex++;
                    result = _commands[nextIndex];
                }
            }
            return result;
        }

        public void PushCommand(Command cmd)
        {
            
        }
    }
}
