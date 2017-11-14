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
        Todo todo;

        /// <summary>
        /// Constructor that creates a TodoViewModel out of a Todo
        /// </summary>
        /// <param name="todo">Todo wrapped into the ViewModel</param>
        public TodoViewModel(Todo todo)
        {
            this.todo = todo;
        }

        public int GetId()
        {
            return todo.Id;
        }

        public DateTime GetDateAssigned()
        {
            return todo.DateAssigned;
        }

        public void SetDateAssigned(DateTime Date)
        {
            todo.DateAssigned = Date;
            PropertyChanged(this, new PropertyChangedEventArgs("DateAssigned"));
        }

        public string GetNote()
        {
            return todo.Note;
        }

        public void SetNote(string Note)
        {
            todo.Note = Note;
            PropertyChanged(this, new PropertyChangedEventArgs("Note"));
        }

        public bool GetRecycled()
        {
            return todo.Recycled;
        }

        public void SetRecycled(bool Recycled)
        {
            todo.Recycled = Recycled;
            PropertyChanged(this, new PropertyChangedEventArgs("Recycled"));
        }
    }
}