using ColossalFramework;
using ColossalFramework.Globalization;
using System.Xml.Serialization;

using static Makaki.CustomNameLists.DebugUtils;

namespace Makaki.CustomNameLists
{
    public class BlackList
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name;
        [XmlArray(ElementName = "strings")]
        public LocalizedStringKey[] Keys;

        public void Apply()
        {
            LocaleManager localeManager = SingletonLite<LocaleManager>.instance;
            foreach (LocalizedStringKey key in Keys)
            {
                localeManager.RemoveRange(new Locale.Key()
                {
                    m_Identifier = key.Identifier,
                    m_Key = key.Key
                });
            }

            Log($"Blacklist {Name} applied.");
        }
    }
}
