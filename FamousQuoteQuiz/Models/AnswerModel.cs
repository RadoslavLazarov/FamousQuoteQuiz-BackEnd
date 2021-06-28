using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamousQuoteQuiz.Models
{
    public class AnswerModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual QuestionModel Question { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public bool IsCorrect { get; set; }
    }
}
