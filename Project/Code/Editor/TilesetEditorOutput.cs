﻿using System;
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

            if (tileset.TilesetName() == Tileset.XP_Tile.Name)
                throw new ConvertException("Tileset cannot be XP because this class not contain the information of original image and the default is -1.");
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
            if (input == null)
                return;

            var tileBtn = (TileButton)sender;
            var bmp = input.selectedImage as Bitmap;

            tileBtn.BackgroundImage = bmp;
            tiles[tileBtn.Index] = bmp;
        }

        /// <summary>Make a tileset of a bitmaps list.</summary>
        /// <returns>Return the tileset as bitmap.</returns>
        public Bitmap TilesToTileset()
        {
            // Get the image of buttons
            List<Bitmap> bmps = new List<Bitmap>();
            foreach (var b in tiles) bmps.Add(b);
           
            int width = tileset.SizeWidth();
            int height = tileset.SizeHeight();
            int spriteSize = tileset.TileSize();

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
            int spriteSize = this.tileset.TileSize();
            ushort i = 0;

            ClearGrid();

            // Verify all the options to see if throws any exception.
            try
            {
                for (int y = 0; y < height; y += spriteSize)
                {
                    for (int x = 0; x < width; x += spriteSize, i++)
                    {
                        TileButton btn = NewButton(null, spriteSize);
                        btn.Click += new EventHandler(ButtonClickEventHandler);
                        grid.Add(btn);
                        tiles.Add(null);
                        btn.Index = i;

                        if (parent != null)
                        {
                            parent.Controls.Add(btn);
                            btn.Location = new Point(x, y);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException) {
                throw new ConvertException(Vocab.GetText("sizeNotMatchErrorMsg"));
            }
        }
    }
}