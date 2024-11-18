using db;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class employee : Form
    {
        private static string all = "select * from Employee";
        public employee()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
        }
        private void LoadData(string query)
        {

            if (dangnhap.chucvu == "letan")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                button3.Enabled = false;
            }
            try
            {
                Connection.ExecuteQuery(query);

                DataTable dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, Connection.GetConnection()))
                {
                    adapter.Fill(dataTable);
                }
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow clickedRow = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = clickedRow.Cells[0].Value?.ToString();
                textBox2.Text = clickedRow.Cells[1].Value?.ToString();
                textBox3.Text = clickedRow.Cells[2].Value?.ToString();
                textBox4.Text = clickedRow.Cells[3].Value?.ToString();
                textBox5.Text = clickedRow.Cells[4].Value?.ToString();
            }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void employee_Load_1(object sender, EventArgs e)
        {
            
            LoadData(all);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO Employee(EmployeeID,Name,IdentityCard,Phone,Position) VALUES ('" +
            $"{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}','{textBox4.Text}','{textBox5.Text}')";
            Connection.ExecuteQuery(sql);
            Console.WriteLine(sql);
            
            all = "select * from Employee";
            LoadData(all);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = $"update Employee set " +
            $"EmployeeID = '{textBox1.Text}',Name = '{textBox2.Text}',IdentityCard = '{textBox3.Text}',Phone = '{textBox4.Text}',Position = '{textBox5.Text}'"
            + $"where EmployeeID = '{textBox1.Text}';";
            Connection.ExecuteQuery(sql);
            LoadData(all);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string sql = $"DELETE FROM Employee WHERE EmployeeID = '{textBox1.Text}'";
            Connection.ExecuteQuery(sql);
            LoadData(all);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

            string all = "select * from Employee where EmployeeID like '%" + textBox6.Text + "%'";
            LoadData(all);
        }
    }
}