using System;
using Intems.LightPlayer.BL;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CommandTest
    {
        [Test]
        public void CreateNewCommand()
        {
            var startTime = TimeSpan.FromSeconds(0);
            var length = TimeSpan.FromSeconds(3.5);

            var cmd = new Command(startTime, length);

            Assert.AreEqual(startTime, cmd.StartTime);
            Assert.AreEqual(length, cmd.Length);
        }

        [Test]
        public void CommandStartRequired()
        {
            var startTime = TimeSpan.FromSeconds(0);
            var length = TimeSpan.FromSeconds(4);

            var cmd = new Command(startTime, length);

            var middle = TimeSpan.FromSeconds(2);
            Assert.IsTrue(cmd.IsStarteRequired(middle));
            //bound checks
            var begin = TimeSpan.FromSeconds(0);
            Assert.IsTrue(cmd.IsStarteRequired(begin));
            var end = TimeSpan.FromSeconds(4);
            Assert.IsFalse(cmd.IsStarteRequired(end));
        }

        [Test]
        public void CommandStartNotRequired()
        {
            var startTime = TimeSpan.FromSeconds(1);
            var length = TimeSpan.FromSeconds(4);

            var cmd = new Command(startTime, length);

            var before = TimeSpan.FromSeconds(0.5);
            Assert.IsFalse(cmd.IsStarteRequired(before));
            var after = startTime + length + TimeSpan.FromSeconds(0.5);
            Assert.IsFalse(cmd.IsStarteRequired(after));
        }
    }
}
