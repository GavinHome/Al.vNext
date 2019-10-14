//-----------------------------------------------------------------------------------
// <copyright file="MongoDbContext.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Al.vNext.Core.Mongo;

namespace Al.vNext.Model.Context
{
    public class MongoDbContext : MongoContext
    {
        public MongoDbContext(MongoDbContextOptions options) : base(options)
        {
        }

        public MongoDbContext()
        {
        }
    }
}
