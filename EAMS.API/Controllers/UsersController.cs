using EAMS.Application.Features.Users.Commands.CreateUser;
using EAMS.Application.Features.Users.Commands.UpdateUser;
using EAMS.Application.Features.Users.Commands.DeactivateUser;
using EAMS.Application.Features.Users.Commands.ChangeUserRole;
using EAMS.Application.Features.Users.Queries.GetUserById;
using EAMS.Application.Features.Users.Queries.GetUsers;
using EAMS.API.Authorization;
using EAMS.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EAMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(Permissions.UserRead)]
    [HttpGet]
    public async Task<IActionResult> GetUsers(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetUsersQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HasPermission(Permissions.UserRead)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetUserByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HasPermission(Permissions.UserCreate)]
    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetUserById),
            new { id = result.Id },
            result);
    }

    [HasPermission(Permissions.UserUpdate)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(
        Guid id,
        [FromBody] UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest(new
            {
                Message = "The route ID does not match the request ID."
            });
        }

        var result = await _sender.Send(
            command,
            cancellationToken);

        return Ok(result);
    }

    [HasPermission(Permissions.UserUpdate)]
    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> DeactivateUser(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new DeactivateUserCommand(id),
            cancellationToken);

        return NoContent();
    }

    [HasPermission(Permissions.UserUpdate)]
    [HttpPut("{id:guid}/role")]
    public async Task<IActionResult> ChangeUserRole(
        Guid id,
        [FromBody] ChangeUserRoleCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.UserId)
        {
            return BadRequest(new
            {
                Message = "The route ID does not match the request user ID."
            });
        }

        var result = await _sender.Send(
            command,
            cancellationToken);

        return Ok(result);
    }
}