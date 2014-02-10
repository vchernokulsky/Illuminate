using System;
using System.Collections.Generic;

namespace Intems.LightPlayer.Transport
{
    public class BroadcastSender : IPackageSender
    {
        public void SendPackage(Package package)
        {
            throw new NotImplementedException();
        }

        public void SendPackages(IEnumerable<Package> packages)
        {
            throw new NotImplementedException();
        }
    }
}
