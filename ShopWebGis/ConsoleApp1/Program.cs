﻿using ShopWebGisDomainShare.Common;
using ShopWebGisMicroService.DynamicCodeGen;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using XC.RSAUtil;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var age = 11;
            Func<string, int> func = x =>
            {
                return 11;
            };

            Expression<Func<Users, bool>> exp = a => a.Id > gettest() && a.Name == "ABC".ToLower() && a.DateTime > DateTime.Now && a.DateTime < DateTime.Now.AddSeconds(10);
            SqlServerExpressionContext expContext = new SqlServerExpressionContext();
            expContext.Resolve(exp, ResolveExpressType.WhereSingle);
            var value = expContext.Result.GetString();
            var pars = expContext.Parameters;// @id:11


        }

        static int gettest()
        {
            return 11;
        }
        #region Expression
        public abstract class ExpressionVisitor35
        {
            protected ExpressionVisitor35() { }

            protected virtual Expression Visit(Expression exp)
            {
                if (exp == null)
                    return exp;
                switch (exp.NodeType)
                {
                    case ExpressionType.Negate:
                    case ExpressionType.NegateChecked:
                    case ExpressionType.Not:
                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked:
                    case ExpressionType.ArrayLength:
                    case ExpressionType.Quote:
                    case ExpressionType.TypeAs:
                        return this.VisitUnary((UnaryExpression)exp);
                    case ExpressionType.Add:
                    case ExpressionType.AddChecked:
                    case ExpressionType.Subtract:
                    case ExpressionType.SubtractChecked:
                    case ExpressionType.Multiply:
                    case ExpressionType.MultiplyChecked:
                    case ExpressionType.Divide:
                    case ExpressionType.Modulo:
                    case ExpressionType.And:
                    case ExpressionType.AndAlso:
                    case ExpressionType.Or:
                    case ExpressionType.OrElse:
                    case ExpressionType.LessThan:
                    case ExpressionType.LessThanOrEqual:
                    case ExpressionType.GreaterThan:
                    case ExpressionType.GreaterThanOrEqual:
                    case ExpressionType.Equal:
                    case ExpressionType.NotEqual:
                    case ExpressionType.Coalesce:
                    case ExpressionType.ArrayIndex:
                    case ExpressionType.RightShift:
                    case ExpressionType.LeftShift:
                    case ExpressionType.ExclusiveOr:
                        return this.VisitBinary((BinaryExpression)exp);
                    case ExpressionType.TypeIs:
                        return this.VisitTypeIs((TypeBinaryExpression)exp);
                    case ExpressionType.Conditional:
                        return this.VisitConditional((ConditionalExpression)exp);
                    case ExpressionType.Constant:
                        return this.VisitConstant((ConstantExpression)exp);
                    case ExpressionType.Parameter:
                        return this.VisitParameter((ParameterExpression)exp);
                    case ExpressionType.MemberAccess:
                        return this.VisitMemberAccess((MemberExpression)exp);
                    case ExpressionType.Call:
                        return this.VisitMethodCall((MethodCallExpression)exp);
                    case ExpressionType.Lambda:
                        return this.VisitLambda((LambdaExpression)exp);
                    case ExpressionType.New:
                        return this.VisitNew((NewExpression)exp);
                    case ExpressionType.NewArrayInit:
                    case ExpressionType.NewArrayBounds:
                        return this.VisitNewArray((NewArrayExpression)exp);
                    case ExpressionType.Invoke:
                        return this.VisitInvocation((InvocationExpression)exp);
                    case ExpressionType.MemberInit:
                        return this.VisitMemberInit((MemberInitExpression)exp);
                    case ExpressionType.ListInit:
                        return this.VisitListInit((ListInitExpression)exp);
                    default:
                        throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));
                }
            }

            protected virtual MemberBinding VisitBinding(MemberBinding binding)
            {
                switch (binding.BindingType)
                {
                    case MemberBindingType.Assignment:
                        return this.VisitMemberAssignment((MemberAssignment)binding);
                    case MemberBindingType.MemberBinding:
                        return this.VisitMemberMemberBinding((MemberMemberBinding)binding);
                    case MemberBindingType.ListBinding:
                        return this.VisitMemberListBinding((MemberListBinding)binding);
                    default:
                        throw new Exception(string.Format("Unhandled binding type '{0}'", binding.BindingType));
                }
            }

            protected virtual ElementInit VisitElementInitializer(ElementInit initializer)
            {
                ReadOnlyCollection<Expression> arguments = this.VisitExpressionList(initializer.Arguments);
                if (arguments != initializer.Arguments)
                {
                    return Expression.ElementInit(initializer.AddMethod, arguments);
                }
                return initializer;
            }

            protected virtual Expression VisitUnary(UnaryExpression u)
            {
                Expression operand = this.Visit(u.Operand);
                if (operand != u.Operand)
                {
                    return Expression.MakeUnary(u.NodeType, operand, u.Type, u.Method);
                }
                return u;
            }

            protected virtual Expression VisitBinary(BinaryExpression b)
            {
                Expression left = this.Visit(b.Left);
                Expression right = this.Visit(b.Right);
                Expression conversion = this.Visit(b.Conversion);
                if (left != b.Left || right != b.Right || conversion != b.Conversion)
                {
                    if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
                        return Expression.Coalesce(left, right, conversion as LambdaExpression);
                    else
                        return Expression.MakeBinary(b.NodeType, left, right, b.IsLiftedToNull, b.Method);
                }
                return b;
            }

            protected virtual Expression VisitTypeIs(TypeBinaryExpression b)
            {
                Expression expr = this.Visit(b.Expression);
                if (expr != b.Expression)
                {
                    return Expression.TypeIs(expr, b.TypeOperand);
                }
                return b;
            }

            protected virtual Expression VisitConstant(ConstantExpression c)
            {
                return c;
            }

            protected virtual Expression VisitConditional(ConditionalExpression c)
            {
                Expression test = this.Visit(c.Test);
                Expression ifTrue = this.Visit(c.IfTrue);
                Expression ifFalse = this.Visit(c.IfFalse);
                if (test != c.Test || ifTrue != c.IfTrue || ifFalse != c.IfFalse)
                {
                    return Expression.Condition(test, ifTrue, ifFalse);
                }
                return c;
            }

            protected virtual Expression VisitParameter(ParameterExpression p)
            {
                return p;
            }

            protected virtual Expression VisitMemberAccess(MemberExpression m)
            {
                Expression exp = this.Visit(m.Expression);
                if (exp != m.Expression)
                {
                    return Expression.MakeMemberAccess(exp, m.Member);
                }
                return m;
            }

            protected virtual Expression VisitMethodCall(MethodCallExpression m)
            {

                //MethodCallExpression mce = m;
                //if (mce.Method.Name == "Like")
                //    return string.Format("({0} like {1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                //else if (mce.Method.Name == "NotLike")
                //    return string.Format("({0} Not like {1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                //else if (mce.Method.Name == "In")
                //    return string.Format("{0} In ({1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                //else if (mce.Method.Name == "NotIn")
                //    return string.Format("{0} Not In ({1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                //MethodCallExpression mce = m;


                Expression obj = this.Visit(m.Object);
                IEnumerable<Expression> args = this.VisitExpressionList(m.Arguments);
                if (obj != m.Object || args != m.Arguments)
                {
                    return Expression.Call(obj, m.Method, args);
                }
                return m;
            }

            protected virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
            {
                List<Expression> list = null;
                for (int i = 0, n = original.Count; i < n; i++)
                {
                    Expression p = this.Visit(original[i]);
                    if (list != null)
                    {
                        list.Add(p);
                    }
                    else if (p != original[i])
                    {
                        list = new List<Expression>(n);
                        for (int j = 0; j < i; j++)
                        {
                            list.Add(original[j]);
                        }
                        list.Add(p);
                    }
                }
                if (list != null)
                {
                    return list.AsReadOnly();
                }
                return original;
            }

            protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
            {
                Expression e = this.Visit(assignment.Expression);
                if (e != assignment.Expression)
                {
                    return Expression.Bind(assignment.Member, e);
                }
                return assignment;
            }

            protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
            {
                IEnumerable<MemberBinding> bindings = this.VisitBindingList(binding.Bindings);
                if (bindings != binding.Bindings)
                {
                    return Expression.MemberBind(binding.Member, bindings);
                }
                return binding;
            }

            protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding)
            {
                IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(binding.Initializers);
                if (initializers != binding.Initializers)
                {
                    return Expression.ListBind(binding.Member, initializers);
                }
                return binding;
            }

            protected virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
            {
                List<MemberBinding> list = null;
                for (int i = 0, n = original.Count; i < n; i++)
                {
                    MemberBinding b = this.VisitBinding(original[i]);
                    if (list != null)
                    {
                        list.Add(b);
                    }
                    else if (b != original[i])
                    {
                        list = new List<MemberBinding>(n);
                        for (int j = 0; j < i; j++)
                        {
                            list.Add(original[j]);
                        }
                        list.Add(b);
                    }
                }
                if (list != null)
                    return list;
                return original;
            }

            protected virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original)
            {
                List<ElementInit> list = null;
                for (int i = 0, n = original.Count; i < n; i++)
                {
                    ElementInit init = this.VisitElementInitializer(original[i]);
                    if (list != null)
                    {
                        list.Add(init);
                    }
                    else if (init != original[i])
                    {
                        list = new List<ElementInit>(n);
                        for (int j = 0; j < i; j++)
                        {
                            list.Add(original[j]);
                        }
                        list.Add(init);
                    }
                }
                if (list != null)
                    return list;
                return original;
            }

            protected virtual Expression VisitLambda(LambdaExpression lambda)
            {
                Expression body = this.Visit(lambda.Body);
                if (body != lambda.Body)
                {
                    return Expression.Lambda(lambda.Type, body, lambda.Parameters);
                }
                return lambda;
            }

            protected virtual NewExpression VisitNew(NewExpression nex)
            {
                IEnumerable<Expression> args = this.VisitExpressionList(nex.Arguments);
                if (args != nex.Arguments)
                {
                    if (nex.Members != null)
                        return Expression.New(nex.Constructor, args, nex.Members);
                    else
                        return Expression.New(nex.Constructor, args);
                }
                return nex;
            }

            protected virtual Expression VisitMemberInit(MemberInitExpression init)
            {
                NewExpression n = this.VisitNew(init.NewExpression);
                IEnumerable<MemberBinding> bindings = this.VisitBindingList(init.Bindings);
                if (n != init.NewExpression || bindings != init.Bindings)
                {
                    return Expression.MemberInit(n, bindings);
                }
                return init;
            }

            protected virtual Expression VisitListInit(ListInitExpression init)
            {
                NewExpression n = this.VisitNew(init.NewExpression);
                IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(init.Initializers);
                if (n != init.NewExpression || initializers != init.Initializers)
                {
                    return Expression.ListInit(n, initializers);
                }
                return init;
            }

            protected virtual Expression VisitNewArray(NewArrayExpression na)
            {
                IEnumerable<Expression> exprs = this.VisitExpressionList(na.Expressions);
                if (exprs != na.Expressions)
                {
                    if (na.NodeType == ExpressionType.NewArrayInit)
                    {
                        return Expression.NewArrayInit(na.Type.GetElementType(), exprs);
                    }
                    else
                    {
                        return Expression.NewArrayBounds(na.Type.GetElementType(), exprs);
                    }
                }
                return na;
            }

            protected virtual Expression VisitInvocation(InvocationExpression iv)
            {
                IEnumerable<Expression> args = this.VisitExpressionList(iv.Arguments);
                Expression expr = this.Visit(iv.Expression);
                if (args != iv.Arguments || expr != iv.Expression)
                {
                    return Expression.Invoke(expr, args);
                }
                return iv;
            }
        }
        public class PartialEvaluator : ExpressionVisitor35
        {
            private Func<Expression, bool> m_fnCanBeEvaluated;
            private HashSet<Expression> m_candidates;

            public PartialEvaluator()
                : this(CanBeEvaluatedLocally)
            { }

            public PartialEvaluator(Func<Expression, bool> fnCanBeEvaluated)
            {
                this.m_fnCanBeEvaluated = fnCanBeEvaluated;
            }

            public Expression Eval(Expression exp)
            {
                this.m_candidates = new Nominator(this.m_fnCanBeEvaluated).Nominate(exp);

                return this.Visit(exp);
            }

            protected override Expression Visit(Expression exp)
            {
                if (exp == null)
                {
                    return null;
                }

                if (this.m_candidates.Contains(exp))
                {
                    return this.Evaluate(exp);
                }

                return base.Visit(exp);
            }

            private Expression Evaluate(Expression e)
            {
                if (e.NodeType == ExpressionType.Constant)
                {
                    return e;
                }

                LambdaExpression lambda = Expression.Lambda(e);
                Delegate fn = lambda.Compile();
                return Expression.Constant(fn.DynamicInvoke(null), e.Type);
            }

            private static bool CanBeEvaluatedLocally(Expression exp)
            {
                return exp.NodeType != ExpressionType.Parameter;
            }

            #region Nominator

            /// <summary>
            /// Performs bottom-up analysis to determine which nodes can possibly
            /// be part of an evaluated sub-tree.
            /// </summary>
            private class Nominator : ExpressionVisitor35
            {
                private Func<Expression, bool> m_fnCanBeEvaluated;
                private HashSet<Expression> m_candidates;
                private bool m_cannotBeEvaluated;

                internal Nominator(Func<Expression, bool> fnCanBeEvaluated)
                {
                    this.m_fnCanBeEvaluated = fnCanBeEvaluated;
                }

                internal HashSet<Expression> Nominate(Expression expression)
                {
                    this.m_candidates = new HashSet<Expression>();
                    this.Visit(expression);
                    return this.m_candidates;
                }

                protected override Expression Visit(Expression expression)
                {
                    if (expression != null)
                    {
                        bool saveCannotBeEvaluated = this.m_cannotBeEvaluated;
                        this.m_cannotBeEvaluated = false;

                        base.Visit(expression);

                        if (!this.m_cannotBeEvaluated)
                        {
                            if (this.m_fnCanBeEvaluated(expression))
                            {
                                this.m_candidates.Add(expression);
                            }
                            else
                            {
                                this.m_cannotBeEvaluated = true;
                            }
                        }

                        this.m_cannotBeEvaluated |= saveCannotBeEvaluated;
                    }

                    return expression;
                }
            }

            #endregion
        }
        internal class ConditionBuilder : ExpressionVisitor35
        {
            /// <summary>
            /// 数据库类型
            /// </summary>
            private string m_dataBaseType = "";
            /// <summary>
            /// 字段是否加引号
            /// </summary>
            private bool m_ifWithQuotationMarks = false;

            private List<object> m_arguments;
            private Stack<string> m_conditionParts;

            public string Condition { get; private set; }

            public object[] Arguments { get; private set; }




            #region 加双引号
            /// <summary>
            /// 加双引号
            /// </summary>
            /// <param name="str">字串</param>
            /// <returns></returns>
            public static string AddQuotationMarks(string str)
            {
                if (str != null)
                {
                    return "\"" + str.Trim() + "\"";
                }
                else
                {
                    return str;
                }
            }

            #endregion

            #region 设置是否加双引号
            public void SetIfWithQuotationMarks(bool ifwith)
            {
                m_ifWithQuotationMarks = ifwith;
            }
            #endregion

            #region 设置是数据库类型
            public void SetDataBaseType(string databaseType)
            {
                if (!string.IsNullOrEmpty(databaseType))
                {
                    m_dataBaseType = databaseType;
                }

            }
            #endregion
            public void Build(Expression expression)
            {
                PartialEvaluator evaluator = new PartialEvaluator();
                Expression evaluatedExpression = evaluator.Eval(expression);

                this.m_arguments = new List<object>();
                this.m_conditionParts = new Stack<string>();

                this.Visit(evaluatedExpression);

                this.Arguments = this.m_arguments.ToArray();
                this.Condition = this.m_conditionParts.Count > 0 ? this.m_conditionParts.Pop() : null;
            }

            protected override Expression VisitBinary(BinaryExpression b)
            {
                if (b == null) return b;

                string opr;
                switch (b.NodeType)
                {
                    case ExpressionType.Equal:
                        opr = "=";
                        break;
                    case ExpressionType.NotEqual:
                        opr = "<>";
                        break;
                    case ExpressionType.GreaterThan:
                        opr = ">";
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        opr = ">=";
                        break;
                    case ExpressionType.LessThan:
                        opr = "<";
                        break;
                    case ExpressionType.LessThanOrEqual:
                        opr = "<=";
                        break;
                    case ExpressionType.AndAlso:
                        opr = "AND";
                        break;
                    case ExpressionType.OrElse:
                        opr = "OR";
                        break;
                    case ExpressionType.Add:
                        opr = "+";
                        break;
                    case ExpressionType.Subtract:
                        opr = "-";
                        break;
                    case ExpressionType.Multiply:
                        opr = "*";
                        break;
                    case ExpressionType.Divide:
                        opr = "/";
                        break;
                    default:
                        throw new NotSupportedException(b.NodeType + "is not supported.");
                }

                this.Visit(b.Left);
                this.Visit(b.Right);

                string right = this.m_conditionParts.Pop();
                string left = this.m_conditionParts.Pop();

                string condition = String.Format("({0} {1} {2})", left, opr, right);
                this.m_conditionParts.Push(condition);

                return b;
            }

            protected override Expression VisitConstant(ConstantExpression c)
            {
                if (c == null) return c;

                this.m_arguments.Add(c.Value);
                this.m_conditionParts.Push(String.Format("{{{0}}}", this.m_arguments.Count - 1));

                return c;
            }

            protected override Expression VisitMemberAccess(MemberExpression m)
            {
                if (m == null) return m;

                PropertyInfo propertyInfo = m.Member as PropertyInfo;
                if (propertyInfo == null) return m;

                //   this.m_conditionParts.Push(String.Format("(Where.{0})", propertyInfo.Name));
                //是否添加引号
                if (m_ifWithQuotationMarks)
                {
                    this.m_conditionParts.Push(String.Format(" {0} ", AddQuotationMarks(propertyInfo.Name)));
                }
                else
                {
                    // this.m_conditionParts.Push(String.Format("[{0}]", propertyInfo.Name));
                    this.m_conditionParts.Push(String.Format(" {0} ", propertyInfo.Name));
                }

                return m;
            }

            #region 其他
            static string BinarExpressionProvider(Expression left, Expression right, ExpressionType type)
            {
                string sb = "(";
                //先处理左边
                sb += ExpressionRouter(left);

                sb += ExpressionTypeCast(type);

                //再处理右边
                string tmpStr = ExpressionRouter(right);

                if (tmpStr == "null")
                {
                    if (sb.EndsWith(" ="))
                        sb = sb.Substring(0, sb.Length - 1) + " is null";
                    else if (sb.EndsWith("<>"))
                        sb = sb.Substring(0, sb.Length - 1) + " is not null";
                }
                else
                    sb += tmpStr;
                return sb += ")";
            }

            static string ExpressionRouter(Expression exp)
            {
                string sb = string.Empty;
                if (exp is BinaryExpression)
                {

                    BinaryExpression be = ((BinaryExpression)exp);
                    return BinarExpressionProvider(be.Left, be.Right, be.NodeType);
                }
                else if (exp is MemberExpression)
                {

                    MemberExpression me = ((MemberExpression)exp);
                    return me.Member.Name;
                }
                else if (exp is NewArrayExpression)
                {
                    NewArrayExpression ae = ((NewArrayExpression)exp);
                    StringBuilder tmpstr = new StringBuilder();
                    foreach (Expression ex in ae.Expressions)
                    {
                        tmpstr.Append(ExpressionRouter(ex));
                        tmpstr.Append(",");
                    }
                    return tmpstr.ToString(0, tmpstr.Length - 1);
                }
                else if (exp is MethodCallExpression)
                {
                    MethodCallExpression mce = (MethodCallExpression)exp;
                    if (mce.Method.Name == "Like")
                        return string.Format("({0} like {1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                    else if (mce.Method.Name == "NotLike")
                        return string.Format("({0} Not like {1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                    else if (mce.Method.Name == "In")
                        return string.Format("{0} In ({1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                    else if (mce.Method.Name == "NotIn")
                        return string.Format("{0} Not In ({1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                    else if (mce.Method.Name == "StartWith")
                        return string.Format("{0} like '{1}%'", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                }
                else if (exp is ConstantExpression)
                {
                    ConstantExpression ce = ((ConstantExpression)exp);
                    if (ce.Value == null)
                        return "null";
                    else if (ce.Value is ValueType)
                        return ce.Value.ToString();
                    else if (ce.Value is string || ce.Value is DateTime || ce.Value is char)
                    {

                        return string.Format("'{0}'", ce.Value.ToString());
                    }
                }
                else if (exp is UnaryExpression)
                {
                    UnaryExpression ue = ((UnaryExpression)exp);
                    return ExpressionRouter(ue.Operand);
                }
                return null;
            }

            static string ExpressionTypeCast(ExpressionType type)
            {
                switch (type)
                {
                    case ExpressionType.And:
                    case ExpressionType.AndAlso:
                        return " AND ";
                    case ExpressionType.Equal:
                        return " =";
                    case ExpressionType.GreaterThan:
                        return " >";
                    case ExpressionType.GreaterThanOrEqual:
                        return ">=";
                    case ExpressionType.LessThan:
                        return "<";
                    case ExpressionType.LessThanOrEqual:
                        return "<=";
                    case ExpressionType.NotEqual:
                        return "<>";
                    case ExpressionType.Or:
                    case ExpressionType.OrElse:
                        return " Or ";
                    case ExpressionType.Add:
                    case ExpressionType.AddChecked:
                        return "+";
                    case ExpressionType.Subtract:
                    case ExpressionType.SubtractChecked:
                        return "-";
                    case ExpressionType.Divide:
                        return "/";
                    case ExpressionType.Multiply:
                    case ExpressionType.MultiplyChecked:
                        return "*";
                    default:
                        return null;
                }
            }
            #endregion


            /// <summary>
            /// ConditionBuilder 并不支持生成Like操作，如 字符串的 StartsWith，Contains，EndsWith 并不能生成这样的SQL： Like ‘xxx%’, Like ‘%xxx%’ , Like ‘%xxx’ . 只要override VisitMethodCall 这个方法即可实现上述功能。
            /// </summary>
            /// <param name="m"></param>
            /// <returns></returns>
            protected override Expression VisitMethodCall(MethodCallExpression m)
            {
                string connectorWords = GetLikeConnectorWords(m_dataBaseType); //获取like链接符

                if (m == null) return m;
                string format;
                switch (m.Method.Name)
                {
                    case "StartsWith":
                        format = "({0} LIKE ''" + connectorWords + "{1}" + connectorWords + "'%')";
                        break;
                    case "Contains":
                        format = "({0} LIKE '%'" + connectorWords + "{1}" + connectorWords + "'%')";
                        break;
                    case "EndsWith":
                        format = "({0} LIKE '%'" + connectorWords + "{1}" + connectorWords + "'')";
                        break;

                    case "Equals":
                        // not in 或者  in 或 not like
                        format = "({0} {1} )";
                        break;

                    default:
                        throw new NotSupportedException(m.NodeType + " is not supported!");
                }
                this.Visit(m.Object);
                this.Visit(m.Arguments[0]);
                string right = this.m_conditionParts.Pop();
                string left = this.m_conditionParts.Pop();

                this.m_conditionParts.Push(String.Format(format, left, right));
                return m;
            }

            /// <summary>
            /// 获得like语句链接符
            /// </summary>
            /// <param name="databaseType"></param>
            /// <returns></returns>
            public static string GetLikeConnectorWords(string databaseType)
            {
                string result = "+";
                switch (databaseType.ToLower())
                {

                    case DataBaseType.PostGreSql:
                    case DataBaseType.Oracle:
                    case DataBaseType.MySql:
                    case DataBaseType.Sqlite:
                        result = "||";
                        break;
                }

                return result;
            }

        }

        public class Command
        {
            public virtual void Execute()
            {
                Console.WriteLine("Command executing...");
                Console.WriteLine("Hello Kitty!");
                Console.WriteLine("Command executed.");
            }
        }


        #endregion

        #region 数据库类型
        /// <summary>
        ///类据库类型
        /// </summary>
        public class DataBaseType
        {

            /// <summary>
            /// sql server
            /// </summary>
            public const string SqlServer = "sqlserver";

            /// <summary>
            ///  OleDb
            /// </summary>
            public const string Access = "access";
            /// <summary>
            ///MySql
            /// </summary>
            public const string MySql = "mysql";
            /// <summary>
            /// Oracle
            /// </summary>
            public const string Oracle = "oracle";

            /// <summary>
            /// Postgresql
            /// </summary>
            public const string PostGreSql = "postgresql";

            /// <summary>
            /// Sqlite
            /// </summary>
            public const string Sqlite = "sqlite";
        }
        #endregion

        public class SqlSugor
        {
            #region Expression 转成 where
            /// <summary>
            /// Expression 转成 Where String
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="predicate"></param>
            /// <param name="databaseType">数据类型（用于字段是否加引号）</param>
            /// <returns></returns>
            public static string GetWhereByLambda<T>(Expression<Func<T, bool>> predicate, string databaseType)
            {
                bool withQuotationMarks = GetWithQuotationMarks(databaseType);

                ConditionBuilder conditionBuilder = new ConditionBuilder();
                conditionBuilder.SetIfWithQuotationMarks(withQuotationMarks); //字段是否加引号（PostGreSql,Oracle）
                conditionBuilder.SetDataBaseType(databaseType);
                conditionBuilder.Build(predicate);

                for (int i = 0; i < conditionBuilder.Arguments.Length; i++)
                {
                    object ce = conditionBuilder.Arguments[i];
                    if (ce == null)
                    {
                        conditionBuilder.Arguments[i] = DBNull.Value;
                    }
                    else if (ce is string || ce is char)
                    {
                        if (ce.ToString().ToLower().Trim().IndexOf(@"in(") == 0 ||
                            ce.ToString().ToLower().Trim().IndexOf(@"not in(") == 0 ||
                             ce.ToString().ToLower().Trim().IndexOf(@" like '") == 0 ||
                            ce.ToString().ToLower().Trim().IndexOf(@"not like") == 0)
                        {
                            conditionBuilder.Arguments[i] = string.Format(" {0} ", ce.ToString());
                        }
                        else
                        {


                            //****************************************
                            conditionBuilder.Arguments[i] = string.Format("'{0}'", ce.ToString());
                        }
                    }
                    else if (ce is DateTime)
                    {
                        conditionBuilder.Arguments[i] = string.Format("'{0}'", ce.ToString());
                    }
                    else if (ce is int || ce is long || ce is short || ce is decimal || ce is double || ce is float || ce is bool || ce is byte || ce is sbyte)
                    {
                        conditionBuilder.Arguments[i] = ce.ToString();
                    }
                    else if (ce is ValueType)
                    {
                        conditionBuilder.Arguments[i] = ce.ToString();
                    }
                    else
                    {

                        conditionBuilder.Arguments[i] = string.Format("'{0}'", ce.ToString());
                    }

                }
                string strWhere = string.Format(conditionBuilder.Condition, conditionBuilder.Arguments);
                return strWhere;
            }

            /// <summary>
            /// 获取是否字段加双引号
            /// </summary>
            /// <param name="databaseType"></param>
            /// <returns></returns>
            public static bool GetWithQuotationMarks(string databaseType)
            {
                bool result = false;
                switch (databaseType.ToLower())
                {

                    case DataBaseType.PostGreSql:
                    case DataBaseType.Oracle:

                        result = true;
                        break;
                }

                return result;


            }


            #endregion
        }

        public class Users
        {
            public string Name { get; set; }

            public int Id { get; set; }

            public DateTime DateTime { get; set; }
        }
    }


}
