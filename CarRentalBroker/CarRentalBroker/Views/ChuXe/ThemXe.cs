using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarRentalBroker.Views.ChuXe
{
    public partial class ThemXe : Form
    {
        // Chuỗi kết nối tới SQL Server
        String connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarRentalBroker;Integrated Security=True;Encrypt=False;";

        public ThemXe()
        {
            InitializeComponent();
        }

        // Hàm xử lý khi nhấn nút "Xác Nhận"
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các ô TextBox
                string ten = txtTen.Text;
                string bienSoXe = txtBienSoXe.Text;
                bool laXeSoSan = chkLaXeSoSan.Checked; // Lấy giá trị từ Checkbox
                bool laXeDien = chkLaXeDien.Checked;   // Lấy giá trị từ Checkbox
                int namSanXuat = int.Parse(txtNamSanXuat.Text); // Chuyển đổi về kiểu số
                int soGhe = int.Parse(txtSoGhe.Text);
                string mau = txtMau.Text;
                int phanKhoi = int.Parse(txtPhanKhoi.Text);
                string thuongHieu = txtThuongHieu.Text;

                // Tạo kết nối với cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Tạo đối tượng SqlCommand để gọi Stored Procedure
                    SqlCommand command = new SqlCommand("ThemXe", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số cho Stored Procedure
                    command.Parameters.AddWithValue("@Ten", ten);
                    command.Parameters.AddWithValue("@BienSoXe", bienSoXe);
                    command.Parameters.AddWithValue("@LaXeSoSan", laXeSoSan);
                    command.Parameters.AddWithValue("@LaXeDien", laXeDien);
                    command.Parameters.AddWithValue("@NamSanXuat", namSanXuat);
                    command.Parameters.AddWithValue("@SoGhe", soGhe);
                    command.Parameters.AddWithValue("@Mau", mau);
                    command.Parameters.AddWithValue("@PhanKhoi", phanKhoi);
                    command.Parameters.AddWithValue("@ThuongHieu", thuongHieu);
                    command.Parameters.AddWithValue("@TrangThai", "Chờ duyệt");
                    command.Parameters.AddWithValue("@MaChuXe", 1); // Giá trị ví dụ cho MaChuXe

                    // Mở kết nối
                    connection.Open();

                    // Thực thi Stored Procedure
                    command.ExecuteNonQuery();

                    // Đóng kết nối
                    connection.Close();

                    MessageBox.Show("Thêm xe thành công!");
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có lỗi xảy ra
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}
