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
        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            TextEditDemo editor = new TextEditDemo();
            editor.Dock = DockStyle.Fill;

            TabPage page = new TabPage("new");
            page.Controls.Add(editor);


            tabControl1.TabPages.Add(page);
        }
    }
}
