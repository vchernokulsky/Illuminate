using System.Runtime.Serialization;
using System.Windows.Media;

namespace Intems.LightPlayer.BL.Commands
{
    public class FadeColor : ISerializable
    {
        private Color _startColor;
        private Color _stopColor;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
