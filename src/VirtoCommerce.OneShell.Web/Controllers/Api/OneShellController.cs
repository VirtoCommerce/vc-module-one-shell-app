using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.OneShell.Core.Models;
using VirtoCommerce.OneShell.Core.Services;
using VirtoCommerce.Platform.Core.Security;
using Permissions = VirtoCommerce.OneShell.Core.ModuleConstants.Security.Permissions;

namespace VirtoCommerce.OneShell.Web.Controllers.Api;

[Authorize]
[Route("api/one-shell")]
public class OneShellController : Controller
{
    private readonly IMainMenuService _mainMenuService;
    private readonly IMainMenuPermissionsFilter _mainMenuPermissionsFilter;

    public OneShellController(IMainMenuService mainMenuService, IMainMenuPermissionsFilter mainMenuPermissionsFilter)
    {
        _mainMenuService = mainMenuService;
        _mainMenuPermissionsFilter = mainMenuPermissionsFilter;
    }

    [HttpGet]
    [Route("{cultureName}")]
    [Authorize(Permissions.Read)]
    public async Task<ActionResult<string>> GetMainMenu([FromRoute] string cultureName = null)
    {
        var result = await _mainMenuService.GetMainMenuAsync(cultureName);

        result = _mainMenuPermissionsFilter.FilterMenuByPermissions(result, User);

        return Ok(result);
    }

    [HttpGet]
    [Route("recent/{cultureName}/{take}")]
    public async Task<ActionResult<string>> GetRecent([FromRoute] string cultureName = null, [FromRoute] int take = 5)
    {
        var userId = User.GetUserId();

        var result = await _mainMenuService.GetRecentMenuItemsAsync(cultureName, userId, take);

        result = _mainMenuPermissionsFilter.FilterItemsByPermissions(result, User);

        return Ok(result);
    }

    [HttpPost]
    [Route("clear-recent")]
    public async Task<ActionResult> ClearRecent()
    {
        var userId = User.GetUserId();

        await _mainMenuService.ClearRecentAsync(userId);

        return NoContent();
    }

    [HttpPost]
    [Route("click-event")]
    public async Task<ActionResult<string>> RecordClickEvent([FromBody] MenuItemClickEvent clickEvent)
    {
        clickEvent.UserId = User.GetUserId();

        await _mainMenuService.RecordClickEventAsync(clickEvent);

        return Ok();
    }
}
