using System.Collections.Generic;
using System.Security.Claims;
using VirtoCommerce.OneShell.Core.Models;

namespace VirtoCommerce.OneShell.Core.Services;

public interface IMainMenuPermissionsFilter
{
    MainMenu FilterMenuByPermissions(MainMenu root, ClaimsPrincipal user);

    IList<MenuItem> FilterItemsByPermissions(IList<MenuItem> items, ClaimsPrincipal user);
}
