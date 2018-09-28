using System;

namespace MiniORM
{
    [KeyAttribute("Person", "ID", "")]
    public class Person
    {
        [DataFieldAttribute("ID", "int")]
        private int ID { get; set; }

        [DataFieldAttribute("name", "nvarchar(50)")]
        public string Name { get; set; }        

        [DataFieldAttribute("age", "int")]
        public int Age { get; set; }

        [DataFieldAttribute("sex", "nvarchar(50)")]
        public string Sex { get; set; }
    }

    public class DataFieldAttribute : Attribute
    {
        public string name { get; }
        public string type { get; }
        public DataFieldAttribute(string n, string t)
        {
            name = n;
            type = t;
        }        
    }

    public class KeyAttribute : Attribute
    {
        public string tableName { get; }
        public string PKName { get; }
        public string FKName { get; }
        public KeyAttribute(string t, string p, string f)
        {
            tableName = t;
            PKName = p;
            FKName = f;
        }
    }
}
