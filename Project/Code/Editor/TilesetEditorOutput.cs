using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace tilecon.Tileset.Editor
{
    /// <summary>Custom tileset grid controller.</summary>
    public class TilesetEditorOutput : TilesetEditorBase
    {
        private TilesetEditorIntput input;

        /// <summary></summary>
        /// <param name="tileset">Tileset type and data.</param>
        /// <param name="control">Where the grid will be attached.</param>
        /// <param name="input">Where is the data of tiles to be attached in the buttons of the grid.</param>
        public TilesetEditorOutput(ITileset tileset, Control control, TilesetEditorIntput input) : base(tileset, control)
        {
            this.input = input;

            if (tileset.GetType() == new Maker.XP_Tile().GetType())
                throw new Exception("Tileset cannot be XP because this class not contain the information of original image and the default is -1.");
            SetUpGrid();
        }

        /// <summary>Implementation of the abstract method ButtonClickEventHandler.</summary>
        /// <param name="sender">Button that called the onclick handler.</param>
        /// <param name="e">EvenArgs params.</param>
        protected override void ButtonClickEventHandler(object sender, EventArgs e)
        {
            SetTileInButton(sender);
        }

        private void SetTileInButton(object sender)
        {
            if (input != null)
               ((Button)sender).BackgroundImage = input.selectedImage;
        }

        /// <summary>Make a tileset of a bitmaps list.</summary>
        /// <returns>Return the tileset as bitmap.</returns>
        public Bitmap TilesToTileset()
        {
            // Get the image of buttons
            List<Bitmap> bmps = new List<Bitmap>();
            foreach (Button b in grid) bmps.Add(b.BackgroundImage as Bitmap);

            int width = tileset.SizeWidth();
            int height = tileset.SizeHeight();
            int spriteSize = tileset.SpriteSize();

            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0, i = 0; y < height; y += spriteSize)
            {
                for (int x = 0; x < width; i++, x += spriteSize)
                {
                    if (bmps[i] != null)
                        bmp = Paste(bmp, bmps[i], x, y, spriteSize, spriteSize);
                }
            }
            return bmp;
        }

        /// <summary>Implementation of the abstract method SetUpGrid.</summary>
        protected override void SetUpGrid()
        {
            int width = this.tileset.SizeWidth();
            int height = this.tileset.SizeHeight();
            int spriteSize = this.tileset.SpriteSize();

            if (control != null) control.Controls.Clear();
            grid = new List<Button>();

            // Verificar todas as possibilidade para ver se alguma vez cai na exception
            try
            {
                for (int y = 0; y < height; y += spriteSize)
                {
                    for (int x = 0; x < width; x += spriteSize)
                    {
                        Button btn = NewButton(null, spriteSize);
                        btn.Click += new EventHandler(ButtonClickEventHandler);
                        grid.Add(btn);
                        if (control != null)
                        {
                            control.Controls.Add(btn);
                            btn.Location = new Point(x, y);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException) { System.Windows.Forms.MessageBox.Show("index out of range ex in set up grid"); }
        }
    }
}
