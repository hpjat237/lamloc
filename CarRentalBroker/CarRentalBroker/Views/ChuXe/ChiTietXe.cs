using CarRentalBroker.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarRentalBroker.Views.ChuXe
{
    public partial class ChiTietXe : Form
    {
        private Xe xe;
        private string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarRentalBroker;Integrated Security=True;Encrypt=False;";

        public ChiTietXe(Xe xe)
        {
            InitializeComponent();
            this.xe = xe;
            ChiTietXe_Load(xe);
        }

        private void ChiTietXe_Load(Xe xe)
        {
            txtTen.Text = xe.Ten;
            txtBienSoXe.Text = xe.BienSoXe;
            chkLaXeSoSan.Checked = xe.LaXeSoSan;
            chkLaXeDien.Checked = xe.LaXeDien;
            txtNamSanXuat.Text = xe.NamSanXuat.ToString();
            txtSoGhe.Text = xe.SoGhe.ToString();
            txtMau.Text = xe.Mau;
            txtPhanKhoi.Text = xe.PhanKhoi;
            txtThuongHieu.Text = xe.ThuongHieu;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa xe này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                XoaXe(xe);
                MessageBox.Show("Xe đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form sau khi xóa
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các trường nhập
            xe.Ten = txtTen.Text;
            xe.BienSoXe = txtBienSoXe.Text;
            xe.LaXeSoSan = chkLaXeSoSan.Checked;
            xe.LaXeDien = chkLaXeDien.Checked;
            xe.NamSanXuat = Convert.ToInt32(txtNamSanXuat.Text);
            xe.SoGhe = Convert.ToInt32(txtSoGhe.Text);
            xe.Mau = txtMau.Text;
            xe.PhanKhoi = txtPhanKhoi.Text;
            xe.ThuongHieu = txtThuongHieu.Text;

            // Gọi phương thức cập nhật trong cơ sở dữ liệu
            UpdateXe(xe);
            MessageBox.Show("Thông tin xe đã được cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close(); // Đóng form sau khi sửa
        }

        private void XoaXe(Xe xe)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DeleteXe", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MaXe", xe.MaXe); // Giả sử bạn có thuộc tính MaXe trong đối tượng Xe

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void UpdateXe(Xe xe)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UpdateXe", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số cho stored procedure
                command.Parameters.AddWithValue("@MaXe", xe.MaXe);
                command.Parameters.AddWithValue("@Ten", xe.Ten);
                command.Parameters.AddWithValue("@BienSoXe", xe.BienSoXe);
                command.Parameters.AddWithValue("@TrangThai", xe.TrangThai); // Bạn có thể cần điều chỉnh để lấy trạng thái

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
