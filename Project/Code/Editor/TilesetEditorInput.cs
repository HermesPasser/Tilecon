using System;
using System.Drawing;
using System.Windows.Forms;
using tilecon.Core.Converter;
using tilecon.Core;

namespace tilecon.Tileset.Editor
{
    /// <summary>Class that control the grid with the loaded tileset.</summary>
    public class TilesetEditorIntput : TilesetEditorBase
    {
        private PictureBox preview;
        private SpriteMode mode;

        /// <summary>Current tile of a button in the grid.</summary>
        public Image selectedImage;

        /// <summary>Image of the tileset.</summary>
        protected Image tilesetImage;

        /// <summary>Current tile of a without sprite mode changes.</summary>
        protected Image selectedImageRaw;

        /// <summary>Default constructor.</summary>
        /// <param name="tileset">Tileset type and data.</param>
        /// <param name="control">Where the grid will be attached.</param>
        /// <param name="tilesetImage">Image of the tileset.</param>
        /// <param name="preview">PictureBox where the selectedImage is shown.</param>
        public TilesetEditorIntput(ITileset tileset, Control control, Image tilesetImage, PictureBox preview) : base(tileset, control)
        {
            this.preview = preview;
            this.tilesetImage = tilesetImage;
            this.mode = SpriteMode.ALIGN_TOP_LEFT;
            SetUpGrid();
        }

        /// <summary>Implementation of the abstract method ButtonClickEventHandler.</summary>
        /// <param name="sender">Button that called the onclick handler.</param>
        /// <param name="e">EvenArgs params.</param>
        protected override void ButtonClickEventHandler(object sender, EventArgs e)
        {
            SetSelectedImage(sender);
        }

        private void SetSelectedImage(object sender)
        {
            selectedImageRaw = ((TileButton)sender).BackgroundImage;
            UpdateSelectedImage(mode);
        }

        /// <summary>Set the selectedImage by the index</summary>
        /// <param name="index">Index.</param>
        public void SetSelectedImage(int index)
        {
            SetSelectedImage(grid[index]);
        }

        /// <summary>Update the sprite of the preview.</summary>
        /// <param name="mode">Mode of the sprite.</param>
        public void UpdateSelectedImage(SpriteMode mode)
        {
            selectedImage = selectedImageRaw;
            this.mode = mode;

            if (selectedImage == null)
                return;

            TilesetConverterBase con;
            con = GetTilesetConverter(mode);
            selectedImage = con.SetModeInSprite(selectedImage, Core.Tileset.MV_A12.Size); // must not be mv sprite size, add param to set this
            if (preview != null) preview.Image = selectedImage;
        }

        private TilesetConverterBase GetTilesetConverter(SpriteMode mode)
        {
            TilesetConverterBase con;
            string name = tileset.TilesetName();

            if (name == Core.Tileset.Alpha.Name)
            {
                con = new TilesetConverterVerticalApha(tileset, mode, false);
            }
            else if (name == Core.Tileset.R95.Name || name == Core.Tileset.S97.Name || name == Core.Tileset.XP_Tile.Name)
            {
                con = new TilesetConverterVertical(tileset, mode, false);
            }
            else if (name == Core.Tileset.XP_Auto.Name)
            {
                con = new TilesetConverterAutotileXP(tileset, mode, false);
            } 
            else if (Core.Tileset.IsR2k_2k3((Core.Tileset)tileset) && name != Core.Tileset.R2k_2k3_AnimObj.Name)
            { // all 2000/2003 tilesets but not an animated object
                con = new TilesetConverterVerticalRM2K3(tileset, mode, false);
            }
            else if (name == Core.Tileset.Custom(0).Name)
            {
                con = new TilesetConverterCustom(tileset, mode, false);
            } 
            else
            {
                con = new TilesetConverterVX(tileset, mode, false);
            }

            return con;
        }

        /// <summary>Implementation of the abstract method SetUpGrid.</summary>
        protected override void SetUpGrid()
        {
            int width = tileset.SizeWidth();
            int height = tileset.SizeHeight();
            int spriteSize = tileset.TileSize();

            if (width == -1) width = tilesetImage.Width;   // custom
            if (height == -1) height = tilesetImage.Height; // XP and custom

            if (spriteSize <= 0)
                throw new ConvertException(Vocab.GetText("sizeIsZeroErrorMsg"));

            Bitmap[] tiles = SplitImageInSprites(tilesetImage, tileset.TileSize());

            ClearGrid();
            ushort i = 0;

            // Verify all the options to see if throws any exception.
            try
            {
                for (int y = 0; y < height; y += spriteSize)
                {
                    for (int x = 0; x < width; i++, x += spriteSize)
                    {
                        TileButton btn = NewButton(tiles[i], spriteSize);
                        btn.Click += new EventHandler(ButtonClickEventHandler);
                        grid.Add(btn);
                        this.tiles.Add(tiles[i]);
                        btn.Index = i;

                        if (parent != null)
                        {
                            parent.Controls.Add(btn);
                            btn.Location = new Point(x, y);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new ConvertException(Vocab.GetText("sizeNotMatchErrorMsg"));
            }
        }
    }
}