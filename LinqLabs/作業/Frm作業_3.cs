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
    
    public partial class Frm作業_3 : Form
    {
        List<Student> students_scores;
        public Frm作業_3()
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
                                            new Student{ Name = "ggg", Class = "CS_106", Chi = 10, Eng = 77, Math = 11, Gender = "Female" },
                                            new Student{ Name = "hhh", Class = "CS_106", Chi = 55, Eng = 52, Math = 22, Gender = "Female" },

                                          };
        }

        private void button33_Click(object sender, EventArgs e)
        {
            int g1=0,g2=0,g3=0;
            foreach(Student s in students_scores)
            {
                if(s.Math >= 90)
                {
                    g1++;
                }
                else if(s.Math >=70)
                {
                    g2++;
                }
                else
                {
                    g3++;
                }
            }
            chart1.DataSource = new List<Point> { new Point(1, g3), new Point(2, g2), new Point(3, g1) };
            this.chart1.Series[0].XValueMember = "X";     //point  X 屬性
            this.chart1.Series[0].YValueMembers = "Y";   //point  Y 屬性
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[0].Color = Color.Red;
            this.chart1.Series[0].BorderWidth = 4;
            this.chart1.Series[0].LegendText = "加強 佳 優 人數";
        }

        private void Frm作業_3_Load(object sender, EventArgs e)
        {

        }

        private void button36_Click(object sender, EventArgs e)
        {
            int i = 0;
            chart1.DataSource = students_scores.Select(p => new Point(++i,p.Chi)).ToList();
            this.chart1.Series[0].XValueMember = "X";     //point  X 屬性
            this.chart1.Series[0].YValueMembers = "Y";   //point  Y 屬性
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[0].Color = Color.Red;
            this.chart1.Series[0].BorderWidth = 4;
            this.chart1.Series[0].LegendText = "國文";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            chart1.DataSource = students_scores.Select(p => new Point(++i, p.Eng)).ToList();
            this.chart1.Series[0].XValueMember = "X";     //point  X 屬性
            this.chart1.Series[0].YValueMembers = "Y";   //point  Y 屬性
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[0].Color = Color.Red;
            this.chart1.Series[0].BorderWidth = 4;
            this.chart1.Series[0].LegendText = "英文";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = 0;
            chart1.DataSource = students_scores.Select(p => new Point(++i, p.Math)).ToList();
            this.chart1.Series[0].XValueMember = "X";     //point  X 屬性
            this.chart1.Series[0].YValueMembers = "Y";   //point  Y 屬性
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[0].Color = Color.Red;
            this.chart1.Series[0].BorderWidth = 4;
            this.chart1.Series[0].LegendText = "數學";
        }

        int st = 0;
        private void button37_Click(object sender, EventArgs e)
        {
            int i = 0;
            chart1.DataSource = new List<Point> { new Point(1,students_scores[st].Chi), new Point(2, students_scores[st].Eng), new Point(3, students_scores[st].Math) };
            this.chart1.Series[0].XValueMember = "X";     //point  X 屬性
            this.chart1.Series[0].YValueMembers = "Y";   //point  Y 屬性
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[0].Color = Color.Red;
            this.chart1.Series[0].BorderWidth = 4;
            this.chart1.Series[0].LegendText = students_scores[st].Name + " 國英數";
            st = (st + 1) % students_scores.Count;
        }
    }
    public class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Chi { get; set; }
        public int Eng { get; internal set; }
        public int Math { get; set; }
        public string Gender { get; set; }

        public string Avg
        {
            get
            {
                return $"{new int[] { Chi, Eng, Math }.Average():N2}";
            }
        }

        public int Sum
        {
            get
            {
                return Chi + Eng + Math;
            }
        }

        public int Max
        {
            get
            {
                return new int[] { Chi, Eng, Math }.Max();
            }
        }
        public int Min
        {
            get
            {
                return new int[] { Chi, Eng, Math }.Min();
            }
        }
    }
}
