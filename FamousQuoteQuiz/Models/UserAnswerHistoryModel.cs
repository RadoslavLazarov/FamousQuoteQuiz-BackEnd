using FamousQuoteQuiz.Models.Authentication;
using System.ComponentModel.DataAnnotations;

namespace FamousQuoteQuiz.Models
{
    public class UserAnswerHistoryModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public virtual QuestionModel Question { get; set; }

        [Required]
        public virtual AnswerModel Answer { get; set; }
    }
}
