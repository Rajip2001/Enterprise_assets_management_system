using EAMS.API.Authorization;
using EAMS.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace EAMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HasPermission(Permissions.AssetRead)]
    [HttpGet("asset-read")]
    public IActionResult AssetRead()
    {
        return Ok(new
        {
            Message = "You have Asset.Read permission."
        });
    }

    [HasPermission(Permissions.AssetCreate)]
    [HttpPost("asset-create")]
    public IActionResult AssetCreate()
    {
        return Ok(new
        {
            Message = "You have Asset.Create permission."
        });
    }
}