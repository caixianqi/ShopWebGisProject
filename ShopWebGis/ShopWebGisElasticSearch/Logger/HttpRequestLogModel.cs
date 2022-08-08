/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisElasticSearch.Logger

 *文件名：  HttpRequestLogModel

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/30 10:36:36

 *描述：Http请求模型类

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisElasticSearch.Logger
{
    public class HttpRequestLogModel
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequestParameter { get; set; }

        public override string ToString()
        {
            return $"RequestMethod:{RequestMethod} RequestUrl:{RequestUrl} RequestParameter: {RequestParameter}";
        }
    }
}
