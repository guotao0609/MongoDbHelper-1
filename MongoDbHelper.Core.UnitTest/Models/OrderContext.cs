using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbHelper.Core.UnitTest.Models
{
    [BsonIgnoreExtraElements]
    public class OrderContext
    {
        /// <summary>
        /// 工单名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 工单创建者
        /// </summary>
        /// <returns></returns>
        public User Creater { get; set; } = new User();
        /// <summary>
        ///  相关用户
        /// </summary>
        /// <returns></returns>
        public List<CustomUser> Users { get; set; } = new List<CustomUser>();
    }
}