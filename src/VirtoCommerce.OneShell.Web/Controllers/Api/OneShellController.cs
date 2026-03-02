using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.OneShell.Core.Models;
using VirtoCommerce.OneShell.Core.Services;
using Permissions = VirtoCommerce.OneShell.Core.ModuleConstants.Security.Permissions;

namespace VirtoCommerce.OneShell.Web.Controllers.Api;

[Authorize]
[Route("api/one-shell")]
public class OneShellController : Controller
{
    private readonly IMainMenuService _mainMenuService;

    public OneShellController(IMainMenuService mainMenuService)
    {
        _mainMenuService = mainMenuService;
    }

    [HttpGet]
    [Route("{cultureName}")]
    [Authorize(Permissions.Read)]
    public async Task<ActionResult<string>> GetMainMenu([FromRoute] string cultureName = null)
    {
        var result = await _mainMenuService.GetMainMenuAsync(cultureName);

        return Ok(result);
    }

    [HttpGet]
    [Route("recent/{take}")]
    public async Task<ActionResult<string>> GetMainMenu([FromRoute] int take = 5)
    {
        var result = await _mainMenuService.GetRecentMenuItems(take);

        return Ok(result);
    }

    [HttpPost]
    [Route("click-event")]
    public async Task<ActionResult<string>> RecordClickEvent([FromBody] MenuItemClickEvent clickEvent)
    {
        clickEvent.UserId = User.Identity?.Name;

        await _mainMenuService.RecordClickEvent(clickEvent);

        return Ok();
    }
}
