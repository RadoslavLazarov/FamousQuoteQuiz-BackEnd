using System.Collections.Generic;

namespace FamousQuoteQuiz.Models
{
    public class QuoteDataModel
    {
        public int quoteId { get; set; }

        public string quoteText { get; set; }

        public List<AnswerDataModel> answers { get; set; }
    }
}
