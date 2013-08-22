using System.Linq;
using System.Security.AccessControl;
using Intems.LightDesigner.GUI.ViewModels;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
using NUnit.Framework;

namespace Intems.LightDesigner.Tests.ViewModelTests
{
    [TestFixture]
    class SequenceViewModelTest
    {
        private SequenceViewModel _sequenceViewModel;

        [SetUp]
        public void Initialization()
        {
            _sequenceViewModel = new SequenceViewModel();
        }

        [Test]
        public void AddSingleFrame()
        {
            var frame = new Frame();
            _sequenceViewModel.Add(frame);

            var viewModel = _sequenceViewModel.FrameViewModels.First();
            Assert.IsNotNull(viewModel);
            Assert.AreEqual(viewModel.Frame, frame);
        }

        [Test]
        public void AddSequenceOfFrames()
        {
            var frames = new[] {new Frame(), new Frame(), new Frame(), new Frame()};
            foreach (var frame in frames)
                _sequenceViewModel.Add(frame);

            var framesFromSequence = _sequenceViewModel.FrameViewModels.Select(model => model.Frame);
            CollectionAssert.AreEqual(frames, framesFromSequence);
        }

        [Test]
        public void InsertAfterFirtsFrame()
        {
            var frameForInsert = new Frame {Command = new SetColor()};
            var frames = new[]
            {
                new Frame {Command = new FadeColor()}, new Frame {Command = new BlinkColor()},
                new Frame {Command = new SetColor()}
            };
            foreach (var frame in frames)
                _sequenceViewModel.Add(frame);

            var first = frames.First();
            _sequenceViewModel.InsertAfter(first, new []{frameForInsert});

            var actual = _sequenceViewModel.FrameViewModels[1].Frame;
            Assert.AreEqual(frameForInsert, actual);
        }

        [Test]
        public void InsertAfterLastFrame()
        {
            var frameForInsert = new Frame {Command = new SetColor()};
            var frames = new[] {new Frame {Command = new FadeColor()}, new Frame {Command = new FadeColor()}, new Frame {Command = new FadeColor()}};
            foreach (var frame in frames)
                _sequenceViewModel.Add(frame);

            var last = frames.Last();
            _sequenceViewModel.InsertAfter(last, new []{frameForInsert});

            var actual = _sequenceViewModel.FrameViewModels.Last().Frame;
            Assert.AreEqual(frameForInsert, actual);
        }

        [Test]
        public void SelectFrameGroupAscDirection()
        {
            var frames = new[] { new Frame(), new Frame(), new Frame(), new Frame(), new Frame() };
            foreach (var frame in frames) 
                _sequenceViewModel.Add(frame);
            var expected = new[] {frames[1], frames[2], frames[3]};

            // первый фрейм в выделении
            _sequenceViewModel.FrameViewModels[1].IsSelected = true;
            var lastFrame = _sequenceViewModel.FrameViewModels[3];

            var actual = _sequenceViewModel.SelectGroup(lastFrame);
            Assert.IsNotNull(actual);
            CollectionAssert.IsNotEmpty(actual);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SelectFrameGroupDscDirection()
        {
            var frames = new[] { new Frame(), new Frame(), new Frame(), new Frame(), new Frame() };
            foreach (var frame in frames)
                _sequenceViewModel.Add(frame);
            var expected = new[] { frames[1], frames[2], frames[3] };

            // первый фрейм в выделении
            _sequenceViewModel.FrameViewModels[3].IsSelected = true;
            var lastFrame = _sequenceViewModel.FrameViewModels[1];

            var actual = _sequenceViewModel.SelectGroup(lastFrame);
            Assert.IsNotNull(actual);
            CollectionAssert.IsNotEmpty(actual);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
