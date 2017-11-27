using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;


namespace Data
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public DateTime DateAssigned { get; set; }
        public bool Recycled { get; set; }
        public bool HasReminder { get; set; }
        public DateTime Reminder { get; set; }

        public Todo()
        {
            this.Id = Guid.NewGuid();
        }

    }
}
