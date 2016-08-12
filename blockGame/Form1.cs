using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace blockGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int N = 4;
        Button[,] buttons = new Button[N, N];

        private void Form1_Load(object sender, EventArgs e)
        {
            //产生所有按钮
            GenerateAllButtons();
        }
        //测试
        private void button1_Click(object sender, EventArgs e)
        {
            //打乱顺序
            Shuffle();
        }

        void GenerateAllButtons()
        {
            int x0 = 100, y0 = 10, w = 45, d = 50;
            for(int r = 0; r < N; r++)
            {
                for(int c = 0; c < N; c++)
                {
                    int num = r * N + c;
                    Button btn = new Button();
                    btn.Text = (num + 1).ToString();
                    btn.Top = y0 + r * d;
                    btn.Left = x0 + c * d;
                    btn.Width = w;
                    btn.Height = w;
                    btn.Visible = true;
                    btn.Tag = r * N + c;

                    //注册事件
                    btn.Click +=new EventHandler(Btn_Click);

                    buttons[r, c] = btn;
                    this.Controls.Add(btn);
                }
            }
            buttons[N - 1, N - 1].Visible = false;
        }

        void Shuffle()
        {
            Random rnd = new Random();
            for(int i = 0; i < 100; i++)
            {
                int a = rnd.Next()%N;
                int b = rnd.Next()%N;
                int c = rnd.Next()%N;
                int d = rnd.Next()%N;
                Swap(buttons[a, b], buttons[c, d]);
            }
        }

        void Swap(Button btna,Button btnb)
        {
            string t = btna.Text;
            btna.Text = btnb.Text;
            btnb.Text = t;

            bool v = btna.Visible;
            btna.Visible = btnb.Visible;
            btnb.Visible = v;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Button blank = FindHiddenButton();

            //更换
            if (isNeighbor(btn,blank))
            {
                Swap(btn, blank);
                blank.Focus();
            }
            //是否完成
            if (ResultIsOk())
            {
                MessageBox.Show("OK！");
            }
            
        }

        Button FindHiddenButton()
        {
            for(int r= 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    if(buttons[r,c].Visible == false)
                    {
                        return buttons[r, c];
                    }
                }
            }
            return null;
        }

        bool isNeighbor(Button btna,Button btnb)
        {
            int a = (int)btna.Tag;
            int b = (int)btnb.Tag;
            int r1 = a / N;int c1 = a % N;
            int r2 = b / N;int c2 = b % N;

            if(r1==r2 && (c1 == c2-1||c2 == c1-1)
                ||(c1==c2) && (r1 == r2-1||r2 == r1 - 1))
            {
                return true;
            }
            return false;
        }
        bool ResultIsOk()
        {
            for(int r = 0; r < N; r++)
            {
                for(int c= 0; c < N; c++)
                {
                    if(buttons[r,c].Text != (r*N + c + 1).ToString())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
