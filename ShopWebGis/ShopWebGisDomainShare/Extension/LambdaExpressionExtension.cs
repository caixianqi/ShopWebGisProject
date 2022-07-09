/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Extension

 *文件名：  LambdaExpressionExtension

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/6/29 23:00:11

 *描述：表达式树拓展方法

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopWebGisDomainShare.Extension
{
    public static class LambdaExpressionExtension
    {
        /// <summary>
        /// 合并表达式树 （And）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Merge<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            //声明传递参数（也就是上面表达式树里面的参数s）
            ParameterExpression s = Expression.Parameter(typeof(T), "s");
            //统一管理参数，保证参数一致，否则会报错 变量未定义
            MyExpressionVisitor visitor = new MyExpressionVisitor(s);
            //表达式树内容
            Expression body1 = visitor.Visit(expr1.Body);
            Expression body2 = visitor.Visit(expr2.Body);
            //合并表达式
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(body1, body2), expr1.Parameters);
        }

        /// <summary>
        /// 合并表达式树 （Or）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            //声明传递参数（也就是上面表达式树里面的参数s）
            ParameterExpression s = Expression.Parameter(typeof(T), "s");
            //统一管理参数，保证参数一致，否则会报错 变量未定义
            MyExpressionVisitor visitor = new MyExpressionVisitor(s);
            //表达式树内容
            Expression body1 = visitor.Visit(expr1.Body);
            Expression body2 = visitor.Visit(expr2.Body);
            //合并表达式
            return Expression.Lambda<Func<T, bool>>(Expression.Or(body1, body2), s);
        }
    }

    public class MyExpressionVisitor : ExpressionVisitor
    {
        public ParameterExpression _Parameter { get; set; }

        public MyExpressionVisitor(ParameterExpression Parameter)
        {
            _Parameter = Parameter;
        }
        protected override Expression VisitParameter(ParameterExpression p)
        {
            return _Parameter;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);//Visit会根据VisitParameter()方法返回的Expression修改这里的node变量
        }
    }
}
