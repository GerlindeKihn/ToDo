using ToDo.Contracts.Dtos;

namespace ToDo.App.Interfaces;

public interface IToDoHolder
{
    ToDoDto? SelectedToDo { get; set; }
}