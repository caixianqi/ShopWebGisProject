/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebJson.Newtonsoft

 *文件名：  NewtonsoftJsonSerializer

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/4/5 16:45:03

 *描述：

/************************************************************************************/

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShopWebGisDomainShare.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopWebJson.Newtonsoft
{
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        private static readonly CamelCaseExceptDictionaryKeysResolver CommonCamelCaseExceptDictionaryKeysResolver = new CamelCaseExceptDictionaryKeysResolver();

        protected List<JsonConverter> _converters { get; }

        public NewtonsoftJsonSerializer(IOptions<NewtonsoftJsonSerializerOptions> options, IServiceProvider serviceProvider)
        {
            /// 获取服务中JsonConvert服务
            _converters = options.Value.Converters.Select(x => (JsonConverter)serviceProvider.GetService(x)).ToList();
        }

        public T Deserialize<T>(string jsonString, bool camelCase = true)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, CreateJsonSerializerSettings(camelCase));
        }

        public string Serialize(object obj, bool camelCase = true, bool indented = false)
        {
            return JsonConvert.SerializeObject(obj, CreateJsonSerializerSettings(camelCase, indented));
        }

        /// <summary>
        /// 序列化反序列设置(提供虚拟方法以便重写)
        /// </summary>
        /// <param name="camelCase"></param>
        /// <param name="indented"></param>
        /// <returns></returns>
        protected virtual JsonSerializerSettings CreateJsonSerializerSettings(bool camelCase = true, bool indented = false)
        {
            var settings = new JsonSerializerSettings();

            settings.Converters.InsertRange(0, _converters);

            if (camelCase)
            {
                settings.ContractResolver = CommonCamelCaseExceptDictionaryKeysResolver;
            }

            if (indented)
            {
                settings.Formatting = Formatting.Indented;
            }

            return settings;
        }

        /// <summary>
        /// 序列化字典时保持大小写
        /// </summary>
        private class CamelCaseExceptDictionaryKeysResolver : CamelCasePropertyNamesContractResolver
        {
            protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
            {
                JsonDictionaryContract contract = base.CreateDictionaryContract(objectType);

                contract.DictionaryKeyResolver = propertyName => propertyName;

                return contract;
            }
        }
    }
}
