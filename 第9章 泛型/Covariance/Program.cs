using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covariance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var child = new Child();
            Parent p = child;

            List<Child> children = new List<Child>();

            //List<T>不支持协变
            //List<Parent> parents = children;
            //List<Parent> parents = (List<Child>)children;

            List<Parent> parents = children.Select(c => (Parent)p).ToList();

            //IEnumerable<T>支持协变
            IEnumerable<Child> children2 = new List<Child>();

            //将IEnumerable<Child>转换为IEnumerable<Parent>
            IEnumerable<Parent> parents2 = children2;
        }
    }

    public class Parent { }
    public class Child : Parent { }
}
