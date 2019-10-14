//-----------------------------------------------------------------------------------
// <copyright file="AuthFunctionService.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Al.vNext.Database.Contracts;
using Al.vNext.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Al.vNext.Services.Implement
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public class AuthFunctionService : IAuthFunctionService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public AuthFunctionService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        /// <summary>
        /// 根据账号获取员工权限
        /// </summary>
        /// <param name="code">账号</param>
        /// <returns>员工权限</returns>
        public IList<string> GetFunctionsByUserName(string code)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var employeeDb = scope.ServiceProvider.GetRequiredService<IEmployeeDb>();
                var user = employeeDb.FindByCode(code);
                return user.Functions;
            }
        }
    }
}
