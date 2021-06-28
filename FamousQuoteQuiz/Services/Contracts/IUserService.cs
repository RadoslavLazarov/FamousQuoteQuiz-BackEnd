using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Models.Authentication;
using FamousQuoteQuiz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Services.Contracts
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string currentUserId);

        Task<List<UsersViewModel>> GetAllUsers();

        Task CreateUser(UserDataModel data);

        Task UpdateUser(UserDataModel data);

        Task DeleteUser(string userId);
    }
}
