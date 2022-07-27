using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };
        }
        List<Student> students_scores;

        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
        private void button36_Click(object sender, EventArgs e)
        {
            button36.Text = $"共{students_scores.Count * 3}個學員成績";
            button36.ForeColor = Color.Red;
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?						

            // 找出 前面三個 的學員所有科目成績					
            // 找出 後面兩個 的學員所有科目成績					

            // 找出 Name 'aaa','bbb','ccc' 的學成績						

            // 找出學員 'bbb' 的成績	                          

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	


            // 數學不及格 ... 是誰 
            #endregion
        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (page > nwDataSet1.Orders.Rows.Count / int.Parse(textBox1.Text))
            {
                return;
            }
            page += 1;
            var q = from p in this.nwDataSet1.Orders.Take(page * int.Parse(textBox1.Text)).Skip((page - 1) * int.Parse(textBox1.Text))
                    select p;
            this.dataGridView1.DataSource = q.ToList();
            List<LinqLabs.NWDataSet.Order_DetailsRow> temp = new List<LinqLabs.NWDataSet.Order_DetailsRow>();
            foreach (var i in q)
            {
                var q2 = from p in this.nwDataSet1.Order_Details
                         where p.OrderID == i.OrderID
                         select p;
                temp.AddRange(q2);
            }
            dataGridView2.DataSource = temp;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files where f.Extension == ".log" select f;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files where f.CreationTime.Year == 2017 select f;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files where f.Length > 100000 select f;

            this.dataGridView1.DataSource = q.ToList();
        }
        int page = 1;
        private void button6_Click(object sender, EventArgs e)
        {
            var q = from p in this.nwDataSet1.Orders
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void Frm作業_1_Load(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);

            foreach (DataRow a in nwDataSet1.Orders.Rows)
            {
                foreach (DataColumn c in nwDataSet1.Orders.Columns)
                {
                    if (a.IsNull(c))
                    {
                        a.Delete();
                        break;
                    }
                }
            }
            nwDataSet1.AcceptChanges();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = from p in this.nwDataSet1.Orders
                    where p.OrderDate.Year == int.Parse(comboBox1.Text)
                    select p;
            this.dataGridView1.DataSource = q.ToList();

            //var q1 = from p in this.nwDataSet1.Orders
            //        join d in this.nwDataSet1.Order_Details on p.OrderID equals d.OrderID
            //        where p.OrderDate.Year == int.Parse(comboBox1.Text)
            //        select p;
            //this.dataGridView2.DataSource = q1.ToList();
            List<LinqLabs.NWDataSet.Order_DetailsRow> temp = new List<LinqLabs.NWDataSet.Order_DetailsRow>();
            foreach (var i in q)
            {
                var q2 = from p in this.nwDataSet1.Order_Details
                         where p.OrderID == i.OrderID
                         select p;
                temp.AddRange(q2);
            }
            dataGridView2.DataSource = temp;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (page <= 1)
            {
                return;
            }
            page -= 1;
            var q = from p in this.nwDataSet1.Orders.Take(page * int.Parse(textBox1.Text)).Skip((page - 1) * int.Parse(textBox1.Text))
                    select p;
            this.dataGridView1.DataSource = q.ToList();
            List<LinqLabs.NWDataSet.Order_DetailsRow> temp = new List<LinqLabs.NWDataSet.Order_DetailsRow>();
            foreach (var i in q)
            {
                var q2 = from p in this.nwDataSet1.Order_Details
                         where p.OrderID == i.OrderID
                         select p;
                temp.AddRange(q2);
            }
            dataGridView2.DataSource = temp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var q = from p in students_scores.Take(3)
                    select p;
            dataGridView1.DataSource = q.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var q = from p in students_scores.Skip(students_scores.Count-2)
                    select p;
            dataGridView1.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var q = from p in students_scores
                    where p.Name == "aaa" || p.Name == "bbb" || p.Name == "ccc"
                    select p;
            dataGridView1.DataSource = q.ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var q = from p in students_scores
                    where p.Name == "bbb"
                    select p;
            dataGridView1.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var q = from p in students_scores
                    where p.Name != "bbb"
                    select p;
            dataGridView1.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var q = from p in students_scores
                    where p.Math <60
                    select p;
            dataGridView1.DataSource = q.ToList();
        }
    }
}
