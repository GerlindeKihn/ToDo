using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Api.DataAccess;
using ToDo.Api.DataAccess.Entities;
using ToDo.Contracts.Dtos;

namespace ToDo.Api.Controllers;

public class UserController : ControllerBase
{
    private readonly ToDoContext dbContext;

    public UserController(ToDoContext dbContext) => this.dbContext = dbContext;

    [HttpGet]
    public async Task<IReadOnlyCollection<UserDto>> Get() =>
        await dbContext.Set<UserEntity>()
            .Where(e => e.Id != CurrentUser.Id)
            .OrderBy(e => e.Username)
            .Select(e => new UserDto(e.Id, e.Username))
            .ToListAsync();

    [HttpGet("Current")]
    public async Task<UserDto> GetCurrent() => 
        await dbContext.Set<UserEntity>()
            .Where(e => e.Id == CurrentUser.Id)
            .Select(e => new UserDto(e.Id, e.Username))
            .SingleAsync();

    [HttpPost, AllowAnonymous]
    public async Task<UserDto> Create(CreateUserDto dto)
    {
        UserEntity entity = new() { Username = dto.Username, Password = dto.Password };
       
        await dbContext.Set<UserEntity>().AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return new(entity.Id, entity.Username);
    }
}
