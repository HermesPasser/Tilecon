using System.Drawing;
using System.Reflection;
using System.IO;

namespace tilecon.Converter.Tests
{
    public class TilesetConverterTestBase
    {
        public TilesetConverterBase converter;

        protected Bitmap BitmapFromResourceStream(string imageName)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream(imageName);
            return new Bitmap(myStream);
        }
    }
}
