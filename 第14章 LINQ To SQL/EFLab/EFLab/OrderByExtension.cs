using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EFLab
{
    public static class OrderByExtension
    {
        //只处理两种排序
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string orderStr)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            if (orderStr == null)
            {
                throw new ArgumentNullException("orderStr");
            }

            string[] orderArray = orderStr.Split(' ');
            if (orderArray.Length % 2 != 0)
            {
                throw new ArgumentException("传入的排序字符串格式不对");
            }

            Console.WriteLine("开始解析字符串");
            string text = "OrderBy";
            string text2 = "OrderByDescending";

            //拿到初始表达式树
            Expression expression = query.Expression;

            for (int i = 0; i < orderArray.Length; i += 2)
            {
                //列名
                string columnName = orderArray[i];

                //排序关键字
                var nextOrderStr = orderArray[i + 1].ToLower();

                if (!new[] { "asc", "desc" }.Contains(nextOrderStr))
                {
                    throw new ArgumentException("排序关键词错误，应为asc或desc，但实际为" + nextOrderStr);
                }

                string order = nextOrderStr == "asc" ? text : text2;

                //集合的类型
                Type collectionType = query.ElementType;

                //通过反射获得欲排序的属性信息
                PropertyInfo propInfo = collectionType.GetProperty(columnName);

                //建立变量表达式，变量类型为集合的类型
                var parameter = Expression.Parameter(collectionType, "m");

                //获得m.columnName表达式，变量类型为反射获得的类型
                MemberExpression memberexp = Expression.MakeMemberAccess(parameter, propInfo);

                //(paramter) => (memberexp)
                LambdaExpression lambdaExp = Expression.Lambda(memberexp, parameter);

                var quoteExp = Expression.Quote(lambdaExp);

                expression = Expression.Call(typeof(Queryable),
                    order,
                    new Type[]
                    {
                        collectionType, propInfo.PropertyType
                    },
                    //可选的参数
                    expression, quoteExp
                );

                text = "ThenBy";
                text2 = "ThenByDescending";
            }

            //返回一个加入了正确排序调用方法的新表达式树
            return query.Provider.CreateQuery<T>(expression);

        }
    }
}
