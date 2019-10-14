//-----------------------------------------------------------------------------------
// <copyright file="IEmployeeDb.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Al.vNext.Model;

namespace Al.vNext.Database.Contracts
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public interface IEmployeeDb : IMongoDbCommon
    {
        /// <summary>
        /// 根据账号获取员工信息
        /// </summary>
        /// <param name="code">账号</param>
        /// <returns>员工信息</returns>
        Employee FindByCode(string code);
    }
}
