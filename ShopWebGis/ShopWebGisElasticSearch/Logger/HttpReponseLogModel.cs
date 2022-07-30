/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisElasticSearch.Logger

 *文件名：  HttpReponseLogModel

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/30 10:41:12

 *描述：Http请求日志模型类

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisElasticSearch.Logger
{
    public class HttpReponseLogModel
    {
        /// <summary>
        /// 响应状态码
        /// </summary>
        public int ReponseStatusCode { get; set; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public string ReponseBody { get; set; }

        /// <summary>
        /// 响应时长
        /// </summary>
        public long ReponseTime { get; set; }

        /// <summary>
        /// 重写
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Reponse ReponseStatusCode:{ReponseStatusCode} ReponseBody:{ReponseBody} in {ReponseTime} ms";
        }
    }
}
