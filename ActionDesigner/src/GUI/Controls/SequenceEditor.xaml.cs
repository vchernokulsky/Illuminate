using System;
using System.Windows;
using System.Windows.Controls;
using Intems.LightDesigner.GUI.ViewModels;
using Intems.LightPlayer.BL.Commands;

namespace Intems.LightDesigner.GUI.Controls
{
    /// <summary>
    /// Interaction logic for SequenceEditor.xaml
    /// </summary>
    public partial class SequenceEditor : UserControl
    {
        private SequenceViewModel _viewModel;

        public SequenceEditor()
        {
            InitializeComponent();
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as SequenceViewModel;
            var btn = sender as Button;

            CmdEnum cmd;
            Enum.TryParse(btn.Tag.ToString(), out cmd);
            switch (cmd)
            {
                case CmdEnum.SetColor:
                    var frame = new LightPlayer.BL.Frame {Command = new SetColor()};
                    if (viewModel != null) 
                        viewModel.Add(frame);
                    break;
            }
        }
    }
}
