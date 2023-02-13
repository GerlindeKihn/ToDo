using Refit;
using ToDo.Contracts.Dtos;

namespace ToDo.App.Interfaces;

public interface IUserApiClient
{
    [Get("/User")]
    Task<IReadOnlyCollection<UserDto>> Get();

    [Get("/User/Current")]
    Task<UserDto> GetCurrent();

    [Post("/User")]
    Task<UserDto> Create([Body]CreateUserDto dto);
}
