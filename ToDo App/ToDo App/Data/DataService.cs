using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.TempData;

namespace Data
{
    public class DataService : IDataProvider
    {

        public void Create(Todo todo)
        {
            if (Todos.Any(x => x.Id.Equals(todo.Id)))
            {
                throw new ArgumentException("Todo with given Id already exists");
            }
            Todos.Add(todo);
        }

        public IEnumerable<Todo> GetRecycledTodos(Func<Todo, bool> lambda)
        {
            return Todos.Where(x => x.Recycled).Where(lambda);
        }

        public IEnumerable<Todo> GetTodos(Func<Todo, bool> lambda)
        {
            return Todos.Where(x => !x.Recycled).Where(lambda);
        }

        public void Recycle(Todo todo)
        {
            todo.Recycled = true;
        }

        public void Recycle(Guid id)
        {
            Todo t = Todos.Where(x => x.Id.Equals(id)).Single();
            t.Recycled = true;
        }

        public void Restore(Todo todo)
        {
            //TODO
        }

        public void Restore(Guid id)
        {
            //TODO
        }

        public void Update(Todo todo)
        {
            Todo t = Todos.Where(x => x.Id.Equals(todo.Id)).Single();
            t.Note = todo.Note;
            t.Recycled = todo.Recycled;
        }
    }
}
