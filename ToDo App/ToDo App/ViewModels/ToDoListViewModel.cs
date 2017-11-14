using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace ViewModels
{
    class ToDoListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<TodoViewModel> Todos;
        ObservableCollection<TodoViewModel> RecycledTodos;
        TodoListModel TodoList;
        int _SelectedIndex;

        /// <summary>
        /// Constructor, creating a new TodoList. Fills
        /// Todos and RecycledTodos with TodoViewModels from
        /// the database
        /// </summary>
        public ToDoListViewModel()
        {
            TodoList = new TodoListModel();
            Todos = new ObservableCollection<TodoViewModel>();
            _SelectedIndex = -1;
            foreach (var todo in TodoList.GetTodos())
            {
                var tdvm = new TodoViewModel(todo);
                this.Todos.Add(tdvm);
            }
            foreach (var todo in TodoList.GetRecycledTodos())
            {
                var tdvm = new TodoViewModel(todo);
                this.RecycledTodos.Add(tdvm);
            }
        }
        
        public ObservableCollection<TodoViewModel> GetTodos()
        {
            return Todos;
        }

        public ObservableCollection<TodoViewModel> GetRecycledTodos()
        {
            return RecycledTodos;
        }

        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (SelectedIndex != value)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedToDo"));
                }
            }
        }

        public TodoViewModel SelectedTodo
        {
            get
            {
                return (SelectedIndex >= 0) ? Todos[SelectedIndex] : null;
            }
        }

        /// <summary>
        /// Adds the given Todo to the appropriate ObservableCollection
        /// and updates the model behind to add it to the data.
        /// </summary>
        /// <param name="Todo">Todo to add</param>
        public void Add(Todo Todo)
        {
            var tdvm = new TodoViewModel(Todo);
            tdvm.PropertyChanged += Todo_PropertyChanged;
            var AddIn = Todos;
            if (tdvm.GetRecycled()) AddIn = RecycledTodos;
            AddIn.Add(tdvm);
            TodoList.Create(Todo);
            SelectedIndex = AddIn.IndexOf(tdvm);
        }

        /// <summary>
        /// Deletes the Todo at SelectedIndex
        /// </summary>
        public void Delete()
        {
            if ((bool) SelectedTodo?.GetRecycled())
            {
                var toDelete = SelectedTodo;
                Todos.RemoveAt(SelectedIndex);
                RecycledTodos.Add(toDelete);
                TodoList.Recycle(toDelete.GetId());
            }
        }

        private void Todo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }


    }
}
