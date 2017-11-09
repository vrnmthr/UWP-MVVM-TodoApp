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
        /// Recycles given todo
        /// </summary>
        /// <param name="todo">Todo to recycle</param>
        void Recycle(Todo todo);

        /// <summary>
        /// Recycles todo with given ID
        /// </summary>
        /// <param name="id">Guid of todo to recycle</param>
        void Recycle(Guid id);

        /// <summary>
        /// Finds the Todo with a matching ID as the given one
        /// and updates the information. 
        /// </summary>
        /// <param name="todo">Todo to update with updated information</param>
        void Update(Todo todo);

        /// <summary>
        /// Get all Todos that are not recycled that satisfy
        /// lambda
        /// </summary>
        /// <returns>List of Todos</returns>
        IEnumerable<Todo> GetTodos(Func<Todo, bool> lambda);

        /// <summary>
        /// Get all recycled todos that satisfy lambda
        /// </summary>
        /// <returns>List of Todos</returns>
        IEnumerable<Todo> GetRecycledTodos(Func<Todo, bool> lambda);

    }
}
