using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
using Intems.LightPlayer.Transport;

namespace Spike
{
    public class Program
    {
        public static FrameProcessor _processor;

        static void ManyShortSends()
        {
            var seq = new FrameSequence();
            for (int i = 0; i < 100; i++)
            {
                var frame = new Frame(TimeSpan.FromSeconds(i), TimeSpan.FromSeconds(1)){Command = new SetColor(1, Color.FromRgb(128,128,128))};
                seq.Push(frame);
            }

            IPackageSender sender = new FakePackageSender();
            _processor = new FrameProcessor(sender, seq);
            _processor.Start();
        }

        static void Main(string[] args)
        {
            var th = new Thread(ManyShortSends);
            th.Start();
            th.Join();
            var t = _processor.AudioReader;
            Thread.Sleep(60 * 000);
        }
    }
}
