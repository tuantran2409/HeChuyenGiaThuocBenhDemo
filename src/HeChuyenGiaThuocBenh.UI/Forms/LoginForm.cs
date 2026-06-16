using HeChuyenGiaThuocBenh.BLL;
using HeChuyenGiaThuocBenh.BLL.Services;

namespace HeChuyenGiaThuocBenh.UI.Forms;

public partial class LoginForm : Form
{
    private readonly IAuthService _authService;

    public LoginForm()
    {
        InitializeComponent();
        _authService = ServiceContainer.AuthService;
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            lblError.Text = "Vui lòng nhập đầy đủ thông tin.";
            return;
        }

        btnLogin.Enabled = false;
        lblError.Text = string.Empty;

        try
        {
            var user = await _authService.LoginAsync(username, password);

            if (user == null)
            {
                lblError.Text = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return;
            }

            AppSession.Login(user);

            var mainForm = new MainForm();
            mainForm.FormClosed += (_, _) => Application.Exit();
            mainForm.Show();
            Hide();
        }
        catch (Exception ex)
        {
            lblError.Text = $"Lỗi kết nối: {ex.Message}";
        }
        finally
        {
            btnLogin.Enabled = true;
        }
    }

    private void txtPassword_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            btnLogin_Click(sender, e);
    }

    private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
    {
        txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
    }
}
