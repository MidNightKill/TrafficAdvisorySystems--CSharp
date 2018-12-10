using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Admin1
{
    class Admin
    {
        private ArrayList zhanghao;
        private ArrayList mima;
        public Admin()
        {
            zhanghao = new ArrayList();
            mima = new ArrayList();
            Get_ZH_Data();
            
        }
        public bool Had_zhanghao(string ZH)
        {
            foreach(string str in zhanghao)
            {
                if(str==ZH)
                {
                    return true;
                }
            }
            return false;
        }
        private void Get_ZH_Data()
        {
            StreamReader srZH = new StreamReader("data/admin.txt");
            string str;
            while ((str = srZH.ReadLine()) != null)
            {
                zhanghao.Add(str);
                str = srZH.ReadLine();
                mima.Add(str);
            }
            srZH.Close();
        }
        private string Jiami_ZH(string ZH)
        {
            return ZH;
        }
        private string Jiami_MM(string MM)
        {
            return MM;
        }
        private string Jiemi_ZH(string ZH)
        {
            return ZH;
        }
        private string Jiemi_MM(string MM)
        {
            return MM;
        }
        public ArrayList Get_ZH_List()
        {
            return zhanghao;
        }
        public ArrayList Get_MM_List()
        {
            return mima;
        }
        public bool ZH_MM_TRUE(string ZH,string MM)
        {
            int i = 0;
            foreach(string str in zhanghao)
            {
                if(str==ZH)
                {
                    if(mima[i].ToString()==MM)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                i++;
            }
            return false;
        }
        public void Add_admin(string ID,string MM)
        {
            if (Had_zhanghao(ID)) return;
            zhanghao.Add(ID);
            mima.Add(MM);
            Out_Updata();
        }
        public void Del_admin(string ID)
        {
            int i=0;
            foreach(string str in zhanghao)
            {
                if(str==ID)
                {
                    zhanghao.RemoveAt(i);
                    mima.RemoveAt(i);
                    return;
                }
                i++;
            }
            Out_Updata();
        }
        public void Out_Updata()
        {
            StreamWriter sw = new StreamWriter("admin.txt");
            for(int i=0;i<zhanghao.Count;i++)
            {
                sw.WriteLine(zhanghao[i].ToString());
                sw.WriteLine(mima[i].ToString());
            }
            sw.Close();
        }
    }
}
