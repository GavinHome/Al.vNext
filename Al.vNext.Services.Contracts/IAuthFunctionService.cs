//-----------------------------------------------------------------------------------
// <copyright file="IAuthFunctionService.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Al.vNext.Services.Contracts
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public interface IAuthFunctionService
    {
        /// <summary>
        /// 根据账号获取员工权限
        /// </summary>
        /// <param name="code">账号</param>
        /// <returns>员工权限</returns>
        IList<string> GetFunctionsByUserName(string code);
    }
}
