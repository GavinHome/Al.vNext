//-----------------------------------------------------------------------------------
// <copyright file="UserInfo.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Al.vNext.ViewModel.Auth
{
    public class UserInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public IList<string> Functions { get; set; }
    }
}
