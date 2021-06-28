using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FamousQuoteQuiz.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<QuestionModel> Questions { get; set; }

        public DbSet<AnswerModel> Answers { get; set; }

        public DbSet<UserAnswerHistoryModel> UserAnswersHistory { get; set; }

        public DbSet<UserQuestionHistoryModel> UserQuestionsHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
