using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Intems.LightDesigner.GUI.ViewModels;

namespace Intems.LightDesigner.GUI.Controls
{
    /// <summary>
    /// Interaction logic for FrameEditor.xaml
    /// </summary>
    public partial class FrameEditor : UserControl
    {
        public FrameEditor()
        {
            InitializeComponent();
        }

        private void OnCellClick(object sender, MouseButtonEventArgs e)
        {
            var curCell = sender as Grid;
            if (curCell == null) return;

            var frameView = curCell.Tag as SequenceViewModel;
            if (frameView == null) return;

//            _viewModel.CurrentView = frameView;
//            switch (Keyboard.Modifiers)
//            {
//                case ModifierKeys.Shift:
//                    _viewModel.SelectGroup(frameView);
//                    break;
//
//                case ModifierKeys.None:
//                    _viewModel.ClearSelection();
//                    frameView.IsSelected ^= true;
//                    break;
//            }
        }
    }
}
