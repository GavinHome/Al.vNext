//-----------------------------------------------------------------------------------
// <copyright file="EntitySetWithCreateAndUpdate.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;

namespace Al.vNext.Core.Entity
{
    public abstract class EntitySetWithCreateAndUpdate : EntitySetWithCreate
    {
        public string UpdateBy { get; set; }
        
        public DateTime? UpdateAt { get; set; }
    }
}
