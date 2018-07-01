using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpTreeToSQL
{
    class ExpressionTreeToSql
    {
        public static string VisitExpression<T>(T enumerable, Expression expression, ref string sql)
        {
            //select头
            if (sql == string.Empty)
                sql = GenerateSelectHeader(enumerable);

            //递归解析Where(m => m.Name == "Frank")的代码
            //分情况为结果sql增加字符串
            switch (expression.NodeType)
            {
                case ExpressionType.Call:
                    MethodCallExpression method = expression as MethodCallExpression;
                    if (method != null)
                    {
                        //获得where这个字符串，也就是方法名
                        sql += method.Method.Name;

                        //继续解析，传入第二个方法参数{m => (m.Name == "Frank")}
                        VisitExpression(enumerable, method.Arguments[1], ref sql);
                    }
                    break;
                case ExpressionType.Quote:
                    UnaryExpression expUnary = expression as UnaryExpression;
                    if (expUnary != null)
                    {
                        VisitExpression(enumerable, expUnary.Operand, ref sql);
                    }
                    break;
                case ExpressionType.Lambda:
                    LambdaExpression expLambda = expression as LambdaExpression;
                    if (expLambda != null)
                    {
                        VisitExpression(enumerable, expLambda.Body, ref sql);
                    }
                    break;
                case ExpressionType.Equal:
                    BinaryExpression expBinary = expression as BinaryExpression;
                    if (expBinary != null)
                    {
                        var left = expBinary.Left;
                        var right = expBinary.Right;
                        sql += " " + left.ToString().Split('.')[1] + " = '" + right.ToString().Replace("\"", "") + "'";
                    }
                    break;
                default:
                    throw new NotSupportedException(string.Format("不支持这种表达式： {0}", expression.NodeType));
            }
            return sql;
        }

        //半硬编码的select
        public static string GenerateSelectHeader<T>(T type)
        {
            return string.Format("select * from {0} ", typeof(T).Name);
        }
    }
}
