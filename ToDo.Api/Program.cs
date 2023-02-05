using Microsoft.EntityFrameworkCore;
using ToDo.Api.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ToDoContext>((services, options) =>
    options.UseSqlite(
        services
            .GetRequiredService<IConfiguration>()
            .GetConnectionString("ToDo")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ensure database is created
using var scope = app.Services.CreateAsyncScope();
using var context = scope.ServiceProvider.GetRequiredService<ToDoContext>();
await context.Database.EnsureCreatedAsync();

await context.DisposeAsync();
await scope.DisposeAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
