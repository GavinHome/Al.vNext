//-----------------------------------------------------------------------------------
// <copyright file="EmployeeDb.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Linq;
using Al.vNext.Core.Mongo;
using Al.vNext.Database.Contracts;
using Al.vNext.DataBase.Implement;
using Al.vNext.Model;

namespace Al.vNext.DataBase.Implement
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public class EmployeeDb : MongoDbCommon, IEmployeeDb
    {
        public EmployeeDb(MongoContext dbcontext) : base(dbcontext)
        {
        }

        /// <summary>
        /// 根据账号获取员工信息
        /// </summary>
        /// <param name="code">账号</param>
        /// <returns>员工信息</returns>
        public Employee FindByCode(string code)
        {
            return Get<Employee>().Where(x => x.Code == code).FirstOrDefault();
        }
    }
}
