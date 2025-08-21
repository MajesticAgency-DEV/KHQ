using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KHQ.Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(IUserService userService, IConfiguration configuration, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLogin.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, userLogin.Password, true, false);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("AdminLoggedIn", "true");

                        // Create the claims for the user
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userLogin.Email),
                        new Claim(ClaimTypes.Role, "Admin")
                    };

                        // Create the identity for the user
                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        // Sign the user in
                        await HttpContext.SignInAsync(
                            IdentityConstants.ApplicationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            new AuthenticationProperties
                            {
                                IsPersistent = true, // Keep the user logged in across requests
                            });

                        return Redirect("/home/index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                }
            }
            return View(userLogin);
        }

        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            HttpContext.Session.Remove("AdminLoggedIn");
            return View(nameof(Login));
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }
    }
}