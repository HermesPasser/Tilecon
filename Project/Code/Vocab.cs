using System.Reflection;
using System.Xml;
using System.IO;

namespace tilecon
{
    static class Vocab
    {
        public enum lang{
            pt, en
        }

        public static string version = "1.8.3";
        public static string aboutHelpText = "2017 - Hermes Passer (hermespasser@gmail.com)";

        public  static lang currentLanguage = lang.en;
        private static XmlDocument xml = null;

        private static void init()
        {
            if (xml == null)
            {
                xml = new XmlDocument();

                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream("tilecon.Resources.stringtable.xml");

                xml.Load(stream);
                stream.Close();
            }
        }

        public static string GetText(string text)
        {
            init();
            XmlNode list = xml.GetElementsByTagName(text)[0];
            return list.Attributes.GetNamedItem(currentLanguage.ToString()).InnerXml;
        }
    }
}
