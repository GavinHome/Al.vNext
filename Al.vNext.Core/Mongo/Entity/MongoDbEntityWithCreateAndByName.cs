//-----------------------------------------------------------------------------------
// <copyright file="MongoDbEntityWithCreateAndByName.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Al.vNext.Core.Mongo.Entity
{
    public abstract class MongoDbEntityWithCreateAndByName : MongoDbEntity
    {
        [BsonRepresentation(BsonType.String)]
        public virtual string CreateByName { get; set; }
    }
}
