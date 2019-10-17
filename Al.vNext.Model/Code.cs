//-----------------------------------------------------------------------------------
// <copyright file="Employee.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Al.vNext.Core.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Al.vNext.Model
{
    /// <summary>
    /// Code
    /// </summary>
    [Table("T_Code")]
    public class Code : EntitySet
    {
        /// <summary>
        /// 父Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// CodeId
        /// </summary>
        public int CodeId { get; set; }

        /// <summary>
        /// Code Type Id
        /// </summary>
        public int CodeTypeId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int DataState { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderIndex { get; set; }
    }
}
