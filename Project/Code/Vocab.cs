using System.Reflection;
using System.Xml;
using System.IO;

namespace tilecon
{
    static class Vocab
    {
        public enum Lang{
            pt, en
        }

        public static string version = "2.2";
        public  static Lang currentLanguage = Lang.en;
        private static XmlDocument xml = null;

        private static void Init()
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
            Init();
            XmlNode list = xml.GetElementsByTagName(text)[0];
            return list.Attributes.GetNamedItem(currentLanguage.ToString()).InnerXml;
        }
    }
}
