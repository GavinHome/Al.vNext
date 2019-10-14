//-----------------------------------------------------------------------------------
// <copyright file="CustomDataSourceRequestCompositeFilter.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Al.vNext.Web.Common.KendoExtensions
{
    public class CustomDataSourceRequestCompositeFilter
    {
        public string Logic { get; set; }
        public IList<CustomDataSourceRequestFilter> Filters { get; set; }
    }
}
