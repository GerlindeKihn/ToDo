using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text;
using ToDo.Api.DataAccess;
using ToDo.Api.DataAccess.Entities;
using ToDo.Contracts.Dtos;

namespace ToDo.Api.Authentication;

public class BasicAuthenticationMiddleware
{
    private readonly RequestDelegate next;
    public BasicAuthenticationMiddleware(RequestDelegate next) => this.next = next;


    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            var authHeader = httpContext.Request.Headers["Authorization"];
            var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
            var authHeaderBytes = Convert.FromBase64String(authHeaderValue.Parameter!);
            var credentials = Encoding.UTF8.GetString(authHeaderBytes).Split(':');
            var dbContext = httpContext.RequestServices.GetRequiredService<ToDoContext>();

            var user = await dbContext
                .Set<UserEntity>()
                .SingleOrDefaultAsync(e => e.Username == credentials[0]);

            if (user is not null && user.Password == credentials[1])
                httpContext.Items["User"] = new UserDto(user.Id, user.Username);
        }
        catch(Exception ex)
        {
            //           
        }

        await next(httpContext);
    }
}
