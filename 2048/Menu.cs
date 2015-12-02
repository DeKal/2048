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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        static private game2048 Game;
        private void Start_Click(object sender, EventArgs e)
        {
            if (Game == null)
            {
                Game = new game2048();
                Game.Show();
                this.Hide();
            }
            else
            {
               
                Continue con = new Continue();
                
                con.Show();
                
                con.WaitState();
                
                if (Continue.state == 1)
                {
                    Game.Show();
                    this.Hide();
                }
                con.Hide();                
                
            }
        }
        
        public void newGame(){
            Game.Dispose();
            Game = new game2048();
            Game.Show();
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public Leading_Board LC;
        private void LeadingBoard_Click(object sender, EventArgs e)
        {
            LC.Show();
            this.Hide();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            if (LC == null)
            {
                LC = new Leading_Board();
                LC.Hide();
            }
        }
    }
}
