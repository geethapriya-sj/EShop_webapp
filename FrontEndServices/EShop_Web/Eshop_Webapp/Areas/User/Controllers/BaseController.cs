using Eshop_Webapp.Helpers;
using Eshop_Webapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Eshop_Webapp.Areas.User.Controllers
{
    [CustomAuthorize(Roles = "User")]
    [Area("User")]
    public class BaseController : Controller
    {
        public UserModel CurrentUser
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    string userData = User.FindFirst(System.Security.Claims.ClaimTypes.UserData)?.Value;
                    return JsonSerializer.Deserialize<UserModel>(userData)
                  }
                return null;
            }
        }
    }
}
