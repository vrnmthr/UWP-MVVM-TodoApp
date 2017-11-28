using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data
{
    /// <summary>
    /// EntityFramework class allowing data access to the Todos table in the 
    /// database via a SQLite provider.
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=todos.db");
        }
    }
}