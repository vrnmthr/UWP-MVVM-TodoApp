using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Implements IDataProvider using the DataContext SQLite
    /// database
    /// </summary>
    class DataService : IDataProvider
    {
        public void Create(Todo todo)
        {
            using (var db = new DataContext())
            {
                db.Todos.Add(todo);
                db.SaveChanges();
            }
        }

        public void Delete(Todo todo)
        {
            using (var db = new DataContext())
            {
                db.Todos.Remove(todo);
                db.SaveChanges();
            }
        }

        public IEnumerable<Todo> GetTodos(Func<Todo, bool> lambda)
        {
            using (var db = new DataContext())
            {
                return db.Todos.Where(lambda).ToList();
            }
        }

        public void Update(Todo todo)
        {
            using (var db = new DataContext())
            {
                db.Todos.Update(todo);
                db.SaveChanges();
            }
        }
    }
}
