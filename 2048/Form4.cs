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
    public partial class Form4 : Form
    {
        private string _name="";
        public Form4()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            _name = textBox1.Text;
            this.Hide();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Text = "Enter Your Name :D";
        }

        internal string getName()
        {
            return _name;
        }
    }
}
