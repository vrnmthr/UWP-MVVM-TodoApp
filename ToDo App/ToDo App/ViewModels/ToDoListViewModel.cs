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
    /// <summary>
    /// ViewModel for TodoLists that wraps a TodoListModel and communicates
    /// with the View. Contains notification sending capabilities. 
    /// </summary>
    public class ToDoListViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Root Todo list
        /// </summary>
        public ObservableCollection<TodoViewModel> Todos { get; }
        TodoListModel Model;
        NotificationSender NotificationService;

        /// <summary>
        /// Constructor for TodoListViewModel that can be backed by any list of Todos, supporting 
        /// filtering and reacting to propertychanged events
        /// </summary>
        /// <param name="Recycled">Bool representing whether to load recycled or non-recycled Todos</param>
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

            NotificationService.Start();
        }

        /// <summary>
        /// ObservableCollection of Todos that supports filtering over the root collection
        /// and is visible to the outside
        /// </summary>
        public ObservableCollection<TodoViewModel> ViewableTodos { get; private set; }
        /// <summary>
        /// Filter with which to create the ViewableTodos collection
        /// </summary>
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
        /// <summary>
        /// Property representing index of currently selected value in ViewableTodos
        /// </summary>
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

        /// <summary>
        /// Currently selected Todo from ViewableTodos
        /// </summary>
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
            //Debug.WriteLine("added new");

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
            if ((bool) !SelectedTodo?.Recycled)
            {
                //Debug.WriteLine("recycled!");

                //updates view model
                SelectedTodo.Recycled = true;
                ViewableTodos.RemoveAt(SelectedIndex);
            }
        }

        /// <summary>
        /// Sets Recycled to be false for the current SelectedTodo
        /// </summary>
        public void Restore()
        {
            if ((bool) SelectedTodo?.Recycled)
            {
                //Debug.WriteLine("restored!");

                //updates view model
                SelectedTodo.Recycled = false;
                Todos.RemoveAt(SelectedIndex);
            }
        }

        /// <summary>
        /// Ensures that whenever a property of the Todos in the ObservableCollection
        /// changes, the underlying Model is updated as well.
        /// </summary>
        /// <param name="sender">TodoViewModel whose property has changed</param>
        /// <param name="e"></param>
        void Todo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Model.Update(((TodoViewModel)sender).Todo);
        }
    }
}
