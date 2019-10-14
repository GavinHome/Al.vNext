//-----------------------------------------------------------------------------------
// <copyright file="DbContextExtensions.cs" company="Al.vNext">
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
using Microsoft.EntityFrameworkCore;
using Al.vNext.Core.Attributes;
using Al.vNext.Core.Entity;
using Al.vNext.Core.Enum;


namespace Al.vNext.Core.Extension
{
    /// <summary>
    /// dbcontext extension class
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// 统一数据查询入口, 可缓存的数据源
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="ctx">数据库上下文</param>
        /// <returns>返回对当前类型的数据库上下文的引用</returns>
        public static IQueryable<T> AsQueryable<T>(this DbContext ctx) where T : class, IEntitySet
        {
            return ctx.Set<T>().AsQueryable();
        }

        /// <summary>
        /// 统一数据查询入口, 可缓存的数据源
        /// </summary>
        /// <param name="ctx">数据库上下文</param>
        /// <param name="type">对于数据库上下文要查询的数据类型</param>
        /// <returns>返回非类型化的数据库上下文的引用</returns>
        public static IQueryable AsQueryable(this DbContext ctx, Type type)
        {
            try
            {
                var methods = typeof(DbContextExtensions).GetTypeInfo().GetDeclaredMethods(nameof(AsQueryable));
                if (methods.IsNotNullOrEmpty())
                {
                    var method = methods.FirstOrDefault(x => x.IsGenericMethod);
                    if (method != null)
                    {
                        return method.MakeGenericMethod(type).Invoke(ctx, new object[] { ctx }) as IQueryable;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
