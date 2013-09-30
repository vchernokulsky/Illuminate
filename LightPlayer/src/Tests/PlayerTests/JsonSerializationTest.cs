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
            var frame1 = new Frame(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(3.5))
            {
                Command = new SetColor(color){Channel = 1}
            };
            var frame2 = new Frame(TimeSpan.FromSeconds(3.5), TimeSpan.FromSeconds(2))
            {
                Command = new SetColor(color){Channel = 1}
            };
            var seq = new FrameSequence();
            seq.Push(frame1);
            seq.Push(frame2);

            var s = JsonSerializer.SerializeToString(seq);
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
