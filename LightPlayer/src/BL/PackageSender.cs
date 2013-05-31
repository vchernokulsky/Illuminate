using System;

namespace Intems.LightPlayer.BL
{
    public interface IPackageSender
    {
        void SendPackage(Package package);
    }

    public class SerialPortSender : IPackageSender
    {
        public void SendPackage(Package package)
        {
            throw new NotImplementedException();
        }
    }
}
