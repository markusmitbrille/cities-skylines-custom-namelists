using System;

namespace Makaki.CustomNameLists
{
    static class Settings
    {
        public static string LocaleExportDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static bool UseBlacklists = true;
    }
}
