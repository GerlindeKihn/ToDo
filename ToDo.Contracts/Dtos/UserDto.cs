namespace ToDo.Contracts.Dtos;

public sealed record CreateUserDto(string Username, string Password);
public sealed record UserDto(int Id, string UserName);