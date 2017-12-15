using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Plugins;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using static Makaki.CustomNameLists.DebugUtils;

namespace Makaki.CustomNameLists
{
    static class NameListManager
    {
        private const string BlackListSearchPattern = "*.blacklist";
        private const string NameListSearchPattern = "*.namelist";

        private const string LocaleExportFileName = "complete.namelist";

        public static void PrintLocale()
        {
            if (!Directory.Exists(Settings.LocaleExportDirectory))
            {
                Error($"Could not print locale; directory '{Settings.LocaleExportDirectory}' not found!");
                return;
            }

            List<LocalizedString> localizedStrings = new List<LocalizedString>();
            foreach (var localizedString in SingletonLite<LocaleManager>.instance.GetLocale().GetLocalizedStrings())
            {
                localizedStrings.Add(new LocalizedString()
                {
                    Identifier = localizedString.Key.m_Identifier,
                    Key = localizedString.Key.m_Key,
                    Value = localizedString.Value
                });
            }

            using (FileStream stream = new FileStream(Path.Combine(Settings.LocaleExportDirectory, LocaleExportFileName), FileMode.Create, FileAccess.ReadWrite))
            {
                NameList completeNameList = new NameList()
                {
                    Name = "Complete Name List",
                    LocalizedStrings = localizedStrings.ToArray()
                };

                XmlSerializer serializer = new XmlSerializer(typeof(NameList));
                serializer.Serialize(stream, completeNameList);
            }

            Log($"Printed locale to {Settings.LocaleExportDirectory}.");
        }

        public static void ApplyBlackLists()
        {
            if (!Settings.UseBlacklists)
            {
                Warning("Blacklists are disabled in mod settings; returning!");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(BlackList));

            foreach (string blackListFile in
                from pluginInfo in Singleton<PluginManager>.instance.GetPluginsInfo()
                where pluginInfo.isEnabled
                from file in Directory.GetFiles(pluginInfo.modPath, BlackListSearchPattern, SearchOption.AllDirectories)
                select file)
            {
                using (XmlReader stream = XmlReader.Create(blackListFile))
                {
                    if (serializer.CanDeserialize(stream))
                    {
                        BlackList blackList = (BlackList)serializer.Deserialize(stream);
                        blackList.Apply();
                    }
                }
            }

            Log("Blacklists applied.");
        }

        public static void ApplyNameLists()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(NameList));

            foreach (string nameListFile in
                from pluginInfo in Singleton<PluginManager>.instance.GetPluginsInfo()
                where pluginInfo.isEnabled
                from file in Directory.GetFiles(pluginInfo.modPath, NameListSearchPattern, SearchOption.AllDirectories)
                select file)
            {
                using (XmlReader stream = XmlReader.Create(nameListFile))
                {
                    if (serializer.CanDeserialize(stream))
                    {
                        NameList nameList = (NameList)serializer.Deserialize(stream);
                        nameList.Apply();
                    }
                }
            }

            Log("Namelists applied.");
        }
    }
}
