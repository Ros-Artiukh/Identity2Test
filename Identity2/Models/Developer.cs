using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity2.Models
{
    public class Developer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
    }
}
