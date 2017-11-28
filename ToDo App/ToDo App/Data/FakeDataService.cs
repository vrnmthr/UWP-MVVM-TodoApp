using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.TempData;

namespace Data
{
    /// <summary>
    /// Static class for faking Todo Data
    /// </summary>
    public static class TempData
    {
        public static List<Todo> Todos = new List<Todo>()
        {
            new Todo(){Id = Guid.NewGuid(), Note = "First Todo!", DateAssigned = DateTime.Now, Recycled = false, HasReminder = true},
            new Todo(){Id = Guid.NewGuid(), Note = "Second Todo!", DateAssigned = DateTime.Now, Recycled = false},
            new Todo(){Id = Guid.NewGuid(), Note = "Third Todo!", DateAssigned = DateTime.Now, Recycled = false}
        };
    }

    /// <summary>
    /// DataService that is connected to the static class defined here. 
    /// Implements IDataProvider.
    /// </summary>
    public class FakeDataService : IDataProvider
    {

        public void Create(Todo todo)
        {
            if (Todos.Any(x => x.Id.Equals(todo.Id)))
            {
                throw new ArgumentException("Todo with given Id already exists");
            }
            Todo t = new Todo();
            t.CopyFrom(todo, true);
            Todos.Add(t);
        }

        public void Delete(Todo todo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Todo> GetTodos(Func<Todo, bool> lambda)
        {
            return Todos.Where(lambda);
        }

        public void Update(Todo todo)
        {
            Todo t = Todos.Where(x => x.Id.Equals(todo.Id)).Single();
            t.CopyFrom(todo);
        }
    }
}
