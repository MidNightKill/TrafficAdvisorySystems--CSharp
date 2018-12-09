using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        private Form1 frm1;
        public Form3(Form1 frm)
        {
            InitializeComponent();
            this.frm1 = frm;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            this.Show();
            
            if (IS_Directory() && IS_File() && Data_True())
            {
                MessageBox.Show("数据完整性检验成功");
            }
            else
            {
                MessageBox.Show("检测到文件不完整，已进行初始化");
            }
            frm1.Show();
            this.Dispose();
        }
        private void Form3_Shown(object sender, EventArgs e)
        {
           
            
        }
        private bool IS_Directory()
        {
            if(Directory.Exists("data"))
            {
                return true;
            }
            else
            {
                Directory.CreateDirectory("data");
                Thread.Sleep(10);
                IS_File();
                return false;
            }
        }
        private bool IS_File()
        {
            if (!File.Exists("data/admin.txt"))
            {
                var fl = File.Create("data/admin.txt");
                fl.Close();
            }
            if (!File.Exists("data/Trans.txt"))
            {
                var fclo = File.Create("data/Trans.txt");
                fclo.Close();
            }
            if (File.Exists("data/CityName.txt") && File.Exists("data/Road.txt"))
            {
                return true;

            }
             else
            {
                var fclo=File.Create("data/CityName.txt");
                fclo.Close();
                fclo=File.Create("data/Road.txt");
                fclo.Close();
                return false;
            }
        }
        private bool Data_True()
        {
            string Road = File.ReadAllText("data/Road.txt");
            StreamReader sr1 = new StreamReader("data/CityName.txt");
            int i = 0;
            while (sr1.ReadLine() != null) i++;
            sr1.Close();
            if ((i*i) != Road.Length)
            {
                var fclo = File.Create("data/CityName.txt");
                fclo.Close();
                fclo = File.Create("data/Road.txt");
                fclo.Close();
                return false;
            }
            else return true;
        }

    }
}
