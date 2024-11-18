using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        
        

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void phòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                new PHONG().Show();
            
        }

        private void thuêPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new THUE_PHONG().Show();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new KHACH_HANG().Show();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NHAN_VIEN().Show();
        }

        private void chứcNăngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
