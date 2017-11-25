using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        public int Id
        {
            get => Todo.Id;
            set => Todo.Id = value;
        }

        public DateTime DateAssigned
        {
            get => Todo.DateAssigned;
            set
            {
                Todo.DateAssigned = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DateAssigned"));
            }
        }

        public string Note
        {
            get => Todo.Note;
            set
            {
                Todo.Note = Note;
                PropertyChanged(this, new PropertyChangedEventArgs("Note"));
            }
        }

        public bool Recycled
        {
            get => Todo.Recycled;
            set
            {
                Todo.Recycled = Recycled;
                PropertyChanged(this, new PropertyChangedEventArgs("Recycled"));
            }
        }
    }
}