using System;

namespace Intems.LightPlayer.BL.Commands
{
    enum CmdEnum
    {
        None,
        SetColor,
        Fade,
        Blink,
        TurnOn,
        TurnOff
    }

    public abstract class Command
    {
        protected byte Channel;
        protected byte Function;

        protected abstract byte[] GetParams();

        protected Command(byte channel, byte function)
        {
            Channel = channel;
            Function = function;
        }

        public byte[] GetBytes()
        {
            var param = GetParams();
            var pkg = new Package(Channel, Function, param);
            return pkg.ToArray();
        }
    }
}