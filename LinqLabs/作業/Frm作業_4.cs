using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_4 : Form
    {
        public Frm作業_4()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = new int[]{ 1,2,3,4,5,6,7,8,9,10};
            List<int> small = new List<int>();
            List<int> mid = new List<int>();
            List<int> large = new List<int>();
            foreach(int i in nums)
            {
                if(i<4)
                {
                    small.Add(i);
                }
                else if(i>6)
                {
                    large.Add(i);
                }
                else
                {
                    mid.Add(i);
                }
            }
            TreeNode x = treeView1.Nodes.Add($"小 ({small.Count})");
            foreach(int i in small)
            {
                x.Nodes.Add(i.ToString());
            }
            x = treeView1.Nodes.Add($"中 ({mid.Count})");
            foreach (int i in mid)
            {
                x.Nodes.Add(i.ToString());
            }
            x = treeView1.Nodes.Add($"大 ({large.Count})");
            foreach (int i in large)
            {
                x.Nodes.Add(i.ToString());
            }
        }
        string MyKey1(long n)
        {
            if (n < 100)
            {
                return "小";

            }
            else if (n > 10000)
            {
                return "大";
            }
            else
            {
                return "中";
            }
        }
        private void button38_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from n in files group n by MyKey1(n.Length) into g select new { MyKey = g.Key, MyCount = g.Count(), MyGroup = g };
            foreach (var g in q)
            {
                TreeNode x = treeView1.Nodes.Add($"{g.MyKey} ({g.MyCount})");
                foreach (var i in g.MyGroup)
                {
                    x.Nodes.Add(i.ToString());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from n in files group n by n.CreationTime.Year into g select new { MyKey = g.Key, MyCount = g.Count(), MyGroup = g };
            foreach (var g in q)
            {
                TreeNode x = treeView1.Nodes.Add($"{g.MyKey} ({g.MyCount})");
                foreach (var i in g.MyGroup)
                {
                    x.Nodes.Add(i.ToString());
                }
            }
        }
        string MyKey2(decimal? n)
        {
            if (n < 10)
            {
                return "低";

            }
            else if (n > 50)
            {
                return "高";
            }
            else
            {
                return "中";
            }
        }
        NorthwindEntities1 dbcontext = new NorthwindEntities1();
        private void button8_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            var q = from n in dbcontext.Products.AsEnumerable() group n by MyKey2(n.UnitPrice) into g select new { MyKey = g.Key, MyCount = g.Count(), MyGroup = g };
            foreach (var g in q)
            {
                TreeNode x = treeView1.Nodes.Add($"{g.MyKey} ({g.MyCount})");
                foreach (var i in g.MyGroup)
                {
                    x.Nodes.Add(i.ProductID.ToString());
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            var q = from n in dbcontext.Orders.AsEnumerable() group n by n.OrderDate.Value.Year into g select new { MyKey = g.Key, MyCount = g.Count(), MyGroup = g };
            foreach (var g in q)
            {
                TreeNode x = treeView1.Nodes.Add($"{g.MyKey} ({g.MyCount})");
                foreach (var i in g.MyGroup)
                {
                    x.Nodes.Add(i.OrderID.ToString());
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            var q = from n in dbcontext.Orders.AsEnumerable() group n by n.OrderDate.Value.Year + "/" + n.OrderDate.Value.Month into g select new { MyKey = g.Key, MyCount = g.Count(), MyGroup = g };
            foreach (var g in q)
            {
                TreeNode x = treeView1.Nodes.Add($"{g.MyKey} ({g.MyCount})");
                foreach (var i in g.MyGroup)
                {
                    x.Nodes.Add(i.OrderID.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var q = from p in dbcontext.Order_Details
                    select p;
            MessageBox.Show($"{q.Sum(i => (double)i.UnitPrice * i.Quantity * (1 - i.Discount)):N2}元");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = from n in dbcontext.Order_Details.AsEnumerable()
                    group n by n.Order.EmployeeID into g
                    orderby g.Sum(i => i.Quantity * (double)i.UnitPrice * (1 - i.Discount)) descending
                    select new { EmployeeID = g.Key, MySales = $"{g.Sum(i => i.Quantity * (double)i.UnitPrice * (1 - i.Discount)):N2}" };
            dataGridView1.DataSource = q.Take(5).ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var q = from p in dbcontext.Products
                    orderby p.UnitPrice descending
                    select new { p.ProductID, p.ProductName, p.Category.CategoryName, p.UnitPrice };
            dataGridView1.DataSource = q.Take(5).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var q = from p in dbcontext.Products
                    select new { p.ProductID, p.ProductName, p.Category.CategoryName, p.UnitPrice };
            MessageBox.Show(q.Any(i => i.UnitPrice>300).ToString());
        }

        private void button34_Click(object sender, EventArgs e)
        {
            chart1.DataSource = null;
            chart1.Series.Clear();
            chart1.Series.Add("S1");
            chart1.Series.Add("S2");
            chart1.Series.Add("S3");
            var q = from n in dbcontext.Order_Details.AsEnumerable()
                    group n by n.Product.Category.CategoryName into g
                    orderby g.Sum(i => i.Quantity * i.UnitPrice) descending
                    select new { MyKey = g.Key, MySales1 = g.Where(i => i.Order.OrderDate.Value.Year==1996).Sum(i => i.Quantity * i.UnitPrice), 
                        MySales2 = g.Where(i => i.Order.OrderDate.Value.Year == 1997).Sum(i => i.Quantity * i.UnitPrice),
                        MySales3 = g.Where(i => i.Order.OrderDate.Value.Year == 1998).Sum(i => i.Quantity * i.UnitPrice),
                        MyGroup = g };
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MySales1";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[0].Color = Color.Green;
            this.chart1.Series[0].BorderWidth = 3;
            this.chart1.Series[0].LegendText = "1996";

            this.chart1.Series[1].XValueMember = "MyKey";
            this.chart1.Series[1].YValueMembers = "MySales2";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].Color = Color.Red;
            this.chart1.Series[1].BorderWidth = 3;
            this.chart1.Series[1].LegendText = "1997";

            this.chart1.Series[2].XValueMember = "MyKey";
            this.chart1.Series[2].YValueMembers = "MySales3";
            this.chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[2].Color = Color.Blue;
            this.chart1.Series[2].BorderWidth = 3;
            this.chart1.Series[2].LegendText = "1998";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            chart1.DataSource = null;
            chart1.Series.Clear();
            chart1.Series.Add("S1");
            chart1.Series.Add("S2");
            var q = from n in dbcontext.Order_Details.AsEnumerable()
                    group n by n.Product.Category.CategoryName into g
                    orderby g.Sum(i => i.Quantity * i.UnitPrice) descending
                    select new
                    {
                        MyKey = g.Key,
                        MySales1 = (g.Where(i => i.Order.OrderDate.Value.Year == 1997).Sum(i => i.Quantity * i.UnitPrice)- g.Where(i => i.Order.OrderDate.Value.Year == 1996).Sum(i => i.Quantity * i.UnitPrice) )/ g.Where(i => i.Order.OrderDate.Value.Year == 1996).Sum(i => i.Quantity * i.UnitPrice),
                        MySales2 = (g.Where(i => i.Order.OrderDate.Value.Year == 1998).Sum(i => i.Quantity * i.UnitPrice)- g.Where(i => i.Order.OrderDate.Value.Year == 1997).Sum(i => i.Quantity * i.UnitPrice) )/ g.Where(i => i.Order.OrderDate.Value.Year == 1997).Sum(i => i.Quantity * i.UnitPrice),
                        MyGroup = g
                    };
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MySales1";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[0].Color = Color.Green;
            this.chart1.Series[0].BorderWidth = 3;
            this.chart1.Series[0].LegendText = "1997/1996";

            this.chart1.Series[1].XValueMember = "MyKey";
            this.chart1.Series[1].YValueMembers = "MySales2";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].Color = Color.Red;
            this.chart1.Series[1].BorderWidth = 3;
            this.chart1.Series[1].LegendText = "1998/1997";
        }
    }
}
