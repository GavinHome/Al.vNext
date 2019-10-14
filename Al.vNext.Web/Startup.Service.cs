//-----------------------------------------------------------------------------------
// <copyright file="StartupService.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Al.vNext.Database.Contracts;
using Al.vNext.DataBase.Implement;
using Al.vNext.Services.Contracts;
using Al.vNext.Services.Implement;

namespace Al.vNext.Web
{
    public static class StartupService
    {
        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            ////Cache
            ////services.AddSingleton<ICache, CustomCache>();

            ////EF & Mongo Services
            ConfigureDbService(services);

            ////Service Service
            ConfigureDomainService(services);
        }

        private static void ConfigureDbService(IServiceCollection services)
        {
            var dbContracts = System.Reflection.Assembly.GetAssembly(typeof(IDbCommon)).GetTypes().Where(x => x.IsInterface && (x.GetInterfaces().Contains(typeof(IDbCommon)) || x.FullName == typeof(IDbCommon).FullName)).ToList();
            var dbImplements = System.Reflection.Assembly.GetAssembly(typeof(DbCommon)).GetTypes().Where(x => x.IsClass && x.GetInterfaces().Contains(typeof(IDbCommon))).ToList();
            foreach (var contract in dbContracts)
            {
                var implement = dbImplements.FirstOrDefault(x => x.GetInterfaces().Contains(contract) && x.FullName == $"{typeof(DbCommon).Namespace}.{contract.Name.Remove(0, 1)}");

                if (implement == null)
                {
                    throw new NotImplementedException(contract.FullName + " Not Implemented");
                }

                services.AddScoped(contract, implement);
            }
        }

        private static void ConfigureDomainService(IServiceCollection services)
        {
            var servContracts = System.Reflection.Assembly.GetAssembly(typeof(IAccountService)).GetTypes().Where(x => x.IsInterface && x.FullName.EndsWith("Service")).ToList();
            var servImplements = System.Reflection.Assembly.GetAssembly(typeof(AccountService)).GetTypes().Where(x => x.IsClass && x.FullName.EndsWith("Service")).ToList();
            foreach (var contract in servContracts)
            {
                var implement = servImplements.FirstOrDefault(x => x.GetInterfaces().Contains(contract) && x.FullName == $"{typeof(AccountService).Namespace}.{contract.Name.Remove(0, 1)}");

                if (implement == null)
                {
                    throw new NotImplementedException(contract.FullName + " Not Implemented");
                }

                if (contract.FullName == typeof(IAuthFunctionService).FullName)
                {
                    services.AddSingleton(contract, implement);
                }
                else
                {
                    services.AddScoped(contract, implement);
                }
            }
        }
    }
}
