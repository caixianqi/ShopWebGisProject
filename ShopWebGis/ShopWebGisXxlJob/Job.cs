/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisXxlJob

 *文件名：  Job

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/6/2 17:32:43

 *描述：Job 抽象类

/************************************************************************************/

using DotXxlJob.Core;
using DotXxlJob.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisXxlJob
{
    public abstract class Job : AbstractJobHandler
    {

        private JobContext jobContext;
        public override Task<ReturnT> Execute(JobExecuteContext context)
        {
            jobContext = new JobContext(context);
            Task<Rtd> task = Execute(jobContext);
            return Task.FromResult(new ReturnT(task.Result.Code, task.Result.Msg));
        }

        public abstract Task<Rtd> Execute(JobContext jobExecuteContext);

        protected void log(string msg)
        {
            if (jobContext != null)
            {
                jobContext.JobExecuteContext.JobLogger.Log(msg);
            }
        }

        protected void LogError(Exception ex)
        {
            if (jobContext != null)
            {
                jobContext.JobExecuteContext.JobLogger.LogError(ex);
            }
        }
    }
}
