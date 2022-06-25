/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.config

 *文件名：  RedisConfiguration

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/6/25 21:36:04

 *描述：Redis配置类

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomain.config
{
    public class RedisConfiguration
    {
        public string Name { get; set; }

        public string Ip
        {
            get; set;
        }

        public int Port { get; set; }

        public int Db { get; set; }

        public int Timeout { get; set; }
    }
}
