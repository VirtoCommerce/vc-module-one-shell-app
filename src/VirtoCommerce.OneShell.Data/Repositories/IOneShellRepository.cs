using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.OneShell.Data.Models;

namespace VirtoCommerce.OneShell.Data.Repositories;

public interface IOneShellRepository : IRepository
{
    IQueryable<MainMenuEventEntity> MenuEvents { get; }

    Task<IList<MainMenuEventEntity>> GetMenuEventsByIdsAsync(IList<string> ids, string responseGroup);
}
