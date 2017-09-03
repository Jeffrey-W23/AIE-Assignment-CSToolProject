// Using, etc
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

//--------------------------------------------------------------------------------------
// Namespace CSToolProject
//--------------------------------------------------------------------------------------
namespace CSToolProject
{
	// Enum for the different tool types
	public enum ToolType
	{
		Pencil,
		Eraser,
		ZoomIn,
		ZoomOut
	}

	//--------------------------------------------------------------------------------------
	// The Form1 object, Form.
	//--------------------------------------------------------------------------------------
	public partial class Form1 : Form
    {
        // Tab spacing vars.
        const int LEADING_SPACE = 12;
        const int CLOSE_SPACE = 15;
        const int CLOSE_AREA = 15;

        // DragNDrop vars
        bool isDataValid;
        string filepath;
        Image image;
        Thread imageThread;

        // Tabpage for tab dragging
        private TabPage draggedTab;

		// PencilColor var.
		Color PencilColor = Color.Red;

        // Tool size var
        int ToolSize = 1;

        // the current tool var
        ToolType currentTool;

        // PencilColor Setter
        public void SetPencilColor(Color c)
        {
			PencilColor = c;
		}

        // PencilColor Getter
        public Color GetPencilColor()
        {
            return PencilColor;
        }

        // ToolType Setter
        public void SetToolType(ToolType t)
        {
            currentTool = t;
        }

        // ToolType Getter
        public ToolType GetToolType()
        {
            return currentTool;
        }

        // Setter for tool size
        public void SetToolSize(int i)
        {
            ToolSize = i;
        }

        // Getter for tool size
        public int GetToolSize()
        {
            return ToolSize;
        }

        //--------------------------------------------------------------------------------------
        // Default Constructor.
        //--------------------------------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------------------------
        // NewTab: Function for creating a new tab.
        //
        // Param:
        //      s: a string for the filename of the tab.
        // Return:
        //		TabView: Returns a TabView usercontorl
        //--------------------------------------------------------------------------------------
        private TabView NewTab(string s)
		{
			// Create a new TabView
			TabView tabview = new TabView();

			// Set form, for passing in color, tool, etc
			tabview.SetForm(this);

			// Set Dock to fill
			tabview.Dock = DockStyle.Fill;

			// Create a new TabPage
			TabPage page = new TabPage(s); // TabPage name is set to "new".

			// Add the tabview to the tabpage.
			page.Controls.Add(tabview);

			// Add the tab to the tab controller.
			tabControl1.TabPages.Insert(0, page);

			// Set the new tab to being selected.
			tabControl1.SelectedTab = page;
			
			// Resize the tab.
			ResizeTabBar();

            // Enable or disable tabs.
            EnableButtons();

            // return the tabview
            return tabview;
		}

		//--------------------------------------------------------------------------------------
		// ResizeTabBar: Resize function for resizing the tab bar area to fit title text.
		//--------------------------------------------------------------------------------------
		private void ResizeTabBar()
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
			}

			// create the new size.
			Size newTabSize = new Size(tabLength, tabControl1.ItemSize.Height);
			tabControl1.ItemSize = newTabSize;
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
            // Create and display a new forms for options on creating the new project.
            var form2 = new Form2();
            form2.StartPosition = FormStartPosition.CenterParent;
            form2.ShowDialog();

            // If the okay button is pressed.
            if (form2.GetOkayPressed())
            {
                // Create new tab
                TabView tabview = NewTab("New Image");

                // Create new bitmap
                Bitmap bitm = new Bitmap(form2.GetImageWidth(), form2.GetImageHeight(), PixelFormat.Format32bppArgb); // Let the user pick a width and hieght in a seperate form.

                // Set the background of the picturebox.
                tabview.SetBackgroundColor(form2.GetImageColor());

                // Set image on the tabview
                tabview.SetImage(bitm);

                // Set the new file to true.
                tabview.SetNewFile(true);
            }
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
            if (e.Index < tabControl1.TabPages.Count)
            {
                //Color the tab headers
                Graphics g = e.Graphics;

                //Rectangle to draw over the tab
                RectangleF headerRect = new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

                // The non-selected tab color.
                SolidBrush sb = new SolidBrush(Color.Transparent); //Color.FromArgb(200,200,200)

                // The selected tab color.
                if (tabControl1.SelectedIndex == e.Index)
                    sb.Color = Color.FromArgb(255, 255, 255); // DodgerBlue; // LightSkyBlue;

                // Apply color changes from above.
                g.FillRectangle(sb, e.Bounds);

                // Draw an x icon on the tab.
                e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - CLOSE_AREA, e.Bounds.Top + 4);
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + LEADING_SPACE, e.Bounds.Top + 4); // WONT WORK ON HOME COMPUTER?? ASK RICHARD.
                e.DrawFocusRectangle();
            }
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
			// Remove default tab that TabControl needs to create to function.
			tabControl1.Controls.Remove(tabPage1);
		}

		//--------------------------------------------------------------------------------------
		// tabControl1_MouseDown: Function for if the mouse is down on the tabcontrol.
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
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 20, 20);
				
                // If the X button is pressed close the tab.
                if (closeButton.Contains(e.Location))
                {
                    // Check if the image is saved or not
                    if (((TabView)tabControl1.SelectedTab.Controls[0]).GetAltered() == true)
                    {
                        // Display dialog box
                        DialogResult result = MessageBox.Show("Do you want to save changes before closing?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        // save and close
                        if (result == DialogResult.Yes)
                        {
                            SaveAsFunction();
                            this.tabControl1.TabPages.RemoveAt(i);
                            EnableButtons();
                            break;
                        }

                        // just close
                        else if (result == DialogResult.No)
                        {
                            this.tabControl1.TabPages.RemoveAt(i);
                            EnableButtons();
                            break;
                        }
                    }
                    else if (((TabView)tabControl1.SelectedTab.Controls[0]).GetAltered() == false)
                    {
                        // Close the tab.
                        this.tabControl1.TabPages.RemoveAt(i);
                        EnableButtons();
                        break;
                    }
                }
			}

			// Check which tab is being dragged.
			draggedTab = TabAt(e.Location);
		}

		//--------------------------------------------------------------------------------------
		// tabControl1_MouseUp: Function for if the mouse is up for the tab control.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: MouseEventArgs type, Provides data for the MouseUp, MouseDown, and MouseMove events.
		//--------------------------------------------------------------------------------------
		private void tabControl1_MouseUp(object sender, MouseEventArgs e)
		{
			
		}

		//--------------------------------------------------------------------------------------
		// tabControl1_MouseMove: Function for if the mouse has moved for the tabcontrol.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: MouseEventArgs type, Provides data for the MouseUp, MouseDown, and MouseMove events.
		//--------------------------------------------------------------------------------------
		private void tabControl1_MouseMove(object sender, MouseEventArgs e)
		{
			// If the mouse isnt down or a tab isnt draggable do nothing.
			if (e.Button != MouseButtons.Left || draggedTab == null)
			{
				return;
			}

			// get a dragabble tab
			TabPage tab = TabAt(e.Location);

			// Do nothing if tab is null or the same as the other tab
			if (tab == null || tab == draggedTab)
			{
				return;
			}

			// Swap the tabs
			Swap(draggedTab, tab);
			tabControl1.SelectedTab = draggedTab;
		}

		//--------------------------------------------------------------------------------------
		// tabControl1_DragOver: Drag over options for tab contorller.
		//
		// Param:
		//		sender: object type, Supports all classes in the .NET Framework class hierarchy.
		//		e: DragEventArgs type, Provides data for the dragging event.
		//--------------------------------------------------------------------------------------
		private void tabControl1_DragOver(object sender, DragEventArgs e)
		{

		}

        //--------------------------------------------------------------------------------------
        // TabAt: Function to check which tab is going to be dragged.
        //
        // Param:
        //		position: Point, the postion of the tab as a vector.
        // Return:
        //      TabPage: returns a tabpage.
        //--------------------------------------------------------------------------------------
        private TabPage TabAt(Point position)
		{
			// Get tab count
			int count = tabControl1.TabPages.Count;

			// Go through each tab page.
			for (int i = 0; i < count; i++)
			{
				// Check if the mouse is in the tab area
				if (tabControl1.GetTabRect(i).Contains(position))
				{
					// return the tab attempting to be moved.
					return tabControl1.TabPages[i];
				}
			}

			return null;
		}

		//--------------------------------------------------------------------------------------
		// Swap: Function to swap the tabs position with another.
		//
		// Param:
		//		a: Tabpage to swap with b.
		//		b: Tabpage to swap with a. 
		//--------------------------------------------------------------------------------------
		private void Swap(TabPage a, TabPage b)
		{
			// get tab indexes.
			int i = tabControl1.TabPages.IndexOf(a);
			int j = tabControl1.TabPages.IndexOf(b);

			// Swap the tabs.
			tabControl1.TabPages[i] = b;
			tabControl1.TabPages[j] = a;
		}

        //--------------------------------------------------------------------------------------
        // openToolStripMenuItem_Click: What to do when the open button is pressed under the menu bar.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: EventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
			// New open file dialog.
            OpenFileDialog dlg = new OpenFileDialog();
            string filename = "";

			// Filter which files can be opened.
			dlg.Filter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|" + "All Files|*.*";

            // Present the open dialog
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // store the file path.
                filename = dlg.SafeFileName;

                // Create a new tab
				TabView tabview = NewTab(filename);

				// Set image to the new tabview.
				Bitmap bitm = new Bitmap(Image.FromFile(dlg.FileName));
                tabview.SetImage(bitm);
                tabview.SetDirectory(dlg.FileName);
                tabview.SetNewFile(true);
            }
        }

        //--------------------------------------------------------------------------------------
        // saveToolStripMenuItem_Click: What to do if the save button is pressed under the menu bar.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: EventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
            // Check to make sure there is something opened.
            if (tabControl1.TabCount > 0)
            {
                SaveFunction();
            }
        }

        //--------------------------------------------------------------------------------------
        // saveAsToolStripMenuItem_Click: What to do if the save as button is pressed under the menu bar.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: EventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check to make sure there is something opened.
            if (tabControl1.TabCount > 0)
            {
                SaveAsFunction();
            }
        }

        //--------------------------------------------------------------------------------------
        // SaveFunction: The saving function when an image already has a save location.
        //--------------------------------------------------------------------------------------
        private void SaveFunction()
        {
            // Get the current tabview
            TabView tabview = ((TabView)tabControl1.SelectedTab.Controls[0]);

            // Check if something has a file path or not
            //if it already does then dont use save dialog and just go ahead and save the file
            if (tabview.GetNewFile() == false)
            {
                if (tabview.GetDirectory() != null)
                {
                    // Get image and save image to save location.
                    Image image = tabview.GetImage();
                    image.Save(tabview.GetDirectory());
                    tabview.SetAltered(false);
                    tabview.SetNewFile(false);
                }
            }

            // The file hasnt been saved before, need thesave dialog
            else if (tabview.GetNewFile() == true)
            {
                SaveAsFunction();
            }
        }

        //--------------------------------------------------------------------------------------
        // SaveAsFunction: The saving function when an image doesnt have a saved file.
        //--------------------------------------------------------------------------------------
        private void SaveAsFunction()
        {
            // New save dialog
            SaveFileDialog save = new SaveFileDialog();

            // Filter which files can be saved.
            save.Filter = "PNG|*.png|BMP|*.bmp|GIF|*.gif|JPG|*.jpg|JPEG|*.jpeg|TIF|*.tif|TIFF|*.tiff";
            
            // Set image format
            ImageFormat format = ImageFormat.Png;
            
            // Show dialog box.
            if (save.ShowDialog() == DialogResult.OK)
            {
                //get filename extensions.
                string ext = System.IO.Path.GetExtension(save.FileName);

                switch (ext)
                {
                    // png.
                    case ".png":
                        format = ImageFormat.Png;
                        break;

                    // bmp.
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;

                    // gif.
                    case ".gif":
                        format = ImageFormat.Gif;
                        break;

                    // jpg.
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;

                    // jpeg.
                    case ".jpeg":
                        format = ImageFormat.Jpeg;
                        break;

                    // tif.
                    case ".tif":
                        format = ImageFormat.Tiff;
                        break;

                    // tiff.
                    case ".tiff":
                        format = ImageFormat.Tiff;
                        break;
                }

                // Get the current tabview
                TabView tabview = ((TabView)tabControl1.SelectedTab.Controls[0]);

                // Get image and save image to save location.
                Image image = tabview.GetImage();
                image.Save(save.FileName, format);

                // Set altered and new to false
                tabview.SetAltered(false);
                tabview.SetNewFile(false);
                tabview.SetDirectory(save.FileName);

                // Set the tab name
                tabControl1.SelectedTab.Text = System.IO.Path.GetFileName(save.FileName);
            }
        }

        //--------------------------------------------------------------------------------------
        // Form1_FormClosing: A function for what to do when the application closes.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: FormClosingEventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Check if anything is open.
            if (tabControl1.TabCount > 0)
            {
                //Looping through the controls.
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    // Check if the image is saved or not
                    if (((TabView)tabControl1.SelectedTab.Controls[0]).GetAltered() == true)
                    {
                        // Display dialog box
                        DialogResult result = MessageBox.Show("Do you want to save changes before closing?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        // save and close
                        if (result == DialogResult.Yes)
                        {
                            // Save project
                            SaveAsFunction();

                            // Remove the current tab.
                            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                            i--;
                        }

                        // just close
                        else if (result == DialogResult.No)
                        {
                            // Remove the current tab.
                            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                            i--;
                        }
                    }

                    else if (((TabView)tabControl1.SelectedTab.Controls[0]).GetAltered() == false)
                    {
                        // Remove all tabs.
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                        i--;
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------------
        // EnableButtons: Enable and Disable buttons throughout application.
        //--------------------------------------------------------------------------------------
        private void EnableButtons()
        {
            // If there are no tabs
            if (tabControl1.TabCount == 0)
            {
                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                pasteToolStripMenuItem.Enabled = false;
                closeTabToolStripMenuItem.Enabled = false;
                closeAllTabsToolStripMenuItem.Enabled = false;
                toolBar1.Enabled = false;
            }

            // If there are tabs opened
            else if (tabControl1.TabCount > 0)
            {
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                //copyToolStripMenuItem.Enabled = true;
                //cutToolStripMenuItem.Enabled = true;
                //pasteToolStripMenuItem.Enabled = true;
                closeTabToolStripMenuItem.Enabled = true;
                closeAllTabsToolStripMenuItem.Enabled = true;
                toolBar1.Enabled = true;
            }
        }

        //--------------------------------------------------------------------------------------
        // exitToolStripMenuItem_Click: What to do if the exit button is pressed under the menu bar.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: FormClosingEventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if anything is open.
            if (tabControl1.TabCount > 0)
            {
                //Looping through the controls.
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    // Check if the image is saved or not
                    if (((TabView)tabControl1.SelectedTab.Controls[0]).GetAltered() == true)
                    {
                        // Display dialog box
                        DialogResult result = MessageBox.Show("Do you want to save changes before closing?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        // save and close
                        if (result == DialogResult.Yes)
                        {
                            // Save project
                            SaveAsFunction();

                            // Remove the current tab.
                            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                            i--;
                        }

                        // just close
                        else if (result == DialogResult.No)
                        {
                            // Remove the current tab.
                            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                            i--;
                        }
                    }

                    else if (((TabView)tabControl1.SelectedTab.Controls[0]).GetAltered() == false)
                    {
                        // Remove all tabs.
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                        i--;
                    }
                }
            }

            // Close application
            this.Close();
        }

        //--------------------------------------------------------------------------------------
        // minimizeToolStripMenuItem_Click: What to do if the minimize button is pressed under the menu bar.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: FormClosingEventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Minimize tab.
            this.WindowState = FormWindowState.Minimized;
        }

        //--------------------------------------------------------------------------------------
        // closeTabToolStripMenuItem_Click: What to do if the close Tab button is pressed under the menu bar.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: FormClosingEventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if anything is open.
            if (tabControl1.TabCount > 0)
            {
                // Check if the image is saved or not
                if (((TabView)tabControl1.SelectedTab.Controls[0]).GetAltered() == true)
                {
                    // Display dialog box
                    DialogResult result = MessageBox.Show("Do you want to save changes before closing?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // save and close
                    if (result == DialogResult.Yes)
                    {
                        // Save project
                        SaveAsFunction();

                        // Remove the current tab.
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);

                        // Disable or enable buttons
                        EnableButtons();
                    }

                    // just close
                    else if (result == DialogResult.No)
                    {
                        // Remove the current tab.
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);

                        // Disable or enable buttons
                        EnableButtons();
                    }
                }
                else if (((TabView)tabControl1.SelectedTab.Controls[0]).GetAltered() == false)
                {
                    // Remove the current tab.
                    tabControl1.TabPages.Remove(tabControl1.SelectedTab);

                    // Disable or enable buttons
                    EnableButtons();
                }
            }
        }

        //--------------------------------------------------------------------------------------
        // closeAllTabsToolStripMenuItem_Click: What to do if the close all Tab button is pressed under the menu bar.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: FormClosingEventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void closeAllTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if anything is open.
            if (tabControl1.TabCount > 0)
            {
                //Looping through the controls.
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    // Check if the image is saved or not
                    if (((TabView)tabControl1.SelectedTab.Controls[0]).GetAltered() == true)
                    {
                        // Display dialog box
                        DialogResult result = MessageBox.Show("Do you want to save changes before closing?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        // save and close
                        if (result == DialogResult.Yes)
                        {
                            // Save project
                            SaveAsFunction();

                            // Remove the current tab.
                            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                            i--;

                            // Disable or enable buttons
                            EnableButtons();
                        }

                        // just close
                        else if (result == DialogResult.No)
                        {
                            // Remove the current tab.
                            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                            i--;

                            // Disable or enable buttons
                            EnableButtons();
                        }
                    }

                    else if (((TabView)tabControl1.SelectedTab.Controls[0]).GetAltered() == false)
                    {
                        // Remove all tabs.
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                        i--;

                        // Disable or enable buttons
                        EnableButtons();
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------------
        // Form1_DragEnter: Function for what to do when something is drag entered.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: DragEventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            // new string for filename
            string filename;

            // Check and get the dropped file
            isDataValid = GetFile(out filename, e);

            // Make sure the file is valid
            if (isDataValid)
            {
                // Set the filepath
                filepath = filename;

                // Start a new thread
                imageThread = new Thread(new ThreadStart(LoadImage));
                imageThread.Start();

                // get the dragged file
                e.Effect = DragDropEffects.Copy;
            }

            else
            {
                // if file isnt valid ignore
                e.Effect = DragDropEffects.None;
            }
        }

        //--------------------------------------------------------------------------------------
        // Form1_DragDrop: Function for what to do when something is drag dropped.
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: DragEventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            // if a valid file has been dropped.
            if (isDataValid)
            {
                // while the thread is still running.
                while (imageThread.IsAlive)
                {
                    Application.DoEvents();

                    // Block the current thread
                    Thread.Sleep(0);
                }

                // Create a new tab
                TabView tabview = NewTab(System.IO.Path.GetFileName(filepath));

                // Set image to the new tabview.
                tabview.SetImage(image);
                tabview.SetDirectory(System.IO.Path.GetFileName(filepath));
            }
        }

        //--------------------------------------------------------------------------------------
        // LoadImage: Creates a new bitmap to pass into a new tab.
        //--------------------------------------------------------------------------------------
        private void LoadImage()
        {
            // Set the temp image to the dropped image
            image = new Bitmap(filepath);
        }

        //--------------------------------------------------------------------------------------
        // GetFile: 
        //
        // Param:
        //		sender: object type, Supports all classes in the .NET Framework class hierarchy.
        //		e: DragEventArgs type, represents the base class for classes that cotain event data.
        //--------------------------------------------------------------------------------------
        private bool GetFile(out string filename, DragEventArgs e)
        {
            // bool for if the file is valid or not
            bool result = false;

            // Set the filename to an empty string.
            filename = String.Empty;

            // Has something be dropped?
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                // Create an array of dropped objects.
                Array data = ((IDataObject)e.Data).GetData("FileDrop") as Array;

                // Continue if not null
                if (data != null)
                {
                    // Check to make sure there is only one dropped file and is a string.
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        // Get the filename from the data string
                        filename = ((string[])data)[0];

                        // Get the files extention.
                        string ext = Path.GetExtension(filename).ToLower();

                        // Supported files that are allowed to be dropped on the applictaion
                        if ((ext == ".jpg") || (ext == ".png") || (ext == ".bmp") || (ext == ".gif") || (ext == ".jpeg") || (ext == ".tif") || (ext == ".tiff"))
                        {
                            // File is valid
                            result = true;
                        }
                    }
                }
            }
            
            // Return if the file is valid or not.
            return result;
        }
    }
}