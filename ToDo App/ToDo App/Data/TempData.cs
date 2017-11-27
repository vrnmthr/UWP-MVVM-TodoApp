using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class TempData
    {
        public static List<Todo> Todos = new List<Todo>()
        {
            new Todo(){Id = Guid.NewGuid(), Note = "First Todo!", DateAssigned = DateTime.Now, Recycled = false, HasReminder = true},
            new Todo(){Id = Guid.NewGuid(), Note = "Second Todo!", DateAssigned = DateTime.Now, Recycled = false},
            new Todo(){Id = Guid.NewGuid(), Note = "Third Todo!", DateAssigned = DateTime.Now, Recycled = false}
        };
    }
}
