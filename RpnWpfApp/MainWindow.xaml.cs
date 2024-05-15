using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RpnLogic;

namespace RpnWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string input = txtboxInput.Text;
            if (txtboxVarX.Text == string.Empty)
            {
                double result = new RpnCalculator(input).Result;
                lblResult.Content = result;
            }
            else
            {
                int varX = int.Parse(txtboxVarX.Text);
                double result = new RpnCalculator(input, varX).Result;
                lblResult.Content = result;
            }
        }
    }
}