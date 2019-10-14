//-----------------------------------------------------------------------------------
// <copyright file="Employee.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Al.vNext.Core.Mongo.Entity;
using MongoDB.Bson.Serialization.Attributes;

namespace Al.vNext.Model
{
    /// <summary>
    /// Employee
    /// </summary>
    [BsonDiscriminator("Employee")]
    public class Employee : MongoDbEntityWithUpdateAndByName
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Description("编号")]
        public string Code { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Description("姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public IList<string> Functions { get; set; }
    }
}
