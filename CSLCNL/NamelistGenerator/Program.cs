using Makaki.CustomNameLists;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NamelistGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvertCSVToNameList(@"C:\Users\makak\Desktop\vienna.txt");
        }

        static void ConvertCSVToNameList(string csvFile)
        {
            List<LocalizedString> strings = new List<LocalizedString>();

            string[] rows = File.ReadAllText(csvFile).Split('\r', '\n');
            foreach (string row in rows)
            {
                string[] cols = row.Split(',');
                string identifier = cols[0];
                string key = cols[1];
                string value = cols[2];

                if (string.IsNullOrEmpty(identifier) ||
                    string.IsNullOrEmpty(key) ||
                    string.IsNullOrEmpty(value))
                {
                    continue;
                }

                strings.Add(new LocalizedString()
                {
                    Identifier = identifier,
                    Key = key,
                    Value = value
                });
            }

            using (FileStream fs = new FileStream($"{Path.GetDirectoryName(csvFile)}/{Path.GetFileNameWithoutExtension(csvFile)}.namelist", FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                new XmlSerializer(typeof(NameList)).Serialize(sw, new NameList() { Name = $"Generated Namelist ({Path.GetFileNameWithoutExtension(csvFile)})", LocalizedStrings = strings.ToArray() });
            }
        }

        static void ConvertNamelistToBlackList(string nameListFile)
        {
            if (!File.Exists(nameListFile))
            {
                return;
            }

            NameList nameList = null;

            using (XmlReader stream = XmlReader.Create(nameListFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(NameList));
                if (serializer.CanDeserialize(stream))
                {
                    nameList = serializer.Deserialize(stream) as NameList;
                }
            }

            if (nameList == null)
            {
                return;
            }

            HashSet<LocalizedStringKey> keys = new HashSet<LocalizedStringKey>();
            foreach (LocalizedString ls in nameList.LocalizedStrings)
            {
                keys.Add(new LocalizedStringKey()
                {
                    Identifier = ls.Identifier,
                    Key = ls.Key
                });
            }

            keys.Distinct();

            BlackList blackList = new BlackList()
            {
                Name = $"Generated Blacklist ({nameList.Name})",
                Keys = keys.ToArray()
            };

            using (FileStream stream = new FileStream($"{Path.GetDirectoryName(nameListFile)}/{Path.GetFileNameWithoutExtension(nameListFile)}.blacklist", FileMode.Create, FileAccess.Write))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BlackList));
                serializer.Serialize(stream, blackList);
            }
        }
    }
}
