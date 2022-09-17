/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.AbstarctClass

 *文件名：  SubRuleAbstarctClass

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/25 16:43:19

 *描述：SubRule抽象类

/************************************************************************************/

using ShopWebGisDomainShare.Const;
using ShopWebGisDomainShare.CustomException;
using ShopWebGisDomainShare.Extension;
using ShopWebGisFreeSql.config;
using ShopWebGisFreeSql.Resolve;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace ShopWebGisFreeSql.AbstarctClass
{
    public abstract class SubRuleAbstarctClass
    {
        public Dictionary<string, object> paramList = new Dictionary<string, object>();

        public List<ExpressionResult> expressionResults = new List<ExpressionResult>();

        /// <summary>
        /// 获取默认表名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string GetDefaultName<T>()
        {
            var type = typeof(T);
            var tableName = "";
            // 判断是否有Table特性，有特性，tableName取特性name，否则取类名
            if (Attribute.IsDefined(type, typeof(TableAttribute)))
            {
                var attribute = (TableAttribute)Attribute.GetCustomAttribute(
                                                       type, typeof(TableAttribute));
                tableName = attribute.Name;
            }
            else
            {
                tableName = type.Name;
            }
            return tableName;
        }

        /// <summary>
        /// 获取分表规则类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected Type GetSubRuleType<T>()
        {
            var t = typeof(T);
            var fullName = $"{t.Namespace}.{t.Name}";
            var domainClassSection = SubSetting._configuration.GetSection(fullName);// 获取实体命名空间以及类名
            var subRule = domainClassSection.GetSection("SubRuleType").Value;// 分表类型
            if (!SubRuleTypeMapper.SubRuleTypeMapperDic.TryGetValue(subRule, out var subRuleType))// 获取Type
            {
                throw new ShopWebGisCustomException("分表类型不存在，无法确定分表规则!");
            }
            return subRuleType;
        }

        /// <summary>
        /// 递归解析BinaryExpression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected void ParsingExpression(BinaryExpression binaryExpression)
        {
            var leftNodeExpression = binaryExpression.Left;
            if (leftNodeExpression.NodeType == ExpressionType.MemberAccess)
            {
                var leftParameterExpression = binaryExpression.Left as MemberExpression;
                var member = leftParameterExpression.Member.Name;
                var value = GetChildValue(binaryExpression.Right);
                var expressionOperator = GetOperStr(binaryExpression.NodeType);
                expressionResults.Add(new ExpressionResult()
                {
                    expressionOperator = expressionOperator,
                    parameter = member,
                    value = value
                });
                return;
            }
            else
            {
                ParsingExpression(binaryExpression.Left as BinaryExpression);
            }

            var rightNodeExpression = binaryExpression.Right;
            if (rightNodeExpression.NodeType == ExpressionType.MemberAccess)
            {
                var leftParameterExpression = binaryExpression.Left as MemberExpression;
                var member = leftParameterExpression.Member.Name;
                var value = GetChildValue(binaryExpression.Right);
                var expressionOperator = GetOperStr(binaryExpression.NodeType);
                expressionResults.Add(new ExpressionResult()
                {
                    expressionOperator = expressionOperator,
                    parameter = member,
                    value = value
                });
                return;
            }
            else
            {
                ParsingExpression(binaryExpression.Right as BinaryExpression);
            }


        }

        /// <summary>
        /// 解析表达式获取值
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public object GetChildValue(Expression exp)
        {
            if (exp == null) return null;
            //类型
            var typeName = exp.GetType().Name;
            switch (typeName)
            {
                case "BinaryExpression":
                case "LogicalBinaryExpression":
                case "MethodBinaryExpression":
                    //类型转换
                    var lExp = exp as BinaryExpression;
                    //递归
                    var ret = GetChildValue(lExp.Left);
                    //返回
                    return IsNullDefaultType(ret) ? GetChildValue(lExp.Right) : ret;
                case "PropertyExpression":
                case "FieldExpression":
                    //类型转换
                    var mberExp = exp as MemberExpression;
                    //返回
                    return GetChildValue(mberExp.Expression);
                case "ConstantExpression":
                    //类型转换
                    var cExp = exp as ConstantExpression;
                    //返回
                    return cExp.Value;
                case "UnaryExpression":
                    //类型转换
                    var unaryExp = exp as UnaryExpression;
                    //返回
                    return GetChildValue(unaryExp.Operand);
                case "InstanceMethodCallExpression1":
                    //类型转换
                    var imExp = exp as MethodCallExpression;
                    //返回
                    return imExp.Arguments.Count > 0 ? GetChildValue(imExp.Arguments[0]) : null;
                case "TypedParameterExpression":
                    //类型转换
                    var parameterExpression = exp as ParameterExpression;
                    //返回
                    return parameterExpression.Name;
                default:
                    return null;
                    //抛出
                    //throw new ShopWebGisExpressionException($"GetChildValue - 未对该类型做任何处理typeName = {typeName}");
            }

        }

        /// <summary>
        /// 是否空或者系统默认基本类型 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool IsNullDefaultType(object obj)
        {
            //校验
            if (obj == null) return true;
            //判断
            return IsDefaultType(obj.GetType());
        }

        /// <summary>
        /// 读取并存储所有参数 
        /// </summary>
        /// <param name="paramObj"></param>
        public void AppendParams(object paramObj)
        {
            //校验
            if (IsNullDefaultType(paramObj)) return;
            //初始化
            if (paramList == null) paramList = new Dictionary<string, object>();
            //读取属性
            var props = paramObj.GetType().GetProperties();
            foreach (var item in props)
            {
                //读取值
                var value = item.GetValue(paramObj);
                if (IsDefaultType(item.PropertyType))
                {
                    //存储
                    if (value != null) paramList.Add(string.Format("@{0}", item.Name), value);
                    //继续
                    continue;
                }
                //递归
                AppendParams(value);
            }
            //读取字段
            var fields = paramObj.GetType().GetFields();
            foreach (var item in fields)
            {
                //读取值
                var value = item.GetValue(paramObj);
                if (IsDefaultType(item.FieldType))
                {
                    //存储
                    if (value != null) paramList.Add(string.Format("@{0}", item.Name), value);
                    //继续
                    continue;
                }
                //递归
                AppendParams(item.GetValue(paramObj));
            }
        }

        /// <summary>
        /// 是否是系统默认基本类型   
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsDefaultType(Type type)
        {
            //初始化
            var e = new Regex(SystemConst._defaultBasicTypeName, RegexOptions.IgnoreCase);
            //校验
            return type.Name.ToLower().Contains("nullable") && type.GenericTypeArguments.Length > 0
                ? e.IsMatch(type.GenericTypeArguments[0].Name)
                : e.IsMatch(type.Name);
        }

        /// <summary>
        /// 编译Expression
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        private object Eval(MemberExpression member)
        {
            UnaryExpression cast = Expression.Convert(member, typeof(object));
            object obj = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
            return obj;
        }

        private string GetOperStr(ExpressionType e_type)
        {
            switch (e_type)
            {
                case ExpressionType.OrElse: return " OR ";
                case ExpressionType.Or: return "|";
                case ExpressionType.AndAlso: return " AND ";
                case ExpressionType.And: return "&";
                case ExpressionType.GreaterThan: return ">";
                case ExpressionType.GreaterThanOrEqual: return ">=";
                case ExpressionType.LessThan: return "<";
                case ExpressionType.LessThanOrEqual: return "<=";
                case ExpressionType.NotEqual: return "<>";
                case ExpressionType.Add: return "+";
                case ExpressionType.Subtract: return "-";
                case ExpressionType.Multiply: return "*";
                case ExpressionType.Divide: return "/";
                case ExpressionType.Modulo: return "%";
                case ExpressionType.Equal: return "=";
            }
            return "";
        }
    }
}
