//-----------------------------------------------------------------------------------
// <copyright file="ExceptionHandlingMiddleware.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Al.vNext.Core.Utility;

namespace Al.vNext.Web.Common.Middleware
{
    /// <summary>
    /// 自定义异常处理中间件
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            GuardUtils.NotNull(context, nameof(context));
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = context.Response.StatusCode;
                log4net.LogManager.GetLogger(typeof(HttpContext)).Error(statusCode.ToString());
                log4net.LogManager.GetLogger(typeof(HttpContext)).Error(ex.ToString());
                await _next(context);
            }
        }
    }
}
