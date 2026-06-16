namespace HeChuyenGiaThuocBenh.UI.Forms;

partial class ChanDoanForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        // ── Controls ──────────────────────────────────────────────────────────
        scOuter         = new SplitContainer();
        scRight         = new SplitContainer();

        // Left panel
        pnlLeft         = new Panel();
        lblLeftTitle    = new Label();
        pnlSearch       = new Panel();
        txtSearch       = new TextBox();
        clbTrieuChung   = new CheckedListBox();
        pnlLeftBottom   = new Panel();
        lblSelectedCount= new Label();
        pnlButtons      = new Panel();
        btnChanDoan     = new Button();
        btnXoa          = new Button();
        lblStatus       = new Label();

        // Right top — results
        pnlResultsTop   = new Panel();
        lblKetQuaTitle  = new Label();
        dgvKetQua       = new DataGridView();
        colBenhTen      = new DataGridViewTextBoxColumn();
        colNhomBenh     = new DataGridViewTextBoxColumn();
        colDoTinCay     = new DataGridViewTextBoxColumn();
        colSoThuoc      = new DataGridViewTextBoxColumn();
        colCanhBao      = new DataGridViewTextBoxColumn();

        // Right bottom — detail
        pnlDetail           = new Panel();
        pnlDetailHeader     = new Panel();
        lblBenhTen          = new Label();
        lblDoTinCay         = new Label();
        lblNhomBenh         = new Label();
        gbMoTa              = new GroupBox();
        txtMoTaBenh         = new TextBox();
        gbThuoc             = new GroupBox();
        dgvThuoc            = new DataGridView();
        colThuocTen         = new DataGridViewTextBoxColumn();
        colHoatChat         = new DataGridViewTextBoxColumn();
        colLieuDung         = new DataGridViewTextBoxColumn();
        colNhomThuoc        = new DataGridViewTextBoxColumn();
        gbCanhBao           = new GroupBox();
        pnlCanhBaoHeader    = new Panel();
        lblCanhBaoTitle     = new Label();
        dgvCanhBao          = new DataGridView();
        colCBThuoc1         = new DataGridViewTextBoxColumn();
        colCBThuoc2         = new DataGridViewTextBoxColumn();
        colCBMucDo          = new DataGridViewTextBoxColumn();
        colCBMoTa           = new DataGridViewTextBoxColumn();

        ((System.ComponentModel.ISupportInitialize)scOuter).BeginInit();
        scOuter.Panel1.SuspendLayout();
        scOuter.Panel2.SuspendLayout();
        scOuter.SuspendLayout();

        ((System.ComponentModel.ISupportInitialize)scRight).BeginInit();
        scRight.Panel1.SuspendLayout();
        scRight.Panel2.SuspendLayout();
        scRight.SuspendLayout();

        ((System.ComponentModel.ISupportInitialize)dgvKetQua).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvThuoc).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvCanhBao).BeginInit();

        pnlLeft.SuspendLayout();
        pnlSearch.SuspendLayout();
        pnlLeftBottom.SuspendLayout();
        pnlButtons.SuspendLayout();
        pnlResultsTop.SuspendLayout();
        pnlDetail.SuspendLayout();
        pnlDetailHeader.SuspendLayout();
        gbMoTa.SuspendLayout();
        gbThuoc.SuspendLayout();
        gbCanhBao.SuspendLayout();
        pnlCanhBaoHeader.SuspendLayout();

        SuspendLayout();

        // ── scOuter (vertical split: left=symptoms, right=content) ───────────
        scOuter.Dock = DockStyle.Fill;
        scOuter.FixedPanel = FixedPanel.Panel1;
        scOuter.Orientation = Orientation.Vertical;
        scOuter.SplitterDistance = 248;
        scOuter.Panel1.Controls.Add(pnlLeft);
        scOuter.Panel2.Controls.Add(scRight);

        // ── pnlLeft ──────────────────────────────────────────────────────────
        pnlLeft.Dock = DockStyle.Fill;
        pnlLeft.BackColor = Color.FromArgb(248, 249, 252);

        // Add to pnlLeft in correct dock order:
        // Bottom items first (status → buttons → selected count), then Fill, then Top items
        pnlLeft.Controls.Add(lblStatus);          // Bottom (very bottom)
        pnlLeft.Controls.Add(pnlButtons);         // Bottom (above status)
        pnlLeft.Controls.Add(lblSelectedCount);   // Bottom (above buttons)
        pnlLeft.Controls.Add(clbTrieuChung);      // Fill
        pnlLeft.Controls.Add(pnlSearch);          // Top (below title)
        pnlLeft.Controls.Add(lblLeftTitle);       // Top (very top)

        lblLeftTitle.Dock = DockStyle.Top;
        lblLeftTitle.Height = 36;
        lblLeftTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLeftTitle.ForeColor = Color.FromArgb(40, 44, 52);
        lblLeftTitle.Padding = new Padding(10, 8, 0, 0);
        lblLeftTitle.Text = "CHỌN TRIỆU CHỨNG";

        pnlSearch.Dock = DockStyle.Top;
        pnlSearch.Height = 38;
        pnlSearch.Padding = new Padding(8, 4, 8, 2);
        pnlSearch.Controls.Add(txtSearch);

        txtSearch.Dock = DockStyle.Fill;
        txtSearch.Font = new Font("Segoe UI", 9.5F);
        txtSearch.PlaceholderText = "🔍  Tìm triệu chứng...";
        txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);

        clbTrieuChung.Dock = DockStyle.Fill;
        clbTrieuChung.Font = new Font("Segoe UI", 9F);
        clbTrieuChung.BorderStyle = BorderStyle.None;
        clbTrieuChung.ItemHeight = 22;
        clbTrieuChung.CheckOnClick = true;
        clbTrieuChung.ItemCheck += new ItemCheckEventHandler(clbTrieuChung_ItemCheck);

        lblSelectedCount.Dock = DockStyle.Bottom;
        lblSelectedCount.Height = 24;
        lblSelectedCount.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblSelectedCount.ForeColor = Color.Gray;
        lblSelectedCount.Padding = new Padding(10, 4, 0, 0);
        lblSelectedCount.Text = "Đã chọn: 0 triệu chứng";

        pnlButtons.Dock = DockStyle.Bottom;
        pnlButtons.Height = 46;
        pnlButtons.Padding = new Padding(8, 4, 8, 6);
        pnlButtons.Controls.Add(btnXoa);
        pnlButtons.Controls.Add(btnChanDoan);

        btnChanDoan.Dock = DockStyle.Fill;
        btnChanDoan.BackColor = Color.FromArgb(0, 120, 215);
        btnChanDoan.FlatStyle = FlatStyle.Flat;
        btnChanDoan.FlatAppearance.BorderSize = 0;
        btnChanDoan.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnChanDoan.ForeColor = Color.White;
        btnChanDoan.Text = "🔬  Chẩn đoán";
        btnChanDoan.Cursor = Cursors.Hand;
        btnChanDoan.Click += new EventHandler(btnChanDoan_Click);

        btnXoa.Dock = DockStyle.Right;
        btnXoa.Width = 72;
        btnXoa.BackColor = Color.FromArgb(220, 220, 228);
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.FlatAppearance.BorderSize = 0;
        btnXoa.Font = new Font("Segoe UI", 9F);
        btnXoa.ForeColor = Color.FromArgb(60, 60, 70);
        btnXoa.Text = "Xóa";
        btnXoa.Cursor = Cursors.Hand;
        btnXoa.Click += new EventHandler(btnXoa_Click);

        lblStatus.Dock = DockStyle.Bottom;
        lblStatus.Height = 22;
        lblStatus.Font = new Font("Segoe UI", 8F);
        lblStatus.ForeColor = Color.FromArgb(100, 100, 110);
        lblStatus.Padding = new Padding(10, 2, 0, 0);
        lblStatus.Text = string.Empty;

        // ── scRight (horizontal split: top=results, bottom=detail) ───────────
        scRight.Dock = DockStyle.Fill;
        scRight.Orientation = Orientation.Horizontal;
        scRight.SplitterDistance = 220;
        scRight.Panel1.Controls.Add(pnlResultsTop);
        scRight.Panel2.Controls.Add(pnlDetail);

        // ── pnlResultsTop ────────────────────────────────────────────────────
        pnlResultsTop.Dock = DockStyle.Fill;
        pnlResultsTop.Controls.Add(dgvKetQua);
        pnlResultsTop.Controls.Add(lblKetQuaTitle);

        lblKetQuaTitle.Dock = DockStyle.Top;
        lblKetQuaTitle.Height = 32;
        lblKetQuaTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblKetQuaTitle.ForeColor = Color.FromArgb(40, 44, 52);
        lblKetQuaTitle.Padding = new Padding(10, 8, 0, 0);
        lblKetQuaTitle.BackColor = Color.FromArgb(240, 242, 248);
        lblKetQuaTitle.Text = "KẾT QUẢ CHẨN ĐOÁN";

        // dgvKetQua
        dgvKetQua.Dock = DockStyle.Fill;
        dgvKetQua.ReadOnly = true;
        dgvKetQua.AllowUserToAddRows = false;
        dgvKetQua.AllowUserToDeleteRows = false;
        dgvKetQua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvKetQua.MultiSelect = false;
        dgvKetQua.RowHeadersVisible = false;
        dgvKetQua.BackgroundColor = Color.White;
        dgvKetQua.BorderStyle = BorderStyle.None;
        dgvKetQua.GridColor = Color.FromArgb(230, 230, 235);
        dgvKetQua.Font = new Font("Segoe UI", 9.5F);
        dgvKetQua.RowTemplate.Height = 30;
        dgvKetQua.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        dgvKetQua.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvKetQua.ColumnHeadersHeight = 28;
        dgvKetQua.EnableHeadersVisualStyles = false;
        dgvKetQua.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
        dgvKetQua.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvKetQua.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 100);
        dgvKetQua.SelectionChanged += new EventHandler(dgvKetQua_SelectionChanged);

        colBenhTen.HeaderText = "Tên bệnh";
        colBenhTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colBenhTen.FillWeight = 40;

        colNhomBenh.HeaderText = "Nhóm bệnh";
        colNhomBenh.Width = 130;

        colDoTinCay.HeaderText = "Độ tin cậy";
        colDoTinCay.Width = 90;

        colSoThuoc.HeaderText = "Số thuốc";
        colSoThuoc.Width = 75;

        colCanhBao.HeaderText = "Cảnh báo";
        colCanhBao.Width = 80;

        dgvKetQua.Columns.AddRange(colBenhTen, colNhomBenh, colDoTinCay, colSoThuoc, colCanhBao);

        // ── pnlDetail ────────────────────────────────────────────────────────
        pnlDetail.Dock = DockStyle.Fill;
        pnlDetail.Visible = false;

        // Add in correct dock order: bottom first, then fill, then top
        pnlDetail.Controls.Add(gbCanhBao);       // Bottom
        pnlDetail.Controls.Add(gbThuoc);         // Fill
        pnlDetail.Controls.Add(gbMoTa);          // Top
        pnlDetail.Controls.Add(pnlDetailHeader); // Top (very top)

        // pnlDetailHeader
        pnlDetailHeader.Dock = DockStyle.Top;
        pnlDetailHeader.Height = 78;
        pnlDetailHeader.BackColor = Color.FromArgb(240, 244, 255);
        pnlDetailHeader.Padding = new Padding(12, 8, 12, 6);
        pnlDetailHeader.Controls.Add(lblNhomBenh);
        pnlDetailHeader.Controls.Add(lblDoTinCay);
        pnlDetailHeader.Controls.Add(lblBenhTen);

        lblBenhTen.Dock = DockStyle.Top;
        lblBenhTen.Height = 30;
        lblBenhTen.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblBenhTen.ForeColor = Color.FromArgb(20, 50, 120);
        lblBenhTen.Text = string.Empty;

        lblDoTinCay.Dock = DockStyle.Top;
        lblDoTinCay.Height = 20;
        lblDoTinCay.Font = new Font("Segoe UI", 9F);
        lblDoTinCay.ForeColor = Color.FromArgb(60, 120, 60);
        lblDoTinCay.Text = string.Empty;

        lblNhomBenh.Dock = DockStyle.Top;
        lblNhomBenh.Height = 18;
        lblNhomBenh.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblNhomBenh.ForeColor = Color.FromArgb(100, 100, 120);
        lblNhomBenh.Text = string.Empty;

        // gbMoTa
        gbMoTa.Dock = DockStyle.Top;
        gbMoTa.Height = 88;
        gbMoTa.Text = "Mô tả bệnh";
        gbMoTa.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        gbMoTa.ForeColor = Color.FromArgb(80, 80, 100);
        gbMoTa.Padding = new Padding(4, 2, 4, 4);
        gbMoTa.Controls.Add(txtMoTaBenh);

        txtMoTaBenh.Dock = DockStyle.Fill;
        txtMoTaBenh.Multiline = true;
        txtMoTaBenh.ReadOnly = true;
        txtMoTaBenh.ScrollBars = ScrollBars.Vertical;
        txtMoTaBenh.BorderStyle = BorderStyle.None;
        txtMoTaBenh.Font = new Font("Segoe UI", 9F);
        txtMoTaBenh.BackColor = Color.White;

        // gbThuoc
        gbThuoc.Dock = DockStyle.Fill;
        gbThuoc.Text = "Thuốc gợi ý điều trị";
        gbThuoc.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        gbThuoc.ForeColor = Color.FromArgb(0, 100, 0);
        gbThuoc.Padding = new Padding(4, 2, 4, 4);
        gbThuoc.Controls.Add(dgvThuoc);

        dgvThuoc.Dock = DockStyle.Fill;
        dgvThuoc.ReadOnly = true;
        dgvThuoc.AllowUserToAddRows = false;
        dgvThuoc.AllowUserToDeleteRows = false;
        dgvThuoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvThuoc.RowHeadersVisible = false;
        dgvThuoc.BackgroundColor = Color.White;
        dgvThuoc.BorderStyle = BorderStyle.None;
        dgvThuoc.Font = new Font("Segoe UI", 9F);
        dgvThuoc.RowTemplate.Height = 26;
        dgvThuoc.ColumnHeadersHeight = 26;
        dgvThuoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvThuoc.EnableHeadersVisualStyles = false;
        dgvThuoc.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 252, 245);
        dgvThuoc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        dgvThuoc.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(0, 100, 0);
        dgvThuoc.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

        colThuocTen.HeaderText = "Tên thuốc";
        colThuocTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colThuocTen.FillWeight = 35;

        colHoatChat.HeaderText = "Hoạt chất";
        colHoatChat.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colHoatChat.FillWeight = 30;

        colLieuDung.HeaderText = "Liều dùng";
        colLieuDung.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colLieuDung.FillWeight = 25;

        colNhomThuoc.HeaderText = "Nhóm";
        colNhomThuoc.Width = 100;

        dgvThuoc.Columns.AddRange(colThuocTen, colHoatChat, colLieuDung, colNhomThuoc);

        // gbCanhBao
        gbCanhBao.Dock = DockStyle.Bottom;
        gbCanhBao.Height = 155;
        gbCanhBao.Text = "Cảnh báo tương tác thuốc";
        gbCanhBao.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        gbCanhBao.ForeColor = Color.FromArgb(150, 50, 0);
        gbCanhBao.Padding = new Padding(4, 2, 4, 4);
        gbCanhBao.Controls.Add(dgvCanhBao);
        gbCanhBao.Controls.Add(pnlCanhBaoHeader);

        pnlCanhBaoHeader.Dock = DockStyle.Top;
        pnlCanhBaoHeader.Height = 28;
        pnlCanhBaoHeader.BackColor = Color.FromArgb(220, 255, 220);
        pnlCanhBaoHeader.Controls.Add(lblCanhBaoTitle);

        lblCanhBaoTitle.Dock = DockStyle.Fill;
        lblCanhBaoTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblCanhBaoTitle.ForeColor = Color.FromArgb(0, 120, 0);
        lblCanhBaoTitle.Padding = new Padding(8, 4, 0, 0);
        lblCanhBaoTitle.Text = "✓  Không có cảnh báo";

        dgvCanhBao.Dock = DockStyle.Fill;
        dgvCanhBao.ReadOnly = true;
        dgvCanhBao.AllowUserToAddRows = false;
        dgvCanhBao.AllowUserToDeleteRows = false;
        dgvCanhBao.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCanhBao.RowHeadersVisible = false;
        dgvCanhBao.BackgroundColor = Color.FromArgb(255, 248, 248);
        dgvCanhBao.BorderStyle = BorderStyle.None;
        dgvCanhBao.Font = new Font("Segoe UI", 9F);
        dgvCanhBao.RowTemplate.Height = 24;
        dgvCanhBao.ColumnHeadersHeight = 24;
        dgvCanhBao.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvCanhBao.EnableHeadersVisualStyles = false;
        dgvCanhBao.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 240, 240);
        dgvCanhBao.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        dgvCanhBao.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkRed;
        dgvCanhBao.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        dgvCanhBao.Visible = false;

        colCBThuoc1.HeaderText = "Thuốc 1";
        colCBThuoc1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colCBThuoc1.FillWeight = 25;

        colCBThuoc2.HeaderText = "Thuốc 2";
        colCBThuoc2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colCBThuoc2.FillWeight = 25;

        colCBMucDo.HeaderText = "Mức độ";
        colCBMucDo.Width = 110;

        colCBMoTa.HeaderText = "Mô tả tương tác";
        colCBMoTa.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colCBMoTa.FillWeight = 50;

        dgvCanhBao.Columns.AddRange(colCBThuoc1, colCBThuoc2, colCBMucDo, colCBMoTa);

        // ── Form ──────────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        Controls.Add(scOuter);
        Name = "ChanDoanForm";
        Text = "Chẩn Đoán";

        ((System.ComponentModel.ISupportInitialize)dgvKetQua).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvThuoc).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvCanhBao).EndInit();

        pnlLeft.ResumeLayout(false);
        pnlSearch.ResumeLayout(false);
        pnlSearch.PerformLayout();
        pnlLeftBottom.ResumeLayout(false);
        pnlButtons.ResumeLayout(false);
        pnlResultsTop.ResumeLayout(false);
        pnlDetail.ResumeLayout(false);
        pnlDetailHeader.ResumeLayout(false);
        gbMoTa.ResumeLayout(false);
        gbThuoc.ResumeLayout(false);
        gbCanhBao.ResumeLayout(false);
        pnlCanhBaoHeader.ResumeLayout(false);

        scOuter.Panel1.ResumeLayout(false);
        scOuter.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)scOuter).EndInit();
        scOuter.ResumeLayout(false);

        scRight.Panel1.ResumeLayout(false);
        scRight.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)scRight).EndInit();
        scRight.ResumeLayout(false);

        ResumeLayout(false);
    }

    // Left panel
    private SplitContainer scOuter;
    private SplitContainer scRight;
    private Panel pnlLeft;
    private Label lblLeftTitle;
    private Panel pnlSearch;
    private TextBox txtSearch;
    private CheckedListBox clbTrieuChung;
    private Panel pnlLeftBottom;
    private Label lblSelectedCount;
    private Panel pnlButtons;
    private Button btnChanDoan;
    private Button btnXoa;
    private Label lblStatus;

    // Results area
    private Panel pnlResultsTop;
    private Label lblKetQuaTitle;
    private DataGridView dgvKetQua;
    private DataGridViewTextBoxColumn colBenhTen;
    private DataGridViewTextBoxColumn colNhomBenh;
    private DataGridViewTextBoxColumn colDoTinCay;
    private DataGridViewTextBoxColumn colSoThuoc;
    private DataGridViewTextBoxColumn colCanhBao;

    // Detail area
    private Panel pnlDetail;
    private Panel pnlDetailHeader;
    private Label lblBenhTen;
    private Label lblDoTinCay;
    private Label lblNhomBenh;
    private GroupBox gbMoTa;
    private TextBox txtMoTaBenh;
    private GroupBox gbThuoc;
    private DataGridView dgvThuoc;
    private DataGridViewTextBoxColumn colThuocTen;
    private DataGridViewTextBoxColumn colHoatChat;
    private DataGridViewTextBoxColumn colLieuDung;
    private DataGridViewTextBoxColumn colNhomThuoc;
    private GroupBox gbCanhBao;
    private Panel pnlCanhBaoHeader;
    private Label lblCanhBaoTitle;
    private DataGridView dgvCanhBao;
    private DataGridViewTextBoxColumn colCBThuoc1;
    private DataGridViewTextBoxColumn colCBThuoc2;
    private DataGridViewTextBoxColumn colCBMucDo;
    private DataGridViewTextBoxColumn colCBMoTa;
}
