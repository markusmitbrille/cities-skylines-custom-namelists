using ColossalFramework;
using ColossalFramework.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

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
        }
    }
}
