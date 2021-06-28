using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Models.Authentication;
using FamousQuoteQuiz.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteManagerController : ControllerBase
    {
        private readonly IQuoteManagerService _quoteManagerService;

        public QuoteManagerController(IQuoteManagerService quoteManagerService)
        {
            this._quoteManagerService = quoteManagerService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var quotes = await this._quoteManagerService.GetAllQuotes();
                return Ok(quotes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Something went wrong!" });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuoteDataModel data)
        {
            try
            {
                await this._quoteManagerService.CreateQuote(data);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Something went wrong!" });
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] QuoteDataModel data)
        {
            try
            {
                await this._quoteManagerService.UpdateQuote(data);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Something went wrong!" });
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int quoteId)
        {
            try
            {
                await this._quoteManagerService.DeleteQuote(quoteId);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Something went wrong!" });
            }
        }
    }
}
