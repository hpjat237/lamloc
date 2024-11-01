using CarRentalBroker.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarRentalBroker.Views.ChuXe
{
    public partial class XeCuaToi : Form
    {
        int maND = 1;
        String connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarRentalBroker;Integrated Security=True;Encrypt=False;";

        public XeCuaToi()
        {
            InitializeComponent();
            LoadDataXeCuaToi();
            AddButtonsToDataGridView();
        }

        private void LoadDataXeCuaToi()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ProceduresViewXe", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MaND", maND); // Thêm tham số vào command

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                dgvXeCuaToi.DataSource = dataTable;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ThemXe themXeForm = new ThemXe();
            themXeForm.Show();
        }

        private Xe LayXeChiTiet(int maXe)
        {
            Xe xe = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("LayXeChiTiet", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MaXe", maXe);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Tạo đối tượng Xe từ kết quả trả về
                    xe = new Xe
                    {
                        MaXe = Convert.ToInt32(reader["MaXe"]),
                        Ten = reader["Ten"].ToString(),
                        BienSoXe = reader["BienSoXe"].ToString(),
                        TrangThai = reader["TrangThai"].ToString(),
                        LaXeSoSan = Convert.ToBoolean(reader["LaXeSoSan"]),
                        LaXeDien = Convert.ToBoolean(reader["LaXeDien"]),
                        NamSanXuat = Convert.ToInt32(reader["NamSanXuat"]),
                        SoGhe = Convert.ToInt32(reader["SoGhe"]),
                        Mau = reader["Mau"].ToString(),
                        PhanKhoi = reader["PhanKhoi"].ToString(),
                        ThuongHieu = reader["ThuongHieu"].ToString()
                    };
                }
                connection.Close();
            }

            return xe;
        }

        private void AddButtonsToDataGridView()
        {
            // Tạo cột button Chi tiết xe
            DataGridViewButtonColumn buttonColumnChiTietXe = new DataGridViewButtonColumn();
            buttonColumnChiTietXe.HeaderText = ""; // Tiêu đề cột
            buttonColumnChiTietXe.Text = "Chi tiết xe"; // Văn bản hiển thị trên nút
            buttonColumnChiTietXe.UseColumnTextForButtonValue = true; // Sử dụng văn bản cột cho tất cả các nút
            buttonColumnChiTietXe.Name = "ChiTietXe"; // Đặt tên cho cột
            dgvXeCuaToi.Columns.Add(buttonColumnChiTietXe);
        }

        private void dgvXeCuaToi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dgvXeCuaToi.Columns["ChiTietXe"].Index)
                {
                    int maXe = Convert.ToInt32(dgvXeCuaToi.Rows[e.RowIndex].Cells["MaXe"].Value);
                    Xe selectedXe = LayXeChiTiet(maXe);

                    if (selectedXe != null)
                    {
                        ChiTietXe chiTietXeForm = new ChiTietXe(selectedXe);
                        chiTietXeForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin xe.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTimKiem.Text.Trim();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Gọi function tìm kiếm dựa trên tên xe
                string query = "SELECT * FROM dbo.TimKiemTheoTenXe(@TenXe)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenXe", searchText);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                dgvXeCuaToi.DataSource = dataTable;
            }
        }
    }
}
