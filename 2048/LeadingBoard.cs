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
    public partial class Leading_Board : Form
    {
        private TextBox[] bName ;
        private TextBox[] bNo ;
        private TextBox[] bScore ;
        public bool isPlayAgain;
        private bool isMenu=true;
        public Leading_Board()
        {
            InitializeComponent();
        }
        public void setCaller(bool Condition)
        {
            isMenu = Condition;
        }
        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (isMenu == true)
            {
                Program.menu.Show();
            }
            else
            {
                isPlayAgain = true;
                isMenu = true;
            }
        }
        
        private void Leading_Board_Load(object sender, EventArgs e)
        {
            isMenu = true;
            isPlayAgain = false;
            highScore HS = Program.HighScore;
            string Path = Program.exePath + "\\Data.txt";
            if (System.IO.File.Exists(Program.exePath + "\\Data.txt") == true)
                highScore.ReadFile(Path);
            else highScore.WriteFile(Path, 1);
            bName = new TextBox[10];
            bNo = new TextBox[10];
            bScore = new TextBox[10];
            
            for (int i = 0; i < 10; ++i)
            {
                bName[i] = new TextBox();
                bNo[i] = new TextBox();
                bScore[i] = new TextBox();
                int score = HS.getRank(i).getScore();
                string name = HS.getRank(i).getName();
                setProperty(bName[i],name);
                setProperty(bNo[i],"No " + (i+1).ToString());
                setProperty(bScore[i],score.ToString());
                bName[i].Location = new Point(200, i * 50);
                bNo[i].Location = new Point(0, i * 50);
                bScore[i].Location = new Point(200*2, i * 50);
            }
            
        }
        public void LoadAgain()
        {
            for (int i = 0; i < 10; ++i)
            {
                highScore HS = Program.HighScore;
                int score = HS.getRank(i).getScore();
                string name = HS.getRank(i).getName();
                setProperty(bName[i], name);
                setProperty(bNo[i], "No " + (i + 1).ToString());
                setProperty(bScore[i], score.ToString());
                bName[i].Location = new Point(200, i * 50);
                bNo[i].Location = new Point(0, i * 50);
                bScore[i].Location = new Point(200 * 2, i * 50);
            }

            string Path = Program.exePath + "\\Data.txt";
            if (System.IO.File.Exists(Path) == true)
                highScore.WriteFile(Path,1);
            else
                highScore.WriteFile(Path);
        }
        private void setProperty(TextBox textBox,string value)
        {
            this.Controls.Add(textBox);
            textBox.BackColor = System.Drawing.Color.Black;
            textBox.Font = new System.Drawing.Font("Segoe Print", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            textBox.Size = new Size(183, 22);
            textBox.Text = value;
            textBox.TextAlign = HorizontalAlignment.Center;
        }

        internal string GetUserName()
        {
            Form4 get = new Form4();
            get.Show();
            string _name;
            while (get.getName() == "")
            {
                Application.DoEvents();
            }
            _name = get.getName();
            if (_name != "")
            {
                get.Dispose();
            }
            return _name ;
        }
    }
}
