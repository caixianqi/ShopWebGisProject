/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebThread

 *文件名：  CancellationTokenProviderExtensions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/4/10 22:25:58

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ShopWebThread
{
    public static class CancellationTokenProviderExtensions
    {
        /// <summary>
        /// 通用取消令牌获取
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="prefferedValue"></param>
        /// <returns></returns>
        public static CancellationToken FallbackToProvider(this ICancellationTokenProvider provider, CancellationToken prefferedValue = default)
        {
            return prefferedValue == default || prefferedValue == CancellationToken.None
                ? provider.Token
                : prefferedValue;
        }
    }
}
