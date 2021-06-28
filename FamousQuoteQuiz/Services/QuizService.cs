using AutoMapper;
using FamousQuoteQuiz.Data;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Services.Contracts;
using FamousQuoteQuiz.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Services
{
    public class QuizService : IQuizService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public QuizService(
            ApplicationDbContext dbContext,
            IUserService userService,
            IMapper mapper)
        {
            this._dbContext = dbContext;
            this._userService = userService;
            this._mapper = mapper;
        }

        private async Task<QuestionViewModel> QuestionGenerator(string currentUserId)
        {
            var userQuestionsHistory = await this._dbContext.UserQuestionsHistory
                .Where(h => h.User.Id == currentUserId)
                .Select(h => h.Question.Id)
                .ToListAsync();

            var questions = await this._dbContext.Questions
                .Where(q => !userQuestionsHistory.Contains(q.Id))
                .ToListAsync();

            if (!questions.Any())
            {
                throw new Exception("There are no more questions! Please restart the game!");
            }

            var random = new Random();
            var randomIndex = random.Next(questions.Count);
            var question = questions[randomIndex];
            var questionViewModel = this._mapper.Map<QuestionViewModel>(question);

            questionViewModel.UserQuestionsCount = questions.Count();

            return questionViewModel;
        }

        private async Task<List<AnswerViewModel>> GetAnswers(int questionId)
        {
            var answers = await this._dbContext.Answers
                .Where(a => a.Question.Id == questionId)
                .ToListAsync();

            var answersViewModel = this._mapper.Map<List<AnswerViewModel>>(answers);
            var random = new Random();

            // Return shffled 
            return answersViewModel.OrderBy(a => random.Next()).ToList();
        }

        private async Task<AnswerModel> GetAnswer(int answerId)
        {
            var answer = await this._dbContext.Answers
                .Where(a => a.Id == answerId)
                .Include(a => a.Question)
                .FirstOrDefaultAsync();

            return answer;
        }

        private async Task<AnswerModel> GetCorrectAnswer(int questionId)
        {
            var answer = await this._dbContext.Answers
                .Where(a => a.Question.Id == questionId && a.IsCorrect == true)
                .FirstOrDefaultAsync();

            return answer;
        }

        private async Task<QuestionModel> GetQuestion(int questionId)
        {
            var question = await this._dbContext.Questions
                .Where(a => a.Id == questionId)
                .FirstOrDefaultAsync();

            return question;
        }

        public async Task<QuestionWithAnswersViewModel> CreateQuestion(string currentUserId)
        {
            QuestionWithAnswersViewModel model = new QuestionWithAnswersViewModel();

            model.Question = await this.QuestionGenerator(currentUserId);
            model.Answers = await this.GetAnswers(model.Question.Id);

            return model;
        }

        public async Task<QuestionCheckingViewModel> QuestionChecking(string currentUserId, dynamic data)
        {
            QuestionCheckingViewModel model = new QuestionCheckingViewModel();
            var answer = await this.GetAnswer(data.answerId);

            await this.SaveHistory(currentUserId, answer);

            if (data.mode == "BINARY")
            {
                if (data.currentAnswer == answer.IsCorrect)
                {
                    model.IsCorrect = true;
                }
                else
                {
                    model.IsCorrect = false;
                }
            }
            else if (data.mode == "MULTIPLE")
            {
                if (answer.IsCorrect)
                {
                    model.IsCorrect = true;
                }
                else
                {
                    model.IsCorrect = false;
                }
            }

            model.CorrectAnswer = await this.GetCorrectAnswer(answer.Question.Id);

            return model;
        }

        private async Task SaveHistory(string currentUserId, AnswerModel answer)
        {
            var user = await this._userService.GetUserByIdAsync(currentUserId);
            var question = await this.GetQuestion(answer.Question.Id);

            // Create answer history
            UserAnswerHistoryModel userAnswerHistoryModel = new UserAnswerHistoryModel();
            userAnswerHistoryModel.User = user;
            userAnswerHistoryModel.Question = question;
            userAnswerHistoryModel.Answer = answer;

            // Create question history
            UserQuestionHistoryModel userQuestionHistoryModel = new UserQuestionHistoryModel();
            userQuestionHistoryModel.User = user;
            userQuestionHistoryModel.Question = question;

            this._dbContext.UserAnswersHistory.Add(userAnswerHistoryModel);
            this._dbContext.UserQuestionsHistory.Add(userQuestionHistoryModel);

            await this._dbContext.SaveChangesAsync();
        }

        public async Task RestartQuiz(string currentUserId)
        {
            this._dbContext.UserAnswersHistory.RemoveRange(
                this._dbContext.UserAnswersHistory.Where(h => h.User.Id == currentUserId));

            this._dbContext.UserQuestionsHistory.RemoveRange(
                this._dbContext.UserQuestionsHistory.Where(h => h.User.Id == currentUserId));

            await this._dbContext.SaveChangesAsync();
        }
    }
}
