using Microsoft.EntityFrameworkCore;
using ToDo.Api.Authentication;
using ToDo.Api.DataAccess;
using ToDo.Api.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ToDoContext>((services, options) =>
    options.UseSqlite(
        services
            .GetRequiredService<IConfiguration>()
            .GetConnectionString("ToDo")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerOptions.SetupBasicAuthorization);
builder.Services.AddHostedService<DatabaseCreationService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<BasicAuthenticationMiddleware>();
app.MapControllers();

app.Run();