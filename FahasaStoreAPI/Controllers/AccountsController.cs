using FahasaStoreAPI.Helpers;
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

        [HttpPost("AddRoleToUser")]
        [Authorize(AppRole.Admin)]
        public async Task<IActionResult> AddRoleToUser(string userId, string role)
        {
            try
            {
                bool result = await _accountService.AddRoleToUser(userId, role);
                if (result)
                {
                    return Ok(new { message = $"Vai trò '{role}' đã được thêm cho người dùng với ID '{userId}'." });
                }
                else
                {
                    return BadRequest(new { message = $"Người dùng với ID '{userId}' đã có vai trò '{userId}' hoặc có lỗi xảy ra." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Có lỗi xảy ra: {ex.Message}" });
            }
        }

        [HttpGet("GetUserRoles/{userId}")]
        [Authorize(AppRole.Admin)]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            try
            {
                var roles = await _accountService.GetUserRoles(userId);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Có lỗi xảy ra: {ex.Message}" });
            }
        }

        [HttpDelete("RemoveRoleFromUser")]
        [Authorize(AppRole.Admin)]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string role)
        {
            try
            {
                bool result = await _accountService.RemoveRoleFromUser(userId, role);
                if (result)
                {
                    return Ok(new { message = $"Vai trò '{role}' đã được gỡ khỏi người dùng với ID '{userId}'." });
                }
                else
                {
                    return BadRequest(new { message = $"Người dùng với ID '{userId}' không có vai trò '{role}' hoặc có lỗi xảy ra." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Có lỗi xảy ra: {ex.Message}" });
            }
        }
    }
}
