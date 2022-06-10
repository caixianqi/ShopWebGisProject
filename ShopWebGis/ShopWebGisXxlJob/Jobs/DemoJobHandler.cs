/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisXxlJob.Jobs

 *文件名：  DemoJobHandler

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/6/2 15:15:06

 *描述：

/************************************************************************************/

using DotXxlJob.Core;
using DotXxlJob.Core.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebGisXxlJob.Jobs
{
    [JobHandler("demoJobHandler")]
    public class DemoJobHandler : Job
    {
        public override Task<Rtd> Execute(JobContext jobExecuteContext)
        {
            base.log("123");
            return Task.FromResult(Rtd.Success("完成"));
        }
    }
}
