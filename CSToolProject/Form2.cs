// Using, etc
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    // The Form2 object, Form.
    //--------------------------------------------------------------------------------------
    public partial class Form2 : Form
    {
        // Background Color
        Color ImageBackColor = Color.White;

        // Image Dimension vars
        int WidthValue = 1;
        int HeightValue = 1;

        // If okay button is pressed.
        bool OkayPressed;

        // Getter for image.
        public Color GetImageColor()
        {
            return ImageBackColor;
        }

        // Getter for Image width
        public int GetImageWidth()
        {
            return WidthValue;
        }

        // Getter for Image height
        public int GetImageHeight()
        {
            return HeightValue;
        }

        // Setter for if okay button is pressed.
        public void SetOkayPressed(bool b)
        {
            OkayPressed = b;
        }

        // Getter for if okay button is pressed.
        public bool GetOkayPressed()
        {
            return OkayPressed;
        }

        //--------------------------------------------------------------------------------------
        // Default Constructor.
        //--------------------------------------------------------------------------------------
        public Form2()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------------------------
        // ColorPicker_Click: The color picker button for picking the image background color.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: EventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void ColorPicker_Click(object sender, EventArgs e)
        {
            // Change radio button to custom
            radioButton3.Checked = true;

            // Create colorpicker ColorDialog
            ColorDialog picker = new ColorDialog();

            // Open a full version of the color picker
            picker.FullOpen = true;
            picker.ShowDialog();

            // Set the back color.
            ImageBackColor = picker.Color;

            // Set the picture box on forms so the color picked can be seen.
            pictureBox1.BackColor = picker.Color;
        }

        //--------------------------------------------------------------------------------------
        // radioButton1_CheckedChanged: function for if checked changes for radiobutton1
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: EventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Set the image color to White.
            ImageBackColor = Color.White;
            pictureBox1.BackColor = ImageBackColor;
        }

        //--------------------------------------------------------------------------------------
        // radioButton2_CheckedChanged: function for if checked changes for radiobutton2
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: EventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Set the image color to transparent.
            ImageBackColor = Color.Transparent;
            pictureBox1.BackColor = ImageBackColor;
        }

        //--------------------------------------------------------------------------------------
        // ApplyOptions: Apply new images options for image creation.
        //--------------------------------------------------------------------------------------
        private void ApplyOptions()
        {
            // Apply width and height values.
            WidthValue = Convert.ToInt32(WidthBox.Value);
            HeightValue = Convert.ToInt32(HeightBox.Value);
        }

        //--------------------------------------------------------------------------------------
        // OkayBtn_Click: The okay button for the form.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: EventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void OkayBtn_Click(object sender, EventArgs e)
        {
            // Apply options.
            ApplyOptions();

            // set okay to pressed.
            SetOkayPressed(true);

            // Close form.
            this.Close();
        }
    }
}
