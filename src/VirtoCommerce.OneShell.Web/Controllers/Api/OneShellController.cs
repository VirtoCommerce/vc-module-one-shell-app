using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Permissions = VirtoCommerce.OneShell.Core.ModuleConstants.Security.Permissions;

namespace VirtoCommerce.OneShell.Web.Controllers.Api;

[Authorize]
[Route("api/one-shell")]
public class OneShellController : Controller
{
    // GET: api/one-shell
    /// <summary>
    /// Get message
    /// </summary>
    /// <remarks>Return "Hello world!" message</remarks>
    [HttpGet]
    [Route("")]
    [Authorize(Permissions.Read)]
    public ActionResult<string> Get()
    {
        return Ok(new { result = "Hello world!" });
    }
}
