using EAMS.API.Authorization;
using EAMS.Application.Features.Roles.Commands.CreateRole;
using EAMS.Application.Features.Roles.Commands.DeleteRole;
using EAMS.Application.Features.Roles.Commands.UpdateRole;
using EAMS.Application.Features.Roles.Queries.GetRoleById;
using EAMS.Application.Features.Roles.Queries.GetRoles;
using EAMS.Application.Features.Roles.Commands.AssignPermission;
using EAMS.Application.Features.Roles.Commands.RemovePermission;
using EAMS.Application.Features.Roles.Queries.GetRolePermissions;
using EAMS.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EAMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly ISender _sender;

    public RolesController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(Permissions.RoleRead)]
    [HttpGet]
    public async Task<IActionResult> GetRoles(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetRolesQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HasPermission(Permissions.RoleRead)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoleById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetRoleByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HasPermission(Permissions.RoleCreate)]
    [HttpPost]
    public async Task<IActionResult> CreateRole(
        [FromBody] CreateRoleCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetRoleById),
            new { id = result.Id },
            result);
    }

    [HasPermission(Permissions.RoleUpdate)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateRole(
        Guid id,
        [FromBody] UpdateRoleCommand command,
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

    [HasPermission(Permissions.RoleDelete)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRole(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new DeleteRoleCommand(id),
            cancellationToken);

        return NoContent();
    }

    [HasPermission(Permissions.RoleRead)]
    [HttpGet("{id:guid}/permissions")]
    public async Task<IActionResult> GetRolePermissions(
    Guid id,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetRolePermissionsQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HasPermission(Permissions.RoleUpdate)]
    [HttpPost("{id:guid}/permissions/{permissionId:guid}")]
    public async Task<IActionResult> AssignPermission(
    Guid id,
    Guid permissionId,
    CancellationToken cancellationToken)
    {
        await _sender.Send(
            new AssignPermissionCommand(
                id,
                permissionId),
            cancellationToken);

        return NoContent();
    }

    [HasPermission(Permissions.RoleUpdate)]
    [HttpDelete("{id:guid}/permissions/{permissionId:guid}")]
    public async Task<IActionResult> RemovePermission(
    Guid id,
    Guid permissionId,
    CancellationToken cancellationToken)
    {
        await _sender.Send(
            new RemovePermissionCommand(
                id,
                permissionId),
            cancellationToken);

        return NoContent();
    }
}