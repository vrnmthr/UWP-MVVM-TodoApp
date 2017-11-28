using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Diagnostics;

namespace Models
{
    /// <summary>
    /// TodoList Class representing a todo list. Interfaces with
    /// an IDataProvider in order to do CRUD operations on a data
    /// backend.
    /// </summary>
    public class TodoListModel
    {
        List<Todo> Todos { get; set; }
        IDataProvider Data;

        /// <summary>
        /// Constructs a TodoListModel with data loaded in from the data
        /// service
        /// </summary>
        public TodoListModel()
        {
            Data = new DataService();
            Todos = new List<Todo>();
            Todos.AddRange(Data.GetTodos((Todo t) => true));
        }
        
        /// <summary>
        /// Adds a Todo to the database, assuming it doesn't already exist
        /// </summary>
        /// <param name="todo">Todo to add</param>
        public void Create(Todo todo)
        {
            if (Todos.Exists((Todo t) => t.Id == todo.Id))
                throw new ArgumentException("Todo with given ID already exists");
            Todos.Add(todo);
            Data.Create(todo);
        }

        /// <summary>
        /// Update should only be called for data fields other than 
        /// Recycled, otherwise will throw an error.
        /// </summary>
        /// <param name="todo">Todo containing updated information</param>
        public void Update(Todo todo)
        {
            Todo existing = Todos.Where((Todo t) => t.Id == todo.Id).Single();
            existing.CopyFrom(todo);
            Data.Update(todo);
        }

        /// <summary>
        /// Returns all Todos that are not recycled
        /// </summary>
        /// <returns>IEnumerable of Todos that are not recycled</returns>
        public IEnumerable<Todo> GetTodos()
        {
            return Todos.Where((Todo x) => !x.Recycled);
        }

        /// <summary>
        /// Returns all Todos that are recycled
        /// </summary>
        /// <returns>IEnumerable of Todos that are recycled</returns>
        public IEnumerable<Todo> GetRecycledTodos()
        {
            return Todos.Where((Todo x) => x.Recycled);
        }
    }
}
