using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static PushdownAutomata.MainWindow;


namespace PushdownAutomata
{
    /// <summary>
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : MahApps.Metro.Controls.MetroWindow
    {
        public Configuration()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        internal void Show(ConfSet settings)
        {
            this.DataContext = settings;
            Show();
            //InitializeComponent();
        }

        private void OperationValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[ab1]");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
