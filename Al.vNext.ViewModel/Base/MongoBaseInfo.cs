//-----------------------------------------------------------------------------------
// <copyright file="MongoBaseInfo.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Security.Claims;
using Al.vNext.Core.ViewModel;

namespace Al.vNext.ViewModel
{
    public class MongoBaseInfo : IViewModelOfType<string>
    {
        public MongoBaseInfo()
        {
            Menus = new List<string>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        object IViewModel.Id => Id;

        /// <summary>
        /// 身份
        /// </summary>
        public ClaimsPrincipal User { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public IList<string> Menus { get; set; }
    }
}
