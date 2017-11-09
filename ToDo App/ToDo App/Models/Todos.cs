using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Models
{
    public class Model
    {
        public IEnumerable<Todo> Todos { get; set; }
        IDataProvider Data = new DataProvider();
        
        public void Create(Todo todo)
        {
            Data.Create(todo);
        }

        public void Recycle(Todo todo)
        {
            Data.Recycle(todo);
        }

        public void Recycle(Guid id)
        {
            Data.Recycle(id);
        }

        public void Update(Todo todo)
        {
            Data.Update(todo);
        }

        public IEnumerable<Todo> GetTodos(Func<Todo, bool> lambda)
        {
            
        }

        public IEnumerable<Todo> GetRecycledTodos(Func<Todo, bool> lambda)
        {
            throw new NotImplementedException();
        }
    }
}
