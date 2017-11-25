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
using System.Diagnostics;

namespace ViewModels
{
    public class ToDoListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<TodoViewModel> Todos { get; }
        public ObservableCollection<TodoViewModel> RecycledTodos { get; }
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
            RecycledTodos = new ObservableCollection<TodoViewModel>();
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

        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (_SelectedIndex != value)
                {
                    _SelectedIndex = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedTodo"));
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedIndex"));
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
        public void Add()
        {
            Debug.WriteLine("added new");
            var td = new Todo() { DateAssigned = DateTime.Now, Recycled = false};
            var tdvm = new TodoViewModel(td);
            tdvm.PropertyChanged += Todo_PropertyChanged;
            Todos.Add(tdvm);
            TodoList.Create(td);
            SelectedIndex = Todos.IndexOf(tdvm);
        }

        /// <summary>
        /// Deletes the Todo at SelectedIndex
        /// </summary>
        public void Delete()
        {
            Debug.WriteLine("delete called");
            if ((bool) !SelectedTodo?.Recycled)
            {
                Debug.WriteLine("deleted");
                var toDelete = SelectedTodo;
                Todos.RemoveAt(SelectedIndex);
                RecycledTodos.Add(toDelete);
                TodoList.Recycle(toDelete.Id);
            }
        }

        public void Restore()
        {
            Debug.WriteLine("restore called");
            if ((bool) SelectedTodo?.Recycled)
            {
                Debug.WriteLine("restored");
                var toRestore = SelectedTodo;
                RecycledTodos.RemoveAt(SelectedIndex);
                Todos.Add(toRestore);
                TodoList.Restore(toRestore.Id);
            }
        }

        void Todo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TodoList.Update(((TodoViewModel)sender).Todo);
        }
    }
}
