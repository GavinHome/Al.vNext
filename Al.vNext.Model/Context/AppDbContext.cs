//-----------------------------------------------------------------------------------
// <copyright file="AppDbContext.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace Al.vNext.Model.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Code> Codes { get; set; }
    }
}
