using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace ViewModels
{
    public class TodoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Todo Todo { get; set; }

        /// <summary>
        /// Constructor that creates a TodoViewModel out of a Todo
        /// </summary>
        /// <param name="todo">Todo wrapped into the ViewModel</param>
        public TodoViewModel(Todo todo)
        {
            this.Todo = todo;
        }

        public Guid Id
        {
            get => Todo.Id;
        }

        public DateTime DateAssigned
        {
            get => Todo.DateAssigned;
            set
            {
                Todo.DateAssigned = value;
                NotifyPropertyChanged("DateAssigned");
            }
        }

        public string Note
        {
            get => Todo.Note;
            set
            {
                Todo.Note = value;
                NotifyPropertyChanged("Note");
            }
        }

        public bool Recycled
        {
            get => Todo.Recycled;
            set
            {
                if (!Todo.Reminder.Equals(value))
                {
                    Todo.Recycled = value;
                    NotifyPropertyChanged("Recycled");
                }
            }
        }

        public bool HasReminder
        {
            get => Todo.HasReminder;
            set
            {
                if (!Todo.HasReminder.Equals(value))
                {
                    Todo.HasReminder = value;
                    NotifyPropertyChanged("HasReminder");
                }
            }
        }

        public DateTime Reminder
        {
            get => Todo.Reminder;
            set
            {
                if (!Todo.Reminder.Equals(value))
                {
                    Todo.Reminder = value;
                    NotifyPropertyChanged("Reminder");
                }
            }
        }

        public TimeSpan ReminderProxy
        {
            get
            {
                //Extract the timespan from the original datetime
                return Reminder - Reminder.Date;
            }
            set
            {
                //See if the timespan is different the the current value
                if (ReminderProxy != value)
                {
                    //If it is, set the original date time to the
                    //original date, plus the new timespan value
                    Reminder = Reminder.Date.Add(value);
                    NotifyPropertyChanged("Reminder");
                }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}