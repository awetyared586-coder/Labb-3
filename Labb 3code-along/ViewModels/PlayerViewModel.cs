using Labb_3code_along.Models;
using Labb_3code_along.Command;
namespace Labb_3code_along.ViewModels;

internal class PlayerViewModel : ViewModelBase
{
    private readonly MainWindowViewModel? _mainWindowViewModel;

    private int _currentQuestionIndex = 0;
    public int CurrentQuestionIndex => _currentQuestionIndex + 1;
    private int _score = 0;
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
        string? answer = parameter?.ToString();

        string correctAnswer = CurrentQuestion?.CorrectAnswer;

        bool isCorrect = answer == correctAnswer;

        if (isCorrect)
        {
            _score++;
             
        }
    }
    
}





