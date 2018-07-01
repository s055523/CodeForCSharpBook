using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpTreeToSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DbHelper())
            {
                db.Connect();
                db.ExecuteSql("DELETE FROM Person");

                //主键自增
                db.ExecuteSql("INSERT INTO Person VALUES ('Frank',30,'M')");
                db.ExecuteSql("INSERT INTO Person VALUES ('Mary',99,'F')");

                //使用ADO.NET获得数据
                var person = db.GetPerson("select * from Person where Name = 'Frank'");

                foreach (var person1 in person)
                {
                    Console.WriteLine("结果的年龄是" + person1.Age);
                }
            }

            //自己的逻辑
            var myqueryable = new MyIQueryable<Person>();

            //这里传入什么都没区别，反正里面是硬编码的
            var ret = myqueryable.Where(m => m.Name == "Frank");

            foreach (var person1 in ret)
            {
                Console.WriteLine("结果的年龄是" + person1.Age);
            }

            var seq = Enumerable.Range(0, 10);
            var a = seq.First();
            var b = seq.Select(s => s / 2 == 0);

            Console.ReadKey();
        }
    }

    public class MyIQueryable<T> : IQueryable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            Console.WriteLine("开始取值");
            var result = Provider.Execute<List<T>>(Expression);
            foreach (var item in result)
            {
                Console.WriteLine("发现一条合格的值");
                yield return item;
            }           
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public Expression Expression { get; private set; }
        public Type ElementType { get; private set; }
        public IQueryProvider Provider { get; private set; }

        //不知道该写什么的构造函数
        public MyIQueryable(Expression exp, MyQueryProvider provider)
        {
            Expression = exp;
            ElementType = typeof(T);
            Provider = provider;
        }

        public MyIQueryable() : this(null, new MyQueryProvider())
        {
            Expression = Expression.Constant(this);
        }
    }

}
