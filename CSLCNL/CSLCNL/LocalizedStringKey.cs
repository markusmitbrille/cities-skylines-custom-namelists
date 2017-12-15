using System.Xml.Serialization;

namespace Makaki.CustomNameLists
{
    public struct LocalizedStringKey
    {
        [XmlAttribute(AttributeName = "identifier")]
        public string Identifier;
        [XmlAttribute(AttributeName = "key")]
        public string Key;
    }
}
