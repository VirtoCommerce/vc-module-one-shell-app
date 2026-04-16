using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Domain;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.OneShell.Data.Models;

namespace VirtoCommerce.OneShell.Data.Repositories;

public class OneShellRepository(OneShellDbContext dbContext, IUnitOfWork unitOfWork = null)
    : DbContextRepositoryBase<OneShellDbContext>(dbContext, unitOfWork),
        IOneShellRepository
{
    public IQueryable<MainMenuEventEntity> MenuEvents => DbContext.Set<MainMenuEventEntity>();

    public virtual async Task<IList<MainMenuEventEntity>> GetMenuEventsByIdsAsync(IList<string> ids, string responseGroup)
    {
        if (ids.IsNullOrEmpty())
        {
            return [];
        }

        return ids.Count == 1
            ? await MenuEvents.Where(x => x.Id == ids.First()).ToListAsync()
            : await MenuEvents.Where(x => ids.Contains(x.Id)).ToListAsync();
    }
}
