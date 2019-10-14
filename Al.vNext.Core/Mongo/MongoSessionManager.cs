//-----------------------------------------------------------------------------------
// <copyright file="MongoSessionManager.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using MongoDB.Driver;

namespace Al.vNext.Core.Mongo
{
    internal static class MongoSessionManager
    {
        private static IMongoDatabase _mongoDatabase = null;
        private static object _locker = new object();

        public static IMongoDatabase GetDBSession(string client, string database)
        {
            if (_mongoDatabase == null)
            {
                ////单实例对象构造
                lock (_locker)
                {
                    if (_mongoDatabase == null)
                    {
                        if (string.IsNullOrEmpty(client) || string.IsNullOrEmpty(database))
                        {
                            throw new System.ArgumentException("Mongo configuration is incorrect or not configured connection string");
                        }
                        else
                        {
                            _mongoDatabase = new MongoClient(client).GetDatabase(database);
                        }
                    }
                }
            }

            return _mongoDatabase;
        }
    }
}
