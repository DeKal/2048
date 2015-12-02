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
    public partial class Continue : Form
    {
        public Continue()
        {
            InitializeComponent();
        }

        public static int state=-1;
        public void WaitState()
        {

            while (Continue.state == -1) Application.DoEvents();
        }
        private void Yes_Click(object sender, EventArgs e)
        {
           // Application.DoEvents();
            state = 1;// go on with old game
        }

        private void No_Click(object sender, EventArgs e)
        {
            state = 0;
            Program.menu.newGame();
        }

        private void Continue_VisibleChanged(object sender, EventArgs e)
        {
            Continue.state = -1;
        }
    }
}
