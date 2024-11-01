using CarRentalBroker.Models;
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
    public partial class ThanhToanHoaDon : Form
    {
        private Xe xe;

        public ThanhToanHoaDon()
        {
            InitializeComponent();
        }

        private void ThanhToanHoaDon_Load(Xe xe)
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

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
