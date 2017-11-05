using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using tilecon.Tileset.Converter;

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
            selectedImageRaw = ((Button)sender).BackgroundImage;
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
            switch (tileset.TilesetName())
            {
                case Maker.Alpha.NAME:
                    con = new TilesetConverterVerticalApha(tileset, mode, false);
                    break;
                case Maker.R95.NAME:
                case Maker.S97.NAME:
                case Maker.XP_Tile.NAME:
                    con = new TilesetConverterVertical(tileset, mode, false);
                    break;
                case Maker.XP_Auto.NAME:
                    con = new TilesetConverterAutotileXP(tileset, mode, false);
                    break;
                case Maker.R2k_2k3_A.NAME:
                case Maker.R2k_2k3_B.NAME:
                case Maker.R2k_2k3_AB.NAME:
                case Maker.R2k_2k3_Auto.NAME:
                    con = new TilesetConverterVerticalRM2K3(tileset, mode, false);
                    break;
                default:
                    con = new TilesetConverterVX(tileset, mode, false);
                    break;
            }
            selectedImage = con.SetModeInSprite(selectedImage, Maker.MV_A12.SPRITE_SIZE); // must not be mv sprite size, add param to set this
            if (preview != null) preview.Image = selectedImage;   
        }

        /// <summary>Implementation of the abstract method SetUpGrid.</summary>
        protected override void SetUpGrid()
        {
            int width = tileset.SizeWidth();
            int height = tileset.SizeHeight();
            int spriteSize = tileset.SpriteSize();
          //  System.Windows.Forms.MessageBox.Show(height + "");
            if (height == -1) height = tilesetImage.Height;

            TilesetConverterVerticalRM2K3 con = new TilesetConverterVerticalRM2K3(tileset, SpriteMode.ALIGN_TOP_LEFT, false);
            Bitmap[] tiles = con.GetSprites(tilesetImage as Bitmap).ToArray();

            if (control != null) control.Controls.Clear();
            grid = new List<Button>();
            int i = 0;

            // Verificar todas as possibilidade para ver se alguma vez cai na exception
            try
            {
                for (int y = 0; y < height; y += spriteSize)
                {
                    for (int x = 0; x < width; i++, x += spriteSize)
                    {
                        Button btn = NewButton(tiles[i], spriteSize);
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
