using db;
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
    public partial class thongke : Form
    {

        private int[] myArray = new int[0];
        public thongke()
        {
            InitializeComponent();
        }

        private void thongke_Load(object sender, EventArgs e)
        {
            List<int> myList = new List<int>(myArray);
            for (int i = 0; i < 12; i++)
            {
                string sql = $"select sum (ClientID) from client where CheckIn > '2024-${i}-2'";
                int a = Convert.ToInt32(Connection.ExecuteQueryvalue(sql));
                myList.Add(a);  
            }
            for (int i = 0; i < myList.LongCount() ; i++)
            {

            }
        }
    }
}
