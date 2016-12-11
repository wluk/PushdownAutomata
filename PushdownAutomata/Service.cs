using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PushdownAutomata
{
    public class Service
    {
        private MainWindow GUI;
        public int Step { get; set; }
        public List<string> Stack = new List<string>();
        public List<string> Input = new List<string>();

        const char AChar = 'a';
        const char BChar = 'b';
        const char EndOfStack = 'F';
        const char PopStack = '1';

        public Service(MainWindow gui)
        {
            GUI = gui;
        }

        private void SelectOperationForB(char onTopStack)
        {
            char result = new char();
            switch (onTopStack)
            {
                case AChar: result = GUI.Settings.BOther; break;
                case BChar: result = GUI.Settings.BSame; break;
                case EndOfStack: result = GUI.Settings.BEndStack; break;
                default:
                    break;
            }

            GUI.Aoperation.Visibility = Visibility.Hidden;
            GUI.Boperation.Visibility = Visibility.Visible;
            GUI.Boperation.Content = Stack.First().ToString() + "/" + result.ToString();

            Operation(result);
        }

        private void Operation(char symbol)
        {
            switch (symbol)
            {
                case AChar:
                    Stack.Insert(0, AChar.ToString());
                    break;
                case BChar:
                    Stack.Insert(0, BChar.ToString());
                    break;
                case PopStack:
                    Stack.Remove(Stack.First());
                    break;
                default:
                    break;
            }
        }

        private void SelectOperationForA(char onTopStack)
        {
            char result = new char();
            switch (onTopStack)
            {
                case AChar: result = GUI.Settings.ASame; break;
                case BChar: result = GUI.Settings.AOther; break;
                case EndOfStack: result = GUI.Settings.AEndStack; break;
                default:
                    break;
            }

            GUI.Boperation.Visibility = Visibility.Hidden;
            GUI.Aoperation.Visibility = Visibility.Visible;
            GUI.Aoperation.Content = Stack.First().ToString() + "/" + result.ToString();

            Operation(result);
        }

        public void Processing()
        {
            if (Stack.Count >= 1 && Input.Count > 0)
            {
                var word = Input.First();
                Input.Remove(Input.First());
                Step++;

                switch (word[0])
                {
                    case AChar: SelectOperationForA(Stack.First()[0]); break;
                    case BChar: SelectOperationForB(Stack.First()[0]); break;
                    default:
                        break;
                }

                GUI.stackGraph.Text = string.Empty;
                foreach (var item in Stack)
                {
                    GUI.stackGraph.Text += item.ToString() + "\n";
                }

                GUI.ProgressBarUpdate();
                GUI.RefreshControls();
                Processing();
            }
            else if (Stack.Count == 0)
            {
                GUI.Aoperation.Visibility = Visibility.Hidden;
                GUI.Boperation.Visibility = Visibility.Hidden;
                GUI.FinishState.Visibility = Visibility.Visible;
                GUI.Foperration.Visibility = Visibility.Visible;
                GUI.ChangeState.Visibility = Visibility.Hidden;
            }
            else
            {
                GUI.Aoperation.Visibility = Visibility.Hidden;
                GUI.Boperation.Visibility = Visibility.Hidden;
                GUI.FinishState.Visibility = Visibility.Visible;
                GUI.Foperration.Visibility = Visibility.Hidden;
                GUI.ChangeState.Visibility = Visibility.Hidden;
            }
        }
    }
}
