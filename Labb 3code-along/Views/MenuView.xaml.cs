using Labb_3code_along.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labb_3code_along.View
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
            KeyDown += MenuView_KeyDown;
        }
        private void MenuView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete &&
                        DataContext is MainWindowViewModel mainViewModel)
            {
                mainViewModel.ConfigurationViewModel.RemoveQuestionCommand.Execute(null);
            }
        }




        

    }
}
