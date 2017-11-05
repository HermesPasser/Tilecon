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

        /// <summary>Where the grid will be attached.</summary>
        protected Control control;

        /// <summary>List with all the buttons used to make the grid.</summary>
        protected List<Button> grid;

        ///// <summary>Get method for grid with all the buttons.</summary>
        //public List<Button> Grid
        //{
        //    get { return Grid; }
        //}

        /// <summary>Default constructor.</summary>
        /// <param name="tileset">Tileset information.</param>
        /// <param name="control">Control to the grid be attached to.</param>
        public TilesetEditorBase(ITileset tileset, Control control)
        {
            this.tileset = tileset;
            this.control = control;
        }

        /// <summary>Set manually the image of a button for be used in the tests.</summary>
        /// <param name="index">Button index, -1 to set index as the grid count.</param>
        /// <param name="img">Image to be attached.</param>
        public void SetGridImage(int index, Image img)
        {
            if (index == -1) index = grid.Count - 1;
            grid[index].BackgroundImage = img;
        }

        ///// <summary>Get  the image of a button for be used in the tests.</summary>
        ///// <param name="index">Button index.</param>
        //public Image GetGridImage(int index)
        //{
        //    return grid[index].BackgroundImage;
        //}

        /// <summary>Create a new button for the grid with the specify image.</summary>
        /// <param name="img">Image sprie to be used as background of button.</param>
        /// <param name="size">Size of the button.</param>
        /// <returns>The button.</returns>
        protected Button NewButton(Image img, int size)
        {
            return new Button
            {
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Transparent,
                Size = new Size(size + 1, size + 1), // Add 1 in size of the sprite
                UseVisualStyleBackColor = false,
                BackgroundImage = img,
                BackgroundImageLayout = ImageLayout.Stretch
            };
        }

        /// <summary>Method calle when a button of the grid is pressed.</summary>
        /// <param name="sender">Button that called the onclick handler.</param>
        /// <param name="e">EvenArgs params.</param>
        protected abstract void ButtonClickEventHandler(object sender, EventArgs e);

        /// <summary>Set Up the grid.</summary>
        protected abstract void SetUpGrid();
    }
}
