using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Intems.LightDesigner.GUI.ViewModels;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightDesigner.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FrameListModel _model;

        private int _commonLength;
        private void InitData()
        {
            var rnd = new Random();
            var frameModels = new List<FrameModel>();
            for (int i = 0; i < 20; i++)
            {
                Command cmd = null;
                int cmdType = rnd.Next(2);
                int length = rnd.Next(1, 15);
                switch (cmdType)
                {
                    case 0:
                        cmd = new SetColor(1, Color.FromRgb(128, 128, 128));
                        break;
                    case 1:
                        cmd = new FadeColor(1, Color.FromRgb(21, 100, 50), Color.FromRgb(255,255,255), (short) length);
                        break;
                    case 2:
                        cmd = new BlinkColor(1, Color.FromRgb(125, 18, 90), (short) length);
                        break;
                }
                var frame = new Frame(TimeSpan.FromSeconds(_commonLength), TimeSpan.FromSeconds(length), cmd);
                frameModels.Add(new FrameModel(frame));
                _commonLength += length;
            }
            _model = new FrameListModel {FrameViews = frameModels};
        }

        public MainWindow()
        {
            InitializeComponent();

            InitData();
            DataContext = _model;
        }
    }
}
