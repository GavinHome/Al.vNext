//-----------------------------------------------------------------------------------
// <copyright file="MongodbContextExtension.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Al.vNext.Core.Const;
using Al.vNext.Core.Enum;
using Al.vNext.Core.Mongo.Entity;
using Al.vNext.Core.Utility;

namespace Al.vNext.Core.Mongo.Extension
{
    public static class MongodbContextExtension
    {
        public static IMongoCollection<T> Get<T>(this IMongoDatabase DBSession)
        {
            GuardUtils.NotNull(DBSession, nameof(DBSession));
            string tableName = BsonClassMap.LookupClassMap(typeof(T)).Discriminator;
            return DBSession.GetCollection<T>(tableName);
        }

        public static async Task<IMongoCollection<T>> GetAsync<T>(this IMongoDatabase DBSession)
        {
            string tableName = BsonClassMap.LookupClassMap(typeof(T)).Discriminator;
            return await Task.Run(() => DBSession.GetCollection<T>(tableName));
        }

        public static void SetEntityPrincipal(this MongoDbEntity entity, System.Security.Claims.ClaimsPrincipal user)
        {
            var code = user.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            var isNew = string.IsNullOrEmpty(entity.Id) || System.Guid.Empty.ToString() == entity.Id;
            var name = user.FindFirst(c => c.Type == JwtClaimNamesConst.UseName);

            if (entity is MongoDbEntity esc)
            {
                if (isNew)
                {
                    esc.Id = System.Guid.NewGuid().ToString();
                    esc.CreateAt = DateTime.Now;
                    esc.CreateBy = code?.Value;
                    esc.DataStatus = DataStatusEnum.Valid;
                }

                if (entity is MongoDbEntityWithCreateAndByName ewcn)
                {
                    ewcn.CreateByName = name?.Value;
                }

                if (entity is MongoDbEntityWithUpdate escu)
                {
                    escu.UpdateAt = DateTime.Now;
                    escu.UpdateBy = code?.Value;
                }

                if (entity is MongoDbEntityWithUpdateAndByName esun)
                {
                    esun.CreateByName = name?.Value;
                    esun.UpdateByName = name?.Value;
                }
            }
        }
    }
}
