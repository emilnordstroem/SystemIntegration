using Microsoft.EntityFrameworkCore;
using Lektion3TodoAPI.Models;

namespace Data.TodoAPI
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}
