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
            PreviewKeyDown += MenuView_PreviewKeyDown;
        }

        private void MenuView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (DataContext is not MainWindowViewModel vm)
                return;

            if (e.Key == Key.Delete)
                vm.ConfigurationViewModel.RemoveQuestionCommand.Execute(null);

            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.N)
                vm.CreateNewPackCommand.Execute(null);

            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.O)
                vm.PackOptionsCommand.Execute(null);

            else
                return;

            e.Handled = true;
        }


    }
}
