using QLKS.DB_Ht;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS
{
    public partial class NHAN_VIEN : Form
    {
        public NHAN_VIEN()
        {
            InitializeComponent();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                IMG_anhNV.Image = new Bitmap(openFileDialog.FileName);
            }
            
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(textMaNV.Text)) && (string.IsNullOrEmpty(textTenNV.Text)) && (string.IsNullOrEmpty(textSDTNV.Text)) && (string.IsNullOrEmpty(textQueQuan.Text)) && (string.IsNullOrEmpty(textChucVu.Text)))
            {
                MessageBox.Show("Vui lòng điền đủ thông tin");
            }
            
            
            bool gtnv;
            if (gtNam.Checked)
            {
                gtnv = false;
            }
            else
            {
                gtnv = true;
            }
            Connection.Connect.Open();
            // Chuyển đổi hình ảnh từ PictureBox thành mảng byte

            MemoryStream ms = new MemoryStream();
            IMG_anhNV.Image.Save(ms, ImageFormat.Jpeg);
            byte[] photo = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(photo, 0, photo.Length);
            // Chèn mảng byte vào cơ sở dữ liệu
            Connection.Command.CommandText = "INSERT INTO NHAN_VIEN (MA_NV, TEN_NV, SDT_NV, GT , CHUC_VU , QUE_NV , ANH_NV) VALUES (@MaNV, @TenNV, @SDTNV, @GT  , @ChucVu , @QueNV , @AnhNV)";
            Connection.Command.Parameters.AddWithValue("@MaNV", textMaNV.Text);
            Connection.Command.Parameters.AddWithValue("@TenNV", textTenNV.Text);
            Connection.Command.Parameters.AddWithValue("@SDTNV", textSDTNV.Text);
            Connection.Command.Parameters.AddWithValue("@GT", gtnv);
            Connection.Command.Parameters.AddWithValue("@AnhNV", photo); // Chèn mảng byte
            Connection.Command.Parameters.AddWithValue("@ChucVu", textChucVu.Text);
            Connection.Command.Parameters.AddWithValue("@QueNV", textQueQuan.Text);
            Connection.Command.ExecuteNonQuery();
            Connection.Connect.Close();

            listView1.Columns.Add("Mã NV ", 96);
            listView1.Columns.Add("Tên NV ", 96);
            listView1.Columns.Add("Số Điện thoại ", 96);
            listView1.Columns.Add("Giới Tính ", 96);
            listView1.Columns.Add("Quê Quán ", 96);
            listView1.Columns.Add("Chức vụ ", 96);
            ListViewItem Nv = new ListViewItem(textMaNV.Text);

            Nv.SubItems.Add(textTenNV.Text);
            Nv.SubItems.Add(textSDTNV.Text);

            if (gtnv == true)
            {
                Nv.SubItems.Add("Nữ");
            }
            else
            {
                Nv.SubItems.Add("Nam");
            }
            Nv.SubItems.Add(textQueQuan.Text);
            Nv.SubItems.Add(textChucVu.Text);
            listView1.Items.Add(Nv);
            textMaNV.Clear();
            textTenNV.Clear();
            textSDTNV.Clear();
            textQueQuan.Clear();
            textChucVu.SelectedIndex = -1;
            gtNam.Checked = false;
            gtNu.Checked = false;
            IMG_anhNV.Image = null;
        }

        
    }
}
