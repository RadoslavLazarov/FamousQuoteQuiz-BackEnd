using FamousQuoteQuiz.Data;
using FamousQuoteQuiz.Services.Contracts;
using FamousQuoteQuiz.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Services
{
    public class AchievementService : IAchievementService
    {
        private readonly ApplicationDbContext _dbContext;
        public readonly IQuizService _quizService;

        public AchievementService(
            ApplicationDbContext dbContext,
            IQuizService quizService)
        {
            this._dbContext = dbContext;
            this._quizService = quizService;
        }

        public async Task<List<UserAnswerHistoryViewModel>> GetUserHistory(string currentUserId)
        {
            List<UserAnswerHistoryViewModel> model = new List<UserAnswerHistoryViewModel>();

            var history = await _dbContext.UserAnswersHistory
                .Where(h => h.User.Id == currentUserId)
                .Include(h => h.User)
                .Include(h => h.Question)
                .Include(h => h.Answer)
                .ToListAsync();

            history.ForEach((el) =>
            {
                var viewModel = new UserAnswerHistoryViewModel();

                viewModel.QuestionText = el.Question.Text;
                viewModel.AnswerText = el.Answer.Text;
                viewModel.IsCorrect = el.Answer.IsCorrect;

                model.Add(viewModel);
            });

            return model;
        }
    }
}
