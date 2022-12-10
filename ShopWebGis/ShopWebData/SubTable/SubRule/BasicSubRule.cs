/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.SubTable.SubRule

 *文件名：  BasicSubRule

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/11/25 15:03:55

 *描述：

/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopWebData.SubTable.SubRule
{
    public abstract class BasicSubRule : ISubRule
    {
        public virtual IList<string> GetTables<T>(DataSubContext<T> context)
        {
            if (ExpressionToRoute(context.Expression.Body, context.SubRoute, out List<string> suffixes))
            {
                return suffixes.Select(suffix => $"{context.DefaultName}_{suffix}").ToList();
            }
            throw new Exception("查询条件没有包含分表字段, 请根据分表字段查询！");
        }

        /// <summary>
        /// 通过lambda表达式确定查询的表
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="subRoute"></param>
        /// <returns></returns>
        protected virtual bool ExpressionToRoute(Expression expression, string subRoute, out List<string> suffixes)
        {
            if (expression is BinaryExpression)
            {
                return BinaryExpressionToRoute((BinaryExpression)expression, subRoute, out suffixes);
            }
            else if (expression is MethodCallExpression)
            {
                return MethodCallExpressionToRoute((MethodCallExpression)expression, subRoute, out suffixes);
            }
            else
            {
                return OtherExpressionToRoute(expression, subRoute, out suffixes);
            }
            //else if(expression is LambdaExpression)
            //{
            //    result.AddRange(LambdaExpressionToRoute());
            //}
        }

        protected virtual bool BinaryExpressionToRoute(BinaryExpression expression, string subRoute, out List<string> suffixes)
        {
            var list = new List<string>();
            var leftRoute = false;
            var rightRoute = false;
            if (expression.NodeType == ExpressionType.AndAlso)
            {
                leftRoute = ExpressionToRoute(expression.Left, subRoute, out List<string> leftList);
                rightRoute = ExpressionToRoute(expression.Right, subRoute, out List<string> rightList);

                if (leftRoute && rightRoute)
                {
                    var intersection = leftList.Intersect(rightList);
                    // 没有交集的时候，说明查询条件矛盾，查询不到数据
                    if (intersection.Any())
                    {
                        list.AddRange(intersection);
                    }
                }
                else if (leftRoute || rightRoute)
                {
                    list.AddRange(leftRoute ? leftList : rightList);
                }
            }
            else if (expression.NodeType == ExpressionType.OrElse)
            {
                leftRoute = ExpressionToRoute(expression.Left, subRoute, out List<string> leftList);
                rightRoute = ExpressionToRoute(expression.Right, subRoute, out List<string> rightList);

                list.AddRange(leftList);
                list.AddRange(rightList);
            }
            else
            {
                leftRoute = IsRoute(expression.Left, subRoute);
                rightRoute = IsRoute(expression.Right, subRoute);

                // 判断当前表达式的字段是否为分表路由字段
                if (leftRoute || rightRoute)
                {
                    var valueExpression = leftRoute ? expression.Right : expression.Left;
                    var values = BinaryExpressionToRoute(expression, valueExpression);
                    if (values.Any()) list.AddRange(values);
                }
            }
            suffixes = list;
            return leftRoute || rightRoute;
        }

        protected virtual bool MethodCallExpressionToRoute(MethodCallExpression expression, string subRoute, out List<string> suffixes)
        {
            var result = new List<string>();
            // 判断当前表达式的字段是否为分表路由字段
            var isRoutes = new List<bool>();
            if (expression.Object != null)
            {
                isRoutes.Add(IsRoute(expression.Object, subRoute));
            }
            foreach (var item in expression.Arguments)
            {
                isRoutes.Add(IsRoute(item, subRoute));
            }
            if (isRoutes.Contains(true))
            {
                var routes = MethodCallExpressionToRoute(expression);
                if (routes.Any()) result.AddRange(routes);
            }
            suffixes = result;
            return isRoutes.Contains(true);
        }

        protected virtual bool OtherExpressionToRoute(Expression expression, string subRoute, out List<string> suffixes)
        {
            if (expression.NodeType == ExpressionType.Invoke)
            {
                return ExpressionToRoute(((InvocationExpression)expression).Expression, subRoute, out suffixes);
            }
            else if (expression.NodeType == ExpressionType.Lambda)
            {
                return ExpressionToRoute(((LambdaExpression)expression).Body, subRoute, out suffixes);
            }
            else
            {
                throw new InvalidOperationException($"分表规则解析暂不支持{expression.NodeType}类型的lambda表达式操作!");
            }
        }

        protected abstract IEnumerable<string> BinaryExpressionToRoute(Expression expression, Expression valueExpression);

        protected abstract IEnumerable<string> MethodCallExpressionToRoute(MethodCallExpression expression);

        /// <summary>
        /// 判断某个lambda表达式中是否包含分表字段
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="subRoute"></param>
        /// <returns></returns>
        protected virtual bool IsRoute(Expression expression, string subRoute)
        {
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                var propertyExpression = (MemberExpression)expression;
                if (propertyExpression.Expression != null && propertyExpression.Member.Name == subRoute) return true;
            }
            return false;
        }

        public virtual IList<string> GetDefaultTables(Type type, string defaultName)
        {
            return new List<string>();
        }
    }
}
