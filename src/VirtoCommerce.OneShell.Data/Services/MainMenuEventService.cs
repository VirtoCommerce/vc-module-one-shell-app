using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.OneShell.Core.Events;
using VirtoCommerce.OneShell.Core.Models;
using VirtoCommerce.OneShell.Core.Services;
using VirtoCommerce.OneShell.Data.Models;
using VirtoCommerce.OneShell.Data.Repositories;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.Platform.Data.GenericCrud;

namespace VirtoCommerce.OneShell.Data.Services;

public class MainMenuEventService(
    Func<IOneShellRepository> repositoryFactory,
    IPlatformMemoryCache platformMemoryCache,
    IEventPublisher eventPublisher)
    : CrudService<MainMenuEvent, MainMenuEventEntity, MainMenuEventChangingEvent, MainMenuEventChangedEvent>
        (repositoryFactory, platformMemoryCache, eventPublisher),
        IMainMenuEventService
{
    protected override Task<IList<MainMenuEventEntity>> LoadEntities(IRepository repository, IList<string> ids, string responseGroup)
    {
        return ((IOneShellRepository)repository).GetMenuEventsByIdsAsync(ids, responseGroup);
    }
}
