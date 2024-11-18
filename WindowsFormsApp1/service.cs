
using System;
using System.Collections.Generic;
using System.ComponentModel;
using db;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace WindowsFormsApp1
{
    public partial class service : Form
    {

        private static string all = "select * from service";
        public service()
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
            }
        }

        private void phong_Load(object sender, EventArgs e)
        {
            LoadData(all);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO Service(serviceid,name,price) VALUES ('" +
            $"{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}')";
            Connection.ExecuteQuery(sql);
            LoadData(all);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = $"update Service set " +
            $"name = '{textBox2.Text}',price = '{textBox3.Text}' " +
            $"where Serviceid = '{textBox1.Text}';";
            Connection.ExecuteQuery(sql);
            LoadData(all);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string sql = $"DELETE FROM Service WHERE Serviceid = '{textBox1.Text}'";
            Connection.ExecuteQuery(sql);
            LoadData(all);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string all = "select * FROM Service WHERE ServiceID like '%" + textBox4.Text + "%'";
            LoadData(all);
        }
    }
}