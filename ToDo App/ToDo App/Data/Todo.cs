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
        
        /// <summary>
        /// Copies all fields over from one Todo to another. Should only
        /// be used for Todos with the same ID - IDs should never be changed.
        /// </summary>
        /// <param name="todo">Todo to copy fields from</param>
        /// <param name="overrideID">Throw errors if ID is changed</param>
        public void CopyFrom(Todo todo, bool overrideID = false)
        {
            if (!overrideID && !todo.Id.Equals(Id))
                throw new ArgumentException("Todos must have same ID");
            if (overrideID) Id = todo.Id;
            Note = todo.Note;
            DateAssigned = todo.DateAssigned;
            Recycled = todo.Recycled;
            HasReminder = todo.HasReminder;
            Reminder = todo.Reminder;
        }

    }
}
