using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarRentalBroker.Views.ChuXe
{
    public partial class LichSuChoThue : Form
    {
        int maND = 4;
        String connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarRentalBroker;Integrated Security=True;Encrypt=False;";

        public LichSuChoThue()
        {
            InitializeComponent();
            LoadDataLichSuChoThue();
            AddButtonsToDataGridView();
        }

        private void LoadDataLichSuChoThue()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("ProceduresViewHopDongBangMaND", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MaND", maND);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dgvLichSuChoThue.DataSource = dataTable;
                        dgvLichSuChoThue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("No contracts found for this user.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading contracts: " + ex.Message);
                }
            }

        }
        private void AddButtonsToDataGridView()
        {
            // Tạo cột button In hợp đồng
            DataGridViewButtonColumn buttonColumnInHopDong = new DataGridViewButtonColumn();
            buttonColumnInHopDong.HeaderText = ""; // Tiêu đề cột
            buttonColumnInHopDong.Text = "In hợp đồng"; // Văn bản hiển thị trên nút
            buttonColumnInHopDong.UseColumnTextForButtonValue = true; // Sử dụng văn bản cột cho tất cả các nút
            buttonColumnInHopDong.Name = "InHopDong"; // Đặt tên cho cột
            dgvLichSuChoThue.Columns.Add(buttonColumnInHopDong);
        }
        private void dgvLichSuChoThue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dgvLichSuChoThue.Columns["InHopDong"].Index)
                {
                    int maHopDong = Convert.ToInt32(dgvLichSuChoThue.Rows[e.RowIndex].Cells["MaHopDong"].Value);
                    HopDong hopDongForm = new HopDong(maHopDong); // Giả sử bạn đã tạo form HopDong
                    hopDongForm.ShowDialog(); // Mở form In hợp đồng
                }
            }
        }


    }
}
