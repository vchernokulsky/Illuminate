using System.Windows;
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;
using UserControl = System.Windows.Controls.UserControl;

namespace CtrlSpike.Controls
{
    /// <summary>
    /// Interaction logic for ColorSelect.xaml
    /// </summary>
    public partial class Colorer : UserControl
    {
        public Colorer()
        {
            InitializeComponent();
        }

        private void OnSelectButtonClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                var dlg = new ColorDialog();
                dlg.ShowDialog();
            }
        }
    }
}
