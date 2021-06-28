
namespace FamousQuoteQuiz.ViewModels
{
    public class AnswerViewModel
    {
        public int Id { get; set; }

        public QuestionViewModel Question { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
