namespace HeChuyenGiaThuocBenh.UI.Forms;

partial class BenhNhanForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlTop = new Panel();
        lblSearchLbl = new Label();
        txtSearch = new TextBox();
        btnTimKiem = new Button();
        btnXemTatCa = new Button();
        lblStatus = new Label();

        scMain = new SplitContainer();
        dgvBenhNhan = new DataGridView();
        colBNHoTen = new DataGridViewTextBoxColumn();
        colBNTuoi = new DataGridViewTextBoxColumn();
        colBNGioiTinh = new DataGridViewTextBoxColumn();
        colBNSDT = new DataGridViewTextBoxColumn();

        pnlRight = new Panel();
        pnlActions = new Panel();
        btnThem = new Button();
        btnLuu = new Button();

        tabControl = new TabControl();
        tabThongTin = new TabPage();
        tabLichSu = new TabPage();

        pnlFormFields = new Panel();
        lblHoTen = new Label();      txtHoTen = new TextBox();
        lblNgaySinh = new Label();   dtpNgaySinh = new DateTimePicker();
        lblGioiTinh = new Label();   cboGioiTinh = new ComboBox();
        lblSDT = new Label();        txtSoDienThoai = new TextBox();
        lblDiaChi = new Label();     txtDiaChi = new TextBox();
        lblTienSu = new Label();     txtTienSuBenh = new TextBox();
        lblDiUng = new Label();      txtDiUng = new TextBox();

        pnlLichSuTop = new Panel();
        lblLichSuCount = new Label();
        dgvLichSu = new DataGridView();
        colLSNgay = new DataGridViewTextBoxColumn();
        colLSKetQua = new DataGridViewTextBoxColumn();
        colLSTrieuChung = new DataGridViewTextBoxColumn();
        colLSThuoc = new DataGridViewTextBoxColumn();
        colLSGhiChu = new DataGridViewTextBoxColumn();

        ((System.ComponentModel.ISupportInitialize)scMain).BeginInit();
        scMain.Panel1.SuspendLayout();
        scMain.Panel2.SuspendLayout();
        scMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvBenhNhan).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvLichSu).BeginInit();
        pnlTop.SuspendLayout();
        pnlRight.SuspendLayout();
        pnlActions.SuspendLayout();
        tabControl.SuspendLayout();
        tabThongTin.SuspendLayout();
        tabLichSu.SuspendLayout();
        pnlFormFields.SuspendLayout();
        SuspendLayout();

        // ── pnlTop ─────────────────────────────────────────────────────────
        pnlTop.Dock = DockStyle.Top;
        pnlTop.Height = 48;
        pnlTop.BackColor = Color.FromArgb(248, 249, 252);

        lblSearchLbl.AutoSize = true;
        lblSearchLbl.Font = new Font("Segoe UI", 9F);
        lblSearchLbl.Location = new Point(8, 14);
        lblSearchLbl.Text = "Tìm kiếm:";

        txtSearch.Font = new Font("Segoe UI", 9.5F);
        txtSearch.Location = new Point(72, 10);
        txtSearch.Size = new Size(220, 26);
        txtSearch.PlaceholderText = "Họ tên / Số điện thoại...";
        txtSearch.KeyDown += new KeyEventHandler(txtSearch_KeyDown);

        btnTimKiem.BackColor = Color.FromArgb(0, 120, 215);
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.FlatAppearance.BorderSize = 0;
        btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTimKiem.ForeColor = Color.White;
        btnTimKiem.Location = new Point(298, 9);
        btnTimKiem.Size = new Size(80, 28);
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.Cursor = Cursors.Hand;
        btnTimKiem.Click += new EventHandler(btnTimKiem_Click);

        btnXemTatCa.FlatStyle = FlatStyle.Flat;
        btnXemTatCa.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 190);
        btnXemTatCa.Font = new Font("Segoe UI", 9F);
        btnXemTatCa.ForeColor = Color.FromArgb(60, 60, 80);
        btnXemTatCa.Location = new Point(384, 9);
        btnXemTatCa.Size = new Size(80, 28);
        btnXemTatCa.Text = "Xem tất cả";
        btnXemTatCa.Cursor = Cursors.Hand;
        btnXemTatCa.Click += new EventHandler(btnXemTatCa_Click);

        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblStatus.ForeColor = Color.Gray;
        lblStatus.Location = new Point(474, 14);
        lblStatus.Text = string.Empty;

        pnlTop.Controls.Add(lblSearchLbl);
        pnlTop.Controls.Add(txtSearch);
        pnlTop.Controls.Add(btnTimKiem);
        pnlTop.Controls.Add(btnXemTatCa);
        pnlTop.Controls.Add(lblStatus);

        // ── scMain ─────────────────────────────────────────────────────────
        scMain.Dock = DockStyle.Fill;
        scMain.Orientation = Orientation.Vertical;
        scMain.SplitterDistance = 310;
        scMain.Panel1.Controls.Add(dgvBenhNhan);
        scMain.Panel2.Controls.Add(pnlRight);

        // ── dgvBenhNhan ────────────────────────────────────────────────────
        dgvBenhNhan.Dock = DockStyle.Fill;
        dgvBenhNhan.ReadOnly = true;
        dgvBenhNhan.AllowUserToAddRows = false;
        dgvBenhNhan.AllowUserToDeleteRows = false;
        dgvBenhNhan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvBenhNhan.MultiSelect = false;
        dgvBenhNhan.RowHeadersVisible = false;
        dgvBenhNhan.BackgroundColor = Color.White;
        dgvBenhNhan.BorderStyle = BorderStyle.None;
        dgvBenhNhan.Font = new Font("Segoe UI", 9.5F);
        dgvBenhNhan.RowTemplate.Height = 30;
        dgvBenhNhan.ColumnHeadersHeight = 30;
        dgvBenhNhan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvBenhNhan.EnableHeadersVisualStyles = false;
        dgvBenhNhan.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
        dgvBenhNhan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvBenhNhan.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 100);
        dgvBenhNhan.SelectionChanged += new EventHandler(dgvBenhNhan_SelectionChanged);

        colBNHoTen.HeaderText = "Họ tên";
        colBNHoTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colBNHoTen.FillWeight = 45;

        colBNTuoi.HeaderText = "Tuổi";
        colBNTuoi.Width = 50;
        colBNTuoi.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        colBNGioiTinh.HeaderText = "Giới tính";
        colBNGioiTinh.Width = 72;
        colBNGioiTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        colBNSDT.HeaderText = "Điện thoại";
        colBNSDT.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colBNSDT.FillWeight = 30;

        dgvBenhNhan.Columns.AddRange(colBNHoTen, colBNTuoi, colBNGioiTinh, colBNSDT);

        // ── pnlRight ───────────────────────────────────────────────────────
        pnlRight.Dock = DockStyle.Fill;
        pnlRight.BackColor = Color.White;
        pnlRight.Controls.Add(tabControl);
        pnlRight.Controls.Add(pnlActions);

        // ── pnlActions ─────────────────────────────────────────────────────
        pnlActions.Dock = DockStyle.Top;
        pnlActions.Height = 46;
        pnlActions.BackColor = Color.FromArgb(248, 249, 252);

        btnThem.BackColor = Color.FromArgb(0, 150, 80);
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.FlatAppearance.BorderSize = 0;
        btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnThem.ForeColor = Color.White;
        btnThem.Location = new Point(8, 8);
        btnThem.Size = new Size(110, 30);
        btnThem.Text = "+ Thêm mới";
        btnThem.Cursor = Cursors.Hand;
        btnThem.Click += new EventHandler(btnThem_Click);

        btnLuu.BackColor = Color.FromArgb(0, 120, 215);
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.FlatAppearance.BorderSize = 0;
        btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLuu.ForeColor = Color.White;
        btnLuu.Location = new Point(126, 8);
        btnLuu.Size = new Size(90, 30);
        btnLuu.Text = "💾 Lưu";
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.Click += new EventHandler(btnLuu_Click);

        pnlActions.Controls.Add(btnThem);
        pnlActions.Controls.Add(btnLuu);

        // ── tabControl ─────────────────────────────────────────────────────
        tabControl.Dock = DockStyle.Fill;
        tabControl.Font = new Font("Segoe UI", 9.5F);
        tabControl.TabPages.Add(tabThongTin);
        tabControl.TabPages.Add(tabLichSu);

        tabThongTin.Text = "Thông tin bệnh nhân";
        tabThongTin.BackColor = Color.White;
        tabThongTin.Controls.Add(pnlFormFields);

        tabLichSu.Text = "Lịch sử chẩn đoán";
        tabLichSu.BackColor = Color.White;
        tabLichSu.Controls.Add(dgvLichSu);
        tabLichSu.Controls.Add(pnlLichSuTop);

        // ── pnlFormFields ──────────────────────────────────────────────────
        pnlFormFields.Dock = DockStyle.Fill;
        pnlFormFields.AutoScroll = true;
        pnlFormFields.Padding = new Padding(16, 10, 16, 10);
        pnlFormFields.BackColor = Color.White;

        int lx = 0, vx = 130, fy = 12, fw = 300, lw = 126, fgap = 34;

        void Field(Label lbl, string text, Control ctrl, int y, int h = 26)
        {
            lbl.AutoSize = false;
            lbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(60, 60, 80);
            lbl.Location = new Point(lx, y + 4);
            lbl.Size = new Size(lw, 20);
            lbl.Text = text;
            ctrl.Font = new Font("Segoe UI", 9.5F);
            ctrl.Location = new Point(vx, y);
            ctrl.Size = new Size(fw, h);
            pnlFormFields.Controls.Add(lbl);
            pnlFormFields.Controls.Add(ctrl);
        }

        Field(lblHoTen, "Họ tên *:", txtHoTen, fy);
        fy += fgap;

        Field(lblNgaySinh, "Ngày sinh:", dtpNgaySinh, fy);
        dtpNgaySinh.Format = DateTimePickerFormat.Short;
        fy += fgap;

        Field(lblGioiTinh, "Giới tính:", cboGioiTinh, fy);
        cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
        cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
        cboGioiTinh.SelectedIndex = 0;
        fy += fgap;

        Field(lblSDT, "Số điện thoại:", txtSoDienThoai, fy);
        fy += fgap;

        Field(lblDiaChi, "Địa chỉ:", txtDiaChi, fy);
        fy += fgap;

        txtTienSuBenh.Multiline = true;
        txtTienSuBenh.ScrollBars = ScrollBars.Vertical;
        Field(lblTienSu, "Tiền sử bệnh:", txtTienSuBenh, fy, 70);
        fy += 80;

        txtDiUng.Multiline = true;
        txtDiUng.ScrollBars = ScrollBars.Vertical;
        Field(lblDiUng, "Dị ứng:", txtDiUng, fy, 60);

        // ── pnlLichSuTop ───────────────────────────────────────────────────
        pnlLichSuTop.Dock = DockStyle.Top;
        pnlLichSuTop.Height = 32;
        pnlLichSuTop.BackColor = Color.FromArgb(248, 249, 252);
        pnlLichSuTop.Controls.Add(lblLichSuCount);

        lblLichSuCount.AutoSize = true;
        lblLichSuCount.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblLichSuCount.ForeColor = Color.Gray;
        lblLichSuCount.Location = new Point(8, 8);
        lblLichSuCount.Text = string.Empty;

        // ── dgvLichSu ──────────────────────────────────────────────────────
        dgvLichSu.Dock = DockStyle.Fill;
        dgvLichSu.ReadOnly = true;
        dgvLichSu.AllowUserToAddRows = false;
        dgvLichSu.AllowUserToDeleteRows = false;
        dgvLichSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLichSu.MultiSelect = false;
        dgvLichSu.RowHeadersVisible = false;
        dgvLichSu.BackgroundColor = Color.White;
        dgvLichSu.BorderStyle = BorderStyle.None;
        dgvLichSu.Font = new Font("Segoe UI", 9F);
        dgvLichSu.RowTemplate.Height = 28;
        dgvLichSu.ColumnHeadersHeight = 30;
        dgvLichSu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvLichSu.EnableHeadersVisualStyles = false;
        dgvLichSu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
        dgvLichSu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvLichSu.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 100);
        dgvLichSu.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

        colLSNgay.HeaderText = "Ngày chẩn đoán";
        colLSNgay.Width = 130;

        colLSKetQua.HeaderText = "Kết quả";
        colLSKetQua.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colLSKetQua.FillWeight = 30;

        colLSTrieuChung.HeaderText = "Triệu chứng";
        colLSTrieuChung.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colLSTrieuChung.FillWeight = 25;

        colLSThuoc.HeaderText = "Thuốc gợi ý";
        colLSThuoc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colLSThuoc.FillWeight = 25;

        colLSGhiChu.HeaderText = "Ghi chú";
        colLSGhiChu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colLSGhiChu.FillWeight = 20;

        dgvLichSu.Columns.AddRange(colLSNgay, colLSKetQua, colLSTrieuChung, colLSThuoc, colLSGhiChu);

        // ── Form ───────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        Controls.Add(scMain);
        Controls.Add(pnlTop);
        Name = "BenhNhanForm";
        Text = "Hồ Sơ Bệnh Nhân";

        ((System.ComponentModel.ISupportInitialize)dgvBenhNhan).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvLichSu).EndInit();
        pnlTop.ResumeLayout(false);
        pnlTop.PerformLayout();
        pnlRight.ResumeLayout(false);
        pnlActions.ResumeLayout(false);
        tabControl.ResumeLayout(false);
        tabThongTin.ResumeLayout(false);
        tabLichSu.ResumeLayout(false);
        pnlFormFields.ResumeLayout(false);
        pnlFormFields.PerformLayout();
        scMain.Panel1.ResumeLayout(false);
        scMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)scMain).EndInit();
        scMain.ResumeLayout(false);
        ResumeLayout(false);
    }

    // top bar
    private Panel pnlTop;
    private Label lblSearchLbl;
    private TextBox txtSearch;
    private Button btnTimKiem;
    private Button btnXemTatCa;
    private Label lblStatus;

    // split + left list
    private SplitContainer scMain;
    private DataGridView dgvBenhNhan;
    private DataGridViewTextBoxColumn colBNHoTen;
    private DataGridViewTextBoxColumn colBNTuoi;
    private DataGridViewTextBoxColumn colBNGioiTinh;
    private DataGridViewTextBoxColumn colBNSDT;

    // right panel
    private Panel pnlRight;
    private Panel pnlActions;
    private Button btnThem;
    private Button btnLuu;
    private TabControl tabControl;
    private TabPage tabThongTin;
    private TabPage tabLichSu;

    // tab 1 form fields
    private Panel pnlFormFields;
    private Label lblHoTen;      private TextBox txtHoTen;
    private Label lblNgaySinh;   private DateTimePicker dtpNgaySinh;
    private Label lblGioiTinh;   private ComboBox cboGioiTinh;
    private Label lblSDT;        private TextBox txtSoDienThoai;
    private Label lblDiaChi;     private TextBox txtDiaChi;
    private Label lblTienSu;     private TextBox txtTienSuBenh;
    private Label lblDiUng;      private TextBox txtDiUng;

    // tab 2 history
    private Panel pnlLichSuTop;
    private Label lblLichSuCount;
    private DataGridView dgvLichSu;
    private DataGridViewTextBoxColumn colLSNgay;
    private DataGridViewTextBoxColumn colLSKetQua;
    private DataGridViewTextBoxColumn colLSTrieuChung;
    private DataGridViewTextBoxColumn colLSThuoc;
    private DataGridViewTextBoxColumn colLSGhiChu;
}
