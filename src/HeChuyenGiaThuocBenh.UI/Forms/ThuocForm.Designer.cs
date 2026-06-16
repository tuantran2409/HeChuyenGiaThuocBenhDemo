namespace HeChuyenGiaThuocBenh.UI.Forms;

partial class ThuocForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        // ── Declare all controls ──────────────────────────────────────────────
        pnlTop          = new Panel();
        lblSearchLbl    = new Label();
        txtSearch       = new TextBox();
        btnTimKiem      = new Button();
        lblNhomLbl      = new Label();
        cboNhom         = new ComboBox();
        btnXemTatCa     = new Button();
        lblStatus       = new Label();

        scMain          = new SplitContainer();

        // Left — drug list
        dgvThuoc        = new DataGridView();
        colTen          = new DataGridViewTextBoxColumn();
        colHoatChat     = new DataGridViewTextBoxColumn();
        colNhom         = new DataGridViewTextBoxColumn();
        colLieu         = new DataGridViewTextBoxColumn();

        // Right — detail
        pnlDetail               = new Panel();
        pnlDetailTop            = new Panel();
        lblTenThuoc             = new Label();
        pnlFields               = new Panel();
        lblHoatChat             = new Label();
        lblHoatChatVal          = new Label();
        lblNhom                 = new Label();
        lblNhomVal              = new Label();
        lblLieuDung             = new Label();
        lblLieuDungVal          = new Label();
        lblCachDung             = new Label();
        lblCachDungVal          = new Label();
        gbChongChiDinh          = new GroupBox();
        txtChongChiDinh         = new TextBox();
        gbTacDungPhu            = new GroupBox();
        txtTacDungPhu           = new TextBox();
        gbTuongTac              = new GroupBox();
        pnlTuongTacTop          = new Panel();
        lblTuongTacHint         = new Label();
        btnKiemTraTuongTac      = new Button();
        lblTuongTacStatus       = new Label();
        dgvTuongTac             = new DataGridView();
        colTTThuoc1             = new DataGridViewTextBoxColumn();
        colTTThuoc2             = new DataGridViewTextBoxColumn();
        colTTMucDo              = new DataGridViewTextBoxColumn();
        colTTMoTa               = new DataGridViewTextBoxColumn();
        colTTHauQua             = new DataGridViewTextBoxColumn();

        ((System.ComponentModel.ISupportInitialize)scMain).BeginInit();
        scMain.Panel1.SuspendLayout();
        scMain.Panel2.SuspendLayout();
        scMain.SuspendLayout();

        ((System.ComponentModel.ISupportInitialize)dgvThuoc).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvTuongTac).BeginInit();

        pnlTop.SuspendLayout();
        pnlDetail.SuspendLayout();
        pnlDetailTop.SuspendLayout();
        pnlFields.SuspendLayout();
        gbChongChiDinh.SuspendLayout();
        gbTacDungPhu.SuspendLayout();
        gbTuongTac.SuspendLayout();
        pnlTuongTacTop.SuspendLayout();

        SuspendLayout();

        // ── pnlTop ────────────────────────────────────────────────────────────
        pnlTop.Dock = DockStyle.Top;
        pnlTop.Height = 50;
        pnlTop.BackColor = Color.FromArgb(248, 249, 252);
        pnlTop.Padding = new Padding(8, 8, 8, 6);

        // Absolute layout within pnlTop (simpler than nested docking)
        lblSearchLbl.AutoSize = true;
        lblSearchLbl.Font = new Font("Segoe UI", 9F);
        lblSearchLbl.Location = new Point(8, 14);
        lblSearchLbl.Text = "Tìm kiếm:";

        txtSearch.Font = new Font("Segoe UI", 9.5F);
        txtSearch.Location = new Point(72, 10);
        txtSearch.Size = new Size(210, 26);
        txtSearch.PlaceholderText = "Tên thuốc / Hoạt chất...";
        txtSearch.KeyDown += new KeyEventHandler(txtSearch_KeyDown);

        btnTimKiem.BackColor = Color.FromArgb(0, 120, 215);
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.FlatAppearance.BorderSize = 0;
        btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTimKiem.ForeColor = Color.White;
        btnTimKiem.Location = new Point(288, 9);
        btnTimKiem.Size = new Size(80, 28);
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.Cursor = Cursors.Hand;
        btnTimKiem.Click += new EventHandler(btnTimKiem_Click);

        lblNhomLbl.AutoSize = true;
        lblNhomLbl.Font = new Font("Segoe UI", 9F);
        lblNhomLbl.Location = new Point(378, 14);
        lblNhomLbl.Text = "Nhóm:";

        cboNhom.DropDownStyle = ComboBoxStyle.DropDownList;
        cboNhom.Font = new Font("Segoe UI", 9F);
        cboNhom.Location = new Point(424, 10);
        cboNhom.Size = new Size(170, 26);
        cboNhom.SelectedIndexChanged += new EventHandler(cboNhom_SelectedIndexChanged);

        btnXemTatCa.FlatStyle = FlatStyle.Flat;
        btnXemTatCa.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 190);
        btnXemTatCa.Font = new Font("Segoe UI", 9F);
        btnXemTatCa.ForeColor = Color.FromArgb(60, 60, 80);
        btnXemTatCa.Location = new Point(602, 9);
        btnXemTatCa.Size = new Size(80, 28);
        btnXemTatCa.Text = "Xem tất cả";
        btnXemTatCa.Cursor = Cursors.Hand;
        btnXemTatCa.Click += new EventHandler(btnXemTatCa_Click);

        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblStatus.ForeColor = Color.Gray;
        lblStatus.Location = new Point(695, 14);
        lblStatus.Text = string.Empty;

        pnlTop.Controls.Add(lblSearchLbl);
        pnlTop.Controls.Add(txtSearch);
        pnlTop.Controls.Add(btnTimKiem);
        pnlTop.Controls.Add(lblNhomLbl);
        pnlTop.Controls.Add(cboNhom);
        pnlTop.Controls.Add(btnXemTatCa);
        pnlTop.Controls.Add(lblStatus);

        // ── scMain ────────────────────────────────────────────────────────────
        scMain.Dock = DockStyle.Fill;
        scMain.Orientation = Orientation.Vertical;
        scMain.SplitterDistance = 460;
        scMain.Panel1.Controls.Add(dgvThuoc);
        scMain.Panel2.Controls.Add(pnlDetail);

        // ── dgvThuoc (left list) ──────────────────────────────────────────────
        dgvThuoc.Dock = DockStyle.Fill;
        dgvThuoc.ReadOnly = true;
        dgvThuoc.AllowUserToAddRows = false;
        dgvThuoc.AllowUserToDeleteRows = false;
        dgvThuoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvThuoc.MultiSelect = true;
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
        dgvThuoc.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        dgvThuoc.SelectionChanged += new EventHandler(dgvThuoc_SelectionChanged);

        colTen.HeaderText = "Tên thuốc";
        colTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colTen.FillWeight = 35;

        colHoatChat.HeaderText = "Hoạt chất";
        colHoatChat.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colHoatChat.FillWeight = 30;

        colNhom.HeaderText = "Nhóm thuốc";
        colNhom.Width = 130;

        colLieu.HeaderText = "Liều dùng";
        colLieu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colLieu.FillWeight = 35;

        dgvThuoc.Columns.AddRange(colTen, colHoatChat, colNhom, colLieu);

        // ── pnlDetail (right side) ────────────────────────────────────────────
        pnlDetail.Dock = DockStyle.Fill;
        pnlDetail.Visible = false;
        pnlDetail.BackColor = Color.White;

        // Add in dock order: Bottom → Fill → Top
        pnlDetail.Controls.Add(gbTuongTac);        // Bottom
        pnlDetail.Controls.Add(gbTacDungPhu);      // Fill
        pnlDetail.Controls.Add(gbChongChiDinh);    // Top
        pnlDetail.Controls.Add(pnlFields);         // Top
        pnlDetail.Controls.Add(pnlDetailTop);      // Top (very top)

        // pnlDetailTop — drug name
        pnlDetailTop.Dock = DockStyle.Top;
        pnlDetailTop.Height = 50;
        pnlDetailTop.BackColor = Color.FromArgb(240, 244, 255);
        pnlDetailTop.Padding = new Padding(12, 10, 12, 8);
        pnlDetailTop.Controls.Add(lblTenThuoc);

        lblTenThuoc.Dock = DockStyle.Fill;
        lblTenThuoc.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTenThuoc.ForeColor = Color.FromArgb(20, 50, 120);
        lblTenThuoc.Text = string.Empty;

        // pnlFields — key-value fields
        pnlFields.Dock = DockStyle.Top;
        pnlFields.Height = 90;
        pnlFields.Padding = new Padding(12, 6, 12, 4);
        pnlFields.BackColor = Color.White;

        // Use absolute layout inside pnlFields
        int fx = 0, fy = 6, fw1 = 80, fgap = 22;

        lblHoatChat.AutoSize = true;
        lblHoatChat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblHoatChat.ForeColor = Color.FromArgb(80, 80, 100);
        lblHoatChat.Location = new Point(fx, fy);
        lblHoatChat.Text = "Hoạt chất:";

        lblHoatChatVal.AutoSize = true;
        lblHoatChatVal.Font = new Font("Segoe UI", 9F);
        lblHoatChatVal.Location = new Point(fx + fw1, fy);
        lblHoatChatVal.Text = string.Empty;
        fy += fgap;

        lblNhom.AutoSize = true;
        lblNhom.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNhom.ForeColor = Color.FromArgb(80, 80, 100);
        lblNhom.Location = new Point(fx, fy);
        lblNhom.Text = "Nhóm thuốc:";

        lblNhomVal.AutoSize = true;
        lblNhomVal.Font = new Font("Segoe UI", 9F);
        lblNhomVal.Location = new Point(fx + fw1, fy);
        lblNhomVal.Text = string.Empty;
        fy += fgap;

        lblLieuDung.AutoSize = true;
        lblLieuDung.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLieuDung.ForeColor = Color.FromArgb(80, 80, 100);
        lblLieuDung.Location = new Point(fx, fy);
        lblLieuDung.Text = "Liều dùng:";

        lblLieuDungVal.AutoSize = true;
        lblLieuDungVal.Font = new Font("Segoe UI", 9F);
        lblLieuDungVal.Location = new Point(fx + fw1, fy);
        lblLieuDungVal.Text = string.Empty;
        fy += fgap;

        lblCachDung.AutoSize = true;
        lblCachDung.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblCachDung.ForeColor = Color.FromArgb(80, 80, 100);
        lblCachDung.Location = new Point(fx, fy);
        lblCachDung.Text = "Cách dùng:";

        lblCachDungVal.AutoSize = true;
        lblCachDungVal.Font = new Font("Segoe UI", 9F);
        lblCachDungVal.Location = new Point(fx + fw1, fy);
        lblCachDungVal.Text = string.Empty;

        pnlFields.Controls.AddRange(new Control[] {
            lblHoatChat, lblHoatChatVal,
            lblNhom, lblNhomVal,
            lblLieuDung, lblLieuDungVal,
            lblCachDung, lblCachDungVal
        });

        // gbChongChiDinh
        gbChongChiDinh.Dock = DockStyle.Top;
        gbChongChiDinh.Height = 80;
        gbChongChiDinh.Text = "Chống chỉ định";
        gbChongChiDinh.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        gbChongChiDinh.ForeColor = Color.DarkRed;
        gbChongChiDinh.Padding = new Padding(4, 2, 4, 4);
        gbChongChiDinh.Controls.Add(txtChongChiDinh);

        txtChongChiDinh.Dock = DockStyle.Fill;
        txtChongChiDinh.Multiline = true;
        txtChongChiDinh.ReadOnly = true;
        txtChongChiDinh.ScrollBars = ScrollBars.Vertical;
        txtChongChiDinh.BorderStyle = BorderStyle.None;
        txtChongChiDinh.Font = new Font("Segoe UI", 9F);
        txtChongChiDinh.BackColor = Color.FromArgb(255, 248, 248);
        txtChongChiDinh.ForeColor = Color.DarkRed;

        // gbTacDungPhu
        gbTacDungPhu.Dock = DockStyle.Fill;
        gbTacDungPhu.Text = "Tác dụng phụ";
        gbTacDungPhu.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        gbTacDungPhu.ForeColor = Color.FromArgb(180, 100, 0);
        gbTacDungPhu.Padding = new Padding(4, 2, 4, 4);
        gbTacDungPhu.Controls.Add(txtTacDungPhu);

        txtTacDungPhu.Dock = DockStyle.Fill;
        txtTacDungPhu.Multiline = true;
        txtTacDungPhu.ReadOnly = true;
        txtTacDungPhu.ScrollBars = ScrollBars.Vertical;
        txtTacDungPhu.BorderStyle = BorderStyle.None;
        txtTacDungPhu.Font = new Font("Segoe UI", 9F);
        txtTacDungPhu.BackColor = Color.FromArgb(255, 253, 245);
        txtTacDungPhu.ForeColor = Color.FromArgb(140, 80, 0);

        // gbTuongTac — drug interaction checker
        gbTuongTac.Dock = DockStyle.Bottom;
        gbTuongTac.Height = 195;
        gbTuongTac.Text = "Kiểm tra tương tác thuốc (chọn nhiều thuốc trong danh sách)";
        gbTuongTac.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        gbTuongTac.ForeColor = Color.FromArgb(100, 60, 0);
        gbTuongTac.Padding = new Padding(4, 2, 4, 4);
        gbTuongTac.Controls.Add(dgvTuongTac);
        gbTuongTac.Controls.Add(pnlTuongTacTop);

        pnlTuongTacTop.Dock = DockStyle.Top;
        pnlTuongTacTop.Height = 38;
        pnlTuongTacTop.Controls.Add(lblTuongTacStatus);
        pnlTuongTacTop.Controls.Add(btnKiemTraTuongTac);
        pnlTuongTacTop.Controls.Add(lblTuongTacHint);

        lblTuongTacHint.AutoSize = true;
        lblTuongTacHint.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblTuongTacHint.ForeColor = Color.Gray;
        lblTuongTacHint.Location = new Point(4, 12);
        lblTuongTacHint.Text = "Giữ Ctrl và click nhiều hàng để chọn nhiều thuốc.";

        btnKiemTraTuongTac.BackColor = Color.FromArgb(200, 80, 0);
        btnKiemTraTuongTac.FlatStyle = FlatStyle.Flat;
        btnKiemTraTuongTac.FlatAppearance.BorderSize = 0;
        btnKiemTraTuongTac.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnKiemTraTuongTac.ForeColor = Color.White;
        btnKiemTraTuongTac.Location = new Point(310, 6);
        btnKiemTraTuongTac.Size = new Size(160, 26);
        btnKiemTraTuongTac.Text = "⚠  Kiểm tra tương tác";
        btnKiemTraTuongTac.Cursor = Cursors.Hand;
        btnKiemTraTuongTac.Click += new EventHandler(btnKiemTraTuongTac_Click);

        lblTuongTacStatus.AutoSize = true;
        lblTuongTacStatus.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblTuongTacStatus.Location = new Point(480, 12);
        lblTuongTacStatus.Text = string.Empty;

        dgvTuongTac.Dock = DockStyle.Fill;
        dgvTuongTac.ReadOnly = true;
        dgvTuongTac.AllowUserToAddRows = false;
        dgvTuongTac.AllowUserToDeleteRows = false;
        dgvTuongTac.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvTuongTac.RowHeadersVisible = false;
        dgvTuongTac.BackgroundColor = Color.FromArgb(255, 248, 235);
        dgvTuongTac.BorderStyle = BorderStyle.None;
        dgvTuongTac.Font = new Font("Segoe UI", 9F);
        dgvTuongTac.RowTemplate.Height = 26;
        dgvTuongTac.ColumnHeadersHeight = 26;
        dgvTuongTac.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvTuongTac.EnableHeadersVisualStyles = false;
        dgvTuongTac.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 240, 210);
        dgvTuongTac.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        dgvTuongTac.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

        colTTThuoc1.HeaderText = "Thuốc 1";
        colTTThuoc1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colTTThuoc1.FillWeight = 20;

        colTTThuoc2.HeaderText = "Thuốc 2";
        colTTThuoc2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colTTThuoc2.FillWeight = 20;

        colTTMucDo.HeaderText = "Mức độ";
        colTTMucDo.Width = 100;

        colTTMoTa.HeaderText = "Mô tả";
        colTTMoTa.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colTTMoTa.FillWeight = 35;

        colTTHauQua.HeaderText = "Hậu quả";
        colTTHauQua.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colTTHauQua.FillWeight = 25;

        dgvTuongTac.Columns.AddRange(colTTThuoc1, colTTThuoc2, colTTMucDo, colTTMoTa, colTTHauQua);

        // ── Form ──────────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        Controls.Add(scMain);
        Controls.Add(pnlTop);
        Name = "ThuocForm";
        Text = "Tra Cứu Thuốc";

        ((System.ComponentModel.ISupportInitialize)dgvThuoc).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvTuongTac).EndInit();

        pnlTop.ResumeLayout(false);
        pnlTop.PerformLayout();
        pnlDetail.ResumeLayout(false);
        pnlDetailTop.ResumeLayout(false);
        pnlFields.ResumeLayout(false);
        pnlFields.PerformLayout();
        gbChongChiDinh.ResumeLayout(false);
        gbTacDungPhu.ResumeLayout(false);
        gbTuongTac.ResumeLayout(false);
        pnlTuongTacTop.ResumeLayout(false);
        pnlTuongTacTop.PerformLayout();

        scMain.Panel1.ResumeLayout(false);
        scMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)scMain).EndInit();
        scMain.ResumeLayout(false);

        ResumeLayout(false);
    }

    // Top bar
    private Panel pnlTop;
    private Label lblSearchLbl;
    private TextBox txtSearch;
    private Button btnTimKiem;
    private Label lblNhomLbl;
    private ComboBox cboNhom;
    private Button btnXemTatCa;
    private Label lblStatus;

    // List
    private SplitContainer scMain;
    private DataGridView dgvThuoc;
    private DataGridViewTextBoxColumn colTen;
    private DataGridViewTextBoxColumn colHoatChat;
    private DataGridViewTextBoxColumn colNhom;
    private DataGridViewTextBoxColumn colLieu;

    // Detail
    private Panel pnlDetail;
    private Panel pnlDetailTop;
    private Label lblTenThuoc;
    private Panel pnlFields;
    private Label lblHoatChat;
    private Label lblHoatChatVal;
    private Label lblNhom;
    private Label lblNhomVal;
    private Label lblLieuDung;
    private Label lblLieuDungVal;
    private Label lblCachDung;
    private Label lblCachDungVal;
    private GroupBox gbChongChiDinh;
    private TextBox txtChongChiDinh;
    private GroupBox gbTacDungPhu;
    private TextBox txtTacDungPhu;

    // Interaction checker
    private GroupBox gbTuongTac;
    private Panel pnlTuongTacTop;
    private Label lblTuongTacHint;
    private Button btnKiemTraTuongTac;
    private Label lblTuongTacStatus;
    private DataGridView dgvTuongTac;
    private DataGridViewTextBoxColumn colTTThuoc1;
    private DataGridViewTextBoxColumn colTTThuoc2;
    private DataGridViewTextBoxColumn colTTMucDo;
    private DataGridViewTextBoxColumn colTTMoTa;
    private DataGridViewTextBoxColumn colTTHauQua;
}
