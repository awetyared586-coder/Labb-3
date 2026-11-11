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
        private bool _allowClose;
        public MainWindow()
        {
        InitializeComponent();
            DataContext = new MainWindowViewModel();

            Loaded += async (_, _) =>
            {
                try
                {
                    await ((MainWindowViewModel)DataContext).LoadPacksAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Kunde inte läsa in frågepaketen: {ex.Message}",
                          "Inläsningsfel",
                          MessageBoxButton.OK,
                          MessageBoxImage.Error);


                }
            };
            
            Closing += MainWindow_Closing;


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
        private async void MainWindow_Closing(
    object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_allowClose)
                return;

            e.Cancel = true;

            try
            {
                await ((MainWindowViewModel)DataContext).SavePacksAsync();

                _allowClose = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Kunde inte spara frågepaketen: {ex.Message}",
                    "Sparfel",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


    }
}

