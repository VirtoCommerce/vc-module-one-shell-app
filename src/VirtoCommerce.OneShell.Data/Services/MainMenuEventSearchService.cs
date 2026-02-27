using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using VirtoCommerce.OneShell.Core.Models;
using VirtoCommerce.OneShell.Core.Services;
using VirtoCommerce.OneShell.Data.Models;
using VirtoCommerce.OneShell.Data.Repositories;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.GenericCrud;
using VirtoCommerce.Platform.Data.GenericCrud;

namespace VirtoCommerce.OneShell.Data.Services;

public class MainMenuEventSearchService(
    Func<IOneShellRepository> repositoryFactory,
    IPlatformMemoryCache platformMemoryCache,
    IMainMenuEventService crudService,
    IOptions<CrudOptions> crudOptions)
    : SearchService<MainMenuEventSearchCriteria, MainMenuEventSearchResult, MainMenuEvent, MainMenuEventEntity>
        (repositoryFactory, platformMemoryCache, crudService, crudOptions),
        IMainMenuEventSearchService
{
    protected override IQueryable<MainMenuEventEntity> BuildQuery(IRepository repository, MainMenuEventSearchCriteria criteria)
    {
        var query = ((IOneShellRepository)repository).MenuEvents;
        return query;
    }

    protected override IList<SortInfo> BuildSortExpression(MainMenuEventSearchCriteria criteria)
    {
        var sortInfos = criteria.SortInfos;

        if (sortInfos.IsNullOrEmpty())
        {
            sortInfos =
            [
                new SortInfo { SortColumn = nameof(MainMenuEventEntity.ModifiedDate), SortDirection = SortDirection.Descending },
                new SortInfo { SortColumn = nameof(MainMenuEventEntity.Id) },
            ];
        }

        return sortInfos;
    }
}
