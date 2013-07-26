using System;
using System.Windows.Media;
using Intems.LightPlayer.BL.Commands;
using Intems.LightPlayer.BL.Helpers;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AllCommandsTest
    {
        [Test]
        public void SetColorToBytes()
        {
            var expected = new byte[] {0x7e, 1, (byte)CmdEnum.SetColor, 0, 128, 0, 128, 0, 128, 0x7f};

            var color = Color.FromRgb(128, 128, 128);
            var cmd = new SetColor(color){Channel = 1};

            var actual = cmd.GetBytes();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void BlinkColorToBytes()
        {
            var expected = new byte[] { 0x7e, 1, (byte)CmdEnum.Blink, 0, 128, 0, 128, 0, 128, 0x00, 137.ToByte(), 0x7f };

            var color = Color.FromRgb(128, 128, 128);
            var cmd = new BlinkColor(color, 137){Channel = 1};

            var actual = cmd.GetBytes();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void FadeColorToBytes()
        {
            var expected = new Byte[]
            {0x7e, 1, (byte) CmdEnum.Fade, 0, 128, 0, 128, 0, 128, 0, 255, 0, 255, 0, 255, 0x00, 0x0a, 0x7f};

            var color1 = Color.FromRgb(128, 128, 128);
            var color2 = Color.FromRgb(255, 255, 255);
            var cmd = new FadeColor(color1, color2, 10);

            var actual = cmd.GetBytes();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
