//-----------------------------------------------------------------------------------
// <copyright file="IViewModel.cs" company="Al.vNext">
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
    public interface IViewModel
    {
        object Id { get; }

        /// <summary>
        /// 身份
        /// </summary>
        ClaimsPrincipal User { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        IList<string> Menus { get; set; }
    }
}
