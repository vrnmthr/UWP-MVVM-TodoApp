using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Models
{
    /// <summary>
    /// TodoList Class representing a todo list. Interfaces with
    /// an IDataProvider in order to do CRUD operations on a data
    /// backend. Maintains parallel lists of recycled todos and 
    /// normal todos. 
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
            Todos = Data.GetTodos((Todo t) => true).ToList();
            RecycledTodos = Data.GetRecycledTodos((Todo t) => true).ToList();
        }
        
        /// <summary>
        /// Adds a Todo to the database, assuming it doesn't already exist
        /// </summary>
        /// <param name="todo">Todo to add</param>
        public void Create(Todo todo)
        {
            var SearchIn = Todos;
            if (todo.Recycled) SearchIn = RecycledTodos;
            if (!SearchIn.Exists((Todo t) => t.Id == todo.Id))
            {
                SearchIn.Add(todo);
                Data.Create(todo);
            }
        }

        /// <summary>
        /// Recycles a Todo if it has not already been recycled,
        /// and does nothing otherwise.
        /// </summary>
        /// <param name="todo">Todo to recycle</param>
        public void Recycle(Todo todo)
        {
            if (Todos.Contains(todo))
            {
                RecycledTodos.Add(todo);
                Todos.Remove(todo);
                Data.Recycle(todo);
            }
        }

        /// <summary>
        /// Recycles a Todo if it has not already been recycled, 
        /// and does nothing otherwise. 
        /// </summary>
        /// <param name="id">Todo to recycle</param>
        public void Recycle(Guid id)
        {
            if (Todos.Exists((Todo t) => t.Id.Equals(id)))
            {
                Todo todo = Todos.Where((Todo t) => t.Id.Equals(id)).Single();
                Todos.Remove(todo);
                RecycledTodos.Add(todo);
                Data.Recycle(id);
            }
        }

        /// <summary>
        /// Restores a Todo if it has already been recycled
        /// and does nothing otherwise.
        /// </summary>
        /// <param name="todo">Todo to restore</param>
        public void Restore(Todo todo)
        {
            if (Todos.Contains(todo))
            {
                RecycledTodos.Remove(todo);
                Todos.Add(todo);
                Data.Restore(todo);
            }
        }

        /// <summary>
        /// Restores a Todo if it has already been recycled
        /// and does nothing otherwise. 
        /// </summary>
        /// <param name="id">Todo to restore</param>
        public void Restore(Guid id)
        {
            if (Todos.Exists((Todo t) => t.Id.Equals(id)))
            {
                Todo todo = Todos.Where((Todo t) => t.Id.Equals(id)).Single();
                RecycledTodos.Remove(todo);
                Todos.Add(todo);
                Data.Restore(id);
            }
        }

        /// <summary>
        /// Update should only be called for data fields other than 
        /// Recycled, otherwise will throw an error.
        /// </summary>
        /// <param name="todo">Todo containing updated information</param>
        public void Update(Todo todo)
        {
            List<Todo> SearchIn = Todos;
            if (todo.Recycled) SearchIn = RecycledTodos;
            Todo existing = SearchIn.Where((Todo t) => t.Id == todo.Id).Single();
            existing.DateAssigned = todo.DateAssigned;
            existing.Note = todo.Note;
            Data.Update(todo);
        }

        public IEnumerable<Todo> GetTodos()
        {
            return Todos;
        }

        public IEnumerable<Todo> GetRecycledTodos()
        {
            return RecycledTodos;
        }
    }
}
