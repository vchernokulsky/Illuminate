using System;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
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
            var actual = seq.CommandByTime(beginBound);
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

            var cmdMid1 = TimeSpan.FromMilliseconds(10);
            var actual1 = seq.CommandByTime(cmdMid1);
            Assert.AreEqual(cmd1, actual1);

            var cmdMid2 = TimeSpan.FromSeconds(2.5);
            var actual2 = seq.CommandByTime(cmdMid2);
            Assert.AreEqual(cmd2, actual2);
        }

        [Test]
        public void GetOneCommandTwice()
        {
            var seq = new CommandSequence();
            var cmd1 = CreateComand(1, 3);
            var cmd2 = CreateComand(4, 2);
            seq.PushCommand(cmd1);
            seq.PushCommand(cmd2);

            var cmdMiddle1 = TimeSpan.FromSeconds(1.5);
            var actual1 = seq.CommandByTime(cmdMiddle1);
            Assert.AreEqual(cmd1, actual1);

            var cmdMiddle2 = TimeSpan.FromSeconds(1.6);
            var actual2 = seq.CommandByTime(cmdMiddle2);
            Assert.IsNull(actual2);
        }
    }
}
