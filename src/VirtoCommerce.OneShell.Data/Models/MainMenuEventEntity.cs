using System.ComponentModel.DataAnnotations;
using VirtoCommerce.OneShell.Core.Models;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Domain;

namespace VirtoCommerce.OneShell.Data.Models;

public class MainMenuEventEntity : AuditableEntity, IDataEntity<MainMenuEventEntity, MainMenuEvent>
{
    [Required]
    [StringLength(64)]
    public string UserId { get; set; }

    [StringLength(256)]
    public string MenuItemId { get; set; }

    [StringLength(64)]
    public string EventType { get; set; } // click, etc

    public virtual MainMenuEvent ToModel(MainMenuEvent model)
    {
        model.UserId = UserId;
        model.MenuItemId = MenuItemId;
        model.EventType = EventType;

        model.Id = Id;
        model.CreatedBy = CreatedBy;
        model.CreatedDate = CreatedDate;
        model.ModifiedBy = ModifiedBy;
        model.ModifiedDate = ModifiedDate;

        return model;
    }

    public virtual MainMenuEventEntity FromModel(MainMenuEvent model, PrimaryKeyResolvingMap pkMap)
    {
        pkMap.AddPair(model, this);

        Id = model.Id;
        CreatedBy = model.CreatedBy;
        CreatedDate = model.CreatedDate;
        ModifiedBy = model.ModifiedBy;
        ModifiedDate = model.ModifiedDate;

        UserId = model.UserId;
        MenuItemId = model.MenuItemId;
        EventType = model.EventType;

        return this;
    }

    public virtual void Patch(MainMenuEventEntity target)
    {
        target.UserId = UserId;
        target.MenuItemId = MenuItemId;
        target.EventType = EventType;
    }
}
