using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicoServiceTest.Model
{
    public class User
    {
        public String id{get;set;}
        public String name { get; set; }
        public DateTime bir { get; set; }
        public User(string id, string name, DateTime bir)
        {
            this.id = id;
            this.name = name;
            this.bir = bir;
        }
    }
}
