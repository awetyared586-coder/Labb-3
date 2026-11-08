using Labb_3code_along.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Labb_3code_along.Dialog
{
    /// <summary>
    /// Interaction logic for CreateNewPackDialog.xaml
    /// </summary>
    public partial class CreateNewPackDialog : Window
    {
        public CreateNewPackDialog()
        {
            InitializeComponent();
        }

        private void CreatePackButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        public string PackName => packNameTextBox.Text;

        public Difficulty SelectedDifficulty =>
         Enum.Parse<Difficulty>(
        ((ComboBoxItem)difficultyComboBox.SelectedItem).Content.ToString()!
    );


    }
}
