using System;
using System.Windows.Media;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
using NUnit.Framework;
using ServiceStack.Text;

namespace Tests
{
    [TestFixture]
    class JsonSerializationTest
    {
        [Test]
        public void SaveFrame()
        {
            var color = Color.FromRgb(128, 128, 128);
            var frame = new Frame(TimeSpan.FromSeconds(0), TimeSpan.FromMilliseconds(3500))
            {
                Command = new SetColor(1, color)
            };

            var s = JsonSerializer.SerializeToString(frame);
            Console.WriteLine(s);
        }

        [Test]
        public void LoadFrame()
        {
            var str =
                "{\"StartTime\":\"PT0S\",\"Length\":\"PT3.5S\",\"Command\":{\"__type\":\"Intems.LightPlayer.BL.Commands.SetColor, BL\",\"Color\":\"#FF808080\"}}";

            var frame = JsonSerializer.DeserializeFromString<Frame>(str);
            Console.WriteLine(frame);
        }
    }
}
