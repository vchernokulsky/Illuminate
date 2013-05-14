using System.Collections.Generic;

namespace Intems.LightPlayer.BL
{
    internal class Package
    {
        private readonly List<byte> _body;
        public Package(byte channel, byte func, byte[] param)
        {
            _body = new List<byte>();
            _body.Add(0x7E);
            _body.Add(channel);
            _body.Add(func);
            _body.AddRange(param);
            _body.Add(0x7F);
        }

        public byte[] ToArray()
        {
            var array = new byte[]{};
            if (_body != null)
                array = _body.ToArray();
            return array;
        }

        public int Length
        {
            get { return _body.Count; }
        }
    }
}
