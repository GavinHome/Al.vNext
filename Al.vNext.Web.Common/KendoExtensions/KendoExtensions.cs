//-----------------------------------------------------------------------------------
// <copyright file="KendoExtensions.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Al.vNext.Core.Utility;

namespace Al.vNext.Web.Common.KendoExtensions
{
    public static class KendoExtensions
    {
        public static Task<DataSourceResult> ToDataSourcePageResultAsync<TModel, TResult>(this IQueryable<TModel> queryable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            request = request.ToConvertDataSourceRequest<TModel>();
            return queryable.ToDataSourceResultAsync<TModel, TResult>(request, selector);
        }

        public static Task<DataSourceResult> ToDataSourcePageResultAsync<TModel, TResult>(this IEnumerable<TModel> queryable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            request = request.ToConvertDataSourceRequest<TModel>();
            return queryable.ToDataSourceResultAsync<TModel, TResult>(request, selector);
        }

        public static DataSourceRequest ToConvertDataSourceRequest<T>(this DataSourceRequest request)
        {
            if (request != null && request.Filters != null)
            {
                foreach (var filter in request.Filters)
                {
                    ConvertFilterValueType<T>(filter);
                }
            }

            return request;
        }

        private static PropertyInfo FindPropertyType<T>(string memberName)
        {
            var _modalType = typeof(T);
            return _modalType.GetProperty(memberName);
        }

        private static void ConvertFilterValueType<T>(IFilterDescriptor filterDescriptor)
        {
            var describ = filterDescriptor as FilterDescriptor;

            if (describ != null)
            {
                var prop = FindPropertyType<T>(describ.Member);

                if (prop != null)
                {
                    SetValue(describ, prop);
                }
            }
            else
            {
                var composDesc = filterDescriptor as CompositeFilterDescriptor;
                if (composDesc != null)
                {
                    foreach (var describFilterSub in composDesc.FilterDescriptors)
                    {
                        ConvertFilterValueType<T>(describFilterSub);
                    }
                }
            }
        }

        private static void SetValue(FilterDescriptor describ, PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
            {
                int nVal = 0;
                if (int.TryParse(describ.Value.ToString(), out nVal))
                {
                    describ.Value = nVal;
                }
            }
            else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
            {
                decimal deVal = 0;
                if (decimal.TryParse(describ.Value.ToString(), out deVal))
                {
                    describ.Value = deVal;
                }
            }
            else if (prop.PropertyType.IsEnum)
            {
                int outInt = 0;
                if (int.TryParse(describ.Value.ToString(), out outInt))
                {
                    describ.Value = Enum.ToObject(prop.PropertyType, outInt);
                }
            }
            else if (IsNullableEnum(prop.PropertyType) && describ.Value != null)
            {
                int outInt = 0;
                if (int.TryParse(describ.Value.ToString(), out outInt))
                {
                    describ.Value = Enum.ToObject(Nullable.GetUnderlyingType(prop.PropertyType), outInt);
                }
            }
            else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
            {
                DateTime dateVal;
                if (DateTime.TryParse(describ.Value.ToString(), out dateVal))
                {
                    describ.Value = dateVal;
                }
                else
                {
                    describ.Value = DateTime.MinValue;
                }
            }
        }

        private static bool IsNullableEnum(Type type)
        {
            Type u = Nullable.GetUnderlyingType(type);
            return (u != null) && u.IsEnum;
        }
    }
}