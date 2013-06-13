﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Intems.LightDesigner.GUI.ViewModels;
using Intems.LightPlayer.BL;
using Intems.LightPlayer.BL.Commands;
using Microsoft.Win32;
using Xceed.Wpf.Toolkit;

namespace Intems.LightDesigner.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FrameListModel _model = new FrameListModel();

        private static Color RandColor(Random rnd)
        {
            return Color.FromRgb((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255));
        }

        private int _commonLength;
        private void InitData()
        {
            var rnd = new Random();
            var frameModels = new List<FrameModel>();
            for (int i = 0; i < 20; i++)
            {
                Command cmd = null;
                int cmdType = rnd.Next(3);
                int length = rnd.Next(1, 15);
                switch (cmdType)
                {
                    case 0:
                        cmd = new SetColor(1, RandColor(rnd));
                        break;
                    case 1:
                        cmd = new FadeColor(1, RandColor(rnd), RandColor(rnd), (short)length);
                        break;
                    case 2:
                        cmd = new BlinkColor(1, RandColor(rnd), (short)length);
                        break;
                }
                var frame = new Frame(TimeSpan.FromSeconds(_commonLength), TimeSpan.FromSeconds(length), cmd);
                _model.Add(frame);
                _commonLength += length;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            InitData();
            DataContext = _model;
        }

        private void OnRectangleMouseUp(object sender, MouseButtonEventArgs e)
        {
            var rect = sender as Rectangle;
            if (rect != null)
            {
//                var picker = new ColorPicker(){};
//                picker.Visibility = Visibility.Visible;
//                picker.SelectedColor
            }
        }
    }
}
