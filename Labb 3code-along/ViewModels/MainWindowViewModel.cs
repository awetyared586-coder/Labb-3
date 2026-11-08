using Labb_3code_along.Models;
using Labb_3code_along.Command;
using System.Collections.ObjectModel;
using System.Windows;


namespace Labb_3code_along.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<QuestionPackViewModel> Packs { get; } = new();

    private QuestionPackViewModel _activePack;
    public QuestionPackViewModel ActivePack
    {
        get => _activePack;
        set
        {
            if (SetProperty(ref _activePack, value))
            {
                ConfigurationViewModel?.RaiseActivePackChanged();
            }
        }
    }

    public PlayerViewModel? PlayerViewModel { get; }
    public ConfigurationViewModel? ConfigurationViewModel { get; }
    private bool _isConfiguratinMode = true;
    public bool IsConfigurationMode
    {
        get => _isConfiguratinMode;
        set => SetProperty(ref _isConfiguratinMode, value);
    }

    public RelayCommand ShowConfigurationCommand { get; }
    public RelayCommand ShowPlayerCommand { get; }
    public RelayCommand CreateNewPackCommand { get; }
    public event EventHandler? OpenCreatePack;


    public MainWindowViewModel()
    {
        

        var questionPack = new QuestionPack("Mina frågor!", Difficulty.Easy);
        questionPack.Questions.Add(new Question("Vad heter sveriges huvudstad?", "Stockholm", "Oslo", "Göteborg", "London"));
        questionPack.Questions.Add(new Question("Vad är 2 + 3?", "5", "10", "8", "1"));

        ActivePack = new QuestionPackViewModel(questionPack);
        Packs.Add(ActivePack);


        PlayerViewModel = new PlayerViewModel(this);
        ConfigurationViewModel = new ConfigurationViewModel(this);
        ShowConfigurationCommand = new RelayCommand(_ => IsConfigurationMode = true);
        ShowPlayerCommand = new RelayCommand(_ => IsConfigurationMode = false);
        CreateNewPackCommand = new RelayCommand(_ => RaiseOpenCreatePack());                 
    }

    private void RaiseOpenCreatePack()
    {
        OpenCreatePack?.Invoke(this, EventArgs.Empty);
    }

    public void CreateNewPack(string name, Difficulty difficulty)
    {
        var questionPack = new QuestionPack(name, difficulty);
        var newPack = new QuestionPackViewModel(questionPack);

        Packs.Add(newPack);
        ActivePack = newPack;
    }
}
