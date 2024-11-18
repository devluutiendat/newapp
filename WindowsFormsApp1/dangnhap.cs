using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using db;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace WindowsFormsApp1
{
    public partial class dangnhap : Form
    {
        public static string chucvu = "";
        public dangnhap()
        {
            InitializeComponent();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string sql = $"update Employee set " +
            $"IdentityCard = '{textBox2.Text}'"
            + $"where EmployeeID = '{textBox1.Text}';";
            Connection.ExecuteQuery(sql);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Chưa nhập thông tin tài khoản hoặc mật khẩu , vui long nhap lai");
            }
            else
            {
                string sql = "Select * from Employee where EmployeeID = '"
                    + textBox1.Text + "' and IdentityCard= '" + textBox2.Text + "'";
                string checkvalue = Connection.ExecuteQueryvalue(sql);
                if (checkvalue != null)
                {
                    string sql2 = "select Position from Employee where EmployeeID = " + textBox1.Text;
                    chucvu = Connection.ExecuteQueryvalue(sql2);
                    menu menu = new menu();
                    menu.Show();
                }
                else
                {
                    MessageBox.Show("Sai thông tin tài khoản hoặc mật khẩu");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn thoát khỏi chương trình này không?",
                "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
                this.Close();
        }

        private void dangnhap_Load(object sender, EventArgs e)
        {
            
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
    }
}