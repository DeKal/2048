using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    enum KEY { LEFTKEY = 37, UPKEY = 38, RIGHTKEY = 39, DOWNKEY = 40 }
    public partial class game2048 : Form
    {
        private int Score=0;
        private string exePath = Program.exePath;
        private const int h2048 = 155;
        private const int w2048 = 155;
        private int INITWIDTH;
        private int INITHEIGHT;
        private int FORMHEIGHT = 800;
        private int FORMWEIGHT = 800;
        private PictureBox[] PB;
        private int[][] Table;
        private int[][] preTable;
        private int nPB;
        private PictureBox GameState;
        private Button repeat;
        
        public game2048()
        {
            InitializeComponent();
        }

        private void game2048_Load(object sender, EventArgs e)
        {

            INITWIDTH = (this.Size.Height/2 - h2048 *2);
            INITHEIGHT = (this.Size.Width/2- w2048 * 2);
            //Memory Allocated
            Table = new int[4][];
            for (int i = 0; i < 4; ++i) Table[i] = new int[4];
            
            preTable = new int[4][];
            for (int i = 0; i < 4; ++i) preTable[i] = new int[4];
            
            PB = new PictureBox[16];
            for (int i = 0; i < 16; ++i)
            {
                PB[i] = new PictureBox();
                this.Controls.Add(PB[i]);
            }
            //Init the Picture
            InitTable();
        }

        private void InitTable()
        {
            isWin = false;//set state of the game
            //Blank table
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    preTable[i][j] = 0;
                    Table[i][j] = 0;
                }
            nPB = 0;//number of Block
            
            ///Initialize
            foreach (PictureBox pb in PB)
            {
                Init(pb);
            }
            //Random
            for (int i = 0; i < 2; ++i)
            {
                int initblock = InitBlock();
                setimage(initblock, InitNumber());
            }
            
          
            DrawTable();    
        }
        
        private bool checkBlock(int num)
        {
            if (num < 0 || num>15) return false;
            if (Table[num % 4][num / 4]>0)
                return false;
            return true;
        }
        private void setimage(int num, int p,int second=0)
        {
            if (num < 0) return;
            //string s="C:\\Users\\DLK\\Documents\\Visual Studio 2013\\Projects\\WindowsFormsApplication1\\WindowsFormsApplication1\\2048_";
            string s = exePath + "\\Resources\\2048_";
            
            if (p == -1)
            {
                s += "m1.png";
                PB[num].ImageLocation = s;
                return;
            }
            Table[num % 4][num / 4] = p;
            string ss = s+ "m1.png";
            PB[num].ImageLocation = ss;
            System.Threading.Thread.Sleep(second);
            s += p + ".png";
            PB[num].Visible = true;
            PB[num].ImageLocation = s;
        }

        private int preRandom=0;
        private int randomSeed(int i=16)
        {
            Random random = new Random();
            preRandom = (preRandom + random.Next(0, 1000)) % i;
            return preRandom;
        }
        private int cLinear(ref int[] pos)
        {
            int tmp = 0, i = -1;
            while (i<15)
            {
                while (checkBlock(i) == false && i<15) pos[tmp]=++i;
                if (Length(pos[tmp]) > 0) ++tmp;
                while (checkBlock(i) == true) ++i;
            }
            return tmp;
        }
        

        private int InitBlock()
        {
            int[] pos = new int[16];
            int initblock = randomSeed(cLinear(ref pos));
            try
            {
                initblock = pos[initblock] + randomSeed(Length(pos[initblock]));
            }
            catch
            {
                initblock= -1;
            }
            return initblock;
        }

        private int Length(int p)
        {
            int tmp = 0;
            for (int i = p; i < 16; ++i)
                if (checkBlock(i) == true) tmp++;
                else return tmp;
            return tmp;
        }
        private void Init(PictureBox pb)
        {
            string s = exePath + "\\Resources\\2048_0";
            pb.ImageLocation=s;
            pb.Location = new Point(INITHEIGHT + (nPB % 4) * h2048, INITWIDTH + (nPB / 4) * w2048);
            pb.Name = "pictureBox1";
            pb.Size = new Size(h2048-15, h2048-15);
            pb.TabIndex = nPB++; 
            pb.TabStop = false;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.BackColor = SystemColors.ControlDark;
        }

        private int InitNumber()
        {
            int initNum, count2 = 0, count4 = 0 ;
            for (int i = 0; i < 200; ++i)
            {
                initNum = randomSeed(2);
                if (initNum == 0) count2++;
            }
            for (int i = 0; i < 40; ++i)
            {
                initNum = randomSeed(2);
                if (initNum == 1) count4++;
            }
            if (count2 >= count4) return 2;
            else return 4;
        }

        private void Repeat()
        {
            repeat = new Button();
            this.Controls.Add(repeat);
            repeat.Click += new System.EventHandler(this.repeat_Click);
            repeat.Location = new Point(FORMHEIGHT / 2 - 150, (FORMWEIGHT * 2 / 5)+(FORMHEIGHT - (FORMWEIGHT * 2 / 5)) / 2-150);
            repeat.Size = new Size(40,40);
            repeat.AutoSize = true;
            repeat.Image = global::WindowsFormsApplication1.Properties.Resources.repeat;
            repeat.FlatStyle = FlatStyle.Flat;
        }

        private void repeat_Click(object sender, EventArgs e)
        {
            repeat.Visible = false;
            GameState.Visible = false;
            
            countScore(null);
            InitTable();
            DrawTable();
            repeat.Dispose();
            GameState.Dispose();
            
        }
   
        private bool isFull()
        {
            int[] h = new int[4] { 0, 0, 1, -1 };
            int[] w = new int[4] { 1, -1, 0, 0 };
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    for (int z = 0; z < 4; ++z)
                        if (Table[isBounded(i + h[z])][isBounded(j + w[z])] == Table[i][j]) 
                                return false;
                    if (Table[i][j] == 0) return false;
                }
            return true;
        }

        private int isBounded(int p)
        {
            if (p > 3) return 2;
            if (p<0) return 1;
            return p;
        }

        private void LoserScreen()
        {
            
            foreach (PictureBox pb in PB)
            {
                pb.Visible = false;
            }
            GameState = new PictureBox();
            this.Controls.Add(GameState);
            GameState.Location=new Point (0,0);
            GameState.Size = new Size(FORMHEIGHT, FORMWEIGHT*2/5);
            GameState.SizeMode = PictureBoxSizeMode.StretchImage;
            GameState.ImageLocation = exePath+"\\Resources\\Lose.bmp";
            
        }

        private void DrawTable()
        {
 
            for (int j = 0; j < 4; ++j)
                for (int i = 0; i < 4; ++i)
                        setimage(generate(j, i), Table[i][j]);
        }

        private int generate(int i, int j)
        {
            return i * 4 + j ;
        }

        private bool moveBlock(int key)
        {
            bool isError=false;
            //initialize the count of each of block add to
            int[][] count = new int[4][];
            for (int i = 0; i < 4; ++i) count[i] = new int[4];
           
            for (int i = 0; i < 4;++i )
                for (int j = 0; j < 4; ++j)
                {
                    preTable[i][j] = Table[i][j];
                    count[i][j] = 0;
                }
            //execute the event 
            switch ((KEY)key)
            {
                case KEY.DOWNKEY:
                    {
                        for (int i = 0; i < 4; ++i)
                            for (int j = 2; j >= 0; --j)
                                if (Table[i][j] != 0)
                                {
                                    int tmp = j + 1;
                                    while (Table[i][tmp] == 0 || Table[i][tmp] == Table[i][tmp - 1])
                                    {
                                        if (count[i][tmp] == 1 || count[i][tmp-1] == 1) break;
                                        if (Table[i][tmp] != 0)
                                        {
                                            EffectPush(i, tmp, (KEY)key, 25);
                                            count[i][tmp]++;
                                        }
                                        Table[i][tmp] += Table[i][tmp - 1];
                                        isError = true;
                                        Table[i][tmp - 1] = 0;
                                        if (++tmp == 4) break; 
                                        //DrawBlock(i, tmp-1, KEY.DOWNKEY);
                                        //DrawBlock(i, tmp - 2, KEY.DOWNKEY);
                                        EffectPush(i, tmp-1, (KEY)key);
                                        //EffectPush(i, tmp-2, (KEY)key);
                                    }
                                    

                                }
                        break;
                    }
                case KEY.UPKEY:
                    {
                        for (int i = 0; i < 4; ++i)
                            for (int j = 1; j < 4; ++j)
                                if (Table[i][j] != 0)
                                {
                                    int tmp = j - 1;
                                    while (Table[i][tmp] == 0 || Table[i][tmp] == Table[i][tmp + 1] )
                                    {
                                        if (count[i][tmp] == 1 || count[i][tmp + 1] == 1) break;
                                        if (Table[i][tmp] != 0)
                                        {
                                            EffectPush(i, tmp, (KEY)key,25);
                                            count[i][tmp]++;
                                        }
                                        Table[i][tmp] += Table[i][tmp + 1];
                                        Table[i][tmp + 1] = 0;
                                        isError = true;
                                        if (--tmp == -1) break;
                                        //DrawBlock(i, tmp + 1, KEY.UPKEY);
                                        //DrawBlock(i, tmp + 2, KEY.UPKEY);
                                        EffectPush(i, tmp+1, (KEY)key);
                                        //EffectPush(i, tmp+2, (KEY)key);
                                    }
                                }
                        break;
                    }
                case KEY.LEFTKEY:
                    {
                        for (int j = 0; j < 4; ++j)
                            for (int i = 1; i < 4; ++i)
                                if (Table[i][j] != 0)
                                {
                                    int tmp = i - 1;
                                    while (Table[tmp][j] == 0 || Table[tmp][j] == Table[tmp + 1][j])
                                    {
                                        if (count[tmp][j] == 1 || count[tmp + 1][j] == 1) break;
                                        if (Table[tmp][j] != 0)
                                        {
                                            count[tmp][j]++;
                                            EffectPush(tmp, j, (KEY)key,25);
                                        }
                                        Table[tmp][j] += Table[tmp + 1][j];
                                        Table[tmp + 1][j] = 0;
                                        isError = true;
                                        if (--tmp == -1) break;
                                        //DrawBlock(tmp + 1, j, KEY.LEFTKEY);
                                        //DrawBlock(tmp + 2, j, KEY.LEFTKEY);
                                        EffectPush(tmp+1,j, (KEY)key);
                                        //EffectPush(tmp+2,j, (KEY)key);
                                    }
                                    
                                }
                        break;
                    }
                case KEY.RIGHTKEY:
                    {
                        for (int j = 0; j < 4; ++j)
                            for (int i = 2; i >= 0; --i)
                                if (Table[i][j] != 0)
                                {
                                    int tmp = i + 1;
                                    while (Table[tmp][j] == 0 || Table[tmp][j] == Table[tmp - 1][j])
                                    {
                                        if (count[tmp][j] == 1 || count[tmp-1][j] == 1) break;
                                        if (Table[tmp][j] != 0)
                                        {
                                            count[tmp][j]++;
                                            EffectPush(tmp, j, (KEY)key,25);
                                        }
                                        Table[tmp][j] += Table[tmp - 1][j];
                                        Table[tmp - 1][j] = 0;
                                        isError = true;
                                        if (++tmp == 4) break;
                                        //DrawBlock(tmp - 1, j, KEY.RIGHTKEY);
                                       // DrawBlock(tmp - 2, j, KEY.RIGHTKEY);
                                        EffectPush(tmp - 1, j, (KEY)key);
                                        //EffectPush(tmp - 2, j, (KEY)key);
                                    }
                                     
                                }
                        break;
                    }

            }
            if (isError) playASound();
            countScore(count);
            return isError;
        }

        private void EffectPush(int i, int tmp, KEY direct,int second=0)
        {
            int block = generate(tmp, i);
            int X = PB[block].Location.X;
            int Y = PB[block].Location.Y;
            int D = 15;
            switch (direct)
            {
                case KEY.LEFTKEY:
                    {
                        PB[block].Location = new Point(X - D, Y);

                        break;
                    }
                case KEY.RIGHTKEY:
                    {
                        PB[block].Location = new Point(X + D, Y);
                        break;
                    }
                case KEY.UPKEY:
                    {
                        PB[block].Location = new Point(X, Y - D);
                        break;
                    }
                case KEY.DOWNKEY:
                    {
                        PB[block].Location = new Point(X, Y + D);
                        break;
                    }
            }
            System.Threading.Thread.Sleep(second);
            PB[block].Location = new Point(X, Y);
        }

        private void playASound()
        {
            System.Media.SoundPlayer player = new System.Media.
                SoundPlayer(exePath+"\\Resources\\Sounds\\move.wav");
            player.Play();
        }

        private void countScore(int[][] count)
        {
            if (count != null)
            {
                for (int i = 0; i < 4; ++i)
                    for (int j = 0; j < 4; ++j)
                    {
                        if (Table[i][j] == 8192) isWin = true;
                        if (count[i][j] == 1)
                            Score += Table[i][j];
                    }
            }
            else Score = 0;
            label2.Text=""+Score;
            if (isWin == true)
                ArrangeLB();
        }

        private void ArrangeLB()
        {
            
            Program.menu.LC.Show();
            Program.menu.LC.setCaller(false);
            string Name = Program.menu.LC.GetUserName();
            Application.DoEvents();
            Program.HighScore.putScore(Score, Name);
            Program.menu.LC.LoadAgain();
            while (Program.menu.LC.isPlayAgain==false)
                Application.DoEvents();
            Program.menu.LC.isPlayAgain = false;
        }

        private void DrawBlock(int i, int tmp, KEY direct =0)
        {
            int block = generate(tmp, i);
            Application.DoEvents();
            setimage(block, Table[i][tmp]);
        }


        private void game2048_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            INITHEIGHT = (control.Size.Width / 2 - h2048 * 2);
            INITWIDTH = (control.Size.Height / 2 - w2048 * 2);
            FORMWEIGHT = control.Size.Width;
            FORMHEIGHT = control.Size.Height;
            nPB = 0;
            if (PB == null)
            {
                this.ClientSize = new System.Drawing.Size(800, 800);
                return;
            }

            if (isWin == true)
            {
                Repeat();
                WinScreen();
                return;
            }
        
            foreach (PictureBox pb in PB)
            {
                pb.Location = new Point(INITHEIGHT + (nPB % 4) * h2048, INITWIDTH + (nPB / 4) * w2048);
                pb.Name = "pictureBox1";
                pb.Size = new Size(h2048 - 15, h2048 - 15);
                nPB++;
            }
            DrawTable();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private bool isWin = false;

        private void WinScreen()
        {
            foreach (PictureBox pb in PB)
            {
                pb.Visible = false;
            }
            GameState = new PictureBox();
            this.Controls.Add(GameState);
            GameState.Location = new Point(0, 0);
            GameState.Size = new Size(FORMHEIGHT, FORMWEIGHT * 2 / 5);
            GameState.SizeMode = PictureBoxSizeMode.StretchImage;
            GameState.ImageLocation = exePath + "\\Resources\\Win.bmp";
        }
        private bool isUp=true;
        private void game2048_KeyUp(object sender, KeyEventArgs e)
        {
            isUp = true;
        }

        private void game2048_KeyDown(object sender, KeyEventArgs e)
        {
            if (isUp == false) return;
            if (isLegel(e.KeyValue)==false) return;
            bool isMove = moveBlock(e.KeyValue);
            
            if (isWin == true)
            {
                WinScreen();
                Repeat();
            }
            else if (isMove == true)
            {
                //Application.DoEvents();
                setimage(InitBlock(), InitNumber(), 45);
                DrawTable();
            }
            else if (isFull() == true)
            {
                this.Hide();
                ArrangeLB();
                this.Show();
                LoserScreen();
                Repeat();
            }
            isUp = false;
        }

        private bool isLegel(int p)
        {
            switch ((KEY) p)
            {
                case KEY.LEFTKEY:
                    {
                        return true;
                    }
                case KEY.RIGHTKEY:
                    {
                        return true;
                    }
                case KEY.UPKEY:
                    {
                        return true;
                    }
                case KEY.DOWNKEY:
                    {
                        return true;
                    }
            }
            return false;
        }

        private void game2048_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==13)
            {
                if (cheatBox.Visible == true) cheatBox.Visible = false;
                else cheatBox.Visible = true;
            }
            
        }

        private void cheatBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void cheatBox_VisibleChanged(object sender, EventArgs e)
        {
            if (cheatBox.Visible==true)
                    cheatBox.Focus();
        }

        private void cheatBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && cheatBox.Text == "WinThisGame")
            {

                int t = 1;
                for (int i = 12; i >= 0; --i)
                {
                    setimage(i, t * 2, 0);
                    t *= 2;
                }
                t = 4;
                int _score = 0;
                for (int j = 0; j < 13; ++j)
                {
                    int scoree = 4;
                    t = 4;
                    for (int i = 0; i < j - 1; ++i)
                    {
                        t *= 2;
                        scoree = 2 * scoree + t;
                    }
                    _score += scoree;
                    label2.Text = "" + _score;
                    cheatBox.Visible = false;
                    cheatBox.Text = "";
                    this.Focus();
                }
                if (e.KeyValue == 13 && cheatBox.Text == "")
                {

                    cheatBox.Visible = false;
                    this.Focus();
                }
            }
        }


        private void game2048_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.menu.Show();
        }

    }
}
