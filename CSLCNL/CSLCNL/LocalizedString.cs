using System.Xml.Serialization;

namespace Makaki.CustomNameLists
{
    public struct LocalizedString
    {
        [XmlAttribute(AttributeName = "identifier")]
        public string Identifier;
        [XmlAttribute(AttributeName = "key")]
        public string Key;
        [XmlText]
        public string Value;
    }
}