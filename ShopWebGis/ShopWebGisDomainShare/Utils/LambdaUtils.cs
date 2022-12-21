/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Utils

 *文件名：  LambdaUtils

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/12 16:14:28

 *描述：Lamada表达式拓展创建方法

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopWebGisDomainShare.Utils
{
    public static class LambdaUtils
    {
        /// <summary>
        ///  创建lambda表达式：p=>p.propertyName == propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateEqual<T>(string propertyName, object propertyValue, Type type)
        {
            //创建参数
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);

            //创建常数
            ConstantExpression constant = Expression.Constant(propertyValue, type);

            return Expression.Lambda<Func<T, bool>>(Expression.Equal(member, Expression.Convert(constant, member.Type)), parameter);
        }
    }
}
