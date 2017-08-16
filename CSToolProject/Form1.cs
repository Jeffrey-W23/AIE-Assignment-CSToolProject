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
    // The Form1 object, Form.
    //--------------------------------------------------------------------------------------
    public partial class Form1 : Form
    {
        // Tab spacing vars.
        const int LEADING_SPACE = 12;
        const int CLOSE_SPACE = 15;
        const int CLOSE_AREA = 15;

        // PencilColor var.
        Color PencilColor;

        // Enum for the different tool types
        public enum ToolType
        {
            Brush = 1,
            Color = 2,
            Eraser = 3
        }

        // the current tool var
        ToolType currentTool;

        // PencilColor Setter
        public void SetPencilColor(Color c)
        {
            PencilColor = c;
        }

        // ToolTypeSetter
        public void SetToolType(int t)
        {
            currentTool = (ToolType)t;
        }

        //--------------------------------------------------------------------------------------
        // Default Constructor.
        //--------------------------------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------------------------
        // newToolStripMenuItem_Click: What to do when the new button is pressed under the menu bar.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: EventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a new TabView
            TabView tabview = new TabView();

            // Set Dock to fill
            tabview.Dock = DockStyle.Fill;

            // Create a new TabPage
            TabPage page = new TabPage("new"); // TabPage name is set to "new".

            // Add the tabview to the tabpage.
            page.Controls.Add(tabview);

            // Add the tab to the tab controller.
            tabControl1.TabPages.Add(page);
        }

        //--------------------------------------------------------------------------------------
        // tabControl1_DrawItem: Draw options for the tabs and tab controller.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: DrawItemEventArgs type, Provides data for the DrawItem event.
        //--------------------------------------------------------------------------------------
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw an x icon on the tab.
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - CLOSE_AREA, e.Bounds.Top + 4);
            e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + LEADING_SPACE, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        //--------------------------------------------------------------------------------------
        // Form1_Load: Load options for the Form1.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: EventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e) // SPLIT THIS INTO 2 NEW FUNCTIONS, 1 BEING A RESIZE FUNCTION ANF THE OTHER A NEW FUNCTION.
        {
            // get the inital length.
            int tabLength = tabControl1.ItemSize.Width;

            // measure the text in each tab and make adjustment to the size.
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                // Get the current tab page.
                TabPage currentPage = tabControl1.TabPages[i];

                // Get current length of the tab.
                int currentTabLength = TextRenderer.MeasureText(currentPage.Text, tabControl1.Font).Width;
                
                // adjust the length for what text is written.
                currentTabLength += LEADING_SPACE + CLOSE_SPACE + CLOSE_AREA;

                // change tab length if its longer than tab length.
                if (currentTabLength > tabLength)
                {
                    tabLength = currentTabLength;
                }

                // Create a new TabView
                TabView tabview = new TabView();

                // Set Dock to fill
                tabview.Dock = DockStyle.Fill;

                // Add the tabview to the tabpage.
                currentPage.Controls.Add(tabview);
            }

            // create the new size.
            Size newTabSize = new Size(tabLength, tabControl1.ItemSize.Height);
            tabControl1.ItemSize = newTabSize;
        }

        //--------------------------------------------------------------------------------------
        // tabControl1_MouseDown: Mouse down options for tab contorller.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: MouseEventArgs type, Provides data for the MouseUp, MouseDown, and MouseMove events.
        //--------------------------------------------------------------------------------------
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            //Looping through the controls.
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                // New rectangle
                Rectangle r = tabControl1.GetTabRect(i);

                //Getting the position of the "x" mark.
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);

                // If the X button is pressed close the tab.
                if (closeButton.Contains(e.Location))
                {
                    //if (MessageBox.Show("Would you like to Close this Tab?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                        this.tabControl1.TabPages.RemoveAt(i);
                        break;
                    //}
                }
            }
        }
    }
}
