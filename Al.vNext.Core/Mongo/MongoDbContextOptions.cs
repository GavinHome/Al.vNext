//-----------------------------------------------------------------------------------
// <copyright file="MongoDbContextOptions.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

namespace Al.vNext.Core.Mongo
{
    public class MongoDbContextOptions
    {
        public MongoDbContextOptions()
        {
            ConnectString = "mongodb://127.0.0.1:27017";
            Database = "local";
        }

        public string Database { get; set; }

        public string ConnectString { get; set; }
    }
}