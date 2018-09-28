using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpTree2
{
    class Program
    {
        static void Main(string[] args)
        {
            var book1 = new Book("深入理解C#", "Jon Skeet", 500);
            var book2 = new Book("CLR via C#", "Jeffery Richter", 800);
            var booklist = new List<Book> { book1, book2 };

            SetAllPropertyExp(booklist, "author", "you");

            foreach (var book in booklist)
            {
                Console.WriteLine(book.author);
            }

            Demo();
            Console.ReadKey();

        }

        static void Demo()
        {
            var booklist = new List<Book>();
            for (int i = 0; i < 1000000; i++)
            {
                booklist.Add(new Book("张三的书", "张三", i));
            }

            var sw = new Stopwatch();
            sw.Start();
            SetAllPropertyExp(booklist, "pageCount", 200);

            sw.Stop();
            Console.WriteLine("使用表达式树：" + sw.ElapsedMilliseconds);

            sw.Restart();
            SetAllProperty(booklist, "pageCount", 200);
            sw.Stop();
            Console.WriteLine("使用反射：" + sw.ElapsedMilliseconds);

        }

        public static void SetAllPropertyExp<T, K>(List<T> data, string propertyName, K propertyValue)
        {
            if (!data.Any()) return;
            var propertyInfo = typeof(T).GetProperty(propertyName);

            if (propertyInfo == null) throw new Exception("属性不存在");
            if (!propertyInfo.CanWrite) throw new Exception("属性不支持写操作");

            //propertyValue变量（我们已经知道了属性的信息，所以可以使用属性的类型作为变量类型）
            var propValExpr = Expression.Parameter(propertyInfo.PropertyType, "propertyValue");

            //d变量
            var invokeObjExpr = Expression.Parameter(typeof(T), "d");
            var methodInfo = propertyInfo.GetSetMethod();

            //表达式主体
            var setMethodExpr = Expression.Call(invokeObjExpr, methodInfo, propValExpr);

            //强类型的委托
            var lambdaExp = Expression.Lambda<Action<T, K>>(setMethodExpr, invokeObjExpr, propValExpr);
            var del = lambdaExp.Compile();

            foreach (var d in data)
            {
                del(d, propertyValue);
            }
        }

        public static void SetAllProperty<T>(List<T> data, string propertyName, object propertyValue)
        {
            if (!data.Any()) return;
            var propertyInfo = typeof(T).GetProperty(propertyName);

            if (propertyInfo == null) throw new Exception("属性不存在");
            if (!propertyInfo.CanWrite) throw new Exception("属性不支持写操作");

            foreach (var d in data)
            {
                propertyInfo.SetValue(d, propertyValue, null);
            }
        }

    }
    public class Book
    {
        public string name { get; set; }
        public string author { get; set; }
        public int pageCount { get; set; }

        public Book(string n, string a, int p)
        {
            name = n;
            author = a;
            pageCount = p;
        }
    }

}
