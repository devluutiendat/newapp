using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clients client = new clients();
            client.Show();
        }


        private void menu_Load(object sender, EventArgs e)
        {
            if(dangnhap.chucvu != "letan" && dangnhap.chucvu != "quanly")
            {

                MessageBox.Show("ban chua co quyen");
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            service services = new service();
            services.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            employee employees = new employee();
            employees.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            phongx phong = new phongx();
            phong.Show();
        }
    }
}
