//-----------------------------------------------------------------------------------
// <copyright file="MiddlewareExtensions.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Al.vNext.Core.Mongo;
using Al.vNext.Model.Context;
using Al.vNext.Web.Common.Middleware;
using Al.vNext.Web.Common.TokenProvider;

namespace Al.vNext.Web.Common
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenProvider(this IApplicationBuilder builder, TokenProviderOptions parameters)
        {
            return builder.UseMiddleware<TokenProviderMiddleware>(Options.Create(parameters));
        }

        public static IServiceCollection AddMongoDbContext<TContext>(this IServiceCollection serviceCollection, Action<MongoDbContextOptionsBuilder> optionsAction) where TContext : MongoContext
        {
            var options = new MongoDbContextOptionsBuilder();
            optionsAction(options);
            serviceCollection.AddSingleton<MongoDbContextOptions>(options.Options);
            serviceCollection.AddSingleton<MongoContext, MongoDbContext>();
            return serviceCollection;
        }

        public static IServiceCollection AddMongoDbContext<TContext>(this IServiceCollection serviceCollection) where TContext : MongoContext
        {
            serviceCollection.AddSingleton<MongoDbContextOptions>(new MongoDbContextOptions());
            serviceCollection.AddSingleton<MongoContext, MongoDbContext>();
            return serviceCollection;
        }
    }
}