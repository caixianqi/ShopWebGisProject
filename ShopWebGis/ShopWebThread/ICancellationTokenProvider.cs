/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebThread

 *文件名：  ICancellationTokenProvider

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2023/4/10 22:15:44

 *描述：取消令牌提供者接口

/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ShopWebThread
{
    public interface ICancellationTokenProvider
    {
        CancellationToken Token { get; }
    }
}
