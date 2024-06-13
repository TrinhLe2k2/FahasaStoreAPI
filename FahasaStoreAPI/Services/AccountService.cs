using FahasaStoreAPI.Entities;
using FahasaStoreAPI.Helpers;
using FahasaStoreAPI.Identity;
using FahasaStoreAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FahasaStoreAPI.Services
{
    public interface IAccountService
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
        Task SignOutAsync();
    }
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly FahasaStoreDBContext _context;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, FahasaStoreDBContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            _context = context;
        }

        public async Task<string> SignInAsync(SignInModel model)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);

                if (user == null || !passwordValid)
                {
                    throw new Exception("User not found or Invalid password.");
                }

                var resultSignIn = await signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);
                if (!resultSignIn.Succeeded)
                {
                    throw new Exception("Login failed.");
                }

                var authClaims = new List<Claim>
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Avatar", user.ImageUrl),
                    new Claim("FullName", user.FullName),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var userRoles = await userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }

                var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
                );

                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                return accessToken;
            }
            catch (JsonException ex)
            {
                throw new Exception("Error occurred while signing in.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while signing in.", ex);
            }
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                FullName = model.FullName.IsNullOrEmpty() ? model.Email.Split('@')[0] : model.FullName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                CreatedAt = DateTime.UtcNow,
                ImageUrl = "https://res-console.cloudinary.com/drk83zqgs/media_explorer_thumbnails/6d6a5b0e8c5f1954f29b609202821745/detailed",
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //kiểm tra role Customer đã có
                if (!await roleManager.RoleExistsAsync(AppRole.Customer))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.Customer));
                }

                await userManager.AddToRoleAsync(user, AppRole.Customer);
                // Tạo giỏ hàng cho người dùng này

                var cart = new Cart();
                cart.UserId = user.Id;
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
