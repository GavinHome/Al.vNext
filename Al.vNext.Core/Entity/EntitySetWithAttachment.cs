//-----------------------------------------------------------------------------------
// <copyright file="EntitySetWithAttachment.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Al.vNext.Core.Entity
{
    public abstract class EntitySetWithAttachment : EntitySet
    {
        public Guid? AttachmentId { get; set; }

        [NotMapped]
        public string FileName { get; set; }
    }
}
