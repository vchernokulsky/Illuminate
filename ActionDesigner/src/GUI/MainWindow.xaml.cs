﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Intems.LightDesigner.GUI.ViewModels;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightDesigner.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FrameListModel _model = new FrameListModel();

        private static Color RandColor(Random rnd)
        {
            return Color.FromRgb((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255));
        }

        private int _commonLength;
        private void InitData()
        {
            var rnd = new Random();
            for (int i = 0; i < 5; i++)
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
                var frame = new LightPlayer.BL.Frame(TimeSpan.FromSeconds(_commonLength), TimeSpan.FromSeconds(length), cmd);
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

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            var cmdEnum = (CmdEnum) Int32.Parse(btn.Tag.ToString());
           _model.PushBack(cmdEnum);
        }

        private void OnSaveBtnClick(object sender, RoutedEventArgs e)
        {
            var name = "composition.json";
            _model.SaveToFile(name);
        }
    }
}
