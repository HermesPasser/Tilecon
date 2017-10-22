using System.Drawing;
using System.Reflection;
using System.IO;

namespace tilecon.Converter.Tests
{
    public class TilesetConverterTestBase
    {
        public TilesetConverterBase converter;
        private Stream stream;

        ~TilesetConverterTestBase()
        {
            if (stream != null)
                stream.Close();
        }

        protected Bitmap BitmapFromResourceStream(string imageName)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            stream = myAssembly.GetManifestResourceStream(imageName);
            return new Bitmap(stream);
        }
    }
}
