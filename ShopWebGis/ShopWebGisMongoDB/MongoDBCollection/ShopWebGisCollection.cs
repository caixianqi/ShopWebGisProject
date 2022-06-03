/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMongoDB.MongoDBCollection

 *文件名：  ShopWebGisCollection

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/18 15:52:51

 *描述：MongoDB Collection集合

/************************************************************************************/

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisMongoDB.MongoDBCollection
{
    class ShopWebGisCollection
    {
    }

    public class test
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public int count { get; set; }

        public object info { get; set; }
    }

    public class test1
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public int count { get; set; }

        public object info { get; set; }
    }
}
