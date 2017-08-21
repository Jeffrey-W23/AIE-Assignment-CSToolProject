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
        // Set up painting vars
        Color PaintColor = Color.Black;
        bool draw = false;
        int x, y, lx, ly = 0;

		Form1 form_1;

		public void SetForm(Form1 f)
		{
			form_1 = f;
		}

		// PaintColor Setter
		public void SetPaintColor(Color c)
        {
            PaintColor = c;
        }

        // Picturebox1 Setter
        public void SetImage(Image i)
        {
            pictureBox1.Image = i;
        }

        //--------------------------------------------------------------------------------------
        // Default Constructor.
        //--------------------------------------------------------------------------------------
        public TabView()
        {
            InitializeComponent();
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Location  = new Point((pictureBox1.Parent.ClientSize.Width / 2), (pictureBox1.Parent.ClientSize.Height / 2));
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
            lx = e.X;
            ly = e.Y;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;
            x = e.X;
            y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Reference to the Form1
            //Form1 mainForm = Parent as Form1;

            //if mainForm isnt null
            //if (mainForm != null)
            //{
                if (draw)
                {
                    Graphics g = this.CreateGraphics();

                    //switch (mainForm.GetToolType())
                    //{
                        //case 1:
                            g.FillRectangle(new SolidBrush(form_1.GetPencilColor()), e.X - x + x, e.Y - y + y, 1, 1);
                            //break;
                        //case 2:
                            //break;
                    //}

                    g.Dispose();
                }
            //}
        }
    }
}
