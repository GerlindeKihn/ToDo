using Refit;
using ToDo.Contracts.Dtos;

namespace ToDo.App.Interfaces;

public interface IToDoApiClient
{
    [Get("/ToDo")]
    Task<IReadOnlyCollection<ToDoDto>> Get();

    [Get("/ToDo/{id}")]
    Task<ToDoDto> Get(int id);

    [Post("/ToDo")]
    Task<ToDoDto> Save([Body]SaveToDoDto dto);

    [Put("/ToDo/{id}")]
    Task<ToDoDto> Save([Body]SaveToDoDto dto, int id);

    [Delete("/ToDo/{id}")]
    Task Delete(int id);
}
