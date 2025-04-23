using Eshop_Webapp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_Webapp.Areas.User.Controllers
{
    [CustomAuthorize(Roles = "User")]
    [Area("User")]
    public class BaseController : Controller
    {
        
    }
}
