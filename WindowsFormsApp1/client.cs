
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



namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
        }
        private void LoadData()
        {
            try
            {
                string query = "SELECT * FROM Service"; 
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

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO Service(serviceid,name,price) VALUES ('" +
                $"{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}')";
            Connection.ExecuteQuery(sql);
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = $"update Service set " +
                $"serviceid = '{textBox1.Text}',name = '{textBox2.Text}',price = '{textBox3.Text}' " +
                $"where Serviceid = '{textBox1.Text}';";
            Connection.ExecuteQuery(sql);
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string sql = $"DELETE FROM Service WHERE Serviceid = '{textBox1.Text}'";
            Connection.ExecuteQuery(sql);
            LoadData();
        }
    }
}