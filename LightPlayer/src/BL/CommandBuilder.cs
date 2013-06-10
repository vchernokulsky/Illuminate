using Intems.LightPlayer.Transport;

namespace Intems.LightPlayer.BL
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

    internal class CommandBuilder
    {
        public Package CreateCommand(CmdEnum cmd, byte[] param)
        {
            Package result = null;
            switch (cmd)
            {
                case CmdEnum.SetColor:
                    result = new Package(1, (int)CmdEnum.SetColor, param);
                    break;
            }
            return result;
        }
    }
}
