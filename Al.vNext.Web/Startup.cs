//-----------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.EntityFrameworkCore;
using Al.vNext.Web.Common.Middleware;
using Al.vNext.Model.Context;
using Al.vNext.Web.Common;
using Al.vNext.Web.Common.Filters;
using Al.vNext.Web.Common.KendoExtensions;
using Al.vNext.Web.PermissionExtensions;
using Al.vNext.Web.VueExtension;

namespace Al.vNext.Web
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            ////add spa static files
            services.AddSpaStaticFiles(options => options.RootPath = "ClientApp/dist");

            ////add spa static files
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = GetMimeTypes();
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.UseRowNumberForPaging());
            });

            ////add MongoDbContext
            services.AddMongoDbContext<MongoDbContext>(options =>
            {
                options.UseMongoServer(Configuration.GetConnectionString("MongodbConnection"));
            });

            services.AddOData();

            ////add business services
            services.ConfigureBusinessServices();

            ////add Authorize services
            ConfigureAuth(services);

            ////add action filter
            services.AddTransient<AsyncActionFilter>();

            ////add authorize handler
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            ////Maintain property names during serialization. See:https://github.com/aspnet/Announcements/issues/194
            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new CustomeModelBinderProvider());
                options.Filters.AddService<AsyncActionFilter>();
            });

            ////add kendo
            services.AddKendo();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            ////app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            ////app.UseEndpoints(endpoints =>
            ////{
            ////    endpoints.MapControllerRoute(
            ////        name: "default",
            ////        pattern: "{controller=Home}/{action=Index}/{id?}");
            ////});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // add following statements
            app.UseSpaStaticFiles();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    // Launch development server for Vue.js
                    spa.UseVueDevelopmentServer();
                }
            });

            ////use jwt token middleware
            app.UseTokenProvider(GetTokenProviderOptions());

            ////use global exception handling middleware
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        private string[] GetMimeTypes()
        {
            var mimeTypes = Configuration.GetSection("MimeTypes").Value;
            return !string.IsNullOrEmpty(mimeTypes) ? mimeTypes.Split(",") : Array.Empty<string>();
        }
    }
}
