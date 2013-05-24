using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media;

namespace Intems.LightPlayer.BL.Commands
{
    public class SetColor : Command
    {
        private readonly Color _color;

        public SetColor(Color color) : base(1, (byte) CmdEnum.SetColor)
        {
            _color = color;
        }

        public SetColor(byte red, byte green, byte blue) : this(Color.FromRgb(red, green, blue))
        {
            _color = new Color { R = red, G = green, B = blue };
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_color", _color);
        }

        protected override byte[] GetParams()
        {
            throw new NotImplementedException();
        }
    }
}
