using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Todo
    {
        public int Id { get; }
        public string Note { get; set; }
        public DateTime DateAssigned { get; set; }
        public bool Recycled { get; set; }

    }
}
