using ICities;
using ColossalFramework;
using ColossalFramework.Globalization;

using static Makaki.CustomNameLists.DebugUtils;

namespace Makaki.CustomNameLists
{
    public class Mod : IUserMod
    {
        public const string ModName = "Custom Name Lists";
        public const string ModDescription = "Provides a framework to add namelists for cities, districts, streets, buildings and much more.";

        public string Name => ModName;
        public string Description => ModDescription;

        public void OnEnabled()
        {
            Log("Mod enabled.");
        }

        public void OnDisable()
        {
            Log("Mod disabled.");
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            helper.AddCheckbox("Remove blacklisted default names", Settings.UseBlacklists, (isChecked) => Settings.UseBlacklists = isChecked);
            helper.AddButton("Export complete namelist", NameListManager.PrintLocale);
            helper.AddButton("Apply Changes", LocaleManager.ForceReload);
        }

    }
}
