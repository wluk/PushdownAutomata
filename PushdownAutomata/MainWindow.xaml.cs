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
        List<string> SimulataStack = new List<string>();
        List<string> xyz = new List<string>();
        ConfSet Settings;
        const char AChar = 'a';
        const char BChar = 'b';
        const char EndOfStack = 'F';
        private static MainWindow _instance = null;
        public int Step { get; set; }
        public int ProgressValue { get; set; }

        public static MainWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainWindow();
                }
                return _instance;
            }
        }
        public class ConfSet
        {
            //a
            public char ASame { get; set; }
            public char AOther { get; set; }
            public char AEndStack { get; set; }
            //b
            public char BSame { get; set; }
            public char BOther { get; set; }
            public char BEndStack { get; set; }
            public int TmieStep { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            Settings = new ConfSet()
            {
                ASame = 'a',
                AOther = 'b',
                AEndStack = 'F',
                BSame = 'a',
                BOther = 'b',
                BEndStack = 'F',
                TmieStep = 5
            };            
            stackGraph.Text += EndOfStack.ToString();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            stackGraph.Text = string.Empty;
            SimulataStack.Clear();
            SimulataStack.Add(EndOfStack.ToString());
            Progress.Value = 0;
            Step = 0;
            xyz = inputText.Text.Select(c => c.ToString()).ToList();
            Processing();
        }

        private void button_Click(object sender, RoutedEventArgs e)
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

        private void InputCharValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(".");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void Processing()
        {
            if (SimulataStack.Count >= 1 && xyz.Count > 0)
            {
                var word = xyz.First();
                xyz.Remove(xyz.First());
                Step++;

                ProcessingChar.Inlines.Clear();
                ProcessingChar.Inlines.Add(new Run("Odczytano -> " + word + " znak numer: " + Step + " z " + inputText.Text.Length));
                ProcessingChar.Inlines.Add(new LineBreak());
                ProcessingChar.Inlines.Add(new Run("na górze stosu " + SimulataStack.First().ToString()));




                switch (word[0])
                {
                    case AChar: SelectOperationForA(SimulataStack.First()[0]); break;
                    case BChar: SelectOperationForB(SimulataStack.First()[0]); break;
                    default:
                        break;
                }


                stackGraph.Text = string.Empty;
                foreach (var item in SimulataStack)
                {
                    stackGraph.Text += item.ToString() + "\n";
                }
                //http://www.wpf-tutorial.com/misc-controls/the-progressbar-control/
                ProgressBarUpdate();
                Upd();
                //Thread.Sleep(Settings.TmieStep * 1000);
                Processing();
            }
            else
            {

            }
        }

        private void ProgressBarUpdate()
        {
            for (int i = 0; i < Progress.Maximum / inputText.Text.Length; i++)
            {
                Progress.Value++;
                Progress.Dispatcher.Invoke(DispatcherPriority.Input, EmptyDelegate);
                if (i%2==0)
                {
                    Thread.Sleep(Settings.TmieStep*10);
                }
            }
        }


        private static Action EmptyDelegate = delegate () { };
        private void Upd()
        {
            stackGraph.Dispatcher.Invoke(DispatcherPriority.Input, EmptyDelegate);
            ProcessingChar.Dispatcher.Invoke(DispatcherPriority.Input, EmptyDelegate);            
        }

        private void SelectOperationForA(char onTopStack)
        {
            char result = new char();
            switch (onTopStack)
            {
                case AChar: result = Settings.ASame; break;
                case BChar: result = Settings.AOther; break;
                case EndOfStack: result = Settings.AEndStack; break;
                default:
                    break;
            }

            ProcessingChar.Inlines.Add(new LineBreak());
            ProcessingChar.Inlines.Add(new Run("operacja -> " + result));

            switch (result)
            {
                case AChar: SimulataStack.Insert(0, AChar.ToString()); break;
                case BChar: SimulataStack.Remove(SimulataStack.First()); break;
                case EndOfStack: SimulataStack.Insert(0, AChar.ToString()); break;
                default:
                    break;
            }
        }

        private void SelectOperationForB(char onTopStack)
        {
            char result = new char();
            switch (onTopStack)
            {
                case AChar: result = Settings.BOther; break;
                case BChar: result = Settings.BSame; break;
                case EndOfStack: result = Settings.BEndStack; break;
                default:
                    break;
            }

            switch (result)
            {
                case AChar: SimulataStack.Insert(0, AChar.ToString()); break;
                case BChar: SimulataStack.Remove(SimulataStack.First()); break;
                case EndOfStack: SimulataStack.Insert(0, AChar.ToString()); break;
                default:
                    break;
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void inputText_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void inputText_TextChanged(object sender, TextChangedEventArgs e)
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
