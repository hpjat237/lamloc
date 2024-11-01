using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarRentalBroker.Views.ChuXe
{
    public partial class HopDong : Form
    {
        private String connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarRentalBroker;Integrated Security=True;Encrypt=False;";
        private int maHopDong;

        public HopDong(int maHopDong)
        {
            InitializeComponent();
            this.maHopDong = maHopDong;
            LoadContractDetails();
        }

        private void LoadContractDetails()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM ViewInHopDong WHERE MaHopDong = @MaHopDong", connection))
                    {
                        command.Parameters.AddWithValue("@MaHopDong", maHopDong);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Đổ dữ liệu vào các label
                            txtTenKhachThue.Text = reader["TenKhachThue"].ToString();
                            txtSDTKhachThue.Text = reader["SoDienThoaiKhach"].ToString();
                            txtTenChuXe.Text = reader["TenChuXe"].ToString();
                            txtSDTChuXe.Text = reader["SoDienThoaiChuXe"].ToString();
                            txtTenXe.Text = reader["TenXe"].ToString();
                            txtNgayBatDau.Text = Convert.ToDateTime(reader["NgayBatDau"]).ToString("dd/MM/yyyy");
                            txtNgayKetThuc.Text = Convert.ToDateTime(reader["NgayKetThuc"]).ToString("dd/MM/yyyy");
                            txtTongTien.Text = reader["TongTien"].ToString();
                            // Tiếp tục với các trường khác cần thiết...
                        }
                        else
                        {
                            MessageBox.Show("No contract details found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading contract details: " + ex.Message);
                }
            }
        }
    }
}
