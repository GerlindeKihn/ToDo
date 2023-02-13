using ToDo.Api.DataAccess;
using ToDo.Api.DataAccess.Entities;

namespace ToDo.Api.Services;

public class DatabaseCreationService : BackgroundService
{
    private readonly Dictionary<string, string> initialUsers = new();
    private readonly IServiceProvider serviceProvider;

    public DatabaseCreationService(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        this.serviceProvider = serviceProvider;

        configuration.GetSection("InitialUsers").Bind(initialUsers);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();
        await using ToDoContext dbContext = scope.ServiceProvider.GetRequiredService<ToDoContext>();

        await dbContext.Database.EnsureCreatedAsync();

        if (dbContext.Set<UserEntity>().Count() > 0) return;

        UserEntity[] users = initialUsers
            .Select(u => new UserEntity { Username = u.Key, Password = u.Value })
            .ToArray();

        await dbContext.Set<UserEntity>().AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
    }
}