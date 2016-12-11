using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MahApps.Metro.Controls;

namespace PushdownAutomata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Configuration ConfWindow;
        public ConfSet Settings;
        Service PushdownAutomat;

        public int ProgressValue { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Settings = new ConfSet('a', '1', 'a', 'b', '1', 'b', 10);
            PushdownAutomat = new Service(this);

            stackGraph.Text = "F";

            LoadGraph();
        }

        private void LoadGraph()
        {
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri("graf.jpg", UriKind.RelativeOrAbsolute);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();
            image.Source = src;
            image.Stretch = Stretch.Uniform;

            FinishState.Visibility = Visibility.Hidden;
            Foperration.Visibility = Visibility.Hidden;
            ChangeState.Visibility = Visibility.Hidden;

            StartState.Visibility = Visibility.Visible;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            stackGraph.Text = string.Empty;
            PushdownAutomat.Stack.Clear();
            PushdownAutomat.Stack.Add("F");
            stackGraph.Text = "F";
            RefreshControls();
            Progress.Value = 0;
            PushdownAutomat.Step = 0;

            PushdownAutomat.Input = inputText.Text.Select(c => c.ToString()).ToList();

            Aoperation.Visibility = Visibility.Hidden;
            Boperation.Visibility = Visibility.Hidden;
            FinishState.Visibility = Visibility.Hidden;
            Foperration.Visibility = Visibility.Hidden;
            ChangeState.Visibility = Visibility.Visible;
            StartState.Visibility = Visibility.Hidden;

            PushdownAutomat.Processing();
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            ConfWindow = new Configuration();
            ConfWindow.Show(Settings);
        }

        private void InputValidation(object sender, TextCompositionEventArgs e)
        {
            string regexPattern = string.Format("[ab]");
            Regex regex = new Regex(regexPattern);
            e.Handled = !regex.IsMatch(e.Text);
        }

        public void ProgressBarUpdate()
        {
            for (int i = 0; i < Progress.Maximum / inputText.Text.Length; i++)
            {
                Progress.Value++;
                Progress.Dispatcher.Invoke(DispatcherPriority.Input, EmptyDelegate);
                if (i % 2 == 0)
                {
                    Thread.Sleep(Settings.TmieStep * 10);
                }
            }
        }

        private static Action EmptyDelegate = delegate () { };
        public void RefreshControls()
        {
            stackGraph.Dispatcher.Invoke(DispatcherPriority.Input, EmptyDelegate);
            Aoperation.Dispatcher.Invoke(DispatcherPriority.Input, EmptyDelegate);
            Boperation.Dispatcher.Invoke(DispatcherPriority.Input, EmptyDelegate);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnStartEnable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)e.Source).Text.Length != 0)
            {
                start.IsEnabled = true;
            }
            else
            {
                start.IsEnabled = false;
            }
        }
    }
}
