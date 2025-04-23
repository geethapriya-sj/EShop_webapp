using Eshop_Webapp.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Claims;
using System.Text.Json;

namespace Eshop_Webapp.Helpers
{
    public abstract class BaseViewPage<TModel> : RazorPage<TModel>
    {
        public UserModel CurrentUser 
        { 
             get
            { 
            if(User.Identity.IsAuthenticated)
            {
                    string strData = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
                    return JsonSerializer.Deserialize<UserModel>(strData);
            }
                return null;
            } 
        }
    }
}
