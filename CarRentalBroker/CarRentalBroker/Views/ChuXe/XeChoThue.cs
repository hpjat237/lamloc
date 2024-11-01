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
    public partial class XeChoThue : Form
    {
        String connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarRentalBroker;Integrated Security=True;Encrypt=False;";
        public XeChoThue()
        {
            InitializeComponent();
            LoadDataXeChoThue();
        }
        private void LoadDataXeChoThue()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand("ViewXeDangChoThue", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                dgvXeChoThue.DataSource = dataTable;
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

                dgvXeChoThue.DataSource = dataTable;
            }
        }

    }
}
