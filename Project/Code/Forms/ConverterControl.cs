using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using tilecon.Tileset.Converter;

namespace tilecon
{
    /// <summary>
    /// Control from the convert tag from Tilecon
    /// </summary>

    public partial class ConverterControl : UserControl
    {
        private int bmpCurrentIndex;
        private string inputTilesetFilepath;
        private Image inputTileset;
        private Bitmap[] convertedBitmaps = new Bitmap[0];

        public Bitmap[] ConvertedTilesets 
        {
            get => convertedBitmaps is null ? new Bitmap[0] : convertedBitmaps;
            private set => convertedBitmaps = (value is null ? new Bitmap[0] : value);
        }

        [Browsable(true), Category("Action"), Description("Fires when the convert button is clicked")]
        public event EventHandler ConvertButtonClickedEvent;

        protected virtual void OnConvertButtonClicked(EventArgs e)
            => ConvertButtonClickedEvent?.Invoke(this, e);

        /// <summary>
        /// Occurs when value from the Checked from the IgnoreAlpha changes
        /// </summary>
        [Description("Fires when the value from IgnoreAlpha is changed")]
        public event EventHandler IgnoreAlphaCheckedChanged;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConverterControl()
        {
            InitializeComponent();

            checkBoxIgnoreAlpha.CheckedChanged += IgnoreAlphaCheckedChanged;
            ChangeLang();
        }

        /// <summary>
        /// Value from the check box IgnoreAlpha 
        /// </summary>
        public bool IgnoreAlpha
        {
            get => checkBoxIgnoreAlpha.Checked;
            set => checkBoxIgnoreAlpha.Checked = value;
        }

        /// <summary>
        /// Loads the tileset
        /// </summary>
        /// <param name="filename"></param>
        public void LoadTileset(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new NullReferenceException($"{nameof(filename)} is null or empty");

            inputTilesetFilepath = filename;
            inputTileset = Image.FromFile(filename);
            pictureBoxInput.Image = inputTileset;
            Enable();
        }

        /// <summary>
        /// Updates the text from the MV's tileset type in the label
        /// </summary>
        /// <param name="mode"></param>
        public void UpdateOutputLabel(SpriteMode mode)
        {
            TilesetConverterVX con = new TilesetConverterVX(new Maker.XP_Tile(), mode, IgnoreAlpha);

            switch (con.SetOutputTileset().TilesetName())
            {
                // TODO: get the substring from the tileset type then is MV instead
                case Maker.MV_A12.NAME:
                    labelMVTilesetName.Text = "A1-2";
                    break;
                case Maker.MV_A3.NAME:
                    labelMVTilesetName.Text = "A3";
                    break;
                case Maker.MV_A4.NAME:
                    labelMVTilesetName.Text = "A4";
                    break;
                case Maker.MV_A5.NAME:
                    labelMVTilesetName.Text = "A5";
                    break;
                case Maker.MV_BE.NAME:
                case Maker.Custom.NAME:
                    labelMVTilesetName.Text = "B-E";
                    break;
                default:
                    labelMVTilesetName.Text = "Character";
                    break;
            }
        }

        /// <summary>
        /// Converts the tileset to a RPG Maker MV tileset
        /// </summary>
        /// <param name="mode">Sprite mode</param>
        /// <param name="tileset">Input tileset kind</param>
        /// <param name="customSize">Tile size (only necessary with the tileset = Maker.Custom) </param>
        public void Convert(SpriteMode mode, ITileset tileset, int customSize = 0)
        {
            TilesetConverterBase con;
            btnConvert.Text = Vocab.GetText("wait");
            try
            {
                // TODO: add some reflection here...
                switch (tileset.TilesetName())
                {
                    case Maker.Alpha.NAME:
                        con = new TilesetConverterVerticalApha(tileset, mode, IgnoreAlpha);
                        break;

                    case Maker.R95.NAME:
                    case Maker.S97.NAME:
                    case Maker.XP_Tile.NAME:
                        con = new TilesetConverterVertical(tileset, mode, IgnoreAlpha);
                        break;

                    case Maker.XP_Auto.NAME:
                        con = new TilesetConverterAutotileXP(tileset, mode, IgnoreAlpha);
                        break;

                    case Maker.R2k_2k3_A.NAME:
                    case Maker.R2k_2k3_B.NAME:
                    case Maker.R2k_2k3_AB.NAME:
                    case Maker.R2k_2k3_Auto.NAME:
                    case Maker.R2k_2k3_AnimObj.NAME:
                        con = new TilesetConverterVerticalRM2K3(tileset, mode, IgnoreAlpha);
                        break;

                    case Maker.Custom.NAME:
                        con = new TilesetConverterCustom(mode, IgnoreAlpha, customSize);
                        break;

                    default:
                        con = new TilesetConverterVX(tileset, mode, IgnoreAlpha);
                        break;
                }
                convertedBitmaps = con.ConvertToMV(inputTileset);
            }
            catch (ConvertException ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }

            System.Diagnostics.Debug.Assert(convertedBitmaps != null);
            if (convertedBitmaps.Length == 0)
            {
                btnConvert.Text = Vocab.GetText("converter");
                return;
            }

            outputPictureBox.Image = convertedBitmaps[0];
            btnNextImg.Enabled = btnPreviusImg.Enabled = false;
            btnTransparency.Enabled = true;

            btnNextImg.Enabled = btnPreviusImg.Enabled = convertedBitmaps.Length > 1 ? true : false;

            bmpCurrentIndex = 0;
            labelMVPagesNumber.Text = bmpCurrentIndex + 1 + "/" + convertedBitmaps.Length;
            btnConvert.Text = Vocab.GetText("converter");
        }

        /// <summary>
        /// Saves the converted tilesets
        /// </summary>
        public void SaveTilesets()
        {
            string dir = inputTilesetFilepath;

            if (convertedBitmaps.Length == 1)
            {
                if (IsPlayerSprite())
                    dir = $@"{Path.GetDirectoryName(dir)}\!${Path.GetFileName(dir)}";
                convertedBitmaps[0].Save(dir);
            }
            else // multiple bitmaps
            {
                dir = $@"{Path.GetDirectoryName(dir)}\{Path.GetFileNameWithoutExtension(dir)}";
                for (int i = 0; i < convertedBitmaps.Length; i++)
                    convertedBitmaps[i].Save($"{dir}_{i + 1}.png");
            }
        }

        /// <summary>
        /// Defines a color to be considered tranparent
        /// </summary>
        public void SetTransparentColor()
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < convertedBitmaps.Length; i++)
                    convertedBitmaps[i] = ImageEditor.SetColorAsAlpha(convertedBitmaps[i], colorDialog.Color);
                
                if (convertedBitmaps.Length > 0)
                    outputPictureBox.Image = convertedBitmaps[bmpCurrentIndex];
            }
        }

        /// <summary>
        /// Raises IgnoreAlphaCheckedChanged
        /// </summary>
        /// <param name="e">The event</param>
        protected virtual void OnIgnoreAlphaCheckedChanged(EventArgs e)
            => IgnoreAlphaCheckedChanged?.Invoke(this, e);

        private bool IsPlayerSprite() // If the bitmap is a character (player sprite)
            => (convertedBitmaps[0].Width == 48 && convertedBitmaps[0].Height == 64) || (convertedBitmaps[0].Width == 144 && convertedBitmaps[0].Height == 192);

        private void Enable()
        {
            btnConvert.Enabled = true;
            checkBoxIgnoreAlpha.Enabled = true;

            // let it disabled if there is no converted images to add transparency
            if (convertedBitmaps.Length > 0)
                btnTransparency.Enabled = true;
        }

        private void NextImage()
        {
            if (++bmpCurrentIndex >= convertedBitmaps.Length)
                bmpCurrentIndex = 0;

            outputPictureBox.Image = convertedBitmaps[bmpCurrentIndex];
            labelMVPagesNumber.Text = bmpCurrentIndex + 1 + "/" + convertedBitmaps.Length;
        }

        private void PreviusImage()
        {
            if (--bmpCurrentIndex < 0)
                bmpCurrentIndex = convertedBitmaps.Length - 1;

            outputPictureBox.Image = convertedBitmaps[bmpCurrentIndex];
            labelMVPagesNumber.Text = bmpCurrentIndex + 1 + "/" + convertedBitmaps.Length;
        }

        internal void ChangeLang()
        {
            btnConvert.Text = Vocab.GetText("convert");
            btnTransparency.Text = Vocab.GetText("setTransparency");
            checkBoxIgnoreAlpha.Text = Vocab.GetText("ignoreAlpha");
            groupConversion.Text = Vocab.GetText("conversion");
        }

        private void btnConvert_Click(object sender, EventArgs e)
            => OnConvertButtonClicked(e);

        private void btnTransparency_Click(object sender, EventArgs e)
            => SetTransparentColor();

        private void btnPreviusImg_Click(object sender, EventArgs e)
            => PreviusImage();

        private void btnNextImg_Click(object sender, EventArgs e)
            => NextImage();

        private void checkBoxIgnoreAlpha_CheckedChanged(object sender, EventArgs e)
            => OnIgnoreAlphaCheckedChanged(e);
    }
}
