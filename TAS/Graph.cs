using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Graph
{
    class Graph1
    {
        private int numCity=0;
        private int numRoad=0;
//        private int[] CityList;
        private int[][] Road=null;
        private string[] CityName=null;
        public Graph1()
        {
            numCity = Get_City_num_Data();
            numRoad = Get_Road_num_Data();
            Get_City_Name_Data();
            Get_Road_Data();
        }
        private int Get_City_num_Data()
        {
            StreamReader sr1 = new StreamReader("data/CityName.txt");
            int num = 0;
            while (sr1.ReadLine() != null) num++;
            sr1.Close();
            return num;
        }
        private int Get_Road_num_Data()
        {
            return numCity*numCity;
        }
        private void Get_City_Name_Data()
        {
            CityName = new string[numCity];
            CityName = File.ReadAllLines("data/CityName.txt");
        }
        private void Get_Road_Data()
        {
            Road= new int[numCity][];
            int m;
           for(int i=0;i< numCity; i++)
            {
                Road[i] = new int[numCity];
            }
            StreamReader sr1 = new StreamReader("data/Road.txt");
            for(int i=0;i< numCity; i++)
            {
                for(int j=0;j< numCity; j++)
                {
                    m = sr1.Read();
                    if(m>='0'&&m<='9')
                    {
                        Road[i][j] = m;
                        Road[i][j] -= 48;
                    }
                   
                }
            }
            sr1.Close();
        }
        private int FirstNeighbor(int ID)
        {
            if (ID < 0 || ID >= numCity) return -1;
            else
            {
                for(int i=0;i<numCity;i++)
                {
                    if (Road[ID][i] > 0) return i;
                }
            }
            return -1;
        }
        private int NextNeighbor(int ID,int w) 
        {
            if (ID < 0 || ID >= numCity) return -1;
            if (w < 0) return -1;
            for(int i=w+1;i<numCity;i++)
            {
                if (Road[ID][i] > 0) return i;
            }
            return -1;
        }
        public int NameToID(string name)
        {
            for(int i=0;i<CityName.Length;i++)
            {
                if(CityName[i]==name)
                {
                    return i;
                }
            }
            return -1;
        }
        public void Change_Road(int ID1,int ID2,int i)
        {
            Road[ID1][ID2] = i;
        }
        public void Change_Road(string Name1, string Name2, int i)
        {
            int ID1 = this.NameToID(Name1);
            int ID2 = this.NameToID(Name2);
            Road[ID1][ID2] = i;
        }
        public string IDToName(int ID)
        {
            return CityName[ID];
        }
        public int Get_City_Num()
        {
            return numCity;
        }
        public bool Road_True(int ID1, int ID2)
        {
            if (ID1 < 0 || ID2 < 0) return false;
            if (Road[ID1][ID2] == 0) return false;
            else return true;
        }
        public bool Road_True(string name1,string name2)
        {
            int ID1 = NameToID(name1);
            int ID2 = NameToID(name2);
            return Road_True(ID1, ID2);
        }
        public bool City_True(string name)
        {
            for(int i=0;i<numCity;i++)
            {
                if (CityName[i] == name) return true;
            }
            return false;
        }
        public bool City_True(int ID)
        {
            string name = IDToName(ID);
            return City_True(name);
        }
        public bool IN_Data_Updata()
        {
            numCity = Get_City_num_Data();
            numRoad = Get_Road_num_Data();
            Get_City_Name_Data();
            Get_Road_Data();
            return true;
        }
        public bool Out_Data_Updata()
        {
            StreamWriter swCityName = new StreamWriter("data/CityName.txt");
            StreamWriter swRoad = new StreamWriter("data/Road.txt");
            for(int i=0;i<numCity;i++)
            {
                swCityName.WriteLine(CityName[i]);
            }
            for(int i=0;i<numCity;i++)
            {
                for(int j=0;j<numCity;j++)
                {
                    swRoad.Write(Road[i][j]);
                }
            }
            swCityName.Close();
            swRoad.Close();
            return true;
        }
        public bool Add_City(string name)
        {
            if (NameToID(name) > -1) return false;
            StreamWriter swCityname = new StreamWriter("data/CityName.txt");
            StreamWriter swRoad = new StreamWriter("data/Road.txt");
            for(int i=0;i<numCity;i++)
            {
                swCityname.WriteLine(CityName[i]);
            }
            swCityname.WriteLine(name);
            for(int i=0;i<numCity;i++)
            {
                for(int j=0;j<numCity;j++)
                {
                    swRoad.Write(Road[i][j]);
                }
                swRoad.Write('0');
            }
            for (int i = 0; i <= numCity; i++) swRoad.Write('0');
            swCityname.Close();
            swRoad.Close();
            IN_Data_Updata();
            return true;
        }
        public bool Del_City(string name)
        {
            int nameid = NameToID(name);
            for(int i=0;i<numCity;i++)
            {
                if(Road[nameid][i]>0)
                {
                    Del_Trans(name, IDToName(i));
                }
                if(Road[i][nameid]>0)
                {
                    Del_Trans(IDToName(i), name);
                }
            }
            StreamWriter swDelCity = new StreamWriter("data/CityName.txt");
            StreamWriter swDelRoad = new StreamWriter("data/Road.txt");
            for (int i=0;i<numCity;i++)
            {
                if (CityName[i] == name) continue;
                else
                {
                    swDelCity.WriteLine(CityName[i]);
                }
            }
            for(int i=0; i<numCity;i++)
            {
                if (i == nameid) continue;
                for(int j=0;j<numCity;++j)
                {
                    if (j == nameid) continue;
                    swDelRoad.Write(Road[i][j]);
                }
            }
            swDelCity.Close();
            swDelRoad.Close();
            this.IN_Data_Updata();
            return true;
        }
        public string Get_Road_Trans(string Name1,string Name2)
        {
            if(!Road_True(Name1,Name2))
            {
                return null;
            }
            string str1 = Name1 +"To"+ Name2;
            StreamReader sr = new StreamReader("data/Trans.txt");
            while (sr.ReadLine() != str1) ;
            str1 = sr.ReadLine();
            sr.Close();
            return str1;
            
        }
        public string Get_Road_Trans(int id1,int id2)
        {
            string name1 = IDToName(id1);
            string name2 = IDToName(id2);
            return Get_Road_Trans(name1, name2);
        }
        public float Get_Money(string Name1, string Name2)
        {
            if (!Road_True(Name1, Name2))
            {
                return 0;
            }
            string str1 = Name1 + "To" + Name2;
            StreamReader sr = new StreamReader("data/Trans.txt");
            while (sr.ReadLine() != str1) ;
            sr.ReadLine();
            str1 = sr.ReadLine();
            float result = 0;
            try
            {
                result = float.Parse(str1);
            }
            catch
            {
                MessageBox.Show("数据转换出现异常!");
                sr.Close();
                return 0;
            }
            sr.Close();
            return result;
        }
        public float Get_Money(int id1,int id2)
        {
            string name1 = IDToName(id1);
            string name2 = IDToName(id2);
            return Get_Money(name1, name2);
        }
        public long Get_Go_Time(string Name1, string Name2)
        {
            if (!Road_True(Name1, Name2))
            {
                return 0;
            }
            string str1 = Name1 + "To" + Name2;
            StreamReader sr = new StreamReader("data/Trans.txt");
            while (sr.ReadLine() != str1) ;
            sr.ReadLine();
            sr.ReadLine();
            str1 = sr.ReadLine();
            long result=0;
            try
            {
                result = long.Parse(str1);
            }
            catch
            {
                MessageBox.Show("数据转换出现异常!");
                sr.Close();
                return 0;
            }
            sr.Close();
            return result;
        }
        public long Get_Go_Time(int ID1,int ID2)
        {
            string name1 = IDToName(ID1);
            string name2 = IDToName(ID2);
            return Get_Go_Time(name1, name2);
        }
        public long Get_Trans_Time(string Name1, string Name2)
        {
            if (!Road_True(Name1, Name2))
            {
                return 0;
            }
            string str1 = Name1 + "To" + Name2;
            StreamReader sr = new StreamReader("data/Trans.txt");
            while (sr.ReadLine() != str1) ;
            sr.ReadLine();
            sr.ReadLine();
            sr.ReadLine();
            str1 = sr.ReadLine();
            long result = 0;
            try
            {
                result = long.Parse(str1);
            }
            catch
            {
                MessageBox.Show("数据转换出现异常!");
                sr.Close();
                return 0;
            }
            sr.Close();
            return result;
        }
        public long Get_Trans_Time(int ID1, int ID2)
        {
            string name1 = IDToName(ID1);
            string name2 = IDToName(ID2);
            return Get_Trans_Time(name1, name2);
        }
        public void Out_Trans_Data(string Name1,string Name2,string trans,
                                   float money,long GO_Time,long Trans_Time)
        {
            if (Road_True(Name1, Name2))
            {
                return;
            }
            StreamWriter swTrans = new StreamWriter("data/Trans.txt", true);
            swTrans.WriteLine(Name1+"To"+Name2);
            swTrans.WriteLine(trans);
            swTrans.WriteLine(money.ToString());
            swTrans.WriteLine(GO_Time.ToString());
            swTrans.WriteLine(Trans_Time.ToString());
            swTrans.Close();
        }
        public void Del_Trans(string Name1,string Name2)
        {
            System.Collections.ArrayList arr1 = new System.Collections.ArrayList();
            StreamReader srDelTrans = new StreamReader("data/Trans.txt");
            string str;
            while(true)
            {
                str = srDelTrans.ReadLine();
                if (str == null) break;
                if(str==(Name1+"To"+Name2))
                {
                    srDelTrans.ReadLine();
                    srDelTrans.ReadLine();
                    srDelTrans.ReadLine();
                    srDelTrans.ReadLine();
                    continue;
                }
                arr1.Add(str);
            }
            srDelTrans.Close();
            StreamWriter swDelTrans = new StreamWriter("data/Trans.txt");
            foreach(string str1 in arr1)
            {
                swDelTrans.WriteLine(str1);
            }
            swDelTrans.Close();
            this.Change_Road(Name1, Name2, 0);
            this.Out_Data_Updata();
        }

        public void Del_Trans(int ID1,int ID2)
        {
            string name1 = IDToName(ID1);
            string name2 = IDToName(ID2);
            Del_Trans(name1, name2);
        }
        public bool Have_Road_DFS_Traversal(int ID1,int ID2)
        {
            int[] Visited = new int[numCity];
            for (int i = 0; i < numCity; i++) Visited[i] = 0;
            bool flag = false;
            if (Have_Road_DFS(ID1, ID2, Visited, ref flag)) return true;
            else return false;
        }
        public bool Have_Road_DFS_Traversal(string name1, string name2)
        {
            int ID1 = NameToID(name1);
            int ID2 = NameToID(name2);
            return Have_Road_DFS_Traversal(ID1, ID2);
        }
        private bool Have_Road_DFS(int ID1,int ID2,int []Visited,ref bool Flag)
        {
            if (ID1 < 0 || ID2 < 0) return false;
            if (Road[ID1][ID2] > 0) Flag = true;
            if (Flag) return true;
            Visited[ID1] = 1;
            int w = FirstNeighbor(ID1);
            while(w!=-1&&Flag!=true)
            {
                if (Visited[w] == 0) Have_Road_DFS(w, ID2, Visited,ref Flag);
                w = NextNeighbor(ID1, w);
            }
            if (Flag) return true;
            return false;
        }
        public Queue<int> Get_Min_Vister_BFS_Traversal(int ID1,int ID2)
        {
            int k, w;
            int[] path = new int[numCity];
            int[] dist = new int[numCity];
            for(int i=0;i<numCity;i++)
            {
                dist[i] = -1;
            }
            Queue <int>Qu1 = new Queue<int>();
            Qu1.Enqueue(ID1);
            dist[ID1] = 0;
            while (Qu1.Count>0)
            {
                k = Qu1.Dequeue();
                w = FirstNeighbor(k);
                while(w!=-1)
                {
                    if(dist[w]==-1)
                    {
                        dist[w] = dist[k] + 1;
                        path[w] = k;
                        Qu1.Enqueue(w);
                    }
                    w = NextNeighbor(k, w);
                }
            }
            Qu1.Clear();
            Stack<int> St1 = new Stack<int>();
            int ii = ID2;
            for (; path[ii] != ID1; ii = path[ii])
            {
                Qu1.Enqueue(ii);
            }
            Qu1.Enqueue(ii);
            Qu1.Enqueue(ID1);
            while (Qu1.Count > 0) St1.Push(Qu1.Dequeue());
            while (St1.Count > 0) Qu1.Enqueue(St1.Pop());
            return Qu1;
        }
        public Queue<int> Get_Min_Vister_BFS_Traversal(string name1,string name2)
        {
            int ID1 = NameToID(name1);
            int ID2 = NameToID(name2);
            return Get_Min_Vister_BFS_Traversal(ID1, ID2);
        }
        private Queue<int> Del_Queue_eq(Queue<int> Qu1,int k)
        {
            Queue<int> Qu2 = new Queue<int>();
            Queue<int> Qu3 = new Queue<int>();
            if (Qu1.Count < 3) return Qu1;
            int m = Qu1.Dequeue(),n;
            Qu2.Enqueue(m);
            while(true)
            {
                n = m;
                for(int i=0;Qu1.Count>0;i++)
                {
                    m = Qu1.Dequeue();
                    if(Road[m][k]>0&&Road[n][m]>0)
                    {
                        Qu2.Enqueue(m);
                        return Qu2;
                    }
                }
                while (Qu3.Count > 0) Qu1.Enqueue(Qu3.Dequeue());
                m = Qu1.Dequeue();
                Qu2.Enqueue(m);
            }

        }
        public Queue<int> ShortestPath_Time(int ID1,int ID2)
        {
            long[] Dist = new long[numCity];
            int[] path = new int[numCity];
            int[] S = new int[numCity];
            long w, min;
            for(int i=0;i<numCity;i++)
            {
                if (Road[ID1][i] > 0) Dist[i] = Get_Trans_Time(ID1, i);
                else Dist[i] = -1;
                S[i] = 0;
                if (i != ID1 && Dist[i] != -1) path[i] = ID1;
                else path[i] = -1;
            }
            S[ID1] = 1;Dist[ID1] = 0;
            for (int i = 0; i < numCity - 1; i++)
            {
                min = -1;
                int u = ID1;
                for (int j = 0; j < numCity; j++)
                {
                    if (Road[ID1][j] == 0&&Dist[j]==-1) continue;
                    if (S[j] == 0 && (Dist[j] < min ||(Dist[j]!=-1&&min==-1)))
                    {
                        u = j;
                        min = Dist[j];
                    }
                }
                S[u] = 1;
                for (int k = 0; k < numCity; k++)
                {
                    if (Road[u][k] == 0) continue;
                    w = Get_Trans_Time(u, k);
                    if(path[u]!=-1)
                    {
                        int n = path[u];
                        long m = Get_Go_Time(n, u) + Get_Trans_Time(n, u);
                        w += Wait_Time(Get_Go_Time(u,k),m);
                    }
                    if (S[k] == 0 && (Dist[u] + w < Dist[k]||Dist[k]==-1))
                    {
                        Dist[k] = Dist[u] + w;
                        path[k] = u;
                    }
                }

            }
            Queue<int> Qu1 = new Queue<int>();
            Stack<int> St1 = new Stack<int>();
            int ii = ID2;
            for (;path[ii]!=ID1;ii=path[ii])
            {
                Qu1.Enqueue(ii);
            }
            Qu1.Enqueue(ii);
            Qu1.Enqueue(ID1);
            while (Qu1.Count > 0) St1.Push(Qu1.Dequeue());
            while (St1.Count > 0) Qu1.Enqueue(St1.Pop());
            return Qu1;
        }
        public Queue<int> ShortestPath_Time(string name1,string name2)
        {
            int id1 = NameToID(name1);
            int id2 = NameToID(name2);
            return ShortestPath_Time(id1, id2);
        }
        public Queue<int> ShortestPath_Money(int ID1, int ID2)
        {
            float[] Dist = new float[numCity];
            int[] path = new int[numCity];
            int[] S = new int[numCity];
            float w, min;
            for (int i = 0; i < numCity; i++)
            {
                if (Road[ID1][i] > 0) Dist[i] = Get_Money(ID1, i);
                else Dist[i] = -1;
                S[i] = 0;
                if (i != ID1 && Dist[i] != -1) path[i] = ID1;
                else path[i] = -1;
            }
            S[ID1] = 1; Dist[ID1] = 0;
            for (int i = 0; i < numCity - 1; i++)
            {
                min = -1;
                int u = ID1;
                for (int j = 0; j < numCity; j++)
                {
                    if (Road[ID1][j] == 0 && Dist[j] == -1) continue;
                    if (S[j] == 0 && (Dist[j] < min || (Dist[j] != -1 && min == -1)))
                    {
                        u = j;
                        min = Dist[j];
                    }
                }
                S[u] = 1;
                for (int k = 0; k < numCity; k++)
                {
                    if (Road[u][k] == 0) continue;
                    w =Get_Money(u, k);
                    if (S[k] == 0 && (Dist[u] + w < Dist[k] || Dist[k] == -1))
                    {
                        Dist[k] = Dist[u] + w;
                        path[k] = u;
                    }
                }

            }
            Queue<int> Qu1 = new Queue<int>();
            Stack<int> St1 = new Stack<int>();
            int ii = ID2;
            for (; path[ii] != ID1; ii = path[ii])
            {
                Qu1.Enqueue(ii);
            }
            Qu1.Enqueue(ii);
            Qu1.Enqueue(ID1);
            while (Qu1.Count > 0) St1.Push(Qu1.Dequeue());
            while (St1.Count > 0) Qu1.Enqueue(St1.Pop());
            return Qu1;
        }
        public Queue<int> ShortestPath_Money(string name1, string name2)
        {
            int id1 = NameToID(name1);
            int id2 = NameToID(name2);
            return ShortestPath_Money(id1, id2);
        }
        private long Wait_Time(long time1,long time2)
        {
            if (time1 >= time2) return time1 - time2;
            else return time1 + 24*60 - time2;
            
        }
    }
}
