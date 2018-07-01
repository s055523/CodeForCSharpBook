using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpTreeToSQL
{
    public class MyQueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            Console.WriteLine("建立一个查询，但不执行");
            return new MyIQueryable<TElement>(expression, this);
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var sql = string.Empty;
            sql = ExpressionTreeToSql.VisitExpression(new Person(), expression, ref sql);
            Console.WriteLine(sql);

            Console.WriteLine(sql);

            using (var db = new DbHelper())
            {
                db.Connect();
                dynamic ret = db.GetPerson(sql);
                return (TResult)ret;
            }
        }
    }

}
