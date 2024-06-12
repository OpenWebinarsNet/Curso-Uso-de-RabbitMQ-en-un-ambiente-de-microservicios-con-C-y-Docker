using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UsersManagement.Application.Interfaces;
using UsersManagement.Domain.Models;
using UsersManagement.Util;

namespace UsersManagement.Api.Controllers
{
    [ApiController]
    [Route(Constants.Routes.Users)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersApplication _usersApplication;

        public UsersController(IUsersApplication usersApplication)
        {
            _usersApplication = usersApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _usersApplication.GetAsync();

            if (result == null)
                return NotFound(Constants.ReturnMessages.NotFound);

            return Ok(result.Object);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _usersApplication.GetByIdAsync(id);

            if (result == null)
                return NotFound(Constants.ReturnMessages.NotFound);

            return Ok(result.Object);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserInputModel userInputModel)
        {
            Application.Results.Result result = await _usersApplication.PostAsync(userInputModel);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserModel userModel)
        {
            var result = await _usersApplication.PutAsync(userModel);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _usersApplication.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginInputModel userLoginInputModel)
        {
            var result = await _usersApplication.LoginAsync(userLoginInputModel);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] Guid id)
        {
            var result = await _usersApplication.LogoutAsync(id);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }
    }
}