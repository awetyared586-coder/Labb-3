using Labb_3code_along.Command;
using Labb_3code_along.Dialog;
using Labb_3code_along.ViewModels;
using System.Windows;

namespace Labb_3code_along
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
        InitializeComponent();
            DataContext = new MainWindowViewModel();

            ((MainWindowViewModel)DataContext).OpenCreatePack += (_, _) =>
            {
                var dialog = new CreateNewPackDialog();
               if ( dialog.ShowDialog() == true)
                {
                    ((MainWindowViewModel)DataContext).CreateNewPack(
                        dialog.PackName,
                        dialog.SelectedDifficulty,
                        dialog.TimeLimitInSeconds);

                }
            };

            ((MainWindowViewModel)DataContext).OpenPackOption += (_, _) =>
            {
                var dialog = new PackOptionsDialog();
                dialog.DataContext = ((MainWindowViewModel)DataContext).ActivePack;
                dialog.Owner = this;
                dialog.ShowDialog();
            };
        }
    }


}

