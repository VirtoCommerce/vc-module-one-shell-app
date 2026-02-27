using System.Collections.Generic;
using VirtoCommerce.OneShell.Core.Models;
using VirtoCommerce.Platform.Core.Events;

namespace VirtoCommerce.OneShell.Core.Events;

public class MainMenuEventChangingEvent(IEnumerable<GenericChangedEntry<MainMenuEvent>> changedEntries)
    : GenericChangedEntryEvent<MainMenuEvent>(changedEntries);
