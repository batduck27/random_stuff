using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
 
    public partial class Form1 : Form
    {   
        private Thread timerThread = null;   
        private int[,] mat, v;
        private Label[] caseta=new Label[16];
        public int maxl,x,y,cnt,mutari;
        static int[] vert={1,-1,0,0},oriz={0,0,1,-1};
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {   NewGame(3);
            Control.CheckForIllegalCrossThreadCalls = false;
            timerThread = new Thread(new ThreadStart(timp));
            timerThread.IsBackground = true;
            timerThread.Start();
        }

        private void treix_Click(object sender, EventArgs e)
        {
            treix.Enabled = false;
            patrux.Enabled = true;
            NewGame(3);
            timerThread.Abort();
            Control.CheckForIllegalCrossThreadCalls = false;
            timerThread = new Thread(new ThreadStart(timp));
            timerThread.IsBackground = true;
            timerThread.Start();
        }

        private void patrux_Click(object sender, EventArgs e)
        {
            treix.Enabled = true;
            patrux.Enabled = false;
            NewGame(4);
            timerThread.Abort();
            Control.CheckForIllegalCrossThreadCalls = false;
            timerThread = new Thread(new ThreadStart(timp));
            timerThread.IsBackground = true;
            timerThread.Start();
        }

        private void New_game_Click(object sender, EventArgs e)
        {   
            
            if (treix.Enabled == false) { NewGame(3); }
            else if (patrux.Enabled == false) { NewGame(4); }
            else MessageBox.Show("Eroare! Restartati aplicatia!");
            timerThread.Abort();
            Control.CheckForIllegalCrossThreadCalls = false;
            timerThread = new Thread(new ThreadStart(timp));
            timerThread.IsBackground = true;
            timerThread.Start();
        }

        private void caseta_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            int lin = lbl.Location.Y,col=lbl.Location.X,val=360/maxl,lol;
            lin /= val;
            col /= val;
            lol = lin + col * maxl;
            int coordx, coordy;
            bool sw=false;
            for (int i = 0; i <= 3 && sw==false; i++)
            {
                coordx=lin+vert[i];
                coordy=col+oriz[i];
                if(coordx==x && coordy==y)
                {
                    sw=true;
                    caseta[cnt].BackColor = Color.SkyBlue;
                    caseta[cnt].BorderStyle = BorderStyle.Fixed3D;
                    caseta[cnt].Visible = true;
                    caseta[cnt].Text = mat[lin, col].ToString();
                    x=lin;y=col;
                    cnt = x * maxl + y;
                    caseta[cnt].BackColor = Color.Transparent;
                    caseta[cnt].BorderStyle = BorderStyle.None;
                    caseta[cnt].Visible = false;
                    caseta[cnt].Text = "";
                    mat[coordx,coordy]=mat[lin,col];
                    mat[lin,col]=0;
                }
            }
            
            if(sw==true) mutari++;
            Mutari.Text = mutari.ToString();
            if (check())
            {
                timerThread.Abort();
                string str=string.Format("Felicitari! Ati aranjat numerele in {0} mutari!",mutari);
                MessageBox.Show(str);
                Control.CheckForIllegalCrossThreadCalls = false;
                timerThread = new Thread(new ThreadStart(timp));
                timerThread.IsBackground = true;
                timerThread.Start();
                NewGame(maxl);
                
            }

        }

        private void NewGame(int val)
        {
            Mutari.Text = 0.ToString();
            mutari = 0;     
            if (val == 3) { treigen(); }
            else if (val == 4) { patrugen(); }
            else MessageBox.Show("Eroare! Restartati aplicatia!");
        }

        public void treigen()
        {
            playground.Controls.Clear();
            mat=new int [3,3] {{1,2,3},{4,5,6},{7,8,0},};
            v = mat;
            maxl = 3;
            while(check())
                amesteca();
            int k=0;
            for (int i = 0; i < 3; i++)
            {
                
                for (int j = 0; j < 3; j++)
                {
                        
                    caseta[k] = new Label();
                    caseta[k].Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    caseta[k].Width = 120;
                    caseta[k].Height = 120;
                    caseta[k].Left = j * 120;
                    caseta[k].Top = i * 120;
                    caseta[k].Tag = 0;
                    caseta[k].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    if (mat[i,j]!= 0)
                    {
                        caseta[k].BackColor = Color.SkyBlue;
                        caseta[k].BorderStyle = BorderStyle.Fixed3D;
                        caseta[k].Text = mat[i, j].ToString();
                    }
                    else
                    {
                        caseta[k].BackColor = Color.Transparent;
                        caseta[k].BorderStyle = BorderStyle.None;
                        caseta[k].Text = "";
                        caseta[k].Visible = false;
                        cnt = k;
                    }
                    caseta[k].Click += new EventHandler(caseta_Click);
                    playground.Controls.Add(caseta[k]);
                    k++;
                }
            }
        }
        public void patrugen()
        {

            playground.Controls.Clear();
            mat = new int[4,4]{{1,2,3,4},{5,6,7,8},{9,10,11,12},{13,14,15,0},};
            v = mat;
            maxl = 4;
            while(check())
                amesteca();
            int k = 0;
            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 4; j++)
                {

                    caseta[k] = new Label();
                    caseta[k].Font = new Font("Arial", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    caseta[k].Width = 90;
                    caseta[k].Height = 90;
                    caseta[k].Left = j * 90;
                    caseta[k].Top = i * 90;
                    caseta[k].Tag = 0;
                    caseta[k].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    if (mat[i, j] != 0)
                    {
                        caseta[k].BackColor = Color.SkyBlue;
                        caseta[k].BorderStyle = BorderStyle.Fixed3D;
                        caseta[k].Text = mat[i, j].ToString();
                    }
                    else
                    {
                        caseta[k].BackColor = Color.Transparent;
                        caseta[k].BorderStyle = BorderStyle.None;
                        caseta[k].Text = "";
                        caseta[k].Visible = false;
                        cnt = k;
                    }
                    caseta[k].Click += new EventHandler(caseta_Click);
                    playground.Controls.Add(caseta[k]);
                    k++;
                }
            }
        }

        private void amesteca()
        {
            Random rnd = new Random();
            int nr = 30+maxl/2*50;
            int directie,fost=0;
            x=maxl-1;
            y=maxl-1;
            maxl--;
            for (int i = 1; i <= nr; i++)
            {
                directie = rnd.Next(4);
                switch (directie)
                {
                        //sus
                    case 0:
                        {
                            if(x>0 && fost!=directie)
                            {
                                mat[x,y]=mat[x-1,y];
                                mat[x-1,y]=0;
                                x--;
                                fost = directie;
                            }
                        }
                        break;
                        //---->
                    case 1:
                        {
                            if(y<maxl && fost!=directie)
                            {
                                mat[x,y]=mat[x,y+1];
                                mat[x,y+1]=0;
                                y++;
                                fost = directie;
                            }
                        }
                        break;
                        //<----
                    case 2:
                        {
                            if(y>0 && fost!=directie)
                            {
                                mat[x,y]=mat[x,y-1];
                                mat[x,y-1]=0;
                                y--;
                                fost = directie;
                            }
                        }
                        break;
                        //jos
                    case 3:
                        {
                            if(x<maxl && fost!=directie)
                            {
                                mat[x,y]=mat[x+1,y];
                                mat[x+1,y]=0;
                                x++;
                                fost = directie;
                            }
                        }
                        break;
                    default:
                        {
                            i--;
                        }
                        break;

                }
            }
            maxl++;
            
        }
        private void timp()
        {
            string str,minut,secunda;
            int min;
            for (min = 0; min < 60; min++)
            {
                if (min < 10) minut = string.Format("00:0{0}:", min);
                else minut = string.Format("00:{0}:", min);
                for (int sec = 0; sec < 60; sec++)
                {
                    if (sec < 10) secunda = string.Format("0{0}", sec);
                    else secunda = string.Format("{0}", sec);
                    str=minut+secunda;
                    Timp.Text = str;
                    Thread.Sleep(1000);
                }
            }
            if (min == 60)
            {
                this.Enabled = false;
                MessageBox.Show(" Si un melc s-ar descurca mai bine ca tine! ");
                Application.Exit();
            }
        }
        private bool check()
        {

            if (maxl == 3) v = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 }, };
            else v = new int[4, 4] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 0 }, };
            for (int i = 0; i < maxl; i++)
            {
                for (int j = 0; j < maxl; j++)
                {
                    if (mat[i, j] != v[i, j])
                        return false;
                }
            }

            return true;
        }

        private void About_Click(object sender, EventArgs e)
        {
            Form f2= new AboutPuzzle();
            f2.Show();
        }
    }
}
