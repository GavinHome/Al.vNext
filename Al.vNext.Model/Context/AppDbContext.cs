﻿//-----------------------------------------------------------------------------------
// <copyright file="AppDbContext.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNet.OData.Builder;
//using Microsoft.OData.Edm;

namespace Al.vNext.Model.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        ////public DbSet<Attachment> Attachments { get; set; }
        ////public DbSet<Code> Codes { get; set; }
        ////public DbSet<Function> Functions { get; set; }
        ////public DbSet<Salary> Salarys { get; set; }
        ////public DbSet<EmployeeRoleFunction> EmployeeRoleFunctions { get; set; }

        ////public static IEdmModel GetEdmModel(IServiceProvider servicePrivider)
        ////{
        ////    var builder = new ODataConventionModelBuilder(servicePrivider);
        ////    var entitySets = typeof(AppDbContext).GetProperties().Where(x => x.MemberType == System.Reflection.MemberTypes.Property && x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)).ToList();
        ////    foreach (var entitySet in entitySets)
        ////    {
        ////        Type type = entitySet.PropertyType.GenericTypeArguments.First();
        ////        EntityTypeConfiguration entityTypeConfiguration = builder.AddEntityType(type);
        ////        builder.AddEntitySet(entitySet.Name, entityTypeConfiguration);
        ////    }

        ////    return builder.GetEdmModel();
        ////}

        ////protected override void OnModelCreating(ModelBuilder modelBuilder)
        ////{            
        ////}
    }
}
