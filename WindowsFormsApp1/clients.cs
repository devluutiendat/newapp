using db;
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

namespace WindowsFormsApp1
{
    public partial class clients : Form
    {
        private int[] myArray = new int[0];

        private string c = "";

        private string all = "SELECT * FROM Client";
        public clients()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
            
        }
        private void LoadData(string query)
        {

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
        private void clients_Load(object sender, EventArgs e)
        {
            LoadData(all);

            try
            {
                string query = "SELECT * FROM Service";
                Connection.ExecuteQuery(query);

                DataTable dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, Connection.GetConnection()))
                {
                    adapter.Fill(dataTable);
                }
                dataGridView2.DataSource = dataTable;
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
                string formattedDateTime1 = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string formattedDateTime2 = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
                DataGridViewRow clickedRow = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = clickedRow.Cells[0].Value?.ToString();
                textBox2.Text = clickedRow.Cells[1].Value?.ToString();
                textBox3.Text = clickedRow.Cells[2].Value?.ToString();
                //textBox4.Text = clickedRow.Cells[3].Value?.ToString();
                formattedDateTime1 = clickedRow.Cells[4].Value?.ToString();
                formattedDateTime2 = clickedRow.Cells[5].Value?.ToString();
            }
        }   
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0)
            {
                DataGridViewRow clickedRow = dataGridView2.Rows[e.RowIndex];
                int value = e.RowIndex;
                List<int> myList = new List<int>(myArray);

                int index = myList.IndexOf(value);

                if (index != -1) // Value found, remove it
                {
                    myList.RemoveAt(index);
                }
                else // Value not found, add it
                {
                    myList.Add(value);
                }

                // Convert List back to array
                myArray = myList.ToArray();

                Console.WriteLine("Array after potential modification: " + string.Join(", ", myArray));
            }
            listView1.Items.Clear();
            //tinh tong tien dich vu
            int b = 0;
            int i = 0;
            c = "";
            foreach (int aValue in myArray)
            {
                // Ensure the index is within the valid range
                if (aValue >= 0 && aValue < dataGridView2.Rows.Count)
                {
                    DataGridViewRow clickedRow = dataGridView2.Rows[aValue];
                    int cellvalue1 =Convert.ToInt32( clickedRow.Cells[2].Value); 
                    // Retrieve the value of the cell in the second column
                    string cellValue = clickedRow.Cells[1].Value?.ToString();
                    string cellvalue2 = cellvalue1.ToString();
                    //add 
                    listView1.Items.Add(new ListViewItem(new[] { cellValue, cellvalue2.ToString() }));
                    i++;
                    c += cellValue;
                    b += cellvalue1;

                }
                else
                {
                    // Handle the case where the index is out of range
                    // For example, you could display a message or handle it differently.
                    Console.WriteLine("Index out of range: " + aValue);
                }
            }
;
            label2.Text = textBox4.Text;
            string query = $"select Price from Room where RoomNumber =  '{ textBox4.Text}'";
            int price =Convert.ToInt32( Connection.ExecuteQueryvalue(query));
            label3.Text = price.ToString();
            label4.Text = textBox5.Text;
            int j = Convert.ToInt32(textBox5.Text) * price;
            label5.Text = j.ToString();
            label6.Text = (j + b).ToString();


            DateTime h = dateTimePicker1.Value.AddHours(Convert.ToInt32(textBox5.Text) * 12);
            Console.WriteLine(h);
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            DateTime h = dateTimePicker1.Value.AddHours(Convert.ToInt32(textBox5.Text) * 12);
            string sql = $"update Client set " +
            $"Customid = '{textBox1.Text}',Name = '{textBox2.Text}',Phone = '{textBox3.Text}'," +
            $"CheckIn = '{dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss")}'" +
            $",CheckOut = '{h.ToString("yyyy-MM-dd HH:mm:ss")}'+ SelectedService = '{c}'" +
            $"where Customid = '{textBox1.Text}';";
            Connection.ExecuteQuery(sql);
            LoadData(all);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            DateTime h = dateTimePicker1.Value.AddHours(Convert.ToInt32(textBox5.Text) * 12);
            string sql = $"INSERT INTO client(ClientID,Name,Phone,CheckIn,CheckOut,SelectedServices,Total) VALUES ('" +
            $"{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}','{dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss")}'," +
            $"'{h.ToString("yyyy-MM-dd HH:mm:ss")}' ,'{c}','{label6.Text}')";
            string query = $"update Room set ClientID = '{textBox1.Text}' where RoomNumber = '{textBox4.Text}'";
            Connection.ExecuteQuery(sql);
            Connection.ExecuteQuery(query);
            LoadData(all);
        }
        private void button3_Click(object sender, EventArgs e)
        {

            string sql = $"DELETE FROM Client WHERE Customid = '{textBox1.Text}'";
            Connection.ExecuteQuery(sql);
            LoadData(all);
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(c);
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string sql = "select * from client where Name = " + textBox6.Text;
            Connection.ExecuteQuery(sql);
            LoadData(all);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            all = "select * from client where" + textBox6.Text;
            LoadData(all);
        }
    }
}