using Eshop_Webapp.HttpClients;
using Eshop_Webapp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Eshop_Webapp.Controllers
{
    public class AccountController : Controller
    {
        AuthServiceClients _authServiceClients;
        public AccountController(AuthServiceClients authServiceClients)
        {
            _authServiceClients = authServiceClients;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model,string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _authServiceClients.LoginAsync(model);
                if (user != null)
                {
                    GenerateTicket(user);
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else if (user.Roles != null && user.Roles.Contains("User"))
                    {
                        
                        return RedirectToAction("Index", "Home",new {area = "User"});
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        private void GenerateTicket(UserModel user)
        {
            string strData = JsonSerializer.Serialize(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserData , strData),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, string.Join(",", user.Roles))
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);
        }
    }
}
