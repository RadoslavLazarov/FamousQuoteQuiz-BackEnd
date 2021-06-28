using Microsoft.AspNetCore.Identity;

namespace FamousQuoteQuiz.Models.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDisabled { get; set; }
    }
}
