using FamousQuoteQuiz.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Services.Contracts
{
    public interface IAchievementService
    {
        Task<List<UserAnswerHistoryViewModel>> GetUserHistory(string currentUserId);
    }
}
