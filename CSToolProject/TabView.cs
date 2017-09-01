// Using, etc
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

//--------------------------------------------------------------------------------------
// Namespace CSToolProject
//--------------------------------------------------------------------------------------
namespace CSToolProject
{
    //--------------------------------------------------------------------------------------
    // The TabView object, UserControl.
    //--------------------------------------------------------------------------------------
    public partial class TabView : UserControl
    {

        // Piece of code to fix blurring issues.
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;
				return cp;
			}
		}

		// paint color.
		Color PencilColor = Color.Red;

        //Altered bool
        bool altered = false;

        // bool for if the file is new or not.
        bool NewFile = false;

		// Bool for if drawing can be done or not.
		bool draw = false;

        // File directory 
        string directory;

        // x and y for the drawing tool.
        int x = 0;
		int y = 0;

		// The zoom level of the image
		int zoomfactor = 1;

		// Var for Form1
		Form1 form_1;

        // Background color of the picturebox.
        Color BackgroundColor = Color.White;

        // the image being drawn to
        Image image;

		// Form setter. For getting data from form1 and other UserContorls.
		public void SetForm(Form1 f)
		{
			form_1 = f;
		}

		// Picturebox1 Setter
		public Image GetImage()
		{
			return image;
		}

		// Set the image of picturebox.
		public void SetImage(Image i)
        {
            // Set vars
            image = i;

            // Set width and height
            pictureBox1.Height = i.Height;
			pictureBox1.Width = i.Width;

            // Call auto zoom.
            AutoZoomStart();
        }

        // Setter for directory and format
        public void SetDirectory(string dir)
        {
            directory = dir;
        }

        // Directory getter
        public string GetDirectory()
        {
            return directory;
        }

		// Picturebox1 Setter
		public PictureBox GetPictureBox()
		{
			return pictureBox1;
		}

		// Picturebox1 Setter
		public void SetPictureBox(PictureBox p)
		{
			pictureBox1 = p;
		}

        // background color getter
        public Color GetBackgroundColor()
        {
            return BackgroundColor;
        }

        // background color setter
        public void SetBackgroundColor(Color c)
        {
            BackgroundColor = c;
            pictureBox1.BackColor = BackgroundColor;
        }

        // Altered getter.
        public bool GetAltered()
        {
            return altered;
        }

        // Altered setter.
        public void SetAltered(bool b)
        {
            altered = b;
        }

        // NewFile getter.
        public bool GetNewFile()
        {
            return NewFile;
        }

        // NewFile setter.
        public void SetNewFile(bool b)
        {
            NewFile = b;
        }

        //--------------------------------------------------------------------------------------
        // Default Constructor.
        //--------------------------------------------------------------------------------------
        public TabView()
        {
            InitializeComponent();
		}

		//--------------------------------------------------------------------------------------
		// pictureBox1_Resize: Function called when a resize of the picturebox occurs.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: EventArgs type, represents the base class for classes that cotain event data.
		//--------------------------------------------------------------------------------------
		private void pictureBox1_Resize(object sender, EventArgs e)
        {

        }

		//--------------------------------------------------------------------------------------
		// pictureBox1_MouseClick: Functon for when the mouse is pressed.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: MouseEventArgs type, Provides data for the MouseUp, MouseDown, and MouseMove events.
		//--------------------------------------------------------------------------------------
		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // Zoomin tool.
			if (form_1.GetToolType() == ToolType.ZoomIn)
				ZoomInCanvas();

            // Zoomout tool.
			if (form_1.GetToolType() == ToolType.ZoomOut)
				ZoomOutCanvas();
		}

		//--------------------------------------------------------------------------------------
		// pictureBox1_MouseDown: Function for when the mouse is down.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: MouseEventArgs type, Provides data for the MouseUp, MouseDown, and MouseMove events.
		//--------------------------------------------------------------------------------------
		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
			// set drawing to true
			draw = true;

			// set the mouse x and y
			x = e.X;
			y = e.Y;

			// Draw for single click
			DrawToImage(sender, e);
		}

		//--------------------------------------------------------------------------------------
		// pictureBox1_MouseUp: Function for when the mouse is up.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: MouseEventArgs type, Provides data for the MouseUp, MouseDown, and MouseMove events.
		//--------------------------------------------------------------------------------------
		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			// set drawing to false.
			draw = false;
		}

		//--------------------------------------------------------------------------------------
		// pictureBox1_MouseMove: Function for when the mouse moves.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: MouseEventArgs type, Provides data for the MouseUp, MouseDown, and MouseMove events.
		//--------------------------------------------------------------------------------------
		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Draw to image when moving mouse
			DrawToImage(sender, e);
		}

        //--------------------------------------------------------------------------------------
        // DrawToImage: The drawtoimage function, used drawing the pencil and eraser tool to image.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: MouseEventArgs type, Provides data for the MouseUp, MouseDown, and MouseMove events.
        //--------------------------------------------------------------------------------------
        private void DrawToImage(object sender, MouseEventArgs e)
		{
			// If able to draw
			if (draw)
			{
				// Create grpahics.
				using (Graphics g = Graphics.FromImage(image))
				{
                    // Set compositing mode.
					g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

					// Switch statement for which tool is selected.
					switch (form_1.GetToolType())
					{
						// Pencil Tool
						case ToolType.Pencil:

							// Draw to screen with the pencil tool.
							g.FillRectangle(new SolidBrush(form_1.GetPencilColor()), ((e.X - x + x) - (form_1.GetToolSize() / 2)) / zoomfactor, ((e.Y - y + y) - (form_1.GetToolSize() / 2)) / zoomfactor, form_1.GetToolSize(), form_1.GetToolSize());
							pictureBox1.Invalidate();
							pictureBox1.Refresh();				// divide by zero error?? Maybe something to do with zoom.
							break;

						// Eraser Tool
						case ToolType.Eraser:

							// erase part of image.
							g.FillRectangle(new SolidBrush(Color.Transparent), ((e.X - x + x) - (form_1.GetToolSize() / 2)) / zoomfactor, ((e.Y - y + y) - (form_1.GetToolSize() / 2)) / zoomfactor, form_1.GetToolSize(), form_1.GetToolSize());
							pictureBox1.Invalidate();
							pictureBox1.Refresh();              // divide by zero error?? Maybe something to do with zoom.
							break;
					}
				}

                // Set the image to altered.
                altered = true;
            }
        }

        //--------------------------------------------------------------------------------------
        // AutoZoomStart: Auto zoom the image in depending on image size.
        //--------------------------------------------------------------------------------------
        private void AutoZoomStart()
        {
            // Check the image size.
            if (pictureBox1.Height < 400 && pictureBox1.Width < 400)
            {
                // Zoom in 3 times.
                ZoomInCanvas();
                ZoomInCanvas();
                ZoomInCanvas();
            }
        }

        //--------------------------------------------------------------------------------------
        // ZoomInCanvas: Function used for working out the zoom level.
        //--------------------------------------------------------------------------------------
        private void ZoomInCanvas()
		{
			if (zoomfactor < 18)
			{
                // Update zoomfactor
				zoomfactor *= 2;

                // apply to image.
                pictureBox1.Height *= 2;
				pictureBox1.Width *= 2;
				pictureBox1.Refresh();
			}
		}

        //--------------------------------------------------------------------------------------
        // ZoomOutCanvas: Function used for working out the zoom level.
        //--------------------------------------------------------------------------------------
        private void ZoomOutCanvas()
		{
			if (zoomfactor > 1)
			{
                // Update zoomfactor
				zoomfactor /= 2;
                
                // apply to image.
				pictureBox1.Height /= 2;
				pictureBox1.Width /= 2;
				pictureBox1.Refresh();
			}
		}

        //--------------------------------------------------------------------------------------
        // pictureBox1_MouseEnter: Function for when the mouse enters the picturebox.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: EventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
		{
            // Focus the image, used for scrolling with scrollwheel.
			pictureBox1.Focus();
		}

        //--------------------------------------------------------------------------------------
        // pictureBox1_Paint: Used for the drawing of the picturebox.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: PaintEventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
            // Return until image isnt null.
			if (image == null)
				return;

            // Draw the image to the pixturebox
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			e.Graphics.SmoothingMode = SmoothingMode.None;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
			e.Graphics.DrawImage(image, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
		}
	}
}
