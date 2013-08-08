using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Intems.LightDesigner.GUI.ViewModels;
using Intems.LightPlayer.BL.Commands;
using Microsoft.Win32;

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
                        cmd = new SetColor(RandColor(rnd)){Channel = 1};
                        break;
                    case 1:
                        cmd = new FadeColor(1, RandColor(rnd), RandColor(rnd), (short)length);
                        break;
                    case 2:
                        cmd = new BlinkColor(RandColor(rnd), (short)length){Channel = 1};
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
            var name = "composition.dat";
            _model.SaveToFile(name);
        }

        private void OnLoadBtnClick(object sender, RoutedEventArgs e)
        {
            var openDlg = new OpenFileDialog();
            if (openDlg.ShowDialog().Value)
                _model.LoadFromFile(openDlg.FileName);
        }

        private void OnCellClick(object sender, MouseButtonEventArgs e)
        {
            var curCell = sender as Grid;
            if (curCell != null)
            {
                var frameModel = curCell.Tag as FrameModel;
                if (frameModel != null)
                    frameModel.IsSelected = true;
            }
        }
    }
}
