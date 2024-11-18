using System;
using QLKS;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS
{
    public partial class DANG_NHAP : Form
    {
        public DANG_NHAP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtUser.Text==" " && txtPW.Text ==" ")
            {
                MessageBox.Show("Nhap Lai");

            }else if(txtUser.Text =="nam" && txtPW.Text == "1")
            {
                new main().Show();
                this.Hide();
            }
            
            
        }

    }
}

    
