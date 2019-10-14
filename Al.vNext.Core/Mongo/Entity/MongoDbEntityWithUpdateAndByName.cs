//-----------------------------------------------------------------------------------
// <copyright file="MongoDbEntityWithUpdateAndByName.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Al.vNext.Core.Mongo.Entity
{
    public abstract class MongoDbEntityWithUpdateAndByName : MongoDbEntityWithUpdate
    {
        [BsonRepresentation(BsonType.String)]
        public virtual string CreateByName { get; set; }

        [BsonRepresentation(BsonType.String)]
        public virtual string UpdateByName { get; set; }
    }
}
