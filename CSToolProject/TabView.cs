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
        //--------------------------------------------------------------------------------------
        // Default Constructor.
        //--------------------------------------------------------------------------------------
        public TabView()
        {
            InitializeComponent();
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Image = picImage;

            pictureBox1.Location  = new Point((pictureBox1.Parent.ClientSize.Width / 2) - (picImage.Width / 2),
                              (pictureBox1.Parent.ClientSize.Height / 2) - (picImage.Height / 2));
        }
    }
}
