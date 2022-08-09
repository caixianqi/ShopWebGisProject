/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.config

 *文件名：  EalsticSearchConfig

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/25 17:16:32

 *描述：Ealstic配置类

/************************************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomain.config
{
    public class EalsticSearchLogOption
    {

        public EalsticSearchLogOption()
        {
            LogMessagetemplate = DefaultMessageTemplate;
        }
        /// <summary>
        /// ES地址
        /// </summary>
        public string Url { get; set; }

        public string Index { get; set; }

        /// <summary>
        /// 序列化
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        /// 默认日志模板
        public string LogMessagetemplate { get; set; }

        public const string DefaultMessageTemplate = "Request: {0} \n Reponse: {1}";

    }
}

