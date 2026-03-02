using System;
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
    private readonly IMainMenuEventService _mainMenuEventService;
    private readonly IMainMenuEventSearchService _mainMenuEventSearchService;

    public MainMenuService(ISettingsManager settingsManager,
        IMainMenuEventService mainMenuEventService,
        IMainMenuEventSearchService mainMenuEventSearchService)
    {
        _settingsManagers = settingsManager;
        _mainMenuEventService = mainMenuEventService;
        _mainMenuEventSearchService = mainMenuEventSearchService;
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

    public async Task RecordClickEvent(MenuItemClickEvent clickEvent)
    {
        var eventsToSave = new List<MainMenuEvent>();

        var searchCriteria = AbstractTypeFactory<MainMenuEventSearchCriteria>.TryCreateInstance();

        searchCriteria.UserId = clickEvent.UserId;
        searchCriteria.MenuItemId = clickEvent.MenuItemId;
        searchCriteria.EventType = ModuleConstants.MainMenuEventClickType;

        var searchResult = await _mainMenuEventSearchService.SearchNoCloneAsync(searchCriteria);

        if (searchResult.Results.Count > 0)
        {
            foreach (var events in searchResult.Results)
            {
                events.ModifiedDate = DateTime.UtcNow;
                eventsToSave.Add(events);
            }
        }
        else
        {
            var newEvent = AbstractTypeFactory<MainMenuEvent>.TryCreateInstance();
            newEvent.UserId = clickEvent.UserId;
            newEvent.MenuItemId = clickEvent.MenuItemId;
            newEvent.EventType = ModuleConstants.MainMenuEventClickType;

            eventsToSave.Add(newEvent);
        }

        await _mainMenuEventService.SaveChangesAsync(eventsToSave);
    }
}
