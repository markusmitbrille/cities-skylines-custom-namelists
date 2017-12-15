﻿using ColossalFramework.Globalization;
using System.Collections.Generic;
using System.Reflection;

using static Makaki.CustomNameLists.DebugUtils;

namespace Makaki.CustomNameLists
{
    static class LocaleManagerExtensions
    {
        const string localeFieldName = "m_Locale";
        const string localizedStringsFieldName = "m_LocalizedStrings";
        const string localizedStringsCountFieldName = "m_LocalizedStringsCount";

        static FieldInfo localeField = typeof(LocaleManager).GetField(localeFieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        static FieldInfo localizedStringsField = typeof(Locale).GetField(localizedStringsFieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        static FieldInfo localizedStringsCountField = typeof(Locale).GetField(localizedStringsCountFieldName, BindingFlags.Instance | BindingFlags.NonPublic);

        public static Locale GetLocale(this LocaleManager localeManager)
        {
            return (Locale)localeField.GetValue(localeManager);
        }

        public static Dictionary<Locale.Key, string> GetLocalizedStrings(this Locale locale)
        {
            return (Dictionary<Locale.Key, string>)localizedStringsField.GetValue(locale);
        }

        public static Dictionary<Locale.Key, int> GetLocalizedStringsCount(this Locale locale)
        {
            return (Dictionary<Locale.Key, int>)localizedStringsCountField.GetValue(locale);
        }

        public static void RemoveRange(this LocaleManager localeManager, Locale.Key id)
        {
            Locale locale = localeManager.GetLocale();

            // Set index to 0 so we can check for the string count
            id.m_Index = 0;

            if (!locale.Exists(id))
            {
                Log($"Could not remove locale range {id}; localized string {id} does not exist!");
                return;
            }

            Dictionary<Locale.Key, string> localizedStrings = locale.GetLocalizedStrings();
            Dictionary<Locale.Key, int> localizedStringsCount = locale.GetLocalizedStringsCount();

            for (int index = 0, lastIndex = locale.CountUnchecked(id); index <= lastIndex; index++, id.m_Index = index)
            {
                localizedStrings.Remove(id);
                localizedStringsCount.Remove(id);
            }

            Log($"Removed locale range {id.m_Identifier}[{id.m_Key}].");
        }

        public static void AddString(this LocaleManager localeManager, LocalizedString localizedString)
        {
            Locale locale = localeManager.GetLocale();

            // Construct 0-index id for the localized string from argument
            Locale.Key id;
            id.m_Identifier = localizedString.Identifier;
            id.m_Key = localizedString.Key;
            id.m_Index = 0;

            // Check if the id already exists; if so find next index
            if (locale.Exists(id))
            {
                // Log message lags game on large namelists
                // Log($"Localized string {localizedString.Identifier}[{localizedString.Key}] already exists, adding it with next available index.");
                id.m_Index = locale.CountUnchecked(id);
            }

            // Add the localized string
            locale.AddLocalizedString(id, localizedString.Value);

            // Set the string counts accordingly
            Dictionary<Locale.Key, int> localizedStringCounts = locale.GetLocalizedStringsCount();

            // The count at the exact index appears to always be 0
            localizedStringCounts[id] = 0;

            // index = 0 appears to be a special case and indicates the count of localized strings with the same identifier and key
            Locale.Key zeroIndexID = id;
            zeroIndexID.m_Index = 0;
            localizedStringCounts[zeroIndexID] = id.m_Index + 1;

            // Log message lags game on large namelists
            // Log($"Added localized string {id} = '{localizedString.Value}', count = {localizedStringCounts[zeroIndexID]}.");
        }
    }
}
