using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSToolProject
{
    public partial class TabView : UserControl
    {
        public TabView()
        {
            InitializeComponent();
        }

        private void TabView_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
