namespace HeChuyenGiaThuocBenh.UI.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.pnlMain = new Panel();
        this.lblTitle = new Label();
        this.lblSubtitle = new Label();
        this.lblUsername = new Label();
        this.txtUsername = new TextBox();
        this.lblPassword = new Label();
        this.txtPassword = new TextBox();
        this.chkShowPassword = new CheckBox();
        this.btnLogin = new Button();
        this.lblError = new Label();
        this.pnlMain.SuspendLayout();
        this.SuspendLayout();

        // pnlMain
        this.pnlMain.BackColor = Color.White;
        this.pnlMain.BorderStyle = BorderStyle.FixedSingle;
        this.pnlMain.Controls.Add(this.lblTitle);
        this.pnlMain.Controls.Add(this.lblSubtitle);
        this.pnlMain.Controls.Add(this.lblUsername);
        this.pnlMain.Controls.Add(this.txtUsername);
        this.pnlMain.Controls.Add(this.lblPassword);
        this.pnlMain.Controls.Add(this.txtPassword);
        this.pnlMain.Controls.Add(this.chkShowPassword);
        this.pnlMain.Controls.Add(this.btnLogin);
        this.pnlMain.Controls.Add(this.lblError);
        this.pnlMain.Location = new Point(80, 60);
        this.pnlMain.Size = new Size(400, 440);

        // lblTitle
        this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        this.lblTitle.ForeColor = Color.FromArgb(0, 120, 215);
        this.lblTitle.Location = new Point(20, 30);
        this.lblTitle.Size = new Size(360, 35);
        this.lblTitle.Text = "HỆ CHUYÊN GIA THUỐC BỆNH";
        this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

        // lblSubtitle
        this.lblSubtitle.Font = new Font("Segoe UI", 9F);
        this.lblSubtitle.ForeColor = Color.Gray;
        this.lblSubtitle.Location = new Point(20, 65);
        this.lblSubtitle.Size = new Size(360, 20);
        this.lblSubtitle.Text = "Đăng nhập để tiếp tục";
        this.lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;

        // lblUsername
        this.lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.lblUsername.Location = new Point(30, 110);
        this.lblUsername.Size = new Size(340, 20);
        this.lblUsername.Text = "Tên đăng nhập";

        // txtUsername
        this.txtUsername.Font = new Font("Segoe UI", 10F);
        this.txtUsername.Location = new Point(30, 133);
        this.txtUsername.Size = new Size(340, 28);
        this.txtUsername.TabIndex = 0;

        // lblPassword
        this.lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.lblPassword.Location = new Point(30, 175);
        this.lblPassword.Size = new Size(340, 20);
        this.lblPassword.Text = "Mật khẩu";

        // txtPassword
        this.txtPassword.Font = new Font("Segoe UI", 10F);
        this.txtPassword.Location = new Point(30, 198);
        this.txtPassword.Size = new Size(340, 28);
        this.txtPassword.UseSystemPasswordChar = true;
        this.txtPassword.TabIndex = 1;
        this.txtPassword.KeyDown += new KeyEventHandler(this.txtPassword_KeyDown);

        // chkShowPassword
        this.chkShowPassword.Font = new Font("Segoe UI", 9F);
        this.chkShowPassword.ForeColor = Color.Gray;
        this.chkShowPassword.Location = new Point(30, 232);
        this.chkShowPassword.Size = new Size(150, 20);
        this.chkShowPassword.Text = "Hiện mật khẩu";
        this.chkShowPassword.TabIndex = 2;
        this.chkShowPassword.CheckedChanged += new EventHandler(this.chkShowPassword_CheckedChanged);

        // btnLogin
        this.btnLogin.BackColor = Color.FromArgb(0, 120, 215);
        this.btnLogin.FlatStyle = FlatStyle.Flat;
        this.btnLogin.FlatAppearance.BorderSize = 0;
        this.btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        this.btnLogin.ForeColor = Color.White;
        this.btnLogin.Location = new Point(30, 270);
        this.btnLogin.Size = new Size(340, 42);
        this.btnLogin.Text = "ĐĂNG NHẬP";
        this.btnLogin.TabIndex = 3;
        this.btnLogin.Cursor = Cursors.Hand;
        this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

        // lblError
        this.lblError.Font = new Font("Segoe UI", 9F);
        this.lblError.ForeColor = Color.Red;
        this.lblError.Location = new Point(30, 322);
        this.lblError.Size = new Size(340, 40);
        this.lblError.Text = string.Empty;
        this.lblError.TextAlign = ContentAlignment.MiddleCenter;

        // LoginForm
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.FromArgb(240, 244, 248);
        this.ClientSize = new Size(560, 560);
        this.Controls.Add(this.pnlMain);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "LoginForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Đăng nhập - Hệ Chuyên Gia Thuốc Bệnh";

        this.pnlMain.ResumeLayout(false);
        this.pnlMain.PerformLayout();
        this.ResumeLayout(false);
    }

    private Panel pnlMain;
    private Label lblTitle;
    private Label lblSubtitle;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblPassword;
    private TextBox txtPassword;
    private CheckBox chkShowPassword;
    private Button btnLogin;
    private Label lblError;
}
