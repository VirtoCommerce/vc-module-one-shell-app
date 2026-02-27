using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.OneShell.Core;

public static class ModuleConstants
{
    public static class Security
    {
        public static class Permissions
        {
            public const string Access = "one-shell:access";
            public const string Create = "one-shell:create";
            public const string Read = "one-shell:read";
            public const string Update = "one-shell:update";
            public const string Delete = "one-shell:delete";

            public static string[] AllPermissions { get; } =
            [
                Access,
                Create,
                Read,
                Update,
                Delete,
            ];
        }
    }

    public static class Settings
    {
        public static class General
        {
            public static SettingDescriptor MainMenu { get; } = new SettingDescriptor
            {
                Name = "OneShell.MainMenu",
                GroupName = "OneShell|User General",
                ValueType = SettingValueType.Json,
                DefaultValue = "{}"
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return MainMenu;
                }
            }
        }

        public static IEnumerable<SettingDescriptor> AllSettings
        {
            get
            {
                return General.AllGeneralSettings;
            }
        }
    }
}
