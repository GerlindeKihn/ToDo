namespace ToDo.Contracts.Dtos;

public sealed record SaveToDoDto(string Topic, DateTime Deadline, int? Assignee);
public sealed record ToDoDto(int Id, string Topic, DateTime Deadline);