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

namespace Menu_Text_Redactor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _currentFilePath;

        public MainWindow()
        {
            InitializeComponent();
            menuItemExit.Click += MenuItemExit_Click;
            menuItemNew.Click += MenuItemNew_Click;
            menuItemOpen.Click += MenuItemOpen_Click;
            menuItemSave.Click += MenuItemSave_Click;
            textBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            textBlockLines.Text = "Ln " + (textBox.Text.Count(x => x == '\n') + 1);
            textBlockChars.Text = "Cn " + (textBox.Text.Count();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                _currentFilePath = saveFileDialog.FileName;
                try
                {
                    using (StreamWriter writer = new StreamWriter(_currentFilePath))
                    {
                        writer.Write(textBox.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving file: " + ex.Message);
                }
                Title = _currentFilePath;
            }
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                _currentFilePath = openFileDialog.FileName;
                try
                {
                    using (StreamReader reader = new StreamReader(_currentFilePath))
                    {
                        textBox.Text = reader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening file: " + ex.Message);
                }
                Title = _currentFilePath;
            }
        }

        private void MenuItemNew_Click(object sender, RoutedEventArgs e)
        {
            if (_currentFilePath != null)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(_currentFilePath))
                    {
                        writer.Write(textBox.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving file: " + ex.Message);
                }
            }

            textBox.Text = string.Empty; 
            _currentFilePath = null; 
            Title = "Untitled";      
        }
    }
}