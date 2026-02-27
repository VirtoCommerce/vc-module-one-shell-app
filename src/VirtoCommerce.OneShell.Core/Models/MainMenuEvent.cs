using System;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.OneShell.Core.Models;

public class MainMenuEvent : AuditableEntity, ICloneable
{
    public string UserId { get; set; }

    public string MenuItemId { get; set; }

    public string EventType { get; set; } // click, etc

    public object Clone()
    {
        return MemberwiseClone();
    }
}
