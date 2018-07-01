using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var entity = new NORTHWNDEntities())
            {
                //正常
                //var ret = entity.Customers.Where(c => c.Address.Contains("s")).OrderBy(c => c.CustomerID);

                //什么都不做
                var ret2 = entity.Customers.Where(c => c.Address.Contains("s")).OrderBy("CustomerID asc");

                foreach (var r in ret2)
                {
                    Console.WriteLine(r.CustomerID + ":" + r.ContactName);
                }
            }

            Console.ReadKey();
        }
    }
}
