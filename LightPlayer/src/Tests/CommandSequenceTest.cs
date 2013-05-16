using Intems.LightPlayer.BL;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CommandSequenceTest
    {
        [Test]
        public void PushOneCommand()
        {
            var seq = new CommandSequence();
            var cmd = new Command();

            seq.PushCommand(cmd);
        }
    }
}
