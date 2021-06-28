using FamousQuoteQuiz.Models.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamousQuoteQuiz.Models
{
    public class UserQuestionHistoryModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public virtual QuestionModel Question { get; set; }
    }
}
