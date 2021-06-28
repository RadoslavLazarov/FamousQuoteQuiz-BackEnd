using AutoMapper;
using FamousQuoteQuiz.Data;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Services.Contracts;
using FamousQuoteQuiz.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Services
{
    public class QuoteManagerService : IQuoteManagerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuoteManagerService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<List<QuestionWithAnswersViewModel>> GetAllQuotes()
        {
            List<QuestionWithAnswersViewModel> models = new List<QuestionWithAnswersViewModel>();

            var quotes = await this._dbContext.Questions.ToListAsync();
            var answers = await this._dbContext.Answers.ToListAsync();

            var quotesViewModel = this._mapper.Map<List<QuestionViewModel>>(quotes);
            var answersViewModel = this._mapper.Map<List<AnswerViewModel>>(answers);

            quotesViewModel.ForEach((el) =>
            {
                var model = new QuestionWithAnswersViewModel();
                var quoteAnswers = answersViewModel.Where(a => a.Question.Id == el.Id).ToList();

                model.Question = el;
                model.Answers = quoteAnswers;

                models.Add(model);
            });

            return models;
        }

        public async Task CreateQuote(QuoteDataModel data)
        {
            QuestionModel questionModel = new QuestionModel();
            questionModel.Text = data.quoteText;

            List<AnswerModel> answersModel = new List<AnswerModel>();

            data.answers.ForEach((el) =>
            {
                AnswerModel answerModel = new AnswerModel();

                answerModel.Question = questionModel;
                answerModel.Text = el.text;
                answerModel.IsCorrect = el.isCorrect;

                answersModel.Add(answerModel);
            });

            this._dbContext.Questions.Add(questionModel);
            this._dbContext.Answers.AddRange(answersModel);

            await this._dbContext.SaveChangesAsync();
        }

        public async Task UpdateQuote(QuoteDataModel data)
        {
            var quote = await this._dbContext.Questions
                .Where(q => q.Id == data.quoteId)
                .FirstOrDefaultAsync();

            quote.Text = data.quoteText;

            var answers = await this._dbContext.Answers
                .Where(a => a.Question.Id == quote.Id)
                .ToListAsync();

            List<AnswerModel> answersModel = new List<AnswerModel>();

            data.answers.ForEach((el) =>
            {
                foreach (var item in answers)
                {
                    if (item.Id == el.id)
                    {
                        item.Question = quote;
                        item.Text = el.text;
                        item.IsCorrect = el.isCorrect;
                    }
                }
            });

            await this._dbContext.SaveChangesAsync();
        }

        public async Task DeleteQuote(int quoteId)
        {
            // Delete Answers history
            var answersHistory = await this._dbContext.UserAnswersHistory
                .Where(h => h.Question.Id == quoteId)
                .ToListAsync();
            this._dbContext.UserAnswersHistory.RemoveRange(answersHistory);

            // Delete Question history
            var questionHistory = await this._dbContext.UserQuestionsHistory
                .Where(h => h.Question.Id == quoteId)
                .ToListAsync();
            this._dbContext.UserQuestionsHistory.RemoveRange(questionHistory);

            // Delete Answers
            var answers = await this._dbContext.Answers
                .Where(a => a.Question.Id == quoteId)
                .ToListAsync();
            _dbContext.Answers.RemoveRange(answers);

            // Delete Question
            var quote = await this._dbContext.Questions
                .Where(q => q.Id == quoteId)
                .FirstOrDefaultAsync();
            this._dbContext.Questions.Remove(quote);

            await this._dbContext.SaveChangesAsync();
        }
    }
}
