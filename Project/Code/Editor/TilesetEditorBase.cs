using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace tilecon.Tileset.Editor
{
    /// <summary>Base of the editor.</summary>
    public abstract class TilesetEditorBase : ImageEditor
    {

        /// <summary>Tileset information.</summary>
        protected ITileset tileset;

        /// <summary>Where the grid will be attached to.</summary>
        protected Control parent;


        internal readonly List<TileButton> grid = new List<TileButton>();
        protected readonly List<Bitmap> tiles = new List<Bitmap>();

        /// <summary>Default constructor.</summary>
        /// <param name="tileset">Tileset information.</param>
        /// <param name="control">Control to the grid be attached to.</param>
        public TilesetEditorBase(ITileset tileset, Control control)
        {
            this.tileset = tileset;
            this.parent = control;
        }

        /// <summary>Clear, dispose, and remove the grid from the attached control.</summary>
        public void ClearGrid()
        {
            foreach (var pb in grid) pb.Dispose();
            foreach (var bm in tiles) bm.Dispose();

            grid.Clear();
            tiles.Clear();
            parent?.Controls.Clear();
        }

        /// <summary>Set manually the image of a button for be used in the tests.</summary>
        /// <param name="index">Button index, -1 to set index as the grid count.</param>
        /// <param name="img">Image to be attached.</param>
        public void SetGridImage(int index, Image img)
        {
            if (index == -1) index = grid.Count - 1;
            grid[index].Image = img as Bitmap;
            //TODO:
            //grid[index].TileImage = img as Bitmap;
            tiles[index] = img as Bitmap;
        }

        /// <summary>Create a new button for the grid with the specify image.</summary>
        /// <param name="img">Image sprite to be used as background of button.</param>
        /// <param name="size">Size of the button.</param>
        /// <returns>The button.</returns>
        internal TileButton NewButton(Image img, int size)
        {
            return new TileButton
            { 
                Size = new Size(size + 1, size + 1), // Add 1 in size of the sprite
                BackgroundImage = img,
            };
        }

        /// <summary>Method called when a button of the grid is pressed.</summary>
        /// <param name="sender">Button that called the onclick handler.</param>
        /// <param name="e">EvenArgs params.</param>
        protected abstract void ButtonClickEventHandler(object sender, EventArgs e);

        /// <summary>Set Up the grid.</summary>
        protected abstract void SetUpGrid();
    }
}