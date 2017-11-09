using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Todo
    {
        public Guid Id;
        public string Note;
        public DateTime DateAssigned;
        public bool Recycled;
    }
}
