//-----------------------------------------------------------------------------------
// <copyright file="ApproveStatusEnum.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Al.vNext.Core.Enum
{
    public enum ApproveStatusEnum : byte
    {
        /// <summary>
        /// 未提交
        /// </summary>
        [Display(Name = "待提交")]
        [Description("未提交")]
        Uncommitted = 0,

        /// <summary>
        /// 已提交
        /// </summary>
        [Display(Name = "已提交")]
        [Description("已提交")]
        Committed = 1,

        /// <summary>
        /// 审核驳回
        /// </summary>
        [Display(Name = "已驳回")]
        [Description("审核驳回")]
        Reject = 2,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Display(Name = "处理完成")]
        [Description("审核通过")]
        Completed = 3,

        /// <summary>
        /// 审核中
        /// </summary>
        [Display(Name = "审核中")]
        [Description("审核中")]
        Auditing = 7,

        /// <summary>
        /// 已终止
        /// </summary>
        [Display(Name = "已终止")]
        [Description("已终止")]
        Close = 8,

        /// <summary>
        /// 请求协审
        /// </summary>
        [Display(Name = "请求协审")]
        [Description("请求协审")]
        Help = 9,

        /// <summary>
        /// 流程中间步骤状态10
        /// </summary>
        [Display(Name = "流程中间步骤状态10")]
        [Description("流程中间步骤状态10")]
        AuditTenStatus = 10,

        /// <summary>
        /// 流程中间步骤状态11
        /// </summary>
        [Display(Name = "流程中间步骤状态11")]
        [Description("流程中间步骤状态11")]
        AuditElevenStatus = 11,

        /// <summary>
        /// 流程中间步骤状态10
        /// </summary>
        [Display(Name = "流程中间步骤状态12")]
        [Description("流程中间步骤状态12")]
        AuditTwelveStatus = 12,
    }
}