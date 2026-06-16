namespace HeChuyenGiaThuocBenh.UI.Forms;

partial class AdminBenhForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        scMain = new SplitContainer();

        // Left
        pnlLeft = new Panel();
        pnlLeftTop = new Panel();
        txtSearchBenh = new TextBox();
        btnThemBenh = new Button();
        btnXoaBenh = new Button();
        lblStatus = new Label();
        dgvBenh = new DataGridView();
        colBTen = new DataGridViewTextBoxColumn();
        colBNhom = new DataGridViewTextBoxColumn();

        // Right
        pnlBenhDetail = new Panel();
        pnlDetailHeader = new Panel();
        lblDetailTitle = new Label();
        tabRight = new TabControl();
        tabInfo = new TabPage();
        tabRules = new TabPage();

        // Tab info fields
        pnlInfoFields = new Panel();
        lblTenLbl = new Label();    txtTen = new TextBox();
        lblNhomLbl = new Label();   txtNhomBenh = new TextBox();
        lblMoTaLbl = new Label();   txtMoTa = new TextBox();
        chkIsActive = new CheckBox();
        btnLuuBenh = new Button();

        // Tab rules
        pnlRulesTop = new Panel();
        btnThemLuat = new Button();
        btnXoaLuat = new Button();
        btnLuuLuat = new Button();
        lblRuleCount = new Label();
        lblRuleHint = new Label();
        dgvRules = new DataGridView();
        colRuleTrieuChung = new DataGridViewComboBoxColumn();
        colRuleTrongSo = new DataGridViewTextBoxColumn();
        colRuleBatBuoc = new DataGridViewCheckBoxColumn();

        ((System.ComponentModel.ISupportInitialize)scMain).BeginInit();
        scMain.Panel1.SuspendLayout();
        scMain.Panel2.SuspendLayout();
        scMain.SuspendLayout();
        pnlLeft.SuspendLayout();
        pnlLeftTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvBenh).BeginInit();
        pnlBenhDetail.SuspendLayout();
        pnlDetailHeader.SuspendLayout();
        tabRight.SuspendLayout();
        tabInfo.SuspendLayout();
        tabRules.SuspendLayout();
        pnlInfoFields.SuspendLayout();
        pnlRulesTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvRules).BeginInit();
        SuspendLayout();

        // ── scMain ─────────────────────────────────────────────────────────
        scMain.Dock = DockStyle.Fill;
        scMain.Orientation = Orientation.Vertical;
        scMain.SplitterDistance = 300;
        scMain.Panel1.Controls.Add(pnlLeft);
        scMain.Panel2.Controls.Add(pnlBenhDetail);

        // ── pnlLeft ────────────────────────────────────────────────────────
        pnlLeft.Dock = DockStyle.Fill;
        pnlLeft.BackColor = Color.White;
        pnlLeft.Controls.Add(dgvBenh);
        pnlLeft.Controls.Add(pnlLeftTop);

        pnlLeftTop.Dock = DockStyle.Top;
        pnlLeftTop.Height = 84;
        pnlLeftTop.BackColor = Color.FromArgb(248, 249, 252);
        pnlLeftTop.Padding = new Padding(6);

        txtSearchBenh.Font = new Font("Segoe UI", 9.5F);
        txtSearchBenh.Location = new Point(6, 8);
        txtSearchBenh.Size = new Size(282, 26);
        txtSearchBenh.PlaceholderText = "Tìm bệnh...";
        txtSearchBenh.KeyDown += new KeyEventHandler(txtSearchBenh_KeyDown);
        txtSearchBenh.TextChanged += new EventHandler(txtSearchBenh_TextChanged);

        btnThemBenh.BackColor = Color.FromArgb(0, 150, 80);
        btnThemBenh.FlatStyle = FlatStyle.Flat;
        btnThemBenh.FlatAppearance.BorderSize = 0;
        btnThemBenh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnThemBenh.ForeColor = Color.White;
        btnThemBenh.Location = new Point(6, 42);
        btnThemBenh.Size = new Size(130, 30);
        btnThemBenh.Text = "+ Thêm bệnh";
        btnThemBenh.Cursor = Cursors.Hand;
        btnThemBenh.Click += new EventHandler(btnThemBenh_Click);

        btnXoaBenh.BackColor = Color.FromArgb(196, 43, 28);
        btnXoaBenh.FlatStyle = FlatStyle.Flat;
        btnXoaBenh.FlatAppearance.BorderSize = 0;
        btnXoaBenh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnXoaBenh.ForeColor = Color.White;
        btnXoaBenh.Location = new Point(143, 42);
        btnXoaBenh.Size = new Size(90, 30);
        btnXoaBenh.Text = "🗑 Xóa";
        btnXoaBenh.Cursor = Cursors.Hand;
        btnXoaBenh.Click += new EventHandler(btnXoaBenh_Click);

        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
        lblStatus.ForeColor = Color.Gray;
        lblStatus.Location = new Point(8, 66);
        lblStatus.Text = string.Empty;

        pnlLeftTop.Controls.Add(txtSearchBenh);
        pnlLeftTop.Controls.Add(btnThemBenh);
        pnlLeftTop.Controls.Add(btnXoaBenh);
        pnlLeftTop.Controls.Add(lblStatus);

        // dgvBenh
        dgvBenh.Dock = DockStyle.Fill;
        dgvBenh.ReadOnly = true;
        dgvBenh.AllowUserToAddRows = false;
        dgvBenh.AllowUserToDeleteRows = false;
        dgvBenh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvBenh.MultiSelect = false;
        dgvBenh.RowHeadersVisible = false;
        dgvBenh.BackgroundColor = Color.White;
        dgvBenh.BorderStyle = BorderStyle.None;
        dgvBenh.Font = new Font("Segoe UI", 9.5F);
        dgvBenh.RowTemplate.Height = 30;
        dgvBenh.ColumnHeadersHeight = 30;
        dgvBenh.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvBenh.EnableHeadersVisualStyles = false;
        dgvBenh.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
        dgvBenh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvBenh.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 100);
        dgvBenh.SelectionChanged += new EventHandler(dgvBenh_SelectionChanged);

        colBTen.HeaderText = "Tên bệnh";
        colBTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colBTen.FillWeight = 60;

        colBNhom.HeaderText = "Nhóm bệnh";
        colBNhom.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colBNhom.FillWeight = 40;

        dgvBenh.Columns.AddRange(colBTen, colBNhom);

        // ── pnlBenhDetail ──────────────────────────────────────────────────
        pnlBenhDetail.Dock = DockStyle.Fill;
        pnlBenhDetail.Visible = false;
        pnlBenhDetail.BackColor = Color.White;
        pnlBenhDetail.Controls.Add(tabRight);
        pnlBenhDetail.Controls.Add(pnlDetailHeader);

        pnlDetailHeader.Dock = DockStyle.Top;
        pnlDetailHeader.Height = 40;
        pnlDetailHeader.BackColor = Color.FromArgb(240, 244, 255);
        pnlDetailHeader.Padding = new Padding(12, 8, 12, 6);
        pnlDetailHeader.Controls.Add(lblDetailTitle);

        lblDetailTitle.Dock = DockStyle.Fill;
        lblDetailTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblDetailTitle.ForeColor = Color.FromArgb(20, 50, 120);
        lblDetailTitle.Text = string.Empty;

        // ── tabRight ───────────────────────────────────────────────────────
        tabRight.Dock = DockStyle.Fill;
        tabRight.Font = new Font("Segoe UI", 9.5F);
        tabRight.TabPages.Add(tabInfo);
        tabRight.TabPages.Add(tabRules);

        tabInfo.Text = "Thông tin bệnh";
        tabInfo.BackColor = Color.White;
        tabInfo.Controls.Add(pnlInfoFields);

        tabRules.Text = "Tập luật (BenhTrieuChung)";
        tabRules.BackColor = Color.White;
        tabRules.Controls.Add(dgvRules);
        tabRules.Controls.Add(pnlRulesTop);

        // ── pnlInfoFields ──────────────────────────────────────────────────
        pnlInfoFields.Dock = DockStyle.Fill;
        pnlInfoFields.AutoScroll = true;
        pnlInfoFields.Padding = new Padding(16, 12, 16, 12);
        pnlInfoFields.BackColor = Color.White;

        int lx = 0, vx = 110, fy = 10, fw = 400, lw = 106, fgap = 32;

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
            pnlInfoFields.Controls.Add(lbl);
            pnlInfoFields.Controls.Add(ctrl);
        }

        Field(lblTenLbl, "Tên bệnh *:", txtTen, fy); fy += fgap;
        Field(lblNhomLbl, "Nhóm bệnh:", txtNhomBenh, fy); fy += fgap;

        txtMoTa.Multiline = true;
        txtMoTa.ScrollBars = ScrollBars.Vertical;
        Field(lblMoTaLbl, "Mô tả:", txtMoTa, fy, 80); fy += 90;

        chkIsActive.AutoSize = true;
        chkIsActive.Font = new Font("Segoe UI", 9.5F);
        chkIsActive.Location = new Point(vx, fy);
        chkIsActive.Text = "Đang hoạt động";
        chkIsActive.Checked = true;
        pnlInfoFields.Controls.Add(chkIsActive);
        fy += 34;

        btnLuuBenh.BackColor = Color.FromArgb(0, 120, 215);
        btnLuuBenh.FlatStyle = FlatStyle.Flat;
        btnLuuBenh.FlatAppearance.BorderSize = 0;
        btnLuuBenh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnLuuBenh.ForeColor = Color.White;
        btnLuuBenh.Location = new Point(vx, fy);
        btnLuuBenh.Size = new Size(120, 32);
        btnLuuBenh.Text = "💾 Lưu bệnh";
        btnLuuBenh.Cursor = Cursors.Hand;
        btnLuuBenh.Click += new EventHandler(btnLuuBenh_Click);
        pnlInfoFields.Controls.Add(btnLuuBenh);

        // ── pnlRulesTop ────────────────────────────────────────────────────
        pnlRulesTop.Dock = DockStyle.Top;
        pnlRulesTop.Height = 78;
        pnlRulesTop.BackColor = Color.FromArgb(248, 249, 252);
        pnlRulesTop.Padding = new Padding(8, 6, 8, 4);

        lblRuleHint.AutoSize = true;
        lblRuleHint.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
        lblRuleHint.ForeColor = Color.Gray;
        lblRuleHint.Location = new Point(8, 6);
        lblRuleHint.Text = "Chọn triệu chứng từ dropdown. TrongSo = trọng số (0.1–10). BatBuoc = luật bắt buộc.";

        btnThemLuat.BackColor = Color.FromArgb(0, 150, 80);
        btnThemLuat.FlatStyle = FlatStyle.Flat;
        btnThemLuat.FlatAppearance.BorderSize = 0;
        btnThemLuat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnThemLuat.ForeColor = Color.White;
        btnThemLuat.Location = new Point(8, 30);
        btnThemLuat.Size = new Size(110, 28);
        btnThemLuat.Text = "+ Thêm luật";
        btnThemLuat.Cursor = Cursors.Hand;
        btnThemLuat.Click += new EventHandler(btnThemLuat_Click);

        btnXoaLuat.BackColor = Color.FromArgb(196, 43, 28);
        btnXoaLuat.FlatStyle = FlatStyle.Flat;
        btnXoaLuat.FlatAppearance.BorderSize = 0;
        btnXoaLuat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnXoaLuat.ForeColor = Color.White;
        btnXoaLuat.Location = new Point(124, 30);
        btnXoaLuat.Size = new Size(90, 28);
        btnXoaLuat.Text = "Xóa luật";
        btnXoaLuat.Cursor = Cursors.Hand;
        btnXoaLuat.Click += new EventHandler(btnXoaLuat_Click);

        btnLuuLuat.BackColor = Color.FromArgb(0, 120, 215);
        btnLuuLuat.FlatStyle = FlatStyle.Flat;
        btnLuuLuat.FlatAppearance.BorderSize = 0;
        btnLuuLuat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLuuLuat.ForeColor = Color.White;
        btnLuuLuat.Location = new Point(220, 30);
        btnLuuLuat.Size = new Size(110, 28);
        btnLuuLuat.Text = "💾 Lưu tập luật";
        btnLuuLuat.Cursor = Cursors.Hand;
        btnLuuLuat.Click += new EventHandler(btnLuuLuat_Click);

        lblRuleCount.AutoSize = true;
        lblRuleCount.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblRuleCount.ForeColor = Color.Gray;
        lblRuleCount.Location = new Point(338, 36);
        lblRuleCount.Text = string.Empty;

        pnlRulesTop.Controls.Add(lblRuleHint);
        pnlRulesTop.Controls.Add(btnThemLuat);
        pnlRulesTop.Controls.Add(btnXoaLuat);
        pnlRulesTop.Controls.Add(btnLuuLuat);
        pnlRulesTop.Controls.Add(lblRuleCount);

        // ── dgvRules ───────────────────────────────────────────────────────
        dgvRules.Dock = DockStyle.Fill;
        dgvRules.AllowUserToAddRows = false;
        dgvRules.AllowUserToDeleteRows = false;
        dgvRules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvRules.MultiSelect = true;
        dgvRules.RowHeadersVisible = false;
        dgvRules.BackgroundColor = Color.White;
        dgvRules.BorderStyle = BorderStyle.None;
        dgvRules.Font = new Font("Segoe UI", 9.5F);
        dgvRules.RowTemplate.Height = 30;
        dgvRules.ColumnHeadersHeight = 30;
        dgvRules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvRules.EnableHeadersVisualStyles = false;
        dgvRules.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
        dgvRules.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvRules.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 100);
        dgvRules.EditMode = DataGridViewEditMode.EditOnEnter;

        colRuleTrieuChung.HeaderText = "Triệu chứng";
        colRuleTrieuChung.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colRuleTrieuChung.FillWeight = 60;
        colRuleTrieuChung.DisplayMember = "Ten";
        colRuleTrieuChung.ValueMember = "Id";

        colRuleTrongSo.HeaderText = "Trọng số";
        colRuleTrongSo.Width = 90;
        colRuleTrongSo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        colRuleBatBuoc.HeaderText = "Bắt buộc";
        colRuleBatBuoc.Width = 80;
        colRuleBatBuoc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        dgvRules.Columns.AddRange(colRuleTrieuChung, colRuleTrongSo, colRuleBatBuoc);

        // ── Form ───────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        Controls.Add(scMain);
        Name = "AdminBenhForm";
        Text = "Quản Lý Bệnh";

        ((System.ComponentModel.ISupportInitialize)dgvBenh).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvRules).EndInit();
        pnlLeft.ResumeLayout(false);
        pnlLeftTop.ResumeLayout(false);
        pnlLeftTop.PerformLayout();
        pnlBenhDetail.ResumeLayout(false);
        pnlDetailHeader.ResumeLayout(false);
        tabRight.ResumeLayout(false);
        tabInfo.ResumeLayout(false);
        tabRules.ResumeLayout(false);
        pnlInfoFields.ResumeLayout(false);
        pnlInfoFields.PerformLayout();
        pnlRulesTop.ResumeLayout(false);
        pnlRulesTop.PerformLayout();
        scMain.Panel1.ResumeLayout(false);
        scMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)scMain).EndInit();
        scMain.ResumeLayout(false);
        ResumeLayout(false);
    }

    // left
    private SplitContainer scMain;
    private Panel pnlLeft;
    private Panel pnlLeftTop;
    private TextBox txtSearchBenh;
    private Button btnThemBenh;
    private Button btnXoaBenh;
    private Label lblStatus;
    private DataGridView dgvBenh;
    private DataGridViewTextBoxColumn colBTen;
    private DataGridViewTextBoxColumn colBNhom;

    // right
    private Panel pnlBenhDetail;
    private Panel pnlDetailHeader;
    private Label lblDetailTitle;
    private TabControl tabRight;
    private TabPage tabInfo;
    private TabPage tabRules;

    // tab info
    private Panel pnlInfoFields;
    private Label lblTenLbl;   private TextBox txtTen;
    private Label lblNhomLbl;  private TextBox txtNhomBenh;
    private Label lblMoTaLbl;  private TextBox txtMoTa;
    private CheckBox chkIsActive;
    private Button btnLuuBenh;

    // tab rules
    private Panel pnlRulesTop;
    private Label lblRuleHint;
    private Button btnThemLuat;
    private Button btnXoaLuat;
    private Button btnLuuLuat;
    private Label lblRuleCount;
    private DataGridView dgvRules;
    private DataGridViewComboBoxColumn colRuleTrieuChung;
    private DataGridViewTextBoxColumn colRuleTrongSo;
    private DataGridViewCheckBoxColumn colRuleBatBuoc;
}
