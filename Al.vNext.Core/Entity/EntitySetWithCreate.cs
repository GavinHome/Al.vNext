//-----------------------------------------------------------------------------------
// <copyright file="EntitySetWithCreate.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using Al.vNext.Core.Enum;

namespace Al.vNext.Core.Entity
{
    public abstract class EntitySetWithCreate : EntitySet
    {
        public DataStatusEnum DataStatus { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
