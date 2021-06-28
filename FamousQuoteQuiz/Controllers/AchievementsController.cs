using FamousQuoteQuiz.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementsController : Controller
    {
        private readonly IAchievementService _achievementService;

        public AchievementsController(IAchievementService achievementService)
        {
            this._achievementService = achievementService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var achievements = await this._achievementService.GetUserHistory(currentUserId);

            return Ok(achievements);
        }
    }
}
