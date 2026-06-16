using HeChuyenGiaThuocBenh.BLL;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.UI.Forms;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        ApplyUserContext();
    }

    private void ApplyUserContext()
    {
        var user = AppSession.CurrentUser!;
        lblWelcome.Text = $"Xin chào, {user.HoTen} ({RoleLabel(user.Role)})";

        // Ẩn menu admin nếu không phải admin
        if (!AppSession.IsAdmin)
        {
            btnAdminUsers.Visible = false;
            btnAdminDrugs.Visible = false;
            btnAdminDiseases.Visible = false;
        }
    }

    private static string RoleLabel(UserRole role) => role switch
    {
        UserRole.Admin => "Quản trị viên",
        UserRole.BacSi => "Bác sĩ",
        UserRole.DuocSi => "Dược sĩ",
        _ => "Người dùng"
    };

    private void btnLogout_Click(object sender, EventArgs e)
    {
        AppSession.Logout();
        new LoginForm().Show();
        Close();
    }

    // Placeholder handlers — các form sẽ được thêm ở Sprint 2-4
    private void btnChanDoan_Click(object sender, EventArgs e)
        => MessageBox.Show("Chức năng chẩn đoán — Sprint 2", "Thông báo");

    private void btnBenhNhan_Click(object sender, EventArgs e)
        => MessageBox.Show("Chức năng hồ sơ bệnh nhân — Sprint 3", "Thông báo");

    private void btnThuoc_Click(object sender, EventArgs e)
        => MessageBox.Show("Chức năng tra cứu thuốc — Sprint 2", "Thông báo");

    private void btnBaoCao_Click(object sender, EventArgs e)
        => MessageBox.Show("Chức năng báo cáo — Sprint 4", "Thông báo");

    private void btnAdminUsers_Click(object sender, EventArgs e)
        => MessageBox.Show("Quản lý người dùng — Sprint 4", "Thông báo");

    private void btnAdminDrugs_Click(object sender, EventArgs e)
        => MessageBox.Show("Quản lý thuốc — Sprint 3", "Thông báo");

    private void btnAdminDiseases_Click(object sender, EventArgs e)
        => MessageBox.Show("Quản lý bệnh — Sprint 3", "Thông báo");
}
