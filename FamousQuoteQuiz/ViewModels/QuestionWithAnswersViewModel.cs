using FamousQuoteQuiz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.ViewModels
{
    public class QuestionWithAnswersViewModel
    {
        public QuestionViewModel Question { get; set; }

        public List<AnswerViewModel> Answers { get; set; }
    }
}
