using ColossalFramework;
using ColossalFramework.Globalization;
using System.Xml.Serialization;

using static Makaki.CustomNameLists.DebugUtils;

namespace Makaki.CustomNameLists
{
    public class NameList
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name;
        [XmlArray(ElementName = "strings")]
        public LocalizedString[] LocalizedStrings;

        public void Apply()
        {
            LocaleManager localeManager = SingletonLite<LocaleManager>.instance;
            foreach (LocalizedString localizedString in LocalizedStrings)
            {
                localeManager.AddString(localizedString);
            }

            Log($"Namelist {Name} applied.");
        }
    }
}
