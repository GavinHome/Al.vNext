//-----------------------------------------------------------------------------------
// <copyright file="EntitySetWithAllStatus.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Al.vNext.Core.Const;
using Al.vNext.Core.Enum;

namespace Al.vNext.Core.Entity
{
    public class EntitySetWithAllStatus : EntitySetWithCreateAndUpdate
    {
        public ApproveStatusEnum? ApprovalStatus { get; set; }

        [MaxLength(CommonConstant.DbStringFieldsLength64)]
        public string ApprovalStatusDescription { get; set; }

        public Guid? CreateByOrganizationId { get; set; }

        [NotMapped]
        public Guid BacklogId { get; set; }

        public DateTime? SubmitAt { get; set; }
    }
}
