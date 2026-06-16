namespace HeChuyenGiaThuocBenh.UI.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.pnlHeader = new Panel();
        this.lblTitle = new Label();
        this.lblWelcome = new Label();
        this.btnLogout = new Button();
        this.pnlSidebar = new Panel();
        this.lblMenuTitle = new Label();
        this.btnChanDoan = new Button();
        this.btnBenhNhan = new Button();
        this.btnThuoc = new Button();
        this.btnBaoCao = new Button();
        this.lblAdminSection = new Label();
        this.btnAdminUsers = new Button();
        this.btnAdminDrugs = new Button();
        this.btnAdminDiseases = new Button();
        this.pnlContent = new Panel();
        this.lblContentPlaceholder = new Label();

        this.pnlHeader.SuspendLayout();
        this.pnlSidebar.SuspendLayout();
        this.pnlContent.SuspendLayout();
        this.SuspendLayout();

        // pnlHeader
        this.pnlHeader.BackColor = Color.FromArgb(0, 120, 215);
        this.pnlHeader.Controls.Add(this.lblTitle);
        this.pnlHeader.Controls.Add(this.lblWelcome);
        this.pnlHeader.Controls.Add(this.btnLogout);
        this.pnlHeader.Dock = DockStyle.Top;
        this.pnlHeader.Height = 60;

        // lblTitle
        this.lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        this.lblTitle.ForeColor = Color.White;
        this.lblTitle.Location = new Point(15, 15);
        this.lblTitle.Size = new Size(400, 30);
        this.lblTitle.Text = "HỆ CHUYÊN GIA THUỐC BỆNH";

        // lblWelcome
        this.lblWelcome.Font = new Font("Segoe UI", 9F);
        this.lblWelcome.ForeColor = Color.FromArgb(200, 235, 255);
        this.lblWelcome.Location = new Point(420, 20);
        this.lblWelcome.Size = new Size(400, 20);
        this.lblWelcome.Text = "Xin chào";

        // btnLogout
        this.btnLogout.BackColor = Color.FromArgb(196, 43, 28);
        this.btnLogout.FlatStyle = FlatStyle.Flat;
        this.btnLogout.FlatAppearance.BorderSize = 0;
        this.btnLogout.Font = new Font("Segoe UI", 9F);
        this.btnLogout.ForeColor = Color.White;
        this.btnLogout.Location = new Point(1120, 13);
        this.btnLogout.Size = new Size(90, 34);
        this.btnLogout.Text = "Đăng xuất";
        this.btnLogout.Cursor = Cursors.Hand;
        this.btnLogout.Click += new EventHandler(this.btnLogout_Click);

        // pnlSidebar
        this.pnlSidebar.BackColor = Color.FromArgb(40, 44, 52);
        this.pnlSidebar.Controls.Add(this.lblMenuTitle);
        this.pnlSidebar.Controls.Add(this.btnChanDoan);
        this.pnlSidebar.Controls.Add(this.btnBenhNhan);
        this.pnlSidebar.Controls.Add(this.btnThuoc);
        this.pnlSidebar.Controls.Add(this.btnBaoCao);
        this.pnlSidebar.Controls.Add(this.lblAdminSection);
        this.pnlSidebar.Controls.Add(this.btnAdminUsers);
        this.pnlSidebar.Controls.Add(this.btnAdminDrugs);
        this.pnlSidebar.Controls.Add(this.btnAdminDiseases);
        this.pnlSidebar.Dock = DockStyle.Left;
        this.pnlSidebar.Width = 200;

        // lblMenuTitle
        this.lblMenuTitle.Font = new Font("Segoe UI", 8F);
        this.lblMenuTitle.ForeColor = Color.FromArgb(120, 120, 130);
        this.lblMenuTitle.Location = new Point(10, 15);
        this.lblMenuTitle.Size = new Size(180, 20);
        this.lblMenuTitle.Text = "CHỨC NĂNG CHÍNH";

        void SidebarBtn(Button b, string text, int y, EventHandler handler)
        {
            b.BackColor = Color.Transparent;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 64, 72);
            b.Font = new Font("Segoe UI", 10F);
            b.ForeColor = Color.FromArgb(200, 200, 210);
            b.Location = new Point(0, y);
            b.Size = new Size(200, 44);
            b.Text = text;
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.Padding = new Padding(15, 0, 0, 0);
            b.Cursor = Cursors.Hand;
            b.Click += handler;
        }

        SidebarBtn(this.btnChanDoan,       "  🔬 Chẩn đoán",        40,  this.btnChanDoan_Click);
        SidebarBtn(this.btnBenhNhan,       "  👤 Hồ sơ bệnh nhân",  84,  this.btnBenhNhan_Click);
        SidebarBtn(this.btnThuoc,          "  💊 Tra cứu thuốc",    128, this.btnThuoc_Click);
        SidebarBtn(this.btnBaoCao,         "  📊 Báo cáo",          172, this.btnBaoCao_Click);

        // lblAdminSection
        this.lblAdminSection.Font = new Font("Segoe UI", 8F);
        this.lblAdminSection.ForeColor = Color.FromArgb(120, 120, 130);
        this.lblAdminSection.Location = new Point(10, 230);
        this.lblAdminSection.Size = new Size(180, 20);
        this.lblAdminSection.Text = "QUẢN TRỊ";

        SidebarBtn(this.btnAdminUsers,    "  👥 Người dùng",        252, this.btnAdminUsers_Click);
        SidebarBtn(this.btnAdminDrugs,    "  💊 Quản lý thuốc",     296, this.btnAdminDrugs_Click);
        SidebarBtn(this.btnAdminDiseases, "  🏥 Quản lý bệnh",      340, this.btnAdminDiseases_Click);

        // pnlContent
        this.pnlContent.BackColor = Color.FromArgb(245, 246, 250);
        this.pnlContent.Controls.Add(this.lblContentPlaceholder);
        this.pnlContent.Dock = DockStyle.Fill;

        // lblContentPlaceholder
        this.lblContentPlaceholder.Font = new Font("Segoe UI", 14F);
        this.lblContentPlaceholder.ForeColor = Color.FromArgb(180, 180, 190);
        this.lblContentPlaceholder.Dock = DockStyle.Fill;
        this.lblContentPlaceholder.Text = "Chọn chức năng từ menu bên trái";
        this.lblContentPlaceholder.TextAlign = ContentAlignment.MiddleCenter;

        // MainForm
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(1240, 720);
        this.Controls.Add(this.pnlContent);
        this.Controls.Add(this.pnlSidebar);
        this.Controls.Add(this.pnlHeader);
        this.MinimumSize = new Size(1000, 600);
        this.Name = "MainForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Hệ Chuyên Gia Thuốc Bệnh";

        this.pnlHeader.ResumeLayout(false);
        this.pnlSidebar.ResumeLayout(false);
        this.pnlContent.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    private Panel pnlHeader;
    private Label lblTitle;
    private Label lblWelcome;
    private Button btnLogout;
    private Panel pnlSidebar;
    private Label lblMenuTitle;
    private Button btnChanDoan;
    private Button btnBenhNhan;
    private Button btnThuoc;
    private Button btnBaoCao;
    private Label lblAdminSection;
    private Button btnAdminUsers;
    private Button btnAdminDrugs;
    private Button btnAdminDiseases;
    private Panel pnlContent;
    private Label lblContentPlaceholder;
}
