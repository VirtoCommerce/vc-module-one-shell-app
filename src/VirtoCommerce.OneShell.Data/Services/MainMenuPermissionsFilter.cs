using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using VirtoCommerce.OneShell.Core.Models;
using VirtoCommerce.OneShell.Core.Services;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Security;

namespace VirtoCommerce.OneShell.Data.Services;

public class MainMenuPermissionsFilter : IMainMenuPermissionsFilter
{
    public MainMenu FilterMenuByPermissions(MainMenu root, ClaimsPrincipal user)
    {
        ArgumentNullException.ThrowIfNull(root);
        ArgumentNullException.ThrowIfNull(user);

        var groups = (root.Groups ?? [])
            .Select(g => FilterGroup(g, user))
            .Where(g => g != null)
            .ToList();

        return new MainMenu
        {
            Title = root.Title,
            Groups = groups,
        };
    }

    public IList<MenuItem> FilterItemsByPermissions(IList<MenuItem> items, ClaimsPrincipal user)
    {
        var filteredItems = items
            .Select(i => FilterItem(i, user))
            .Where(i => i != null)
            .ToList();

        return filteredItems;
    }

    protected MenuGroup FilterGroup(MenuGroup group, ClaimsPrincipal user)
    {
        // remove group immediately if not allowed
        if (!IsAllowed(user, group.Permission))
        {
            return null;
        }

        var filteredItems = FilterItemsByPermissions(group.Items ?? [], user);

        // remove group if no children after filtering
        if (filteredItems.Count == 0)
        {
            return null;
        }

        return new MenuGroup
        {
            Id = group.Id,
            Title = group.Title,
            Description = group.Description,
            Permission = group.Permission,
            Items = filteredItems,
        };
    }

    protected MenuItem FilterItem(MenuItem item, ClaimsPrincipal user)
    {
        var filteredChildren = (item.Children ?? [])
            .Select(c => FilterItem(c, user))
            .Where(c => c != null)
            .ToList();

        var selfAllowed = IsAllowed(user, item.Permission);

        // Rules:
        // show item if allowed
        // or it has allowed children (so we don't lose a branch)
        if (!selfAllowed && filteredChildren.Count == 0)
        {
            return null;
        }

        return new MenuItem
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Permission = item.Permission,
            Icon = item.Icon,
            IconUrl = item.IconUrl,
            Type = item.Type,
            App = item.App,
            Children = filteredChildren,
        };
    }

    private static bool IsAllowed(ClaimsPrincipal user, string permission)
    {
        return permission.IsNullOrEmpty() || user.HasGlobalPermission(permission);
    }


}
