using System.Drawing;
using System.Windows.Forms;

namespace tilecon { 

    internal class TileButton : Button
    {
        private bool disposed = false;

        public ushort Index = 0;
        public Bitmap TileImage;


        public TileButton() : base()
        {
            BackgroundImageChanged += OnBGChanged;
            BackgroundImageLayout = ImageLayout.Stretch;
            BackColor = Color.Transparent;
            ForeColor = Color.Transparent;
            UseVisualStyleBackColor = false;
            FlatStyle = FlatStyle.Flat;
            SetStyle(ControlStyles.UserPaint, true); 
         }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            if (System.Windows.SystemParameters.HighContrast && BackgroundImage != null)
            {
                if (TileImage == null && !disposed)
                    TileImage = new Bitmap(BackgroundImage);

                pevent.Graphics.DrawImage(TileImage, pevent.ClipRectangle);
                
				// TODO: duplicate the images for outside of button grid because
				// if drawrect be called, will draw on top of BackgroundImage
				// and then dirt the output tileset
				//pevent.Graphics.DrawRectangle(Pens.Black, pevent.ClipRectangle);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;
	    
            TileImage?.Dispose();
            disposed = true;
            base.Dispose(disposing);
        }

        private void OnBGChanged(object sender, System.EventArgs args)
        {
            TileImage?.Dispose();
            TileImage = null;
        }
    }
}