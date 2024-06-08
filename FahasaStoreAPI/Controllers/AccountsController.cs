using FahasaStoreAPI.Models;
using FahasaStoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("SignUp")] // [FromForm]SignUpModel signUpModel
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await _accountService.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {

                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var accessToken = await _accountService.SignInAsync(signInModel);

            if (accessToken == null)
            {
                return Unauthorized();
            }

            return Ok(accessToken);
        }

        [HttpPost("SignOut")]
        [Authorize]
        public async Task<IActionResult> SignOff()
        {
            await _accountService.SignOutAsync();
            return Ok(new { message = "SignOut successful" });
        }
    }
}
