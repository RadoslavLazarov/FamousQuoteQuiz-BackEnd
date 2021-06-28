using FamousQuoteQuiz.ViewModels;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Services.Contracts
{
    public interface IQuizService
    {
        /// <summary>
        ///  Generate random question depending on user history
        /// </summary>
        /// <param name="currentUserId">Currently logged user id</param>
        /// <returns>Question with three answers. Only one answer is correct.</returns>
        Task<QuestionWithAnswersViewModel> CreateQuestion(string currentUserId);

        /// <summary>
        ///  Check question is it true or false
        /// </summary>
        /// <param name="currentUserId">Currently logged user id</param>
        /// <param name="data">Answer model</param>
        /// <returns>QuestionCheckingViewModel </returns>
        Task<QuestionCheckingViewModel> QuestionChecking(string currentUserId, dynamic data);

        /// <summary>
        ///  Delete all user history to restart the game.
        /// </summary>
        /// <param name="currentUserId">Currently logged user id</param>
        Task RestartQuiz(string currentUserId);
    }
}
