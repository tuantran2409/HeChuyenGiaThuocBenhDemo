namespace HeChuyenGiaThuocBenh.UI.Forms;

partial class AdminUserForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.splitMain = new SplitContainer();
        this.pnlListArea = new Panel();
        this.pnlListButtons = new Panel();
        this.btnThemMoi = new Button();
        this.btnToggleActive = new Button();
        this.dgvUsers = new DataGridView();
        this.pnlFormArea = new Panel();
        this.lblFormTitle = new Label();
        this.lblUsername = new Label();
        this.txtUsername = new TextBox();
        this.lblHoTen = new Label();
        this.txtHoTen = new TextBox();
        this.lblEmail = new Label();
        this.txtEmail = new TextBox();
        this.lblRole = new Label();
        this.cmbRole = new ComboBox();
        this.chkIsActive = new CheckBox();
        this.lblPassword = new Label();
        this.txtPassword = new TextBox();
        this.btnLuu = new Button();
        this.btnResetPassword = new Button();

        ((System.ComponentModel.ISupportInitialize)this.splitMain).BeginInit();
        this.splitMain.Panel1.SuspendLayout();
        this.splitMain.Panel2.SuspendLayout();
        this.splitMain.SuspendLayout();
        this.pnlListArea.SuspendLayout();
        this.pnlListButtons.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.dgvUsers).BeginInit();
        this.pnlFormArea.SuspendLayout();
        this.SuspendLayout();

        // splitMain
        this.splitMain.Dock = DockStyle.Fill;
        this.splitMain.SplitterDistance = 440;
        this.splitMain.Panel1.Controls.Add(this.pnlListArea);
        this.splitMain.Panel2.Controls.Add(this.pnlFormArea);

        // pnlListArea
        this.pnlListArea.Dock = DockStyle.Fill;
        this.pnlListArea.Controls.Add(this.dgvUsers);
        this.pnlListArea.Controls.Add(this.pnlListButtons);

        // pnlListButtons
        this.pnlListButtons.BackColor = Color.FromArgb(250, 250, 252);
        this.pnlListButtons.Controls.Add(this.btnThemMoi);
        this.pnlListButtons.Controls.Add(this.btnToggleActive);
        this.pnlListButtons.Dock = DockStyle.Top;
        this.pnlListButtons.Height = 54;
        this.pnlListButtons.Padding = new Padding(10, 10, 10, 0);

        void ActionBtn(Button b, string text, Color bg, int x)
        {
            b.BackColor = bg;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            b.ForeColor = Color.White;
            b.Location = new Point(x, 10);
            b.Size = new Size(130, 34);
            b.Text = text;
            b.Cursor = Cursors.Hand;
        }

        ActionBtn(this.btnThemMoi, "+ Thêm mới", Color.FromArgb(16, 137, 62), 10);
        ActionBtn(this.btnToggleActive, "🔒 Bật/Tắt", Color.FromArgb(200, 100, 0), 155);

        this.btnThemMoi.Click += new EventHandler(this.btnThemMoi_Click);
        this.btnToggleActive.Click += new EventHandler(this.btnToggleActive_Click);

        // dgvUsers
        this.dgvUsers.AllowUserToAddRows = false;
        this.dgvUsers.AllowUserToDeleteRows = false;
        this.dgvUsers.BackgroundColor = Color.White;
        this.dgvUsers.BorderStyle = BorderStyle.None;
        this.dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvUsers.Dock = DockStyle.Fill;
        this.dgvUsers.Font = new Font("Segoe UI", 9F);
        this.dgvUsers.GridColor = Color.FromArgb(220, 220, 220);
        this.dgvUsers.ReadOnly = true;
        this.dgvUsers.RowHeadersVisible = false;
        this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 44, 52);
        this.dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        this.dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.dgvUsers.EnableHeadersVisualStyles = false;
        this.dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 252);
        this.dgvUsers.SelectionChanged += new EventHandler(this.dgvUsers_SelectionChanged);

        // pnlFormArea
        this.pnlFormArea.BackColor = Color.White;
        this.pnlFormArea.Dock = DockStyle.Fill;
        this.pnlFormArea.Padding = new Padding(20, 15, 20, 15);
        this.pnlFormArea.Controls.Add(this.lblFormTitle);
        this.pnlFormArea.Controls.Add(this.lblUsername);
        this.pnlFormArea.Controls.Add(this.txtUsername);
        this.pnlFormArea.Controls.Add(this.lblHoTen);
        this.pnlFormArea.Controls.Add(this.txtHoTen);
        this.pnlFormArea.Controls.Add(this.lblEmail);
        this.pnlFormArea.Controls.Add(this.txtEmail);
        this.pnlFormArea.Controls.Add(this.lblRole);
        this.pnlFormArea.Controls.Add(this.cmbRole);
        this.pnlFormArea.Controls.Add(this.chkIsActive);
        this.pnlFormArea.Controls.Add(this.lblPassword);
        this.pnlFormArea.Controls.Add(this.txtPassword);
        this.pnlFormArea.Controls.Add(this.btnLuu);
        this.pnlFormArea.Controls.Add(this.btnResetPassword);

        void FormLabel(Label l, string text, int y)
        {
            l.AutoSize = true;
            l.Font = new Font("Segoe UI", 9F);
            l.ForeColor = Color.FromArgb(80, 80, 90);
            l.Location = new Point(20, y);
            l.Text = text;
        }

        void FormField(TextBox t, int y)
        {
            t.Font = new Font("Segoe UI", 10F);
            t.Location = new Point(20, y);
            t.Size = new Size(300, 28);
            t.BorderStyle = BorderStyle.FixedSingle;
        }

        // lblFormTitle
        this.lblFormTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        this.lblFormTitle.ForeColor = Color.FromArgb(40, 44, 52);
        this.lblFormTitle.Location = new Point(20, 15);
        this.lblFormTitle.Size = new Size(300, 28);
        this.lblFormTitle.Text = "Chi tiết người dùng";

        FormLabel(this.lblUsername, "Tên đăng nhập:", 60);
        FormField(this.txtUsername, 78);

        FormLabel(this.lblHoTen, "Họ và tên: *", 118);
        FormField(this.txtHoTen, 136);

        FormLabel(this.lblEmail, "Email:", 176);
        FormField(this.txtEmail, 194);

        FormLabel(this.lblRole, "Vai trò:", 234);
        this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
        this.cmbRole.Font = new Font("Segoe UI", 10F);
        this.cmbRole.Location = new Point(20, 252);
        this.cmbRole.Size = new Size(200, 28);

        this.chkIsActive.AutoSize = true;
        this.chkIsActive.Font = new Font("Segoe UI", 10F);
        this.chkIsActive.ForeColor = Color.FromArgb(60, 60, 70);
        this.chkIsActive.Location = new Point(20, 296);
        this.chkIsActive.Text = "Tài khoản hoạt động";
        this.chkIsActive.Checked = true;

        FormLabel(this.lblPassword, "Mật khẩu:", 334);
        this.txtPassword.Font = new Font("Segoe UI", 10F);
        this.txtPassword.Location = new Point(20, 352);
        this.txtPassword.Size = new Size(300, 28);
        this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
        this.txtPassword.PasswordChar = '●';
        this.txtPassword.Enabled = false;

        // btnLuu
        this.btnLuu.BackColor = Color.FromArgb(0, 120, 215);
        this.btnLuu.FlatStyle = FlatStyle.Flat;
        this.btnLuu.FlatAppearance.BorderSize = 0;
        this.btnLuu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        this.btnLuu.ForeColor = Color.White;
        this.btnLuu.Location = new Point(20, 398);
        this.btnLuu.Size = new Size(120, 36);
        this.btnLuu.Text = "💾 Lưu";
        this.btnLuu.Cursor = Cursors.Hand;
        this.btnLuu.Click += new EventHandler(this.btnLuu_Click);

        // btnResetPassword
        this.btnResetPassword.BackColor = Color.FromArgb(180, 50, 50);
        this.btnResetPassword.FlatStyle = FlatStyle.Flat;
        this.btnResetPassword.FlatAppearance.BorderSize = 0;
        this.btnResetPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        this.btnResetPassword.ForeColor = Color.White;
        this.btnResetPassword.Location = new Point(155, 398);
        this.btnResetPassword.Size = new Size(165, 36);
        this.btnResetPassword.Text = "🔑 Reset mật khẩu";
        this.btnResetPassword.Cursor = Cursors.Hand;
        this.btnResetPassword.Visible = false;
        this.btnResetPassword.Click += new EventHandler(this.btnResetPassword_Click);

        // AdminUserForm
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.FromArgb(245, 246, 250);
        this.Controls.Add(this.splitMain);
        this.Name = "AdminUserForm";
        this.Text = "Quản lý người dùng";

        this.splitMain.Panel1.ResumeLayout(false);
        this.splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.splitMain).EndInit();
        this.splitMain.ResumeLayout(false);
        this.pnlListArea.ResumeLayout(false);
        this.pnlListButtons.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.dgvUsers).EndInit();
        this.pnlFormArea.ResumeLayout(false);
        this.pnlFormArea.PerformLayout();
        this.ResumeLayout(false);
    }

    private SplitContainer splitMain;
    private Panel pnlListArea;
    private Panel pnlListButtons;
    private Button btnThemMoi;
    private Button btnToggleActive;
    private DataGridView dgvUsers;
    private Panel pnlFormArea;
    private Label lblFormTitle;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblHoTen;
    private TextBox txtHoTen;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblRole;
    private ComboBox cmbRole;
    private CheckBox chkIsActive;
    private Label lblPassword;
    private TextBox txtPassword;
    private Button btnLuu;
    private Button btnResetPassword;
}
