using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Models.Authentication;

namespace FamousQuoteQuiz.ViewModels
{
    public class UserAnswerHistoryViewModel
    {
        public string QuestionText { get; set; }

        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }
    }
}
