//-----------------------------------------------------------------------------------
// <copyright file="AsyncActionFilter.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Al.vNext.Core.Utility;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

namespace Al.vNext.Web.Common.Filters
{
    public class AsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            GuardUtils.NotNull(context, nameof(context));
            if (context.HttpContext.User != null && context.HttpContext.Request.Method.ToLower() == HttpMethod.Post.ToString().ToLower())
            {
                foreach (var param in context.ActionArguments.Values)
                {
                    PropertyInfo[] fields = param.GetType().GetProperties().Where(x => x.PropertyType == typeof(ClaimsPrincipal)).ToArray();
                    foreach (PropertyInfo field in fields)
                    {
                        field.SetValue(param, context.HttpContext.User, null);
                    }
                }
            }

            await next();
        }
    }
}