using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Models.Authentication;
using FamousQuoteQuiz.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersManagerController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersManagerController(IUserService userService)
        {
            this._userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await this._userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = e.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDataModel data)
        {
            try
            {
                await this._userService.CreateUser(data);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = e.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDataModel data)
        {
            try
            {
                await this._userService.UpdateUser(data);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Something went wrong!" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string userId)
        {
            try
            {
                await this._userService.DeleteUser(userId);
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
