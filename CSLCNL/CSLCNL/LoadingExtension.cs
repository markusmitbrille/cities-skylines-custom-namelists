using ICities;
using ColossalFramework.Globalization;

using static Makaki.CustomNameLists.DebugUtils;

namespace Makaki.CustomNameLists
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnCreated(ILoading loading)
        {
            Log("Adding locale change event handlers...");

            // Add post locale change event handlers
            LocaleManager.eventLocaleChanged += NameListManager.ApplyBlackLists;
            LocaleManager.eventLocaleChanged += NameListManager.ApplyNameLists;

            Log("Added locale change event handlers.");

            // Reload the current locale once to effect changes
            LocaleManager.ForceReload();
        }

        public override void OnReleased()
        {
            Log("Removing locale change event handlers...");
 
            // Remove post locale change event handlers
            LocaleManager.eventLocaleChanged -= NameListManager.ApplyBlackLists;
            LocaleManager.eventLocaleChanged -= NameListManager.ApplyNameLists;

            Log("Removed locale change event handlers.");

            // Reload the current locale once to effect changes
            LocaleManager.ForceReload();
        }
    }
}
