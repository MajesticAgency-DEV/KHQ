using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KHQ.Srv.Services
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;

        public UserService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ValidateUserResult> Login(UserLogin userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.Email);
            if (user == null)
            {
                return new ValidateUserResult()
                {
                    IsValid = false,
                    UserInfo = null,
                };
            }
            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, true, false);
            if (!result.Succeeded)
            {
                return new ValidateUserResult()
                {
                    IsValid = false,
                    UserInfo = null,
                };
            }
            return new ValidateUserResult()
            {
                IsValid = true,
                UserInfo = new UserInfo()
                {
                    UserName = user.UserName,
                    MobileNumber = user.PhoneNumber,
                    Email = user.Email,
                    Token = GenerateToken(user)
                },
            };

        }

        public async Task Register(UserRegister userRegister)
        {
            if (await _userManager.FindByNameAsync(userRegister.UserName) != null)
            {
                throw new Exception("Username is used before.");
            }

            if (await _userManager.FindByEmailAsync(userRegister.Email) != null)
            {
                throw new Exception("Email is used before.");
            }

            var user = new ApplicationUser { UserName = userRegister.UserName, Email = userRegister.Email };
            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Something went wrong!\nMake sure the password has small letters, capital letters, numbers, and symbols!");
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private string GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserName", user.UserName),
                new Claim("Id", user.Id),
                new Claim("Email", user.Email),
                //new Claim("MobileNumber", user.PhoneNumber)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_Secret.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var returnToken = new JwtSecurityToken
                (
                issuer: JWT_Secret.Issuer,
                audience: JWT_Secret.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(returnToken);
        }
    }
}
