namespace FamousQuoteQuiz.Models
{
    public class UserDataModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public bool IsDisabled { get; set; }

        public string Role { get; set; }
    }
}
