
using Newtonsoft.Json;

namespace VirtoCommerce.OneShell.Core.Models;

public class MenuItemClickEvent
{
    public string MenuItemId { get; set; }

    [JsonIgnore]
    public string UserId { get; set; }
}
