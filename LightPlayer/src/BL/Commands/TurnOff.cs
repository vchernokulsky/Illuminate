using System;

namespace Intems.LightPlayer.BL.Commands
{
    [Serializable]
    public class TurnOff : Command
    {
        public override byte Channel { get; set; }

        public override byte Function
        {
            get { return (byte) CmdEnum.TurnOff; }
            set { }
        }
        protected override byte[] GetParams()
        {
            return new byte[0];
        }
    }
}
