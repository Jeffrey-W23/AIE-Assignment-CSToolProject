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
		Color PencilColor = Color.Black;

		// Bool for if drawing can be done or not.
		bool draw = false;

		// x and y for the drawing tool.
		int x = 0;
		int y = 0;

		// The zoom level of the image
		int zoomfactor = 1;

		// Image Width and Height
		int ImageHeight = 1;
		int ImageWidth = 1;

		// Background image color
		Color BackgroundColor = Color.White;

		// Var for Form1
		Form1 form_1;

		// Form setter. For getting data from form1 and other UserContorls.
		public void SetForm(Form1 f)
		{
			form_1 = f;
		}

		// Picturebox1 Setter
		public Image GetImage()
		{
			return pictureBox1.Image;
		}

		// Picturebox1 Setter
		public void SetImage(Image i)
        {

            pictureBox1.Image = i;

			pictureBox1.Height = i.Height;
			pictureBox1.Width = i.Width;
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

		// ImageHeight Getter
		public int GetImageHeight()
		{
			return ImageHeight;
		}

		// ImageHeight Setter
		public void SetImageHeight(int i)
		{
			ImageHeight = i;
		}

		// ImageWidth Getter
		public int GetImageWidth()
		{
			return ImageWidth;
		}

		// ImageWidth Setter
		public void SetImageWidth(int i)
		{
			ImageWidth = i;
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
		// pictureBox1_MouseClick: Mouse click options for tab contorller.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: MouseEventArgs type, Provides data for the MouseUp, MouseDown, and MouseMove events.
		//--------------------------------------------------------------------------------------
		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
			if (form_1.GetToolType() == ToolType.ZoomIn)
				ZoomInCanvas();

			if (form_1.GetToolType() == ToolType.ZoomOut)
				ZoomOutCanvas();	
		}

		//--------------------------------------------------------------------------------------
		// pictureBox1_MouseDown: Mouse down options for tab contorller.
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
        }

		//--------------------------------------------------------------------------------------
		// pictureBox1_MouseUp: Mouse up options for tab contorller.
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
		// pictureBox1_MouseMove: Mouse move options for tab contorller.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: MouseEventArgs type, Provides data for the MouseUp, MouseDown, and MouseMove events.
		//--------------------------------------------------------------------------------------
		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
			// If able to draw
			if (draw)
			{


				int wid = pictureBox1.Image.Width;
				int hgt = pictureBox1.Image.Height;
				Bitmap bm = new Bitmap(pictureBox1.Image, wid, hgt);
				//pictureBox1.Image = null;
				//pictureBox1.Refresh();
				
				// Create grpahics.
				using (Graphics g = Graphics.FromImage(bm))
				{
					g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

					g.InterpolationMode = InterpolationMode.Bicubic;

					// Switch statement for which tool is selected.
					switch (form_1.GetToolType())
					{
						// Pencil Tool
						case ToolType.Pencil:

							// Draw to screen with the pencil tool.
							g.FillRectangle(new SolidBrush(form_1.GetPencilColor()), (e.X - x + x) / zoomfactor, (e.Y - y + y) / zoomfactor, form_1.GetToolSize(), form_1.GetToolSize());
							pictureBox1.Invalidate();

							pictureBox1.Image = bm;
							pictureBox1.Refresh();

							break;

						// Eraser Tool
						case ToolType.Eraser:

							// erase part of image.
							g.FillRectangle(new SolidBrush(Color.Transparent), (e.X - x + x) / zoomfactor, (e.Y - y + y) / zoomfactor, form_1.GetToolSize(), form_1.GetToolSize());
							pictureBox1.Invalidate();

							pictureBox1.Image = bm;
							pictureBox1.Refresh();

							break;
					}
				}
			}
		}










		private void ZoomInCanvas()
		{
			zoomfactor *= 2;

			pictureBox1.Height *= 2;
			pictureBox1.Width *= 2;
			pictureBox1.Refresh();
		}

		private void ZoomOutCanvas()
		{
			zoomfactor /= 2;

			pictureBox1.Height /= 2;
			pictureBox1.Width /= 2;
			pictureBox1.Refresh();
		}

		private void pictureBox1_MouseEnter(object sender, EventArgs e)
		{
			pictureBox1.Focus();
		}
	}
}
