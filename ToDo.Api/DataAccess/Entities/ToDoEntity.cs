namespace ToDo.Api.DataAccess.Entities;

public class ToDoEntity
{
    public virtual int Id { get; set; }

    public virtual required string Topic { get; set; }
    public virtual required DateTime Deadline { get; set; }

    public virtual int UserId { get; set; }
    public virtual UserEntity User { get; set; } = default!;
}