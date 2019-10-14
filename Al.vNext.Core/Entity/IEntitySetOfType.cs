//-----------------------------------------------------------------------------------
// <copyright file="IEntitySetOfType.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

namespace Al.vNext.Core.Entity
{
    public interface IEntitySetOfType<TKey> : IEntitySet
    {
        new TKey Id { get; set; }
    }
}
