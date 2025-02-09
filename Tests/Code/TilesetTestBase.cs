using System.Reflection;
using System.Drawing;
using System.IO;
using tilecon.Core.Converter;
using tilecon.Tileset.Editor;

namespace tilecon.Tileset.Tests
{
    public class TilesetTestBase
    {
        public TilesetConverterBase converter;
        public TilesetEditorIntput editorInput;
        public TilesetEditorOutput editorOutput;

        private Stream stream;

        ~TilesetTestBase()
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
