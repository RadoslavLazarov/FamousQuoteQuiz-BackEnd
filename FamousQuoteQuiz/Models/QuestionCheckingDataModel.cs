using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Models
{
    public class QuestionCheckingDataModel
    {
        public string mode { get; set; }

        public int answerId { get; set; }

        public bool currentAnswer { get; set; }
    }
}
