using LiveChartsCore.SkiaSharpView.WinForms;

namespace HeChuyenGiaThuocBenh.UI.Forms;

partial class BaoCaoForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.pnlTop = new Panel();
        this.lblFrom = new Label();
        this.dtpFrom = new DateTimePicker();
        this.lblTo = new Label();
        this.dtpTo = new DateTimePicker();
        this.btnTimKiem = new Button();
        this.btnXuatPDF = new Button();
        this.lblTongSo = new Label();
        this.splitMain = new SplitContainer();
        this.chart = new CartesianChart();
        this.dgvRecords = new DataGridView();

        this.pnlTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.splitMain).BeginInit();
        this.splitMain.Panel1.SuspendLayout();
        this.splitMain.Panel2.SuspendLayout();
        this.splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.dgvRecords).BeginInit();
        this.SuspendLayout();

        // pnlTop
        this.pnlTop.BackColor = Color.White;
        this.pnlTop.Controls.Add(this.lblFrom);
        this.pnlTop.Controls.Add(this.dtpFrom);
        this.pnlTop.Controls.Add(this.lblTo);
        this.pnlTop.Controls.Add(this.dtpTo);
        this.pnlTop.Controls.Add(this.btnTimKiem);
        this.pnlTop.Controls.Add(this.btnXuatPDF);
        this.pnlTop.Controls.Add(this.lblTongSo);
        this.pnlTop.Dock = DockStyle.Top;
        this.pnlTop.Height = 60;
        this.pnlTop.Padding = new Padding(10, 10, 10, 5);

        // lblFrom
        this.lblFrom.AutoSize = true;
        this.lblFrom.Font = new Font("Segoe UI", 9F);
        this.lblFrom.Location = new Point(10, 22);
        this.lblFrom.Text = "Từ ngày:";

        // dtpFrom
        this.dtpFrom.Format = DateTimePickerFormat.Short;
        this.dtpFrom.Location = new Point(75, 18);
        this.dtpFrom.Size = new Size(110, 25);

        // lblTo
        this.lblTo.AutoSize = true;
        this.lblTo.Font = new Font("Segoe UI", 9F);
        this.lblTo.Location = new Point(200, 22);
        this.lblTo.Text = "Đến ngày:";

        // dtpTo
        this.dtpTo.Format = DateTimePickerFormat.Short;
        this.dtpTo.Location = new Point(270, 18);
        this.dtpTo.Size = new Size(110, 25);

        // btnTimKiem
        this.btnTimKiem.BackColor = Color.FromArgb(0, 120, 215);
        this.btnTimKiem.FlatStyle = FlatStyle.Flat;
        this.btnTimKiem.FlatAppearance.BorderSize = 0;
        this.btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.btnTimKiem.ForeColor = Color.White;
        this.btnTimKiem.Location = new Point(395, 14);
        this.btnTimKiem.Size = new Size(100, 32);
        this.btnTimKiem.Text = "🔍 Tìm kiếm";
        this.btnTimKiem.Cursor = Cursors.Hand;
        this.btnTimKiem.Click += new EventHandler(this.btnTimKiem_Click);

        // btnXuatPDF
        this.btnXuatPDF.BackColor = Color.FromArgb(232, 17, 35);
        this.btnXuatPDF.FlatStyle = FlatStyle.Flat;
        this.btnXuatPDF.FlatAppearance.BorderSize = 0;
        this.btnXuatPDF.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.btnXuatPDF.ForeColor = Color.White;
        this.btnXuatPDF.Location = new Point(510, 14);
        this.btnXuatPDF.Size = new Size(110, 32);
        this.btnXuatPDF.Text = "📄 Xuất PDF";
        this.btnXuatPDF.Cursor = Cursors.Hand;
        this.btnXuatPDF.Enabled = false;
        this.btnXuatPDF.Click += new EventHandler(this.btnXuatPDF_Click);

        // lblTongSo
        this.lblTongSo.AutoSize = true;
        this.lblTongSo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.lblTongSo.ForeColor = Color.FromArgb(0, 120, 215);
        this.lblTongSo.Location = new Point(640, 22);
        this.lblTongSo.Text = "Tổng số chẩn đoán: —";

        // splitMain
        this.splitMain.Dock = DockStyle.Fill;
        this.splitMain.Orientation = Orientation.Horizontal;
        this.splitMain.SplitterDistance = 280;
        this.splitMain.Panel1.Controls.Add(this.chart);
        this.splitMain.Panel2.Controls.Add(this.dgvRecords);

        // chart
        this.chart.Dock = DockStyle.Fill;
        this.chart.BackColor = Color.White;
        this.chart.Series = Array.Empty<LiveChartsCore.ISeries>();

        // dgvRecords
        this.dgvRecords.AllowUserToAddRows = false;
        this.dgvRecords.AllowUserToDeleteRows = false;
        this.dgvRecords.BackgroundColor = Color.White;
        this.dgvRecords.BorderStyle = BorderStyle.None;
        this.dgvRecords.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvRecords.Dock = DockStyle.Fill;
        this.dgvRecords.Font = new Font("Segoe UI", 9F);
        this.dgvRecords.GridColor = Color.FromArgb(220, 220, 220);
        this.dgvRecords.ReadOnly = true;
        this.dgvRecords.RowHeadersVisible = false;
        this.dgvRecords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvRecords.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
        this.dgvRecords.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        this.dgvRecords.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        this.dgvRecords.EnableHeadersVisualStyles = false;
        this.dgvRecords.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

        // BaoCaoForm
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.White;
        this.Controls.Add(this.splitMain);
        this.Controls.Add(this.pnlTop);
        this.Name = "BaoCaoForm";
        this.Text = "Báo cáo & Thống kê";

        this.pnlTop.ResumeLayout(false);
        this.pnlTop.PerformLayout();
        this.splitMain.Panel1.ResumeLayout(false);
        this.splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.splitMain).EndInit();
        this.splitMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.dgvRecords).EndInit();
        this.ResumeLayout(false);
    }

    private Panel pnlTop;
    private Label lblFrom;
    private DateTimePicker dtpFrom;
    private Label lblTo;
    private DateTimePicker dtpTo;
    private Button btnTimKiem;
    private Button btnXuatPDF;
    private Label lblTongSo;
    private SplitContainer splitMain;
    private CartesianChart chart;
    private DataGridView dgvRecords;
}
