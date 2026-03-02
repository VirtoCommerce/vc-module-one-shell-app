using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<IList<MenuItem>> GetRecentMenuItemsAsync(string cultureName, string userId, int take)
    {
        var mainMenu = await GetMainMenuAsync(cultureName);

        // recent user Ids
        var searchCriteria = AbstractTypeFactory<MainMenuEventSearchCriteria>.TryCreateInstance();
        searchCriteria.UserId = userId;
        searchCriteria.EventType = ModuleConstants.MainMenuEventClickType;
        searchCriteria.Take = take;

        var searchResult = await _mainMenuEventSearchService.SearchNoCloneAsync(searchCriteria);

        var recents = new List<MenuItem>();
        foreach (var menuItem in mainMenu.Groups.SelectMany(x => x.Items))
        {
            if (searchResult.Results.Any(x => x.MenuItemId == menuItem.Id))
            {
                recents.Add(menuItem);
            }
        }

        return recents;
    }

    public async Task RecordClickEventAsync(MenuItemClickEvent clickEvent)
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

    public async Task ClearRecentAsync(string userId)
    {
        var searchCriteria = AbstractTypeFactory<MainMenuEventSearchCriteria>.TryCreateInstance();

        searchCriteria.UserId = userId;

        await foreach (var searchResult in _mainMenuEventSearchService.SearchBatchesNoCloneAsync(searchCriteria))
        {
            var ids = searchResult.Results.Select(x => x.Id).ToArray();

            await _mainMenuEventService.DeleteAsync(ids);
        }
    }
}
