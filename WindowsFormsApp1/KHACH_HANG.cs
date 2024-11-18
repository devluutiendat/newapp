using QLKS.DB_Ht;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS
{
    public partial class KHACH_HANG : Form
    {
        public KHACH_HANG()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textMaKhach.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textTenKhach.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textSoDienThoai.Text = listView1.SelectedItems[0].SubItems[2].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(textMaKhach.Text)) && (string.IsNullOrEmpty(textTenKhach.Text)) && (string.IsNullOrEmpty(textSoDienThoai.Text)))
            {
                MessageBox.Show("Vui lòng điền đủ thông tin");
            }
            bool gtk;
            if (gtNam.Checked)
            {
                gtk = false;
            }
            else
            {
                gtk = true;
            }
            Connection.Connect.Open();
            Connection.Command.CommandText = 
                $"INSERT INTO KHACH_HANG (MA_KH, TEN_KH, SDT, GT) VALUES ('" +
                $"{textMaKhach.Text}', '{textTenKhach.Text}', '{textSoDienThoai.Text}', '{gtk}')"; 
            Connection.Command.ExecuteNonQuery();
            Connection.Connect.Close();

            listView1.Columns.Add("Mã Khách ", 200);
            listView1.Columns.Add("Tên Khách ", 200);
            listView1.Columns.Add("Số Điện thoại ", 200);
            listView1.Columns.Add("Giới Tính ", 114);
            ListViewItem Na = new ListViewItem(textMaKhach.Text);

            Na.SubItems.Add(textTenKhach.Text);
            Na.SubItems.Add(textSoDienThoai.Text);
            if (gtk == true)
            {
                Na.SubItems.Add("Nữ");
            }
            else
            {
                Na.SubItems.Add("Nam");
            }
            listView1.Items.Add(Na);
            textMaKhach.Clear();
            textTenKhach.Clear();
            textSoDienThoai.Clear();
            gtNam.Checked = false;
            gtNu.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool gtk;
            if (gtNam.Checked)
            {
                gtk = false;
            }
            else
            {
                gtk = true;
            }

            if (listView1.Items.Count > 0)
            {

                Connection.Connect.Open();
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    // Lấy thông tin từ item
                    string maKhach = item.SubItems[0].Text;
                    string tenKhach = item.SubItems[1].Text;
                    string soDT = item.SubItems[2].Text;

                    // Sử dụng thông tin trong câu lệnh SQL với tham số
                    Connection.Command.CommandText = "UPDATE KHACH_HANG SET MA_KH = @MaKH, SDT = @SDT, TEN_KH = @TenKH, GT = @GT WHERE MA_KH = @MaKH";

                    // Thêm tham số
                    Connection.Command.Parameters.Clear(); // Xóa tham số cũ trước khi thêm tham số mới
                    listView1.SelectedItems[0].SubItems[0].Text = textMaKhach.Text;
                    listView1.SelectedItems[0].SubItems[1].Text = textTenKhach.Text;
                    listView1.SelectedItems[0].SubItems[2].Text = textSoDienThoai.Text;
                    Connection.Command.Parameters.AddWithValue("@MaKH", maKhach);
                    Connection.Command.Parameters.AddWithValue("@TenKH", tenKhach);
                    Connection.Command.Parameters.AddWithValue("@SDT", soDT);
                    Connection.Command.Parameters.AddWithValue("@GT", gtk);

                    Connection.Command.ExecuteNonQuery();
                }
                Connection.Connect.Close();

                // Cập nhật thông tin của các item đã chọn
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    item.SubItems[3].Text = gtk ? "Nữ" : "Nam";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Selected)
                    listView1.Items[i].Remove();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
    }
}
