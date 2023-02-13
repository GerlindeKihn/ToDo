using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Api.DataAccess;
using ToDo.Api.DataAccess.Entities;
using ToDo.Contracts.Dtos;

namespace ToDo.Api.Controllers;

public class ToDoController : ControllerBase
{
    private readonly ToDoContext dbContext;
    
    public ToDoController(ToDoContext dbContext) => this.dbContext = dbContext;

    private IQueryable<ToDoEntity> ToDos => dbContext
        .Set<ToDoEntity>()
        .Where(e => e.UserId == CurrentUser.Id);


    [HttpGet]
    public async Task<IReadOnlyCollection<ToDoDto>> Get() =>
        await ToDos
            .Select(e => new ToDoDto(e.Id, e.Topic, e.Deadline))
            .ToListAsync();

    [HttpGet("{id}")]
    public async Task<ToDoDto> Get(int id) =>
        await ToDos
            .Select(e => new ToDoDto(e.Id, e.Topic, e.Deadline))
            .SingleAsync(e => e.Id == id);

    [HttpPost]
    public async Task<ToDoDto> Save(SaveToDoDto dto)
    {
        ToDoEntity entity = new()
        {
            Topic = dto.Topic,
            Deadline = dto.Deadline,
            UserId = dto.Assignee ?? CurrentUser.Id
        };

        await dbContext.Set<ToDoEntity>().AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return new(entity.Id, entity.Topic, entity.Deadline);
    }

    [HttpPut("{id}")]
    public async Task<ToDoDto> Save(SaveToDoDto dto, int id)
    {
        ToDoEntity entity = await ToDos.SingleAsync(e => e.Id == id);

        entity.Topic = dto.Topic;
        entity.Deadline = dto.Deadline;
        entity.UserId = dto.Assignee ?? CurrentUser.Id;

        await dbContext.SaveChangesAsync();

        return new(entity.Id, entity.Topic, entity.Deadline);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        ToDoEntity entity = await ToDos.SingleAsync(e => e.Id == id);
        
        dbContext.Set<ToDoEntity>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}