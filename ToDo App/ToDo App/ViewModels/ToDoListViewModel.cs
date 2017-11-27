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
using Notifications;

namespace ViewModels
{
    public class ToDoListViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<TodoViewModel> Todos { get; }
        TodoListModel Model;
        NotificationSender NotificationService;

        /// <summary>
        /// Constructor, creating a new TodoList. Fills
        /// Todos and RecycledTodos with TodoViewModels from
        /// the database
        /// </summary>
        public ToDoListViewModel(bool Recycled = false)
        {
            Model = new TodoListModel();
            Todos = new ObservableCollection<TodoViewModel>();
            NotificationService = new NotificationSender(Todos);
            ViewableTodos = Todos;
            _Filter = (x => true);
            _SelectedIndex = -1;
            foreach (var todo in
                Recycled ? Model.GetRecycledTodos() : Model.GetTodos())
            {
                var tdvm = new TodoViewModel(todo);
                tdvm.PropertyChanged += Todo_PropertyChanged;
                this.Todos.Add(tdvm);
            }

            Task.Factory.StartNew(NotificationService.Start);
        }

        public ObservableCollection<TodoViewModel> ViewableTodos { get; private set; }
        Func<TodoViewModel, bool> _Filter;
        public Func<TodoViewModel, bool> Filter
        {
            get => _Filter;
            set
            {
                if (!_Filter.Equals(value))
                {
                    _Filter = value;
                    ViewableTodos = new ObservableCollection<TodoViewModel>();
                    foreach (var t in Todos.Where(_Filter))
                    {
                        ViewableTodos.Add(t);
                    }
                    PropertyChanged(this, new PropertyChangedEventArgs("Filter"));
                    PropertyChanged(this, new PropertyChangedEventArgs("ViewableTodos"));
                }
            }
        }

        int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                Debug.WriteLine("selected index" + value);
                if (_SelectedIndex != value)
                {
                    _SelectedIndex = value;
                    Debug.WriteLine("changed selected index: " + value);
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedTodo"));
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedIndex"));
                }
            }
        }

        public TodoViewModel SelectedTodo
        {
            get
            {
                return (SelectedIndex >= 0) ? ViewableTodos[SelectedIndex] : null;
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
            
            //updates model
            Model.Create(td);

            //updates view model
            tdvm.PropertyChanged += Todo_PropertyChanged;
            Todos.Add(tdvm);
            SelectedIndex = Todos.IndexOf(tdvm);
        }

        /// <summary>
        /// Recycles the todo at selected index
        /// </summary>
        public void Recycle()
        {
            Debug.WriteLine("recycle called");
            if ((bool) !SelectedTodo?.Recycled)
            {
                Debug.WriteLine("recycled!");

                //updates model
                Model.Recycle(SelectedTodo.Id);

                //updates view model
                SelectedTodo.Recycled = true;
                ViewableTodos.RemoveAt(SelectedIndex);
            }
        }

        public void Restore()
        {
            Debug.WriteLine("restore called");
            if ((bool) SelectedTodo?.Recycled)
            {
                Debug.WriteLine("restored!");

                //updates model
                Model.Restore(SelectedTodo.Id);

                //updates view model
                SelectedTodo.Recycled = false;
                Todos.RemoveAt(SelectedIndex);
            }
        }

        void Todo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Model.Update(((TodoViewModel)sender).Todo);
        }
    }
}
