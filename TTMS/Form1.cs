using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graph;
using System.Threading;
using System.Runtime.InteropServices;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        System.Media.SoundPlayer music1;
        [DllImportAttribute("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("User32.dll")]
        public static extern bool ReleaseCapture();
        private Graph1 grp1;
        public Form1()
        {
            InitializeComponent();
            tabPage1.Parent = null;
            tabPage6.Parent = null;
            tabPage7.Parent = null;
            button14.Hide();
            button15.Hide();
            button16.Hide();
            button1.Hide();
            tabControl1.SelectedTab = tabPage8;
            //music1 = new System.Media.SoundPlayer("MU1.wav");
            //music1.PlayLooping();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3(this);
            this.Hide();
            frm3.Show();
            grp1 = new Graph1();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Click(object sender, EventArgs e)
        {
            //button1.Text = "窗口1";
            //if(frm2==null)
            //{
            //    frm2 = new Form2(this);
            //}
            //frm2.Show();
            //this.Hide();
        }
        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {

        }



        private void Get_Min_Visier_Data(string str1, string str2)
        {
            Queue<int> Qu1 = grp1.Get_Min_Vister_BFS_Traversal(str1, str2);
            int index;
            int n = Qu1.Dequeue();
            int m;
            long time = 0;
            long day = 0;
            long end_time = 0;
            dataGridView3.Rows.Clear();
            while (Qu1.Count > 0)
            {
                m = n;
                n = Qu1.Dequeue();
                index = dataGridView3.Rows.Add();
                dataGridView3.Rows[index].Cells[0].Value = grp1.IDToName(m);
                dataGridView3.Rows[index].Cells[1].Value = grp1.IDToName(n);
                if (end_time > grp1.Get_Go_Time(m, n))
                {
                    day++;
                }
                time = grp1.Get_Go_Time(m, n) + day * 24 * 60;
                end_time = grp1.Get_Trans_Time(m, n) + grp1.Get_Go_Time(m, n);
                dataGridView3.Rows[index].Cells[2].Value = Min_To_Time(time);
                time += grp1.Get_Trans_Time(m, n);
                dataGridView3.Rows[index].Cells[3].Value = Min_To_Time(time);
                dataGridView3.Rows[index].Cells[4].Value = Min_To_Time(grp1.Get_Trans_Time(m, n));
                dataGridView3.Rows[index].Cells[5].Value = grp1.Get_Money(m, n) + "元";
                dataGridView3.Rows[index].Cells[6].Value = grp1.Get_Road_Trans(m, n);
            }
        }

        private void Get_Min_Time_Data(string str1, string str2)
        {
            Queue<int> Qu1 = grp1.ShortestPath_Time(str1, str2);
            int index;
            int n = Qu1.Dequeue();
            int m;
            long time = 0;
            long day = 0;
            long end_time = 0;
            dataGridView1.Rows.Clear();
            while (Qu1.Count > 0)
            {
                m = n;
                n = Qu1.Dequeue();
                index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = grp1.IDToName(m);
                dataGridView1.Rows[index].Cells[1].Value = grp1.IDToName(n);
                if (end_time > grp1.Get_Go_Time(m, n))
                {
                    day++;
                }
                time = grp1.Get_Go_Time(m, n) + day * 24 * 60;
                end_time = grp1.Get_Trans_Time(m, n) + grp1.Get_Go_Time(m, n);
                dataGridView1.Rows[index].Cells[2].Value = Min_To_Time(time);
                time += grp1.Get_Trans_Time(m, n);
                dataGridView1.Rows[index].Cells[3].Value = Min_To_Time(time);
                dataGridView1.Rows[index].Cells[4].Value = Min_To_Time(grp1.Get_Trans_Time(m, n));
                dataGridView1.Rows[index].Cells[5].Value = grp1.Get_Money(m, n) + "元";
                dataGridView1.Rows[index].Cells[6].Value = grp1.Get_Road_Trans(m, n);
            }
        }
        private void Get_Min_Money_Data(string str1, string str2)
        {
            Queue<int> Qu1 = grp1.ShortestPath_Money(str1, str2);
            int index;
            int n = Qu1.Dequeue();
            int m;
            long time = 0;
            long day = 0;
            long end_time = 0;
            dataGridView2.Rows.Clear();
            while (Qu1.Count > 0)
            {
                m = n;
                n = Qu1.Dequeue();
                index = dataGridView2.Rows.Add();
                dataGridView2.Rows[index].Cells[0].Value = grp1.IDToName(m);
                dataGridView2.Rows[index].Cells[1].Value = grp1.IDToName(n);

                if (end_time > grp1.Get_Go_Time(m, n))
                {
                    day++;
                }
                time = grp1.Get_Go_Time(m, n) + day * 24 * 60;
                end_time = grp1.Get_Trans_Time(m, n) + grp1.Get_Go_Time(m, n);
                dataGridView2.Rows[index].Cells[2].Value = Min_To_Time(time);
                time += grp1.Get_Trans_Time(m, n);
                dataGridView2.Rows[index].Cells[3].Value = Min_To_Time(time);
                dataGridView2.Rows[index].Cells[4].Value = Min_To_Time(grp1.Get_Trans_Time(m, n));
                dataGridView2.Rows[index].Cells[5].Value = grp1.Get_Money(m, n) + "元";
                dataGridView2.Rows[index].Cells[6].Value = grp1.Get_Road_Trans(m, n);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Add_City_Is_NumberOrNull())
            {
                MessageBox.Show("城市名不能为空或者纯数字");
                return;
            }
            if (grp1.Add_City(textBox3.Text.ToString()))
            {
                MessageBox.Show("数据添加成功");
            }
            else
            {
                MessageBox.Show("已存在此城市");
            }
        }
        private bool Add_City_Is_NumberOrNull()
        {
            string str = textBox3.Text.ToString();
            for (int i = 0; i < str.Length; i++)
            {
                if ((int)str[i] > 127) return false;
                if ((int)str[i] >= 'a' && (int)str[i] <= 'z') return false;
                if ((int)str[i] >= 'A' && (int)str[i] <= 'Z') return false;
            }
            return true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2(this);
            frm2.Show();
            this.Hide();
        }
        public void Show_TabPag1()
        {
            tabPage1.Parent = this.tabControl1;
        }
        public void Show_TabPag6()
        {
            tabPage6.Parent = this.tabControl1;
        }
        public void Show_TabPag7()
        {
            tabPage7.Parent = this.tabControl1;
        }
        public void Show_Admin_Button()
        {
            button14.Show();
            button15.Show();
            button16.Show();
            button3.Hide();
            button1.Show();

        }
        private void Init_Time_Menu()
        {
            for (int i = 0; i < 24; i++)
            {
                comboBox4.Items.Add(i);
            }
            for (int i = 0; i < 60; i++)
            {
                comboBox5.Items.Add(i);
                comboBox6.Items.Add(i);
            }
            comboBox4.Text = comboBox5.Text = comboBox6.Text = "0";
        }
        private bool TextBox4_Is_Number()
        {
            int a;
            try
            {
                a = int.Parse(textBox4.Text.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool TextBox5_Is_Number()
        {
            float a;
            try
            {
                a = float.Parse(textBox5.Text.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        private long Text_To_Go_Time()
        {
            long hour = long.Parse(comboBox4.Text.ToString());
            long Min = long.Parse(comboBox5.Text.ToString());
            return hour * 60 + Min;
        }
        private long Text_To_Trans_Time()
        {
            long hour = long.Parse(textBox4.Text.ToString());
            long Min = long.Parse(comboBox6.Text.ToString());
            return hour * 60 + Min;
        }
        private float Text_To_Money()
        {
            float f1 = float.Parse(textBox5.Text.ToString());
            return f1;
        }
        private string Min_To_Time(long a)
        {
            long m = a % 60;
            a /= 60;
            long h = a % 24;
            a /= 24;
            long d = a;
            string str = "";
            if (d > 0)
            {
                str += d + "天";
            }
            str += h + "小时" + m + "分钟";
            return str;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            for (int i = 0; i < grp1.Get_City_Num(); i++)
            {
                comboBox1.Items.Add(grp1.IDToName(i));
                comboBox2.Items.Add(grp1.IDToName(i));
            }
            if (grp1.Get_City_Num() > 0)
            {
                comboBox1.Text = grp1.IDToName(0);
                comboBox2.Text = grp1.IDToName(0);
                comboBox3.Text = "飞碟";
            }
            Init_Time_Menu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string str1 = comboBox1.Text.ToString();
            string str2 = comboBox2.Text.ToString();
            if (str1 == "" || str2 == "")
            {
                MessageBox.Show("内容不能为空");
                return;
            }
            if (str1 == str2)
            {
                MessageBox.Show("出发地和目的地不能相等");
                return;
            }
            if (grp1.Road_True(str1, str2))
            {
                MessageBox.Show("当前路线已有安排");
                return;
            }
            if (!TextBox5_Is_Number())
            {
                MessageBox.Show("旅行费用必须为数字！");
                return;
            }
            if (!TextBox4_Is_Number())
            {
                MessageBox.Show("旅途时间格式不正确!");
                return;
            }
            grp1.Out_Trans_Data(comboBox1.Text.ToString(),
                                comboBox2.Text.ToString(),
                                comboBox3.Text.ToString(),
                                Text_To_Money(),
                                Text_To_Go_Time(),
                                Text_To_Trans_Time());
            grp1.Change_Road(str1, str2, 1);
            grp1.Out_Data_Updata();
            MessageBox.Show("添加线路成功");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string str1 = textBox6.Text.ToString();
            string str2 = textBox7.Text.ToString();
            if (!grp1.Road_True(str1, str2))
            {
                MessageBox.Show("当前旅途未建立路线");
                return;
            }
            label15.Text = "费用:" + grp1.Get_Money(str1, str2) + "元";
            label16.Text = "出发时刻:" + Min_To_Time(grp1.Get_Go_Time(str1, str2));
            label17.Text = "旅途时间:" + Min_To_Time(grp1.Get_Trans_Time(str1, str2));
            label19.Text = "出行方式:" + grp1.Get_Road_Trans(str1, str2);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr1 = MessageBox.Show("确认删除此条路线吗？",
                "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr1 == DialogResult.No)
            {
                return;
            }
            else
            {
                string str1 = textBox6.Text.ToString();
                string str2 = textBox7.Text.ToString();
                if (!grp1.Road_True(str1, str2))
                {
                    MessageBox.Show("当前旅途未建立路线");
                    return;
                }
                grp1.Del_Trans(str1, str2);
                MessageBox.Show("路线删除完毕");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Add_City_Is_NumberOrNull()) return;
            if (!grp1.City_True(textBox3.Text.ToString()))
            {
                MessageBox.Show("没有此城市");
                return;
            }
            grp1.Del_City(textBox3.Text.ToString());
            MessageBox.Show("城市信息删除完毕");
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string str1 = textBox1.Text.ToString();
            string str2 = textBox2.Text.ToString();

            if (str1 == "" || str2 == "")
            {
                MessageBox.Show("请输入需要查询的路线");
                return;
            }
            if (grp1.Have_Road_DFS_Traversal(str1, str2))
            {
                //                MessageBox.Show("存在通路");
                //                Get_Min_Visier_Data(str1, str2);
                Get_Min_Time_Data(str1, str2);
                //              Get_Min_Money_Data(str1, str2);
            }
            else
            {
                MessageBox.Show("当前路线没有开通");
            }
            tabControl1.SelectedTab = tabPage3;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string str1 = textBox1.Text.ToString();
            string str2 = textBox2.Text.ToString();

            if (str1 == "" || str2 == "")
            {
                MessageBox.Show("请输入需要查询的路线");
                return;
            }
            if (grp1.Have_Road_DFS_Traversal(str1, str2))
            {
                //                MessageBox.Show("存在通路");
                //                Get_Min_Visier_Data(str1, str2);
                //Get_Min_Time_Data(str1, str2);
                Get_Min_Money_Data(str1, str2);
            }
            else
            {
                MessageBox.Show("当前路线没有开通");
            }
            tabControl1.SelectedTab = tabPage4;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string str1 = textBox1.Text.ToString();
            string str2 = textBox2.Text.ToString();
            if (str1 == "" || str2 == "")
            {
                MessageBox.Show("请输入需要查询的路线");
                return;
            }
            if (grp1.Have_Road_DFS_Traversal(str1, str2))
            {
                //                MessageBox.Show("存在通路");
                Get_Min_Visier_Data(str1, str2);
                //Get_Min_Time_Data(str1, str2);
                //              Get_Min_Money_Data(str1, str2);
            }
            else
            {
                MessageBox.Show("当前路线没有开通");
            }
            tabControl1.SelectedTab = tabPage5;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            int index;

            for (int i = 0, j = grp1.Get_City_Num(); i < j; i++)
            {
                //index = dataGridView4.Rows.Add();
                //dataGridView4.Rows[index].Cells[0].Value = grp1.IDToName(i);
                for (int m = 0; m < j; m++)
                {
                    if (grp1.Road_True(i, m))
                    {
                        index = dataGridView4.Rows.Add();
                        dataGridView4.Rows[index].Cells[0].Value = grp1.IDToName(i);
                        dataGridView4.Rows[index].Cells[1].Value = grp1.IDToName(m);
                        dataGridView4.Rows[index].Cells[4].Value = Min_To_Time(grp1.Get_Trans_Time(grp1.IDToName(i), grp1.IDToName(m)));
                        dataGridView4.Rows[index].Cells[5].Value = grp1.Get_Money(grp1.IDToName(i), grp1.IDToName(m)) + "元";
                        dataGridView4.Rows[index].Cells[6].Value = grp1.Get_Road_Trans(i, m);
                        dataGridView4.Rows[index].Cells[2].Value = Min_To_Time(grp1.Get_Go_Time(i, m));
                        dataGridView4.Rows[index].Cells[3].Value = Min_To_Time(grp1.Get_Go_Time(i, m) + grp1.Get_Trans_Time(i, m));
                    }
                }
            }
            tabControl1.SelectedTab = tabPage2;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage6;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage7;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.Show();
        }
    }
}
