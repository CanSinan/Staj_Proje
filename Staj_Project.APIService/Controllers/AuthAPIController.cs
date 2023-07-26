using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Staj_Project.APIService.Services.IServices;
using Staj_Project.Identity.Core.Dtos;

namespace Staj_Project.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;

        }


        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            var seerRoles = await _authService.SeedRolesAsync();

            return Ok(seerRoles);
        }

        [HttpPost]
        [Route("CustomerRegister")]
        public async Task<IActionResult> CustomerRegister([FromBody] RegisterDto model)
        {
            var registerResult = await _authService.CustomerRegisterAsync(model);

            if (registerResult.IsSucceed)
                return Ok(registerResult);
            return BadRequest(registerResult);
        }


        [HttpPost]
        [Route("ExpertRegister")]
        public async Task<IActionResult> ExpertRegister([FromBody] RegisterDto model)
        {
            var registerResult = await _authService.ExpertRegisterAsync(model);

            if (registerResult.IsSucceed)
                return Ok(registerResult);

            return BadRequest(registerResult);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var loginResult = await _authService.LoginAsync(model);

            if (loginResult.IsSucceed)
                return Ok(loginResult);

            return Unauthorized(loginResult);


        }

    }
}
