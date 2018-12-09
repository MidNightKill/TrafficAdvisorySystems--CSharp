using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private Form1 frm1 = null;
        private Thread thread1 = null;
        private delegate void c_button();
        private delegate void c_lable();
        private c_button button1_enable=null;
        private c_lable label3_Do = null;
        private c_lable label3_End = null;
        private Admin1.Admin admin;
        private bool Flag = false;
        public Form2(Form1 frm)
        {
            InitializeComponent();
            this.frm1 = frm;
            button1_enable=new c_button(Set_button1_Enable_true);
            label3_Do = new c_lable(Set_label_Do);
            label3_End = new c_lable(Set_label_Null);
             admin= new Admin1.Admin();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void Form2_Closing(object sender, FormClosingEventArgs e)
        {

            //this.frm1.Show();
            //this.Hide();
            //e.Cancel = true;
            // System.Environment.Exit(0);
            this.frm1.Show();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Form2";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_sleep()
        {
            try
            {
                label3.Invoke(label3_Do);
                Thread.Sleep(1000);
                label3.Invoke(label3_End);
                while (true)
                {
                    button1.Invoke(button1_enable);
                    if (button1.Enabled == true)
                    {
                        thread1.Abort();
                        return;
                    }
                }
            
            }
            catch
            {

            }
           
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Flag = false;
            button1.Enabled = false;
            thread1 = new Thread(new ThreadStart(button1_sleep));
            thread1.Start();
            string ID = textBox1.Text.ToString();
            string MM = textBox2.Text.ToString();
            if (admin.ZH_MM_TRUE(ID, MM))
            {
                Flag = true;

            }
            else
            {
                Flag = false;
            }
            if (Flag)
            {
                Admin_Success(frm1);
                this.Dispose();
            }
            else
            {
                label3.Text = "账号或密码错误";
                MessageBox.Show("登录失败");
            }
        }
        private void Set_button1_Enable_true()
        {
            button1.Enabled = true;
        }
        private void Set_label_Null()
        {
            label3.Text = "";
        }
        private void Set_label_Do()
        {
            label3.Text = "数据处理中";
        }
        public bool Admin_Success()
        {
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin_Success(frm1);
            this.Dispose();
        }
        void Admin_Success(Form1 frm)
        {
            frm.Text = "交通咨询 状态：已登录";
            frm.Show_TabPag1();
            frm.Show_TabPag6();
            frm.Show_TabPag7();
            frm.Show_Admin_Button();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm1.Show();
            this.Dispose();
        }
    }
}
