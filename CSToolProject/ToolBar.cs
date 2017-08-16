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
    // The ToolBar object, UserContorl.
    //--------------------------------------------------------------------------------------
    public partial class ToolBar : UserControl
    {
        //--------------------------------------------------------------------------------------
        // Default Constructor.
        //--------------------------------------------------------------------------------------
        public ToolBar()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------------------------
        // button3_Click: The click event for the Color picker button.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: EventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            // Create colorpicker ColorDialog
            ColorDialog picker = new ColorDialog();

            // Open a full version of the color picker
            picker.FullOpen = true;
            picker.ShowDialog();

            // Reference to the Form1
            Form1 mainForm = Parent as Form1;

            //if mainForm isnt null
            if (mainForm != null)
            {
                // Set the color of the pencil in main form1
                mainForm.SetPencilColor(picker.Color);

                // Set the tool type.
                mainForm.SetToolType(3);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Reference to the Form1
            Form1 mainForm = Parent as Form1;

            //if mainForm isnt null
            if (mainForm != null)
            {
                // Set the tool type.
                mainForm.SetToolType(1);
            }
        }
    }
}
