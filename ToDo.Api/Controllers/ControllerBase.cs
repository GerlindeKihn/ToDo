using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Authentication;
using ToDo.Contracts.Dtos;

namespace ToDo.Api.Controllers;

[ApiController, Route("[controller]"), Authorize]
public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected UserDto CurrentUser => (UserDto)HttpContext.Items["User"]!;
}