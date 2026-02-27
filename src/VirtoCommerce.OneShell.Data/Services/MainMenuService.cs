using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VirtoCommerce.OneShell.Core;
using VirtoCommerce.OneShell.Core.Models;
using VirtoCommerce.OneShell.Core.Services;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.OneShell.Data.Services;

public class MainMenuService : IMainMenuService
{
    private readonly ISettingsManager _settingsManagers;

    public MainMenuService(ISettingsManager settingsManager)
    {
        _settingsManagers = settingsManager;
    }

    public async Task<MainMenu> GetMainMenuAsync(string cultureName)
    {
        var mainMenuJsonString = await _settingsManagers.GetValueAsync<string>(ModuleConstants.Settings.General.MainMenu);

        if (mainMenuJsonString.IsNullOrEmpty())
        {
            return new MainMenu();
        }

        var result = JsonConvert.DeserializeObject<MainMenu>(mainMenuJsonString);

        return result;
    }

    public async Task<IList<MenuItem>> GetRecentMenuItems(int take)
    {
        return [];
    }

    public Task RecordClickEvent(MenuItemClickEvent clickEvent)
    {
        return Task.CompletedTask;
    }
}
