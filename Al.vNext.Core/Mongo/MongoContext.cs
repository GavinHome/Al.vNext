//-----------------------------------------------------------------------------------
// <copyright file="MongoContext.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Threading.Tasks;
using MongoDB.Driver;
using Al.vNext.Core.Mongo.Extension;

namespace Al.vNext.Core.Mongo
{
    public class MongoContext
    {
        private MongoDbContextOptions options;

        public MongoContext(MongoDbContextOptions options)
        {
            this.options = options;
        }

        public MongoContext()
        {
        }

        protected IMongoDatabase DBSession
        {
            get
            {
                return MongoSessionManager.GetDBSession(options.ConnectString, options.Database);
            }
        }

        public virtual IMongoCollection<T> Of<T>()
        {
            return DBSession.Get<T>();
        }

        public virtual Task<IMongoCollection<T>> OfAsync<T>()
        {
            return DBSession.GetAsync<T>();
        }
    }
}
