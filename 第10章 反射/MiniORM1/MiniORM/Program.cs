using System;
using System.Diagnostics;
using System.Reflection;

namespace MiniORM
{
    class Program
    {
        public delegate int ReadProperty();
        static void Main(string[] args)
        {
            Demo1();
            Demo2();
            Console.ReadKey();
        }

        static void Demo1()
        {
            var personTypeObject = typeof(Person);
            var properties = personTypeObject.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var property in properties)
            {
                var name = property.Name;

                //通过GetCustomAttributes获得指定类型的特性值
                var attribute = property.GetCustomAttributes(typeof(DataFieldAttribute), false);
                Console.WriteLine(string.Format("{0} -> 数据库列名{1}, 数据库类型{2}", name,
                    ((DataFieldAttribute)attribute[0]).name,
                    ((DataFieldAttribute)attribute[0]).type));
            }
        }

        static void Demo2()
        {
            var writer = new ORMHelper(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=testDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var type = typeof(Person);

            writer.CreateTable(type);

            var person = new Person
            {
                Name = "张三",
                Age = new Random().Next(0, 100),
                Sex = "male"
            };

            for (int i = 1; i <= 1000; i++)
            {
                writer.Insert(person);
            }

            Console.WriteLine("表中记录数：" + writer.Count(type));

            var sw = new Stopwatch();
            sw.Start();

            //强类型
            var record = (Person)writer.SelectById(type, 1);

            for (int i = 1; i < 1000000; i++)
            {
                var value = record.Age;
            }

            sw.Stop();
            Console.WriteLine("直接获得属性的值：" + sw.ElapsedMilliseconds);

            sw.Restart();

            var propertyInfo = typeof(Person).GetProperty("Age");
            for (int i = 1; i < 1000000; i++)
            {
                var value = propertyInfo.GetValue(record, null);
            }

            sw.Stop();
            Console.WriteLine("使用反射获得属性的值：" + sw.ElapsedMilliseconds);

            sw.Restart();

            //获得getter方法
            var methodInfo = propertyInfo.GetMethod;

            //建立对应的委托
            var del2 = (ReadProperty)Delegate.CreateDelegate(typeof(ReadProperty), record, methodInfo);

            for (int i = 1; i < 1000000; i++)
            {
                var value = del2();
            }

            sw.Stop();
            Console.WriteLine("使用委托的优化：" + sw.ElapsedMilliseconds);
            writer.DeleteTable(type);

            Console.ReadKey();
        }
    }
}
