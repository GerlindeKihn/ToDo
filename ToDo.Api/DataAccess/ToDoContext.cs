using Microsoft.EntityFrameworkCore;

namespace ToDo.Api.DataAccess;

public class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoContext).Assembly);
    }
}