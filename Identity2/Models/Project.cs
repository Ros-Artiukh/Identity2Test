using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity2.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Client { get; set; }
        public List<Developer> Developers { get; set; }
    }
}
