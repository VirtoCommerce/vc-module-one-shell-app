using System.Collections.Generic;
using Newtonsoft.Json;

namespace VirtoCommerce.OneShell.Core.Models;

public class MainMenu
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("groups")]
    public List<MenuGroup> Groups { get; set; } = new();
}

public class MenuGroup
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("permission")]
    public string Permission { get; set; }

    [JsonProperty("items")]
    public IList<MenuItem> Items { get; set; } = [];
}

public class MenuItem
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("permission")]
    public string Permission { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }

    [JsonProperty("iconUrl")]
    public string IconUrl { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("app")]
    public AppInfo App { get; set; }

    [JsonProperty("children")]
    public IList<MenuItem> Children { get; set; } = [];
}

public class AppInfo
{
    [JsonProperty("url")]
    public string Url { get; set; }
}
