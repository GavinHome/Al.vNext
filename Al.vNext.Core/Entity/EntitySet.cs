//-----------------------------------------------------------------------------------
// <copyright file="EntitySet.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Al.vNext.Core.Entity
{
    public abstract class EntitySet : IEntitySetOfType<Guid>
    {
        [Key]
        [Description("唯一标识")]
        public Guid Id { get; set; }
        object IEntitySet.Id => Id;
    }
}
