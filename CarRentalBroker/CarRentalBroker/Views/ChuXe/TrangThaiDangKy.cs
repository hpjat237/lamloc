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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace CarRentalBroker.Views.ChuXe
{
    public partial class TrangThaiDangKy : Form
    {
        int maND = 2;
        String connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarRentalBroker;Integrated Security=True;Encrypt=False;";

        public TrangThaiDangKy()
        {
            InitializeComponent();
            LoadDataTrangThaiDangKy();
        }
        private void LoadDataTrangThaiDangKy()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Use stored procedure
                    SqlCommand command = new SqlCommand("ProceduresViewTrangThaiDangKy", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MaChuXe", maND);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    connection.Open();
                    adapter.Fill(dataTable);

                    // Set DataSource and configure DataGridView
                    dgvTrangThaiDangKy.DataSource = dataTable;
                    dgvTrangThaiDangKy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Add "Thanh Toán" button column if not already added
                    if (!dgvTrangThaiDangKy.Columns.Contains("ThanhToanButton"))
                    {
                        DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                        btnColumn.Name = "ThanhToanButton";
                        btnColumn.HeaderText = "Thanh Toán";
                        btnColumn.Text = "Thanh Toán";
                        btnColumn.UseColumnTextForButtonValue = true;
                        dgvTrangThaiDangKy.Columns.Add(btnColumn);
                    }

                    // Handle button click event
                    dgvTrangThaiDangKy.CellClick += dgvTrangThaiDangKy_CellClick;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void dgvTrangThaiDangKy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvTrangThaiDangKy.Columns["ThanhToanButton"].Index && e.RowIndex >= 0)
            {
                string trangThai = dgvTrangThaiDangKy.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString();

                if (trangThai == "Chưa Duyệt")
                {
                    int maXe = Convert.ToInt32(dgvTrangThaiDangKy.Rows[e.RowIndex].Cells["MaXe"].Value);
                    string tenXe = dgvTrangThaiDangKy.Rows[e.RowIndex].Cells["TenXe"].Value.ToString();

                    ThanhToanHoaDon thanhToanForm = new ThanhToanHoaDon(maXe);
                    thanhToanForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Thanh toán chỉ khả dụng khi trạng thái là 'Chưa Duyệt'.");
                }
            }
        }


    }
}
