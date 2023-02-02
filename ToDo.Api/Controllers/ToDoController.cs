using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Api.DataAccess;
using ToDo.Api.DataAccess.Entities;
using ToDo.Api.Dtos;

namespace ToDo.Api.Controllers;

public class ToDoController : ControllerBase
{
    private readonly ToDoContext dbContext;

    public ToDoController(ToDoContext dbContext) => this.dbContext = dbContext;

    [HttpGet]
    public async Task<IReadOnlyCollection<ToDoDto>> Get() =>
        await dbContext.Set<ToDoEntity>()
            .Select(e => new ToDoDto(e.Id, e.Topic, e.Deadline))
            .ToListAsync();

    [HttpGet("{id}")]
    public async Task<ToDoDto> Get(int id) =>
        await dbContext.Set<ToDoEntity>()
            .Where(e => e.Id == id)
            .Select(e => new ToDoDto(e.Id, e.Topic, e.Deadline))
            .SingleAsync();

    [HttpPost]
    public async Task<ToDoDto> Save(SaveToDoDto dto)
    {
        ToDoEntity entity = new() { Topic = dto.Topic, Deadline = dto.Deadline };

        await dbContext.Set<ToDoEntity>().AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return new(entity.Id, entity.Topic, entity.Deadline);
    }

    [HttpPut("{id}")]
    public async Task<ToDoDto> Save(int id, SaveToDoDto dto)
    {
        ToDoEntity entity = await dbContext.Set<ToDoEntity>().SingleAsync(e => e.Id == id);

        entity.Topic = dto.Topic;
        entity.Deadline = dto.Deadline;

        await dbContext.SaveChangesAsync();

        return new(entity.Id, entity.Topic, entity.Deadline);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        ToDoEntity entity = await dbContext.Set<ToDoEntity>().SingleAsync(e => e.Id == id);
        
        dbContext.Set<ToDoEntity>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}