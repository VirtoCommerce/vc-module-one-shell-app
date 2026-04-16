using VirtoCommerce.OneShell.Core.Models;
using VirtoCommerce.Platform.Core.GenericCrud;

namespace VirtoCommerce.OneShell.Core.Services;

public interface IMainMenuEventSearchService : ISearchService<MainMenuEventSearchCriteria, MainMenuEventSearchResult, MainMenuEvent>;
