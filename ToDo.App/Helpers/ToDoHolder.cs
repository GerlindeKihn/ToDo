using ToDo.App.Interfaces;
using ToDo.Contracts.Dtos;

namespace ToDo.App.Helpers
{
    internal class ToDoHolder : IToDoHolder
    {
        public ToDoDto? SelectedToDo { get; set; }
    }
}