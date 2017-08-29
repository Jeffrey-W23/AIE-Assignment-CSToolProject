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

		// Picturebox1 Setter
		public void SetImage(Image i)
        {

			image = i;

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
			pictureBox1.BackColor = BackgroundColor;
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

			// Draw for single click
			DrawToImage(sender, e);
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
			DrawToImage(sender, e);
		}


		private void DrawToImage(object sender, MouseEventArgs e)
		{
			// If able to draw
			if (draw)
			{
				// Create grpahics.
				using (Graphics g = Graphics.FromImage(image))
				{
					g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

					// Switch statement for which tool is selected.
					switch (form_1.GetToolType())
					{
						// Pencil Tool
						case ToolType.Pencil:

							// Draw to screen with the pencil tool.
							g.FillRectangle(new SolidBrush(form_1.GetPencilColor()), ((e.X - x + x) - (form_1.GetToolSize() / 2)) / zoomfactor, ((e.Y - y + y) - (form_1.GetToolSize() / 2)) / zoomfactor, form_1.GetToolSize(), form_1.GetToolSize());
							pictureBox1.Invalidate();
							pictureBox1.Refresh();				// divide by zero error.
							break;

						// Eraser Tool
						case ToolType.Eraser:

							// erase part of image.
							g.FillRectangle(new SolidBrush(Color.Transparent), ((e.X - x + x) - (form_1.GetToolSize() / 2)) / zoomfactor, ((e.Y - y + y) - (form_1.GetToolSize() / 2)) / zoomfactor, form_1.GetToolSize(), form_1.GetToolSize());
							pictureBox1.Invalidate();
							pictureBox1.Refresh();              // divide by zero error.
							break;
					}
				}
			}
		}









		private void ZoomInCanvas()
		{
			//if (zoomfactor)
			//{
				zoomfactor *= 2;

				pictureBox1.Height *= 2;
				pictureBox1.Width *= 2;
				pictureBox1.Refresh();
			//}
		}

		private void ZoomOutCanvas()
		{
			//if (zoomfactor)
			//{
				zoomfactor /= 2;

				pictureBox1.Height /= 2;
				pictureBox1.Width /= 2;
				pictureBox1.Refresh();
			//}
		}

		private void pictureBox1_MouseEnter(object sender, EventArgs e)
		{
			pictureBox1.Focus();
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			if (image == null)
				return;

			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			e.Graphics.SmoothingMode = SmoothingMode.None;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
			e.Graphics.DrawImage(image, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
		}
	}
}
