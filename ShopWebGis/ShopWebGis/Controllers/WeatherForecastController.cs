
using IRepository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using ShopWebGis.Attributes;
using ShopWebGisApplicationContract.User.Models;
using ShopWebGisDomain.User;
using ShopWebGisFreeSql.Aop;
using ShopWebGisFreeSql.config;
using ShopWebGisFreeSql.Extension;
using ShopWebGisIoc;
using ShopWebGisMongoDB.Base;
using ShopWebGisMongoDB.MongoDBCollection;
using ShopWebGisMongoDB.MongoDBConfig;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGis.Controllers
{
    [ApiController] //Authorize]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRepository<int, UserInfo> _iUserInfoRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;
        private readonly IShopWebGisDatabaseSettings _shopWebGisDatabaseSettings;
        private readonly IMongoDBRepository<ObjectId, BsonDocument, test> _testimongoDBRepository;
        private readonly IMongoDBRepository<ObjectId, BsonDocument, test1> _test1imongoDBRepository;
        private readonly IFreeSql _fsql;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepository<int, UserInfo> iUserInfoRepository, IDistributedCache distributedCache, IConfiguration configuration,
            IMongoDBRepository<ObjectId, BsonDocument, test> testMongoDBRepository, IMongoDBRepository<ObjectId, BsonDocument, test1> test1MongoDBRepository,
            IFreeSql fsql)
        {
            _logger = logger;
            _iUserInfoRepository = iUserInfoRepository;
            _distributedCache = distributedCache;
            _testimongoDBRepository = testMongoDBRepository;
            _test1imongoDBRepository = test1MongoDBRepository;
            _fsql = fsql;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            //var client = new MongoClient(_shopWebGisDatabaseSettings.ConnectionString);
            //var database = client.GetDatabase(_shopWebGisDatabaseSettings.DatabaseName);
            //var collection = database.GetCollection<BsonDocument>("test");
            ////            var document = new BsonDocument
            ////{
            ////    { "name", "MongoDB" },
            ////    { "type", "Database" },
            ////    { "count", 1 },
            ////    { "info", new BsonDocument
            ////        {
            ////            { "x", 203 },
            ////            { "y", 102 }
            ////        }}
            ////};
            ////            collection.InsertOne(document);

            //// redis operate
            ////var key = "test_key";
            ////var valueByte = await _distributedCache.GetAsync(key);
            ////if (valueByte == null)
            ////{
            ////    await _distributedCache.SetAsync(key, Encoding.UTF8.GetBytes("world22222"));
            ////    valueByte = await _distributedCache.GetAsync(key);
            ////}
            ////var valueString = Encoding.UTF8.GetString(valueByte);
            var filter = Builders<BsonDocument>.Filter.Empty;
            //var document = collection.Find(filter).ToList();
            //foreach (var item in document)
            //{
            //    var test = item.;
            //}
            //return document;
            var documents = await _test1imongoDBRepository.GetAllList(x =>
            {
                x.Sort = Builders<BsonDocument>.Sort.Ascending("type");
            });
            foreach (var item in documents)
            {
                var tt = BsonSerializer.Deserialize<test>(item);
            }
            return "12";
        }


        [HttpGet("test")]
        public async Task<object> MondoDBOperation()
        {
            //var client = new MongoClient(_shopWebGisDatabaseSettings.ConnectionString);
            //var database = client.GetDatabase(_shopWebGisDatabaseSettings.DatabaseName);
            //var collection = database.GetCollection<BsonDocument>("test");
            var document = new BsonDocument
            {
                { "name", "MongoDB" },
                { "type", "Database" },
                { "count", 1 },
                { "info", new BsonDocument
                    {
                        { "x", 203 },
                        { "y", 102 }
                    }}
            };
            _test1imongoDBRepository.InsertOneAsync(document);

            //// redis operate
            ////var key = "test_key";
            ////var valueByte = await _distributedCache.GetAsync(key);
            ////if (valueByte == null)
            ////{
            ////    await _distributedCache.SetAsync(key, Encoding.UTF8.GetBytes("world22222"));
            ////    valueByte = await _distributedCache.GetAsync(key);
            ////}
            ////var valueString = Encoding.UTF8.GetString(valueByte);
            //var filter = Builders<BsonDocument>.Filter.Eq("info.x", 203);
            //var document = collection.Find(filter).ToList();
            //foreach (var item in document)
            //{
            //    var test = item.;
            //}
            //return document;
            // 条件过滤
            //public static FilterDefinitionBuilder<TDocument> Filter { get; }

            //        // 索引过滤
            //        public static IndexKeysDefinitionBuilder<TDocument> IndexKeys { get; }

            //        // 映射器，相当于使用 Linq 的 .Select() 查询自己只需要的字段
            //        public static ProjectionDefinitionBuilder<TDocument> Projection { get; }

            //        // 排序，创建排序规则，如工具年龄排序
            //        public static SortDefinitionBuilder<TDocument> Sort { get; }

            //        // 更新，更新某些字段的值等
            //        public static UpdateDefinitionBuilder<TDocument> Update { get; }
            var sort = Builders<BsonDocument>.Sort.Ascending("type");
            //await _test1imongoDBRepository.GetFirstOrDefault()
            var filter = Builders<BsonDocument>.Filter;
            var documents = await _test1imongoDBRepository.GetAllList(x =>
            {
                x.Sort = Builders<BsonDocument>.Sort.Ascending("count");
                x.Projection = Builders<BsonDocument>.Projection.Include("name");
            });
            List<test> list = new List<test>();
            foreach (var item in documents)
            {
                var tt = BsonSerializer.Deserialize<test>(item);
                list.Add(tt);
            }
            return list;
        }

        [HttpGet("FreesqlTest")]
        public void FreesqlTest()
        {
            var test = _fsql.Select<UserInfo>();
            Expression<Func<UserInfo, bool>> expression = (x => x.Id == 1 && x.isSoftDelete == false && x.UserPhone == "123");
            test.SubTableSelect(expression, null);
        }

        [HttpGet("FreesqlTest1")]
        public void FreesqlTest1(string ttttt)
        {
            _logger.LogInformation("123");
            var test = _fsql.Select<UserInfo>().Where(x => x.Id == 1).ToList();
            List<string> vs = new List<string>()
            {
                "1232",
                "41123123",
                "34324"
            };
            Expression<Func<UserInfo, bool>> expression = (x => x.UpdateUserId == "dddd");
            // test.SubTableSelect(expression, null);
        }
    }
}
