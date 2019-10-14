//-----------------------------------------------------------------------------------
// <copyright file="AuthorizePermissionAttribute.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Mvc;

namespace Al.vNext.Web.PermissionExtensions
{
    public class AuthorizePermissionAttribute : TypeFilterAttribute
    {
        public AuthorizePermissionAttribute(params string[] permissions)
            : base(typeof(PermissionFilterAttribute))
        {
            Arguments = new[] { new PermissionRequirement(permissions) };
            Order = int.MinValue;
        }
    }
}
