using System;
using Intems.LightPlayer.BL;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CommandSequenceTest
    {
        private Command CreateComand(double start, double length)
        {
            return new Command(TimeSpan.FromSeconds(start), TimeSpan.FromSeconds(length));
        }

        [Test]
        public void PushOneCommand()
        {
            var seq = new CommandSequence();
            var expected = CreateComand(0, 3);
            seq.PushCommand(expected);

            var beginBound = TimeSpan.FromSeconds(0);
            var actual = seq.GetCommand(beginBound);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PushTwoSequencedCommands()
        {
            var seq = new CommandSequence();
            var cmd1 = CreateComand(0, 2);
            var cmd2 = CreateComand(2, 3.5);
            seq.PushCommand(cmd1);
            seq.PushCommand(cmd2);

            var cmd1Middle = TimeSpan.FromMilliseconds(10);
            var actual1 = seq.GetCommand(cmd1Middle);
            Assert.AreEqual(cmd1, actual1);

            var cmd2Middle = TimeSpan.FromSeconds(2.5);
            var actual2 = seq.GetCommand(cmd2Middle);
            Assert.AreEqual(cmd2, actual2);
        }
    }
}
