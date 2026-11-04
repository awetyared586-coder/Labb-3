using Labb_3code_along.Models;
using Labb_3code_along.Command;
namespace Labb_3code_along.ViewModels;

internal class PlayerViewModel : ViewModelBase
{
    private readonly MainWindowViewModel? _mainWindowViewModel;

    private int _currentQuestionIndex = 0;
    public int CurrentQuestionIndex => _currentQuestionIndex + 1;
    private int _score = 0;

    private string[] _suffledAnswers = Array.Empty<string>();

    private void ShuffleAnswers()
    {
        _suffledAnswers = new[] { CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3 };
    }
    
    public int score => _score;




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

    public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;

          AnswerCommand = new RelayCommand(AnswerQuestion);

    }

    private void AnswerQuestion(object? parameter)
    {
        int answerIndex= int.Parse(parameter?.ToString()??"-1");

        string? selectedAnswer = answerIndex switch
        {
            0 => CorrectAnswer,
            1 => IncorrectAnswer1,
            2 => IncorrectAnswer2,
            3 => IncorrectAnswer3,
            _ => null
        };

        bool isCorrect = selectedAnswer == CorrectAnswer;

        if (isCorrect)
        {
            _score++;
        }

        
    }
    
}





