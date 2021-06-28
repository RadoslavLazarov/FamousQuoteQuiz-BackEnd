using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Services.Contracts
{
    public interface IQuoteManagerService
    {
        Task<List<QuestionWithAnswersViewModel>> GetAllQuotes();

        Task CreateQuote(QuoteDataModel data);

        Task UpdateQuote(QuoteDataModel data);

        Task DeleteQuote(int quoteId);
    }
}
