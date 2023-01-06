/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMicroService

 *文件名：  AbstractCodeTemplate

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/1/4 17:14:28

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisMicroService
{
    public abstract class AbstractCodeTemplate
    {
        /// <summary>
        /// 获取类模板
        /// </summary>
        /// <returns></returns>
        public abstract string GetClassTemplate();

        /// <summary>
        /// 获取Get方法模板
        /// </summary>
        /// <returns></returns>
        public abstract string GetMethodTemplate();

        /// <summary>
        /// 获取Post方法模板
        /// </summary>
        /// <returns></returns>
        public abstract string PostMethodTemplate();
    }
}
