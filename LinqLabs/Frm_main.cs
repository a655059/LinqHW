using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqLabs.作業;
using MyHomeWork;

namespace LinqLabs
{
    public partial class Frm_main : Form
    {
        public Frm_main()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Frm作業_1 fm = new Frm作業_1();
            fm.MdiParent = this;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Frm作業_2 fm = new Frm作業_2();
            fm.MdiParent = this;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Frm作業_3 fm = new Frm作業_3();
            fm.MdiParent = this;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Frm作業_4 fm = new Frm作業_4();
            fm.MdiParent = this;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
        }
    }
}
