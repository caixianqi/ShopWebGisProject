/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisXxlJob

 *文件名：  Rtd

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/6/2 17:44:07

 *描述：Rtd XxlJob执行结果

/************************************************************************************/

using DotXxlJob.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisXxlJob
{
    public class Rtd: ReturnT
    {
        public Rtd(int code, string msg):base(code,msg)
        {

        }

        public new static void  Success(string msg)
        {
            new Rtd(200, msg);
        }

        public new static void Failed(string msg)
        {
            new Rtd(500, msg);
        }
    }
}
