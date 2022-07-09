/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Const

 *文件名：  SystemConst

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/30 17:22:44

 *描述：系统常量

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Const
{
    public class SystemConst
    {
        public const string _defaultBasicTypeName = @"String|Boolean|Double|Int32|Int64|Int16|Single|DateTime|Decimal|Char|Object|Guid";

        public const string RegisterFailed = "注册失败:";

        public const string RegisterSuccess = "注册成功!";

        public const string LoginFailed = "登录失败,";

        public const string InvalidAuthorizationmMode = "无效的授权方式";

        public const string Parsefailure = "解析失败,请重新登录";

        public const string UserHaveBeenLock = "用户已被锁定!";

        public const string UserHasBeenDisabled = "用户已被禁用";

        public const string NotAffectedRow = "无影响行数!";

        /// <summary>
        /// 限制登录次数
        /// </summary>
        public const int LimitLoginTimes = 5;

        /// <summary>
        /// 限制登录时间间隔(秒)
        /// </summary>
        public const int LimitLoginPeriod = 600;

        /// <summary>
        /// 登录冻结时间间隔(分钟)
        /// </summary>
        public const int LoginFreezeTimeSpan = 60;
    }
}
