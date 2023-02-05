namespace ToDo.Contracts.Dtos;

public sealed record SaveToDoDto(string Topic, DateTime Deadline);
public sealed record ToDoDto(int Id, string Topic, DateTime Deadline);