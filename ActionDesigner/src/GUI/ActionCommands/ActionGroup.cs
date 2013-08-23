﻿using System;
using System.Linq;
using System.Windows.Input;
using Intems.LightDesigner.GUI.ViewModels;
using Intems.LightPlayer.BL;

namespace Intems.LightDesigner.GUI.ActionCommands
{
    public class ActionGroup 
    {
        private readonly SequenceViewModel _sequenceViewModel;
        private readonly FrameBuffer _frameBuffer;

        public ActionGroup(SequenceViewModel sequenceViewModel)
        {
            _frameBuffer = new FrameBuffer();
            _sequenceViewModel = sequenceViewModel;
        }

        public ICommand CopySelected
        {
            get
            {
                var action = new Action<FrameViewModel>(
                    frameViewModel =>
                    {
                        var frames = _sequenceViewModel.FrameViewModels.Where(model => model.IsSelected).Select(model => (Frame) model.Frame.Clone()).ToList();
                        _frameBuffer.Copy(frames);
                    });
                var copyCommand = new CommonCommand(action);
                return copyCommand;
            }
        }

        public ICommand Paste
        {
            get
            {
                var action =
                    new Action<FrameViewModel>(
                        frameViewModel =>
                        {
                            var frame = frameViewModel.Frame;
                            var bufferedFrames = _frameBuffer.GetFrames();
                            _sequenceViewModel.InsertAfter(frame, bufferedFrames);
                        }
                        );
            var pasteCommand = new CommonCommand(action);
                return pasteCommand;
            }
        }
    }
}