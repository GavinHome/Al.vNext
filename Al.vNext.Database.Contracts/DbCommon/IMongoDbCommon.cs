//-----------------------------------------------------------------------------------
// <copyright file="IMongoDbCommon.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

namespace Al.vNext.Database.Contracts
{
    /// <summary>
    /// Mongo Db Common
    /// </summary>
    public interface IMongoDbCommon : IDbCommon
    {
        /// <summary>
        /// 生成新Id
        /// </summary>
        /// <returns>Id</returns>
        string NewId();

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <typeparam name="TModel">TModel</typeparam>
        /// <param name="model">model</param>
        /// <returns>Model</returns>
        TModel Add<TModel>(TModel model);
    }
}
