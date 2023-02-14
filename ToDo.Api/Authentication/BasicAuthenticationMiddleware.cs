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
            string authHeader = httpContext.Request.Headers["Authorization"];
            AuthenticationHeaderValue authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
            byte[] authHeaderBytes = Convert.FromBase64String(authHeaderValue.Parameter!);
            string[] credentials = Encoding.UTF8.GetString(authHeaderBytes).Split(':');
            ToDoContext dbContext = httpContext.RequestServices.GetRequiredService<ToDoContext>();

            UserEntity user = await dbContext
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
