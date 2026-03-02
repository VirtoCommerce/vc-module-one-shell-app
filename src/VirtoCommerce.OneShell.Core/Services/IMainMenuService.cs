using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.OneShell.Core.Models;

namespace VirtoCommerce.OneShell.Core.Services;

public interface IMainMenuService
{
    Task<MainMenu> GetMainMenuAsync(string cultureName);

    Task<IList<MenuItem>> GetRecentMenuItemsAsync(string cultureName, string userId, int take);

    Task RecordClickEventAsync(MenuItemClickEvent clickEvent);

    Task ClearRecentAsync(string userId);
}
