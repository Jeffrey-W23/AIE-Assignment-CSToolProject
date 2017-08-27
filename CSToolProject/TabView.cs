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
		// paint color.
        Color PencilColor = Color.Black;

		// Bool for if drawing can be done or not.
		bool draw = false;

		// x and y for the drawing tool.
		int x = 0;
		int y = 0;

		// the amount to zoom in or out by.
		int zoomFactor;

		// Var for Form1
		Form1 form_1;

		// Form setter. For getting data from form1 and other UserContorls.
		public void SetForm(Form1 f)
		{
			form_1 = f;
		}

        // Picturebox1 Setter
        public void SetImage(Image i)
        {
            pictureBox1.Image = i;
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
				// Create grpahics.
				Graphics g = pictureBox1.CreateGraphics();

				// Switch statement for which tool is selected.
				switch (form_1.GetToolType())
				{
					// Pencil Tool
					case ToolType.Pencil:

						// Draw to screen with the pencil tool.
						g.FillRectangle(new SolidBrush(form_1.GetPencilColor()), e.X - x + x, e.Y - y + y, form_1.GetToolSize(), form_1.GetToolSize());
						break;

					// Eraser Tool
					case ToolType.Eraser:

                        // erase part of image.
                        g.FillRectangle(new SolidBrush(form_1.GetBackgroundImageColor()), e.X - x + x, e.Y - y + y, form_1.GetToolSize(), form_1.GetToolSize());
                        break;

					// ZoomIn Tool
					case ToolType.ZoomIn:

						// Check if allowed to zoom
						if (zoomFactor < 50)
						{
							// change zoom level
							zoomFactor += 2;
						}

						// apply the zoom.
						pictureBox1.Width = pictureBox1.Width * zoomFactor;
						pictureBox1.Height = pictureBox1.Height * zoomFactor;
						pictureBox1.Refresh();

						break;

					// ZoomOut Tool
					case ToolType.ZoomOut:

						// Check if allowed to zoom
						if (zoomFactor > 0)
						{
							// change zoom level
							zoomFactor -= 2;
						}

						break;
				}

				// Dispose of the grpahics class
				g.Dispose();
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}
	}
}
