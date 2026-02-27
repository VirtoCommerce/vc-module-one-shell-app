using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.OneShell.Core.Models;

public class MainMenuEventSearchCriteria : SearchCriteriaBase
{
    public string EventType { get; set; }

    public string UserId { get; set; }
}
