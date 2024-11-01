using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalBroker.Views.ChuXe
{
    public partial class ChuXeTrangChu : Form
    {
        private Form currentFormchild;

        public ChuXeTrangChu()
        {
            InitializeComponent();
            OpenChildForm(new XeChoThue());
        }


        private void OpenChildForm(Form childForm)
        {
            // Kiểm tra xem form con đã mở chưa
            if (currentFormchild != null)
            {
                currentFormchild.Close(); // Đóng form hiện tại (nếu bạn muốn chỉ có một form con hiển thị tại một thời điểm)
            }

            currentFormchild = childForm;
            childForm.TopLevel = false; // Đặt TopLevel là false để nó có thể được nhúng vào panel
            childForm.FormBorderStyle = FormBorderStyle.None; // Xóa viền của form
            childForm.Dock = DockStyle.Fill; // Chiếm toàn bộ panel

            pnlChuXeTrangChu.Controls.Add(childForm); // Thêm form vào panel
            pnlChuXeTrangChu.Tag = childForm; // Lưu form vào tag
            childForm.BringToFront(); // Đưa form lên trên cùng
            childForm.Show(); // Hiển thị form
        }



        private void btnXeChoThue_Click(object sender, EventArgs e)
        {
            OpenChildForm(new XeChoThue());
        }

        private void btnXeCuaToi_Click(object sender, EventArgs e)
        {
            OpenChildForm(new XeCuaToi());
        }

        private void btnTrangThaiDangKy_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TrangThaiDangKy());
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Xử lý sự kiện tìm kiếm nếu cần
        }

        private void btnLichSuChoThue_Click(object sender, EventArgs e)
        {
            OpenChildForm(new LichSuChoThue());

        }

        private void btnLichSuThanhToan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new LichSuThanhToan());

        }
    }
}
