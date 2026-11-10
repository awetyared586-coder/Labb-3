
using Labb_3code_along.Models;
using Labb_3code_along.Command;
using System.Windows.Threading;
using System.Drawing;
using System.Windows;
namespace Labb_3code_along.ViewModels;

internal class PlayerViewModel : ViewModelBase
{
    private readonly MainWindowViewModel? _mainWindowViewModel;

    private readonly DispatcherTimer _timer = new();
    private int _timeRemaining;
    public string TimeRemaining => _timeRemaining.ToString();
    private int _currentQuestionIndex = 0;
    public int CurrentQuestionIndex => _currentQuestionIndex + 1;
    private int _score = 0;
    private bool _hasAnswered = false;
    private bool _isQuizFinished = false;
    public bool IsQuizFinished => _isQuizFinished;
   
    private string[] _feedbackSymbols = new string[4];
    public string[] FeedbackSymbols => _feedbackSymbols;

    private string[] _shuffledAnswers = Array.Empty<string>();

    private void ShuffleAnswers()
    {
        _shuffledAnswers = new[] { CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3 };
        Random.Shared.Shuffle(_shuffledAnswers);
        
    }
    public String[] ShuffledAnswers => _shuffledAnswers;

    public int Score => _score;
    public string FinalResult => _isQuizFinished  
        ?$"Du fick{_score} av {TotalQuestions} rätt!":"";
    public bool IsQuizInProgress => !_isQuizFinished;
   


    private string _fedbackSymobol = "";
    public string FeedbackSymbol
    {
        get => _fedbackSymobol;
        set => SetProperty(ref _fedbackSymobol, value);
    }
    




    private Question? CurrentQuestion =>

    _mainWindowViewModel?.ActivePack.Questions[_currentQuestionIndex];
    public string CurrentQuestionText => CurrentQuestion?.Query ?? "";
    public string CorrectAnswer => CurrentQuestion?.CorrectAnswer ?? "";
    public string IncorrectAnswer1 => CurrentQuestion?.IncorrectAnswers[0] ?? "";
    public string IncorrectAnswer2 => CurrentQuestion?.IncorrectAnswers[1] ?? "";
    public string IncorrectAnswer3 => CurrentQuestion?.IncorrectAnswers[2] ?? "";
    public int TotalQuestions => _mainWindowViewModel?.ActivePack.Questions.Count ?? 0;
    public string QuestionProgress => $"Fråga {CurrentQuestionIndex} av {TotalQuestions}";
    public RelayCommand AnswerCommand { get; }
    public RelayCommand NextQuestionCommand { get; }

    public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;

        _timeRemaining = mainWindowViewModel?.ActivePack.TimeLimitInSeconds ?? 0;

        _timer.Interval = TimeSpan.FromSeconds(1);

        _timer.Tick += Timer_Tick;


        AnswerCommand = new RelayCommand(AnswerQuestion);
        NextQuestionCommand = new RelayCommand( _ => NextQuestion());

        ShuffleAnswers();

    }

    private void AnswerQuestion(object? parameter)
    {
        if (_hasAnswered)
            return;
        
        int answerIndex= int.Parse(parameter?.ToString()??"-1");

        _hasAnswered= true;
       
       
        
        

        string? selectedAnswer = _shuffledAnswers[answerIndex];

       
        bool isCorrect = selectedAnswer == CorrectAnswer;

        if (isCorrect)
        {
            _score++;
            RaisePropertyChanged(nameof(Score));
           
        }

        _feedbackSymbols[answerIndex] = isCorrect ? "✓" : "✗";
        _feedbackSymbols[Array.IndexOf(_shuffledAnswers, CorrectAnswer)] = "✓";
        RaisePropertyChanged(nameof(FeedbackSymbols));
        




    }
    private void NextQuestion()
    {
        if (_currentQuestionIndex >= TotalQuestions - 1)
        {
            _isQuizFinished = true;
            RaisePropertyChanged(nameof(IsQuizFinished));
            RaisePropertyChanged(nameof(IsQuizInProgress));
            RaisePropertyChanged(nameof(FinalResult));
            return;

        }


        _currentQuestionIndex++;
        _timeRemaining = _mainWindowViewModel?.ActivePack.TimeLimitInSeconds ?? 0;
        RaisePropertyChanged(nameof(TimeRemaining));


        ShuffleAnswers();
        RaisePropertyChanged(nameof(ShuffledAnswers));
        RaisePropertyChanged(nameof(CurrentQuestionText));
        RaisePropertyChanged(nameof(QuestionProgress));
        _feedbackSymbols = new string[4];
        RaisePropertyChanged(nameof(FeedbackSymbols));
        _hasAnswered = false;

    }
    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining--;
            RaisePropertyChanged(nameof(TimeRemaining));
        }
        if(_timeRemaining <= 0)
        {
            _timer.Stop();
            _isQuizFinished = true;
            RaisePropertyChanged(nameof(IsQuizFinished));
            RaisePropertyChanged(nameof(IsQuizInProgress));
            RaisePropertyChanged(nameof(FinalResult));
        }
    }
    public void StartTimer()
    {
        _timer.Start();
    }
   
    

}





