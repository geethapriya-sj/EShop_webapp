﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eshop_Webapp.Helpers
{
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
            }
            else
            {
                if(!context.HttpContext.User.IsInRole(Roles))
                {
                    context.Result = new RedirectToActionResult("UnAuthorize", "Account", new { area = "" });
                }
            }
        }
    }
}
