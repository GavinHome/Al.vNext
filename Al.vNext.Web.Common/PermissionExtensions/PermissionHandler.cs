//-----------------------------------------------------------------------------------
// <copyright file="PermissionHandler.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Al.vNext.Core.Utility;
using Al.vNext.Services.Contracts;

namespace Al.vNext.Web.PermissionExtensions
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IAuthFunctionService _authFunctionService;

        public PermissionHandler(IAuthFunctionService authFunctionService)
        {
            _authFunctionService = authFunctionService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            GuardUtils.NotNull(context, nameof(context));
            GuardUtils.NotNull(requirement, nameof(requirement));
            Claim claim = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);

            if (requirement.Permissions.Any() && claim != null)
            {
                var functions = _authFunctionService.GetFunctionsByUserName(claim.Value);
                if (functions.Intersect(requirement.Permissions).Any())
                {
                    context.Succeed(requirement);
                }
            }
            else if (!requirement.Permissions.Any())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
