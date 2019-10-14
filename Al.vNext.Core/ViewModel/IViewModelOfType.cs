//-----------------------------------------------------------------------------------
// <copyright file="IViewModelOfType.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Security.Claims;

namespace Al.vNext.Core.ViewModel
{
    public interface IViewModelOfType<TKey> : IViewModel
    {
        new TKey Id { get; set; }
    }
}
