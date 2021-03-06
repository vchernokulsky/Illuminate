﻿using System;
using Intems.LightPlayer.BL;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class FrameTest
    {
        [Test]
        public void CreateNewFrame()
        {
            var startTime = TimeSpan.FromSeconds(0);
            var length = TimeSpan.FromSeconds(3.5);

            var cmd = new Frame(startTime, length);

            Assert.AreEqual(startTime, cmd.StartTime);
            Assert.AreEqual(length, cmd.Length);
        }

        [Test]
        public void FrameStartRequired()
        {
            var startTime = TimeSpan.FromSeconds(0);
            var length = TimeSpan.FromSeconds(4);

            var cmd = new Frame(startTime, length);

            var middle = TimeSpan.FromSeconds(2);
            Assert.IsTrue(cmd.IsStartRequired(middle));
            //bound checks
            var begin = TimeSpan.FromSeconds(0);
            Assert.IsTrue(cmd.IsStartRequired(begin));
            var end = TimeSpan.FromSeconds(4);
            Assert.IsFalse(cmd.IsStartRequired(end));
        }

        [Test]
        public void FrameStartNotRequired()
        {
            var startTime = TimeSpan.FromSeconds(1);
            var length = TimeSpan.FromSeconds(4);

            var cmd = new Frame(startTime, length);

            var before = TimeSpan.FromSeconds(0.5);
            Assert.IsFalse(cmd.IsStartRequired(before));
            var after = startTime + length + TimeSpan.FromSeconds(0.5);
            Assert.IsFalse(cmd.IsStartRequired(after));
        }
    }
}
