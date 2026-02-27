using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.OneShell.Core.Models;

namespace VirtoCommerce.OneShell.Core.Services;

public interface IMainMenuService
{
    Task<MainMenu> GetMainMenuAsync(string cultureName);

    Task<IList<MenuItem>> GetRecentMenuItems(int take);

    Task RecordClickEvent(MenuItemClickEvent clickEvent);
}
