using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataProvider
    {
        /// <summary>
        /// Add a Todo to data
        /// </summary>
        /// <param name="todo">Todo to add</param>
        void Create(Todo todo);

        /// <summary>
        /// Finds the Todo with a matching ID as the given one
        /// and updates the information. 
        /// </summary>
        /// <param name="todo">Todo to update with updated information</param>
        void Update(Todo todo);

        /// <summary>
        /// Finds the Todo with ID matching the given one and deletes it
        /// </summary>
        /// <param name="todo">Todo to delete</param>
        void Delete(Todo todo);

        /// <summary>
        /// Gets all Todos that satisfy lambda
        /// </summary>
        /// <returns>List of Todos</returns>
        IEnumerable<Todo> GetTodos(Func<Todo, bool> lambda);
    }
}
