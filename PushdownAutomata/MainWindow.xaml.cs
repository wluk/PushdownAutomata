using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace PushdownAutomata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Configuration ConfWindow;
        Stack Stack = new Stack();
        List<string> SimulataStack = new List<string>();
        List<string> xyz = new List<string>();
        char[] Input;
        ConfSet Settings;
        const char AChar = 'a';
        const char BChar = 'b';
        const char EndOfStack = 'F';
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
            };
            Stack.Push(EndOfStack);
            SimulataStack.Add(EndOfStack.ToString());
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            //start
            xyz = inputText.Text.Select(c => c.ToString()).ToList();
            Processing();
            //STATR
            //

            SimulataStack.Reverse();
            foreach (var item in SimulataStack)
            {
                listBox.Items.Add(item);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ConfWindow = new Configuration();
            ConfWindow.Show(Settings);
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {

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

                switch (word[0])
                {
                    case AChar: SelectOperationForA(SimulataStack.First()[0]); break;
                    case BChar: SelectOperationForB(SimulataStack.First()[0]); break;
                    default:
                        break;
                }

                Application.Current.Dispatcher.BeginInvoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(() => this.ProcessingChar.Content = word)
                    );

                Thread.Sleep(10000);
                listBox.Items.Clear();
                foreach (var item in SimulataStack)
                {
                    listBox.Items.Add(item);
                }
                Processing();
            }
            else
            {

            }
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
    }
}
