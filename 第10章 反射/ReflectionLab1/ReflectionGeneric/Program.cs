using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionGeneric
{
    class Program
    {
        static void Main()
        {
            var shapeList = new List<Shapes> { new Rectangle(), new Circle() };
            var method = typeof(Processor).GetMethod("Process");

            foreach (var shapes in shapeList)
            {
                //对每个shapes，获得它的类型，然后构造相应的泛型方法
                var generateRef = method.MakeGenericMethod(shapes.GetType());

                //调用方法
                generateRef.Invoke(new Processor(), null);
            }

            Console.ReadKey();
        }
    }

    class Processor
    {
        public void Process<T>() where T : Shapes, new()
        {
            var t = typeof(T);
            t.GetMethod("Print").Invoke(new T(), null);
        }
    }

    class Shapes
    {
        public void Print()
        {
            Console.WriteLine("我是形状");
        }
    }

    class Rectangle : Shapes
    {
        public void Print()
        {
            Console.WriteLine("我是长方形");
        }
    }

    class Circle : Shapes
    {
        public void Print()
        {
            Console.WriteLine("我是圆形");
        }
    }
}
