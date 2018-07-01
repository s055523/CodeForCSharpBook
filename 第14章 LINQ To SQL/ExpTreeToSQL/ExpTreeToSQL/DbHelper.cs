using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTreeToSQL
{
    public class DbHelper : IDisposable
    {
        private SqlConnection _conn;

        public bool Connect()
        {
            _conn = new SqlConnection
            {
                ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=testDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            };
            _conn.Open();
            return true;
        }

        public void ExecuteSql(string sql)
        {
            var cmd = new SqlCommand(sql, _conn);
            cmd.ExecuteNonQuery();
        }

        public List<Person> GetPerson(string sql)
        {
            var person = new List<Person>();
            var cmd = new SqlCommand(sql, _conn);
            var sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                person.Add(new Person
                {
                    ID = sdr.GetInt32(0),
                    Name = sdr.GetString(1),
                    Age = sdr.GetInt32(2),
                    Sex = sdr.GetString(3)
                });
            }
            return person;
        }

        public void Dispose()
        {
            _conn.Close();
            _conn = null;
        }
    }

    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
    }

}
