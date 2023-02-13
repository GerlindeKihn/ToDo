namespace ToDo.Api.DataAccess.Entities
{
    public class UserEntity
    {
        public virtual int Id { get; set; }
        public virtual required string Username { get; set; }
        public virtual required string Password { get; set; }

        public virtual ICollection<ToDoEntity> ToDos { get; set; }
    }
}
