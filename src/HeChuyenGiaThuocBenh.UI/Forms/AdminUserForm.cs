using HeChuyenGiaThuocBenh.BLL;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.UI.Forms;

public partial class AdminUserForm : Form
{
    private List<User> _users = new();
    private User? _selected;
    private bool _isNew;

    public AdminUserForm()
    {
        InitializeComponent();
        cmbRole.Items.AddRange(new object[] { "Admin", "Bác sĩ", "Dược sĩ" });
        cmbRole.SelectedIndex = 1;
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await LoadUsersAsync();
    }

    private async Task LoadUsersAsync()
    {
        try
        {
            var result = await ServiceContainer.UserRepository.GetAllAsync();
            _users = result.ToList();
            BindGrid();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải danh sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BindGrid()
    {
        dgvUsers.DataSource = null;
        dgvUsers.DataSource = _users.Select(u => new
        {
            u.Id,
            u.Username,
            u.HoTen,
            u.Email,
            VaiTro = RoleLabel(u.Role),
            TrangThai = u.IsActive ? "Hoạt động" : "Vô hiệu",
        }).ToList();

        if (dgvUsers.Columns.Count > 0)
        {
            dgvUsers.Columns["Id"].Width = 40;
            dgvUsers.Columns["Username"].Width = 110;
            dgvUsers.Columns["HoTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUsers.Columns["VaiTro"].Width = 90;
            dgvUsers.Columns["TrangThai"].Width = 85;
            dgvUsers.Columns["Id"].HeaderText = "ID";
            dgvUsers.Columns["HoTen"].HeaderText = "Họ tên";
            dgvUsers.Columns["VaiTro"].HeaderText = "Vai trò";
            dgvUsers.Columns["TrangThai"].HeaderText = "Trạng thái";
        }
    }

    private static string RoleLabel(UserRole role) => role switch
    {
        UserRole.Admin => "Admin",
        UserRole.BacSi => "Bác sĩ",
        UserRole.DuocSi => "Dược sĩ",
        _ => "?"
    };

    private static UserRole RoleFromIndex(int idx) => idx switch
    {
        0 => UserRole.Admin,
        2 => UserRole.DuocSi,
        _ => UserRole.BacSi,
    };

    private void dgvUsers_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvUsers.CurrentRow == null) return;
        int idx = dgvUsers.CurrentRow.Index;
        if (idx < 0 || idx >= _users.Count) return;

        _selected = _users[idx];
        _isNew = false;
        LoadForm(_selected);
    }

    private void LoadForm(User u)
    {
        txtUsername.Text = u.Username;
        txtUsername.ReadOnly = true;
        txtHoTen.Text = u.HoTen;
        txtEmail.Text = u.Email ?? "";
        cmbRole.SelectedIndex = u.Role switch
        {
            UserRole.Admin => 0,
            UserRole.DuocSi => 2,
            _ => 1,
        };
        chkIsActive.Checked = u.IsActive;
        txtPassword.Text = "";
        txtPassword.Enabled = false;
        lblPassword.Text = "Mật khẩu mới (bỏ trống để giữ):";
        btnResetPassword.Visible = true;
    }

    private void btnThemMoi_Click(object sender, EventArgs e)
    {
        _selected = null;
        _isNew = true;
        txtUsername.Text = "";
        txtUsername.ReadOnly = false;
        txtHoTen.Text = "";
        txtEmail.Text = "";
        cmbRole.SelectedIndex = 1;
        chkIsActive.Checked = true;
        txtPassword.Text = "";
        txtPassword.Enabled = true;
        lblPassword.Text = "Mật khẩu: *";
        btnResetPassword.Visible = false;
        txtUsername.Focus();
    }

    private async void btnLuu_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtHoTen.Text))
        {
            MessageBox.Show("Vui lòng nhập họ tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_isNew)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải từ 6 ký tự trở lên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newUser = new User
            {
                Username = txtUsername.Text.Trim(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text, 11),
                HoTen = txtHoTen.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                Role = RoleFromIndex(cmbRole.SelectedIndex),
                IsActive = chkIsActive.Checked,
                CreatedAt = DateTime.Now,
            };

            try
            {
                await ServiceContainer.UserRepository.CreateAsync(newUser);
                MessageBox.Show("Tạo người dùng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadUsersAsync();
                _isNew = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tạo người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            if (_selected == null) return;

            _selected.HoTen = txtHoTen.Text.Trim();
            _selected.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
            _selected.Role = RoleFromIndex(cmbRole.SelectedIndex);

            try
            {
                await ServiceContainer.UserRepository.UpdateAsync(_selected);
                await ServiceContainer.UserRepository.SetActiveAsync(_selected.Id, chkIsActive.Checked);

                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    if (txtPassword.Text.Length < 6)
                    {
                        MessageBox.Show("Mật khẩu phải từ 6 ký tự trở lên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var hash = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text, 11);
                    await ServiceContainer.UserRepository.ResetPasswordAsync(_selected.Id, hash);
                }

                MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadUsersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private async void btnToggleActive_Click(object sender, EventArgs e)
    {
        if (_selected == null)
        {
            MessageBox.Show("Chọn người dùng trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (_selected.Id == AppSession.CurrentUser!.Id)
        {
            MessageBox.Show("Không thể vô hiệu hóa tài khoản đang đăng nhập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        bool newState = !_selected.IsActive;
        string action = newState ? "kích hoạt" : "vô hiệu hóa";
        var confirm = MessageBox.Show(
            $"Xác nhận {action} người dùng '{_selected.Username}'?",
            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        try
        {
            await ServiceContainer.UserRepository.SetActiveAsync(_selected.Id, newState);
            await LoadUsersAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnResetPassword_Click(object sender, EventArgs e)
    {
        if (_selected == null) return;

        var confirm = MessageBox.Show(
            $"Reset mật khẩu '{_selected.Username}' về 'Admin@123'?",
            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        try
        {
            var hash = BCrypt.Net.BCrypt.HashPassword("Admin@123", 11);
            await ServiceContainer.UserRepository.ResetPasswordAsync(_selected.Id, hash);
            MessageBox.Show("Reset mật khẩu thành công! Mật khẩu mới: Admin@123", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
