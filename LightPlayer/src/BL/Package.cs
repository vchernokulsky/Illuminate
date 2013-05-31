﻿using System;
using System.Collections.Generic;

namespace Intems.LightPlayer.BL
{
    public class Package
    {
        private readonly List<byte> _body;

        public Package(IEnumerable<byte> bytes)
        {
            _body = new List<byte>(bytes);
        }

        public Package(byte channel, byte func, byte[] param)
        {
            _body = new List<byte> {0x7E, channel, func};
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

        public override bool Equals(object obj)
        {
            var isEqual = false;

            var arg = obj as Package;
            if (arg != null && arg.Length == Length)
            {
                isEqual = true;
                for (int i = 0; i < _body.Count; i++)
                    isEqual &= (_body[i] == arg._body[i]);
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            return _body.GetHashCode();
        }
    }
}
