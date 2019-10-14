//-----------------------------------------------------------------------------------
// <copyright file="PermissionRequirement.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;

namespace Al.vNext.Web.PermissionExtensions
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string[] permissions)
        {
            Permissions = permissions;
        }

        public string[] Permissions { get; set; }
    }
}
