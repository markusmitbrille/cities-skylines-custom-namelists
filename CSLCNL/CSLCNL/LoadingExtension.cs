using ICities;
using ColossalFramework.Globalization;

using static Makaki.CustomNameLists.DebugUtils;

namespace Makaki.CustomNameLists
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnCreated(ILoading loading)
        {
            // Add post locale change event handlers
            LocaleManager.eventLocaleChanged += OnLocaleChanged;

            Log("Added locale change event handlers.");

            // Reload the current locale once to effect changes
            LocaleManager.ForceReload();
        }

        public override void OnReleased()
        {
            // Remove post locale change event handlers
            LocaleManager.eventLocaleChanged -= OnLocaleChanged;

            Log("Removed locale change event handlers.");

            // Reload the current locale once to effect changes
            LocaleManager.ForceReload();
        }

        private void OnLocaleChanged()
        {
            Log("Locale changed callback started.");

            if (Settings.UseBlacklists)
            {
                NameListManager.ApplyBlackLists();
            }
            if (Settings.UseNamelists)
            {
                NameListManager.ApplyNameLists();
            }

            Log("Locale changed callback finished.");
        }
    }
}
