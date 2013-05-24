using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Intems.LightPlayer.BL.Commands;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AllCommandsTest
    {
        [Test]
        public void SetColorParams()
        {
            var expected = new byte[] {0x7e, 1, 1, 0, 128, 0, 128, 0, 128, 0x7f};

            var color = Color.FromRgb(128, 128, 128);
            var cmd = new SetColor(color);

            var actual = cmd.GetBytes();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
