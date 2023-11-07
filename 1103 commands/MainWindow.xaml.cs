using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace _1103_commands
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        bool trial = true;
        bool unregistered = true;
        string mode = "unregistered";
        public MainWindow()
        {
            InitializeComponent();

            CommandBinding binding;
            binding = new CommandBinding(ApplicationCommands.New);
            binding.Executed += NewCommand_Executed;
            binding.CanExecute += NewCommand_CanExecute;
            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Open);
            binding.Executed += OpenCommand_Executed;
            binding.CanExecute += OpenCommand_CanExecute;
            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Save);
            binding.Executed += SaveCommand_Executed;
            binding.CanExecute += SaveCommand_CanExecute;
            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Cut);
            binding.Executed += CutCommand_Executed;
            binding.CanExecute += CutCommand_CanExecute;
            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Copy);
            binding.Executed += CopyCommand_Executed;
            binding.CanExecute += CopyCommand_CanExecute;
            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Paste);
            binding.Executed += PasteCommand_Executed;
            binding.CanExecute += PasteCommand_CanExecute;
            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Close);
            binding.Executed += CloseCommand_Executed;
            CommandBindings.Add(binding);

        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TextEditor.Text = "";
            isChanged = false;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
                TextEditor.Text = File.ReadAllText(openFileDialog.FileName);
            isChanged = false;
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, TextEditor.Text);
        }
        private void CutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New command triggered with " + e.Source.ToString());
        }

        private void CopyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New command triggered with " + e.Source.ToString());
        }

        private void PasteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Save command triggered with " + e.Source.ToString());
        }
        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
        private void RegisterCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            if (DialogResult == rw.ShowDialog())
            {
                mode = rw.registermode;
            }
            mode= rw.registermode;
        }

        private bool isChanged = false;
        private void txt_TextChanged(object sender, RoutedEventArgs e)
        {
            isChanged = true;
        }
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (mode == "trial" || mode == "pro")
            {
                e.CanExecute = true;
            }
        }
        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (mode == "trial" || mode == "pro")
            {
                e.CanExecute = true;
            }
        }
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((mode=="trial"||mode=="pro") && isChanged == true)
            {
                e.CanExecute = true;
            }
        }
        private void CutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (mode == "trial" || mode == "pro")
            {
                e.CanExecute = true;
            }

        }
        private void CopyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (trial == false && unregistered == false)
            {
                e.CanExecute = true;
            }
        }
        private void PasteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (trial == false && unregistered==false)
            {
                e.CanExecute = true;
            }
        }
        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if ((e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Paste) && mode != "pro")
            {
                e.Handled = true;
            }
        }

    }
    public class DataCommands
    {
        private static RoutedUICommand Register;
        static DataCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));
            Register = new RoutedUICommand(
              "Register", "Register", typeof(DataCommands), inputs);
        }
        public static RoutedUICommand _Register
        {
            get { return Register; }
        }
    }
}
