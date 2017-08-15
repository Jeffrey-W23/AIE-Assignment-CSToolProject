using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSToolProject
{
    public partial class Form1 : Form
    {
        const int LEADING_SPACE = 12;
        const int CLOSE_SPACE = 15;
        const int CLOSE_AREA = 15;

        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabView tabview = new TabView();
            tabview.Dock = DockStyle.Fill;

            TabPage page = new TabPage("new");
            page.Controls.Add(tabview);

            tabControl1.TabPages.Add(page);
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - CLOSE_AREA, e.Bounds.Top + 4);
            e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + LEADING_SPACE, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // get the inital length
            int tabLength = tabControl1.ItemSize.Width;

            // measure the text in each tab and make adjustment to the size
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                TabPage currentPage = tabControl1.TabPages[i];

                int currentTabLength = TextRenderer.MeasureText(currentPage.Text, tabControl1.Font).Width;
                
                // adjust the length for what text is written
                currentTabLength += LEADING_SPACE + CLOSE_SPACE + CLOSE_AREA;

                if (currentTabLength > tabLength)
                {
                    tabLength = currentTabLength;
                }
            }

            // create the new size
            Size newTabSize = new Size(tabLength, tabControl1.ItemSize.Height);
            tabControl1.ItemSize = newTabSize;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            //Looping through the controls.
            for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                Rectangle r = tabControl1.GetTabRect(i);
                //Getting the position of the "x" mark.
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
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
