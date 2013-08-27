using System.Collections.Generic;

namespace Intems.LightPlayer.Transport
{
    public interface IPackageSender
    {
        void SendPackage(Package package);
        void SendPackages(IEnumerable<Package> packages);
    }
}