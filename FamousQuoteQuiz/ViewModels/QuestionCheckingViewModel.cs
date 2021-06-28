using FamousQuoteQuiz.Models;

namespace FamousQuoteQuiz.ViewModels
{
    public class QuestionCheckingViewModel
    {
        public bool IsCorrect { get; set; }

        public AnswerModel CorrectAnswer { get; set; }
    }
}
