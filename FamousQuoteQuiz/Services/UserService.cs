using AutoMapper;
using FamousQuoteQuiz.Data;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Models.Authentication;
using FamousQuoteQuiz.Services.Contracts;
using FamousQuoteQuiz.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext,
            IMapper mapper)
        {
            this._userManager = userManager;
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string currentUserId)
        {
            var user = await _userManager.FindByIdAsync(currentUserId);
            return user;
        }

        public async Task<List<UsersViewModel>> GetAllUsers()
        {
            var users = await this._dbContext.Users.ToListAsync();
            var usersViewModel = this._mapper.Map<List<UsersViewModel>>(users);
            var index = 0;

            foreach (var user in users)
            {
                var role = await this._userManager.GetRolesAsync(user);

                usersViewModel[index].Role = role[0];

                index++;
            }

            return usersViewModel;
        }

        public async Task CreateUser(UserDataModel data)
        {
            var userExists = await _userManager.FindByNameAsync(data.UserName);

            if (userExists != null)
            {
                throw new Exception("User exist!");
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = data.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = data.UserName
            };

            await this._userManager.CreateAsync(user, data.Password);
            await this._userManager.AddToRoleAsync(user, data.Role);
        }

        public async Task UpdateUser(UserDataModel data)
        {
            var user = await this._userManager.FindByIdAsync(data.Id);

            user.Email = data.Email;
            user.UserName = data.UserName;

            if (!string.IsNullOrEmpty(data.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, data.Password);
            }

            var currentRoles = await this._userManager.GetRolesAsync(user);

            await this._userManager.RemoveFromRolesAsync(user, currentRoles);
            await this._userManager.AddToRoleAsync(user, data.Role);

            await this._dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(string userId)
        {
            var user = await this._userManager.FindByIdAsync(userId);

            this._dbContext.UserAnswersHistory.RemoveRange(
                this._dbContext.UserAnswersHistory.Where(h => h.User.Id == user.Id));

            this._dbContext.UserQuestionsHistory.RemoveRange(
                this._dbContext.UserQuestionsHistory.Where(h => h.User.Id == user.Id));

            await this._dbContext.SaveChangesAsync();
            await this._userManager.DeleteAsync(user);
        }
    }
}
