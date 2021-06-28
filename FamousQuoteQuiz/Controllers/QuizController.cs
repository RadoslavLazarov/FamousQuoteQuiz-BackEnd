using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Models.Authentication;
using FamousQuoteQuiz.Services.Contracts;
using FamousQuoteQuiz.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            this._quizService = quizService;
        }

        [Authorize]
        [HttpGet]
        [Route("question-generator")]
        public async Task<IActionResult> QuestionGenerator()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            QuestionWithAnswersViewModel question;

            try
            {
                question = await this._quizService.CreateQuestion(currentUserId);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = e.Message });
            }

            return Ok(question);
        }

        [Authorize]
        [HttpPost]
        [Route("question-checking")]
        public async Task<IActionResult> QuestionChecking([FromBody] QuestionCheckingDataModel data)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var checkedQuestion = await this._quizService.QuestionChecking(currentUserId, data);

            return Ok(new { isCorrect = checkedQuestion.IsCorrect, correctAnswer = checkedQuestion.CorrectAnswer });
        }

        [Authorize]
        [HttpGet]
        [Route("restart-quiz")]
        public async Task<IActionResult> RestartQuiz()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await this._quizService.RestartQuiz(currentUserId);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Something went wrong!" });
            }
        }
    }
}
