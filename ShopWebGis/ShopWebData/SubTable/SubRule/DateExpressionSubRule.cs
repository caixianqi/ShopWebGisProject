/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebData.SubTable.SubRule

 *文件名：  DateExpressionSubRule

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/14 16:30:26

 *描述：

/************************************************************************************/

using FastLambda;
using Microsoft.Extensions.Options;
using ShopWebGisDomainShare.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopWebData.SubTable.SubRule
{
    public class DateExpressionSubRule : BasicSubRule
    {
        private readonly IEvaluator _evaluator;
        private readonly TableSubRuleOptions _options;
        private TableSubRule _rules;
        private IList<DateRuleContent> _ruleContent;
        private string _expression;
        public DateExpressionSubRule(IEvaluator evaluator, IOptions<TableSubRuleOptions> options)
        {
            _evaluator = evaluator;
            _options = options.Value;
        }

        public override IList<string> GetDefaultTables(Type type, string defaultName)
        {
            _rules = _options.GetOrDefault(type.FullName);
            if (!(_rules != null && _rules.DateExpressionRules != null && _rules.DateExpressionRules.Any())) throw new Exception($"未配置分表具体规则!");

            var tableNames = new List<string>();
            foreach (var rule in _rules.DateExpressionRules)
            {
                foreach (var item in rule.Expressions)
                {
                    tableNames.AddRange(rule.Rules.Select(d => $"{defaultName}_{item}_{d.Suffix}"));
                }
            }
            return tableNames;
        }

        /// <summary>
        /// 解析查询涉及到的表名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IList<string> GetTables<T>(DataSubContext<T> context)
        {
            if (context.SubRouteType != typeof(DateTime)) throw new Exception("分表字段不是时间类型, 与分表规则不匹配!");
            _rules = _options.GetOrDefault(typeof(T).FullName);
            if (!(_rules != null && _rules.DateExpressionRules != null && _rules.DateExpressionRules.Any())) throw new Exception($"未配置分表具体规则!");

            // 当前解析未设置外部表达式，解析失败!
            _expression = context.Extension?.GetExpression();
            if (string.IsNullOrEmpty(_expression)) throw new Exception("当前解析未设置外部表达式，解析失败!");

            var content = _rules.DateExpressionRules.FirstOrDefault(c => c.Expressions.Contains(_expression));
            if (content == null) throw new Exception($"未匹配到分表规则，请检查分表设置!");
            _ruleContent = content.Rules;

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
            var value = ((DateTime)_evaluator.Eval(valueExpression)).ToTimeStamp();
            switch (expression.NodeType)
            {
                case ExpressionType.Equal:
                    return _ruleContent.Where(r => r.StartTimeStamp <= value && r.EndTimeStamp >= value).Select(d => $"{_expression}_{d.Suffix}");
                case ExpressionType.GreaterThanOrEqual:
                    return _ruleContent.Where(d => d.EndTimeStamp >= value).Select(d => $"{_expression}_{d.Suffix}");
                case ExpressionType.LessThanOrEqual:
                    return _ruleContent.Where(d => d.StartTimeStamp <= value).Select(d => $"{_expression}_{d.Suffix}");
                case ExpressionType.GreaterThan:
                    return _ruleContent.Where(d => d.EndTimeStamp > value).Select(d => $"{_expression}_{d.Suffix}");
                case ExpressionType.LessThan:
                    return _ruleContent.Where(d => d.StartTimeStamp < value).Select(d => $"{_expression}_{d.Suffix}");
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
                    foreach (var rule in _ruleContent)
                    {
                        // 查询时间段与当前时间段没有交集的时候, 该表跳过
                        if (rule.EndTimeStamp < startTime) continue;
                        if (rule.StartTimeStamp > endTime) continue;
                        yield return $"{_expression}_{rule.Suffix}";
                    }
                    break;
                case "Equals":
                    var value = ((DateTime)_evaluator.Eval(expression.Arguments[0])).ToTimeStamp();
                    var item = _ruleContent.FirstOrDefault(r => r.StartTimeStamp <= value && r.EndTimeStamp >= value);
                    if (item != null) yield return $"{_expression}_{item.Suffix}";
                    break;
                default:
                    throw new Exception("按时间分表,不支持此方法,时间范围查询请使用Between方法!");
            }
        }
    }
}
