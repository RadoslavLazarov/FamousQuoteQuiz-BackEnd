using System.ComponentModel.DataAnnotations;

namespace FamousQuoteQuiz.Models
{
    public class QuestionModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
