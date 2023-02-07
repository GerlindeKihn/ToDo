using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Contracts.Dtos;

namespace ToDo.App.Interfaces
{
    public interface IToDoHolder
    {
        ToDoDto? SelectedToDo { get; set; }
    }
}
