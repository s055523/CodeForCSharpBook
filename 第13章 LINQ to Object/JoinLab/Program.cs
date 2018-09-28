using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var customerList = new List<Customers>
            {
                new Customers {CustomerId = 1, Name = "张三", Address = "白宫", PhoneNumber = 12345678},
                new Customers {CustomerId = 2, Name = "李四", Address = "克里姆林宫", PhoneNumber = 23456781},
                new Customers {CustomerId = 3, Name = "王五", Address = "红场", PhoneNumber = 34567812}
            };

            var orderList = new List<Orders>
            {
                new Orders {CustomerId = 3, Id = 1, Number = 2572},
                new Orders {CustomerId = 3, Id = 2, Number = 7375},
                new Orders {CustomerId = 1, Id = 3, Number = 7520},
                new Orders {CustomerId = 1, Id = 4, Number = 1054},
                new Orders {CustomerId = 5, Id = 5, Number = 1257}
            };


        }
    }
    public class Customers
    {
        public int CustomerId;
        public string Name;
        public string Address;
        public int PhoneNumber;
    }

    public class Orders
    {
        public int Id;
        public int Number;
        public int CustomerId;
    }

}
