﻿using System;
using System.IO;

namespace Intems.LightPlayer.Transport.Stubs
{
    public class FakePackageSender : IPackageSender
    {
        private readonly StreamWriter _writer;

        public FakePackageSender()
        {
            _writer = new StreamWriter("send.log");
        }

        public void SendPackage(Package package)
        {
            var dt = DateTime.Now;
            _writer.Write(dt.ToString("mm:ss.fff") + "    :    ");
            _writer.WriteLine(package.ToString());
            _writer.Flush();
        }
    }
}
