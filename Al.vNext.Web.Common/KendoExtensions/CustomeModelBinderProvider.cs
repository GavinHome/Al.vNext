//-----------------------------------------------------------------------------------
// <copyright file="CustomeModelBinderProvider.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Kendo.Mvc.UI;
using Al.vNext.Core.Utility;

namespace Al.vNext.Web.Common.KendoExtensions
{
    public class CustomeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            GuardUtils.NotNull(context, nameof(context));
            if (context.Metadata.ModelType == typeof(DataSourceRequest))
            {
                return new CustomDataSourceRequestModelBinder();
            }

            return null;
        }
    }
}
