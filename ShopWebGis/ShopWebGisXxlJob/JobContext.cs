/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisXxlJob

 *文件名：  JobContext

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/6/2 17:48:28

 *描述：JobContext封装Job上下文

/************************************************************************************/

using DotXxlJob.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisXxlJob
{
    public class JobContext
    {
        public JobExecuteContext JobExecuteContext;
        public JobContext(JobExecuteContext jobExecuteContext)
        {
            JobExecuteContext = jobExecuteContext;
        }
    }
}
