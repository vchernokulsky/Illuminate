using System;
using Intems.LightPlayer.Transport;

namespace Intems.LightPlayer.BL.Commands
{
    public enum CmdEnum
    {
        None,
        SetColor,
        Fade,
        Blink,
        TurnOn,
        TurnOff
    }

    [Serializable]
    public abstract class Command
    {
        public static byte DefaultChannel = 1;

        public abstract byte Channel { get; set; }

        public abstract byte Function { get; set; }

        protected abstract byte[] GetParams();

        //comman methods for all inheritance
        public byte[] GetBytes()
        {
            var param = GetParams();
            var pkg = new Package(Channel, Function, param);
            return pkg.ToArray();
        }
    }
}