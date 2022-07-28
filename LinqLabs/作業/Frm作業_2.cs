using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            this.productPhotoTableAdapter1.Fill(this.awDataSet1.ProductPhoto);

            comboBox3.Items.Clear();
            var t = awDataSet1.ProductPhoto.Select(p => p.ModifiedDate.Year.ToString()).Distinct().ToArray();
            Array.Sort(t);
            comboBox3.Items.AddRange(t);

            comboBox3.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from p in this.awDataSet1.ProductPhoto
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var q = from p in this.awDataSet1.ProductPhoto
                    where p.ModifiedDate > dateTimePicker1.Value && p.ModifiedDate < dateTimePicker2.Value
                    select p;
            this.dataGridView1.DataSource = q.ToList();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var q = from p in this.awDataSet1.ProductPhoto
                    where p.ModifiedDate.Year == int.Parse(comboBox3.Text)
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var q = from p in this.awDataSet1.ProductPhoto
                    where (p.ModifiedDate.Month-1)/3 == comboBox2.SelectedIndex
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }
    }
}
