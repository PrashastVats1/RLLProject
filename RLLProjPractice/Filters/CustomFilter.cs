using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RLLProjPractice.Filters
{
    public class CustomFilter
    {
    }
    public class AuthorizeUserType : AuthorizeAttribute
    {
        private readonly string[] _allowedRoles;
        public AuthorizeUserType(params string[] roles)
        {
            _allowedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userRole = (string)httpContext.Session["UserRole"];
            if (userRole != null && _allowedRoles.Contains(userRole))
            {
                return true;
            }
            return false;
        }
    }
}