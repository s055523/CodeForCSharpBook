using System;

namespace DelegateCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            var r1 = new Rectangle(1, 6);
            var r2 = new Rectangle(2, 4);
            Compare(r1, r2, CompareRectangle);

            var c1 = new Circle(3);
            var c2 = new Circle(2);
            Compare(c1, c2, CompareCircle);

            Console.ReadKey();
        }

        public static void Compare<T>(T o1, T o2, Func<T, T, int> rule)
        {
            var ret = rule.Invoke(o1, o2);
            if (ret == 1) Console.WriteLine("第一个大");
            if (ret == -1) Console.WriteLine("第二个大");
            if (ret == 0) Console.WriteLine("一样大");
        }

        public static int CompareRectangle(Rectangle r1, Rectangle r2)
        {
            double r1Area = r1.Length * r1.Width;
            double r2Area = r2.Length * r2.Width;
            if (r1Area > r2Area) return 1;
            if (r1Area < r2Area) return -1;
            return 0;
        }

        public static int CompareCircle(Circle c1, Circle c2)
        {
            if (c1.Radius > c2.Radius) return 1;
            if (c1.Radius < c2.Radius) return -1;
            return 0;
        }

    }

    public struct Rectangle
    {
        public double Length { get; set; }
        public double Width { get; set; }

        public Rectangle(double l, double w) : this()
        {
            Length = l;
            Width = w;
        }
    }

    public struct Circle
    {
        public double Radius { get; set; }

        public Circle(double r) : this()
        {
            Radius = r;
        }
    }

}
