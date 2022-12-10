/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.SubTable.SubRule

 *文件名：  DateSubRule

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/11/25 15:02:44

 *描述：

/************************************************************************************/

using FastLambda;
using Microsoft.Extensions.Options;
using ShopWebGisDomainShare.CustomException;
using ShopWebGisDomainShare.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopWebData.SubTable.SubRule
{
    public class DateSubRule : BasicSubRule
    {
        private readonly IEvaluator _evaluator;
        private readonly TableSubRuleOptions _options;
        private TableSubRule _rules;

        public DateSubRule(IEvaluator evaluator, IOptions<TableSubRuleOptions> options)
        {
            _evaluator = evaluator;
            _options = options.Value;
        }
        public override IList<string> GetDefaultTables(Type type, string defaultName)
        {
            _rules = _options.GetOrDefault(type.FullName);
            if (!(_rules != null && _rules.DateRules != null && _rules.DateRules.Any())) throw new Exception($"未配置分表具体规则!");

            return _rules.DateRules.Select(r => $"{defaultName}_{r.Suffix}").ToList();
        }

        public override IList<string> GetTables<T>(DataSubContext<T> context)
        {
            if (context.SubRouteType != typeof(DateTime)) throw new Exception("分表字段不是时间类型, 与分表规则不匹配!");
            _rules = _options.GetOrDefault(typeof(T).FullName);
            if (!(_rules != null && _rules.DateRules != null && _rules.DateRules.Any())) throw new Exception($"未配置分表具体规则!");
            return base.GetTables(context);
        }

        /// <summary>
        /// lambda表达式的运算符解析
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="valueExpression"></param>
        /// <returns></returns>
        protected override IEnumerable<string> BinaryExpressionToRoute(Expression expression, Expression valueExpression)
        {
            var result = new List<string>();
            //// 只支持 "==" 的情况
            //if (expression.NodeType != ExpressionType.Equal)
            //{
            //    throw new AbpException("按时间分表, 时间范围查询请使用Between方法!");
            //}
            //var value = ((DateTime)_evaluator.Eval(valueExpression)).ToTimeStamp();
            //var rule = _rules.Rules.FirstOrDefault(r => r.StartTimeStamp <= value && r.EndTimeStamp >= value);
            //return rule == null ? string.Empty : rule.Suffix;

            var value = ((DateTime)_evaluator.Eval(valueExpression)).ToTimeStamp();
            switch (expression.NodeType)
            {
                case ExpressionType.Equal:
                    return _rules.DateRules.Where(r => r.StartTimeStamp <= value && r.EndTimeStamp >= value).Select(d => d.Suffix);
                case ExpressionType.GreaterThanOrEqual:
                    return _rules.DateRules.Where(d => d.EndTimeStamp >= value).Select(d => d.Suffix);
                case ExpressionType.LessThanOrEqual:
                    return _rules.DateRules.Where(d => d.StartTimeStamp <= value).Select(d => d.Suffix);
                case ExpressionType.GreaterThan:
                    return _rules.DateRules.Where(d => d.EndTimeStamp > value).Select(d => d.Suffix);
                case ExpressionType.LessThan:
                    return _rules.DateRules.Where(d => d.StartTimeStamp < value).Select(d => d.Suffix);
                default:
                    throw new Exception("时间分表规则不支持此查询表达式!");
            }
        }

        /// <summary>
        /// lambda表达式的中的方法解析
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected override IEnumerable<string> MethodCallExpressionToRoute(MethodCallExpression expression)
        {
            // 按时间分表，只支持通过Between进行时间范围查询
            switch (expression.Method.Name)
            {
                case "Between":
                case "BetweenEnd":
                    var startTime = ((DateTime)_evaluator.Eval(expression.Arguments[1])).ToTimeStamp();
                    var endTime = ((DateTime)_evaluator.Eval(expression.Arguments[2])).ToTimeStamp();
                    foreach (var rule in _rules.DateRules)
                    {
                        // 查询时间段与当前时间段没有交集的时候, 该表跳过
                        if (rule.EndTimeStamp < startTime) continue;
                        if (rule.StartTimeStamp > endTime) continue;
                        yield return rule.Suffix;
                    }
                    break;
                case "Equals":
                    var value = ((DateTime)_evaluator.Eval(expression.Arguments[0])).ToTimeStamp();
                    var item = _rules.DateRules.FirstOrDefault(r => r.StartTimeStamp <= value && r.EndTimeStamp >= value);
                    if (item != null) yield return item.Suffix;
                    break;
                default:
                    throw new MethodNotSupportException("按时间分表,不支持此方法,时间范围查询请使用Between方法!");
            }
        }
    }
}
