using EAMS.API.Authorization;
using EAMS.Application.Features.Permissions.Queries.GetPermissionById;
using EAMS.Application.Features.Permissions.Queries.GetPermissions;
using EAMS.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EAMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly ISender _sender;

    public PermissionsController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(Permissions.RoleRead)]
    [HttpGet]
    public async Task<IActionResult> GetPermissions(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetPermissionsQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HasPermission(Permissions.RoleRead)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPermissionById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetPermissionByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }
}
