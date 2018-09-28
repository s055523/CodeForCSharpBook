using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateOperator
{
    public enum Operator
    {
        Add, Subtract, Multiply, Divide
    }

    public class Program
    {
        public delegate double CalculateDelegate(double a, double b);
        static void Main(string[] args)
        {
            double a = 1;
            double b = 2;
            Console.WriteLine("Result: {0}", Calculate(a, b, Add));
            Console.WriteLine("Result: {0}", Calculate(a, b, Subtract));
        }

        public static double Calculate(double a, double b, CalculateDelegate cd)
        {
            return cd.Invoke(a, b);
        }

        //static void Main(string[] args)
        //{
        //    double a = 1;
        //    double b = 2;

        //    Console.WriteLine("Result: {0}", Calculate(a, b, Operator.Divide));
        //}

        public static double Calculate(double a, double b, Operator o)
        {
            switch (o)
            {
                case Operator.Add:
                    return Add(a, b);
                case Operator.Subtract:
                    return Subtract(a, b);
                case Operator.Multiply:
                    return Multiply(a, b);
                case Operator.Divide:
                    return Divide(a, b);
                default:
                    return 0;
            }
        }

        public static double Add(double a, double b)
        {
            return a + b;
        }
        public static double Subtract(double a, double b)
        {
            return a - b;
        }
        public static double Multiply(double a, double b)
        {
            return a * b;
        }
        public static double Divide(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException();
            return a / b;
        }
    }

}
