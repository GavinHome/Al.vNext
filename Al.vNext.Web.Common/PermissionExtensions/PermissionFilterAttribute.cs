//-----------------------------------------------------------------------------------
// <copyright file="PermissionFilterAttribute.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Al.vNext.Core.Utility;

namespace Al.vNext.Web.PermissionExtensions
{
    public class PermissionFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService _authService;
        private readonly PermissionRequirement _requirement;

        public PermissionFilterAttribute(IAuthorizationService authService, PermissionRequirement requirement)
        {
            _authService = authService;
            _requirement = requirement;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            GuardUtils.NotNull(context, nameof(context));
            AuthorizationResult ok = await _authService.AuthorizeAsync(context.HttpContext.User, null, _requirement);

            if (!ok.Succeeded)
            {
                context.Result = new ChallengeResult();
            }
        }
    }
}
