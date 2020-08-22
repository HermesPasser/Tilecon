using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using tilecon.Tileset.Editor;

namespace tilecon
{
    /// <summary>Tileset RM<x> to RMMV</x>editor control</summary>
    public partial class EditorControl : UserControl
    {
        private TilesetEditorIntput gridIn;
        private TilesetEditorOutput gridOut;
        private ITileset mvTilesetType;
        private string originFilename;
        private int customSpriteSize;

        [Description("add...")]
        public event EventHandler TilesetLoaded;

        protected virtual void OnTilesetLoaded(EventArgs e)
        {
            TilesetLoaded?.Invoke(this, e);
        }

        /// <summary>Default constructor.</summary>
        public EditorControl()
        {
            InitializeComponent();
            cbOutput.SelectedIndex = 4;
            ChangeLang();
        }

        /// <summary>Clear the preview PictureBox.</summary>
        public void ClearPreview()
        {
            pictureBoxPreview.Image = null;
            if (gridIn != null)
                gridIn.selectedImage = null;
        }

        /// <summary>Saves the edited tileset</summary>
        /// <param name="filename">the file name</param>
        public void Save(string filename)
        {
            (gridOut?.TilesToTileset()).Save(filename);
        }

        public void LoadTileset(string originTilesetFilename, ITileset originTileset)
        {
            btnSetInputTileset.Enabled = true;
            mvTilesetType = originTileset;
            originFilename = originTilesetFilename;
        }

        /// <summary>Stores the value to set Maker.Custom.SPRITE_SIZE when generete the grid.</summary>
        public int CustomSpriteSize
        {
            set => customSpriteSize = value;
            get => customSpriteSize;
        }

        public void LoadGrid()
        {
            Maker.Custom.SPRITE_SIZE = customSpriteSize;

            if (mvTilesetType.TilesetName() == Maker.Custom.NAME && Maker.Custom.SPRITE_SIZE == 0)
            {
                MessageBox.Show(Vocab.GetText("sizeIsZeroErrorMsg"));
                return;
            }

            Image img = Image.FromFile(originFilename);
            pictureBoxPreview.Image = null;

            try
            {
                gridIn = new TilesetEditorIntput(mvTilesetType, inputPanel, img, pictureBoxPreview);
                SetOutputGrid();
            }
            catch (ConvertException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>Update the input tileset image.</summary>
        /// <param name="mode"></param>
        public void UpdateImage(SpriteMode mode)
        {
            gridIn?.UpdateSelectedImage(mode);   
        }

        /// <summary>Set up the output grid with the selected mv tileset type.</summary>
        public void SetOutputGrid()
        {
            ITileset tileset;

            switch (cbOutput.SelectedIndex)
            {
                case 0: tileset = new Maker.MV_A12(); break;
                case 1: tileset = new Maker.MV_A3(); break;
                case 2: tileset = new Maker.MV_A4(); break;
                case 3: tileset = new Maker.MV_A5(); break;
                case 4:
                default: tileset = new Maker.MV_BE(); break;
            }
            gridOut = new TilesetEditorOutput(tileset, outputPanel, gridIn);
        }

        internal void ChangeLang()
        { 

            btnSetInputTileset.Text = Vocab.GetText("setTileset");
            btnClearPreview.Text = Vocab.GetText("clearSelectedTile");
            btnClearAndSet.Text = Vocab.GetText("clearAndSetTileset");
        }

        /// <summary>Defines the value of the CheckBox that shows the output tileset type (0: A1-2, 1: A3, 2: A4, 3: A5, 4: B-C).</summary>
        public int OutputComboBoxCurrentIndex
        {
            set => cbOutput.SelectedIndex =  (value < cbOutput.Items.Count) 
                ? value 
                : OutputComboBoxCurrentIndex;
            get => cbOutput.SelectedIndex;
    }
		
        private void BtnClearAndSet_Click(object sender, EventArgs e)
        {
            SetOutputGrid();
        }

        private void BtnClearPreview_Click(object sender, EventArgs e)
        {
			ClearPreview();
        }

        private void BtnSetInputTileset_Click(object sender, EventArgs e)
        {
            OnTilesetLoaded(e);
            LoadGrid();
        }
    }
}
