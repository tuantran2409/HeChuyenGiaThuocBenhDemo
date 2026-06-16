namespace HeChuyenGiaThuocBenh.UI.Forms;

partial class AdminThuocForm
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
        lblNhomLbl = new Label();
        cboNhom = new ComboBox();
        btnXemTatCa = new Button();
        btnThem = new Button();
        btnXoa = new Button();
        lblStatus = new Label();

        scMain = new SplitContainer();
        dgvThuoc = new DataGridView();
        colTen = new DataGridViewTextBoxColumn();
        colHoatChat = new DataGridViewTextBoxColumn();
        colNhom = new DataGridViewTextBoxColumn();

        pnlEdit = new Panel();
        pnlEditTop = new Panel();
        lblEditTitle = new Label();
        pnlEditScroll = new Panel();

        lblTen = new Label();        txtTen = new TextBox();
        lblHoatChatLbl = new Label(); txtHoatChat = new TextBox();
        lblNhomThuocLbl = new Label(); txtNhomThuoc = new TextBox();
        lblLieuDungLbl = new Label(); txtLieuDung = new TextBox();
        lblCachDungLbl = new Label(); txtCachDung = new TextBox();
        lblChongChiDinhLbl = new Label(); txtChongChiDinh = new TextBox();
        lblTacDungPhuLbl = new Label(); txtTacDungPhu = new TextBox();
        lblMoTaLbl = new Label(); txtMoTa = new TextBox();
        chkIsActive = new CheckBox();

        pnlEditButtons = new Panel();
        btnLuu = new Button();

        ((System.ComponentModel.ISupportInitialize)scMain).BeginInit();
        scMain.Panel1.SuspendLayout();
        scMain.Panel2.SuspendLayout();
        scMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvThuoc).BeginInit();
        pnlTop.SuspendLayout();
        pnlEdit.SuspendLayout();
        pnlEditTop.SuspendLayout();
        pnlEditScroll.SuspendLayout();
        pnlEditButtons.SuspendLayout();
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
        txtSearch.Location = new Point(70, 10);
        txtSearch.Size = new Size(180, 26);
        txtSearch.PlaceholderText = "Tên / Hoạt chất...";
        txtSearch.KeyDown += new KeyEventHandler(txtSearch_KeyDown);

        btnTimKiem.BackColor = Color.FromArgb(0, 120, 215);
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.FlatAppearance.BorderSize = 0;
        btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTimKiem.ForeColor = Color.White;
        btnTimKiem.Location = new Point(256, 9);
        btnTimKiem.Size = new Size(80, 28);
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.Cursor = Cursors.Hand;
        btnTimKiem.Click += new EventHandler(btnTimKiem_Click);

        lblNhomLbl.AutoSize = true;
        lblNhomLbl.Font = new Font("Segoe UI", 9F);
        lblNhomLbl.Location = new Point(344, 14);
        lblNhomLbl.Text = "Nhóm:";

        cboNhom.DropDownStyle = ComboBoxStyle.DropDownList;
        cboNhom.Font = new Font("Segoe UI", 9F);
        cboNhom.Location = new Point(390, 10);
        cboNhom.Size = new Size(150, 26);
        cboNhom.SelectedIndexChanged += new EventHandler(cboNhom_SelectedIndexChanged);

        btnXemTatCa.FlatStyle = FlatStyle.Flat;
        btnXemTatCa.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 190);
        btnXemTatCa.Font = new Font("Segoe UI", 9F);
        btnXemTatCa.ForeColor = Color.FromArgb(60, 60, 80);
        btnXemTatCa.Location = new Point(548, 9);
        btnXemTatCa.Size = new Size(80, 28);
        btnXemTatCa.Text = "Xem tất cả";
        btnXemTatCa.Cursor = Cursors.Hand;
        btnXemTatCa.Click += new EventHandler(btnXemTatCa_Click);

        btnThem.BackColor = Color.FromArgb(0, 150, 80);
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.FlatAppearance.BorderSize = 0;
        btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnThem.ForeColor = Color.White;
        btnThem.Location = new Point(638, 9);
        btnThem.Size = new Size(100, 28);
        btnThem.Text = "+ Thêm mới";
        btnThem.Cursor = Cursors.Hand;
        btnThem.Click += new EventHandler(btnThem_Click);

        btnXoa.BackColor = Color.FromArgb(196, 43, 28);
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.FlatAppearance.BorderSize = 0;
        btnXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnXoa.ForeColor = Color.White;
        btnXoa.Location = new Point(744, 9);
        btnXoa.Size = new Size(80, 28);
        btnXoa.Text = "🗑 Xóa";
        btnXoa.Cursor = Cursors.Hand;
        btnXoa.Click += new EventHandler(btnXoa_Click);

        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblStatus.ForeColor = Color.Gray;
        lblStatus.Location = new Point(834, 14);
        lblStatus.Text = string.Empty;

        pnlTop.Controls.Add(lblSearchLbl);
        pnlTop.Controls.Add(txtSearch);
        pnlTop.Controls.Add(btnTimKiem);
        pnlTop.Controls.Add(lblNhomLbl);
        pnlTop.Controls.Add(cboNhom);
        pnlTop.Controls.Add(btnXemTatCa);
        pnlTop.Controls.Add(btnThem);
        pnlTop.Controls.Add(btnXoa);
        pnlTop.Controls.Add(lblStatus);

        // ── scMain ─────────────────────────────────────────────────────────
        scMain.Dock = DockStyle.Fill;
        scMain.Orientation = Orientation.Vertical;
        scMain.SplitterDistance = 400;
        scMain.Panel1.Controls.Add(dgvThuoc);
        scMain.Panel2.Controls.Add(pnlEdit);

        // ── dgvThuoc ───────────────────────────────────────────────────────
        dgvThuoc.Dock = DockStyle.Fill;
        dgvThuoc.ReadOnly = true;
        dgvThuoc.AllowUserToAddRows = false;
        dgvThuoc.AllowUserToDeleteRows = false;
        dgvThuoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvThuoc.MultiSelect = false;
        dgvThuoc.RowHeadersVisible = false;
        dgvThuoc.BackgroundColor = Color.White;
        dgvThuoc.BorderStyle = BorderStyle.None;
        dgvThuoc.Font = new Font("Segoe UI", 9.5F);
        dgvThuoc.RowTemplate.Height = 30;
        dgvThuoc.ColumnHeadersHeight = 30;
        dgvThuoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvThuoc.EnableHeadersVisualStyles = false;
        dgvThuoc.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
        dgvThuoc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvThuoc.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 100);
        dgvThuoc.SelectionChanged += new EventHandler(dgvThuoc_SelectionChanged);

        colTen.HeaderText = "Tên thuốc";
        colTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colTen.FillWeight = 40;

        colHoatChat.HeaderText = "Hoạt chất";
        colHoatChat.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colHoatChat.FillWeight = 35;

        colNhom.HeaderText = "Nhóm thuốc";
        colNhom.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colNhom.FillWeight = 25;

        dgvThuoc.Columns.AddRange(colTen, colHoatChat, colNhom);

        // ── pnlEdit ────────────────────────────────────────────────────────
        pnlEdit.Dock = DockStyle.Fill;
        pnlEdit.Visible = false;
        pnlEdit.BackColor = Color.White;
        pnlEdit.Controls.Add(pnlEditScroll);
        pnlEdit.Controls.Add(pnlEditButtons);
        pnlEdit.Controls.Add(pnlEditTop);

        // pnlEditTop — title
        pnlEditTop.Dock = DockStyle.Top;
        pnlEditTop.Height = 40;
        pnlEditTop.BackColor = Color.FromArgb(240, 244, 255);
        pnlEditTop.Padding = new Padding(12, 8, 12, 6);
        pnlEditTop.Controls.Add(lblEditTitle);

        lblEditTitle.Dock = DockStyle.Fill;
        lblEditTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblEditTitle.ForeColor = Color.FromArgb(20, 50, 120);
        lblEditTitle.Text = string.Empty;

        // pnlEditButtons — bottom
        pnlEditButtons.Dock = DockStyle.Bottom;
        pnlEditButtons.Height = 44;
        pnlEditButtons.BackColor = Color.FromArgb(248, 249, 252);
        pnlEditButtons.Controls.Add(btnLuu);

        btnLuu.BackColor = Color.FromArgb(0, 120, 215);
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.FlatAppearance.BorderSize = 0;
        btnLuu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnLuu.ForeColor = Color.White;
        btnLuu.Location = new Point(8, 7);
        btnLuu.Size = new Size(100, 30);
        btnLuu.Text = "💾 Lưu";
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.Click += new EventHandler(btnLuu_Click);

        // pnlEditScroll — scrollable form fields
        pnlEditScroll.Dock = DockStyle.Fill;
        pnlEditScroll.AutoScroll = true;
        pnlEditScroll.Padding = new Padding(16, 10, 16, 10);
        pnlEditScroll.BackColor = Color.White;

        int lx = 0, vx = 120, fy = 8, fw = 320, lw = 116, fgap = 32;

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
            pnlEditScroll.Controls.Add(lbl);
            pnlEditScroll.Controls.Add(ctrl);
        }

        Field(lblTen, "Tên thuốc *:", txtTen, fy); fy += fgap;
        Field(lblHoatChatLbl, "Hoạt chất *:", txtHoatChat, fy); fy += fgap;
        Field(lblNhomThuocLbl, "Nhóm thuốc:", txtNhomThuoc, fy); fy += fgap;
        Field(lblLieuDungLbl, "Liều dùng:", txtLieuDung, fy); fy += fgap;
        Field(lblCachDungLbl, "Cách dùng:", txtCachDung, fy); fy += fgap;

        txtChongChiDinh.Multiline = true;
        txtChongChiDinh.ScrollBars = ScrollBars.Vertical;
        txtChongChiDinh.ForeColor = Color.DarkRed;
        Field(lblChongChiDinhLbl, "Chống chỉ định:", txtChongChiDinh, fy, 60); fy += 70;

        txtTacDungPhu.Multiline = true;
        txtTacDungPhu.ScrollBars = ScrollBars.Vertical;
        txtTacDungPhu.ForeColor = Color.FromArgb(140, 80, 0);
        Field(lblTacDungPhuLbl, "Tác dụng phụ:", txtTacDungPhu, fy, 60); fy += 70;

        txtMoTa.Multiline = true;
        txtMoTa.ScrollBars = ScrollBars.Vertical;
        Field(lblMoTaLbl, "Mô tả:", txtMoTa, fy, 60); fy += 70;

        chkIsActive.AutoSize = true;
        chkIsActive.Font = new Font("Segoe UI", 9.5F);
        chkIsActive.Location = new Point(vx, fy);
        chkIsActive.Text = "Đang hoạt động (IsActive)";
        chkIsActive.Checked = true;
        pnlEditScroll.Controls.Add(chkIsActive);

        // ── Form ───────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        Controls.Add(scMain);
        Controls.Add(pnlTop);
        Name = "AdminThuocForm";
        Text = "Quản Lý Thuốc";

        ((System.ComponentModel.ISupportInitialize)dgvThuoc).EndInit();
        pnlTop.ResumeLayout(false);
        pnlTop.PerformLayout();
        pnlEdit.ResumeLayout(false);
        pnlEditTop.ResumeLayout(false);
        pnlEditScroll.ResumeLayout(false);
        pnlEditScroll.PerformLayout();
        pnlEditButtons.ResumeLayout(false);
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
    private Label lblNhomLbl;
    private ComboBox cboNhom;
    private Button btnXemTatCa;
    private Button btnThem;
    private Button btnXoa;
    private Label lblStatus;

    // split + left list
    private SplitContainer scMain;
    private DataGridView dgvThuoc;
    private DataGridViewTextBoxColumn colTen;
    private DataGridViewTextBoxColumn colHoatChat;
    private DataGridViewTextBoxColumn colNhom;

    // right edit panel
    private Panel pnlEdit;
    private Panel pnlEditTop;
    private Label lblEditTitle;
    private Panel pnlEditScroll;
    private Panel pnlEditButtons;
    private Button btnLuu;

    // edit form fields
    private Label lblTen;          private TextBox txtTen;
    private Label lblHoatChatLbl;  private TextBox txtHoatChat;
    private Label lblNhomThuocLbl; private TextBox txtNhomThuoc;
    private Label lblLieuDungLbl;  private TextBox txtLieuDung;
    private Label lblCachDungLbl;  private TextBox txtCachDung;
    private Label lblChongChiDinhLbl; private TextBox txtChongChiDinh;
    private Label lblTacDungPhuLbl;   private TextBox txtTacDungPhu;
    private Label lblMoTaLbl;      private TextBox txtMoTa;
    private CheckBox chkIsActive;
}
