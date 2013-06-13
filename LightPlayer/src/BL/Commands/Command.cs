﻿using System.Windows.Media;
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

    public abstract class Command
    {
        protected static byte DefaultChannel = 1;

        protected Command(byte channel)
        {
            Channel = channel;
        }

        public abstract byte Function { get; }

        public byte Channel { get; set; }

        protected abstract byte[] GetParams();

        public byte[] GetBytes()
        {
            var param = GetParams();
            var pkg = new Package(Channel, Function, param);
            return pkg.ToArray();
        }
    }
}