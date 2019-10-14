//-----------------------------------------------------------------------------------
// <copyright file="MongoDbEntity.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using Al.vNext.Core.Enum;
using Al.vNext.Core.Entity;

namespace Al.vNext.Core.Mongo.Entity
{
    public abstract class MongoDbEntity : IEntitySetOfType<string>
    {
        [BsonRequired]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public virtual string Id { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public virtual DataStatusEnum DataStatus { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public virtual DateTime CreateAt { get; set; }

        [BsonRepresentation(BsonType.String)]
        public virtual string CreateBy { get; set; }

        /// <summary>
        /// 额外元素，所有未包含在映射中元素会存在于此,类型可以为：IDictionary_string, object 或 BsonDocument
        /// </summary>
        [BsonExtraElements]
        public virtual IDictionary<string, object> CatchAll { get; set; }

        object IEntitySet.Id => Id;
    }
}
