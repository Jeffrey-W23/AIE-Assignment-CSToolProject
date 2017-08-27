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
		// PencilToolBtn_Click: The click event to select the pencil tool.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: EventArgs type, represents the base class for classes that cotain event data.
		//--------------------------------------------------------------------------------------
		private void PencilToolBtn_Click(object sender, EventArgs e)
		{
			// Reference to the Form1
			Form1 mainForm = Parent as Form1;

			//if mainForm isnt null
			if (mainForm != null)
			{
				// Set the tool type.
				mainForm.SetToolType(ToolType.Pencil);
			}
		}

		//--------------------------------------------------------------------------------------
		// EraserToolBtn_Click: The click event to select the eraser tool.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: EventArgs type, represents the base class for classes that cotain event data.
		//--------------------------------------------------------------------------------------
		private void EraserToolBtn_Click(object sender, EventArgs e)
		{
			// Reference to the Form1
			Form1 mainForm = Parent as Form1;

			//if mainForm isnt null
			if (mainForm != null)
			{
				// Set the tool type.
				mainForm.SetToolType(ToolType.Eraser);
			}
		}

		//--------------------------------------------------------------------------------------
		// ColorPickerBtn_Click: The click event for the Color picker button.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: EventArgs type, represents the base class for classes that cotain event data.
		//--------------------------------------------------------------------------------------
		private void ColorPickerBtn_Click(object sender, EventArgs e)
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
			}
		}

		//--------------------------------------------------------------------------------------
		// ZoomInBtn_Click: The click event for the zoom in button.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: EventArgs type, represents the base class for classes that cotain event data.
		//--------------------------------------------------------------------------------------
		private void ZoomInBtn_Click(object sender, EventArgs e)
		{
			// Reference to the Form1
			Form1 mainForm = Parent as Form1;

			//if mainForm isnt null
			if (mainForm != null)
			{
				// Set the tool type.
				mainForm.SetToolType(ToolType.ZoomIn);
			}
		}

		//--------------------------------------------------------------------------------------
		// ZoomOutBtn_Click: The click event for the zoom out button.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: EventArgs type, represents the base class for classes that cotain event data.
		//--------------------------------------------------------------------------------------
		private void ZoomOutBtn_Click(object sender, EventArgs e)
		{
			// Reference to the Form1
			Form1 mainForm = Parent as Form1;

			//if mainForm isnt null
			if (mainForm != null)
			{
				// Set the tool type.
				mainForm.SetToolType(ToolType.ZoomOut);
			}
		}
        
        //--------------------------------------------------------------------------------------
        // ToolSize_ValueChanged: Called when the value has changed in the numeric box.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: EventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void ToolSize_ValueChanged(object sender, EventArgs e)
        {
            // Reference to the Form1
            Form1 mainForm = Parent as Form1;

            //if mainForm isnt null
            if (mainForm != null)
            {
                // Set the tool size.
                mainForm.SetToolSize(Convert.ToInt32(ToolSize.Value));
            }
        }
    }
}

