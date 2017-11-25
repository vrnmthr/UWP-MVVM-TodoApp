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
                Todo.Recycled = value;
                NotifyPropertyChanged("Recycled");
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                Debug.WriteLine("PropertyChanged");
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                Debug.WriteLine("PropertyChanged = null");
            }
        }

    }
}