using Microsoft.EntityFrameworkCore;

namespace TodoListApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TodoItem> TodoList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>()
                .Property(t => t.Status)
                .HasConversion<string>();
        }
    }
}
