using System.Collections.Generic;
using System.Text;

namespace Intems.Illuminate.HardwareTester
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var b in _body)
            {
                sb.Append(b.ToString("X"));
                sb.Append(' ');
            }
            var lastIdx = _body.Count - 1;
            sb.Remove(lastIdx, 1);

            return sb.ToString();
        }

    }
}
