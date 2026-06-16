using HeChuyenGiaThuocBenh.BLL;
using HeChuyenGiaThuocBenh.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp;

namespace HeChuyenGiaThuocBenh.UI.Forms;

public partial class BaoCaoForm : Form
{
    private List<LichSuChanDoan> _currentData = new();

    public BaoCaoForm()
    {
        InitializeComponent();
        dtpFrom.Value = DateTime.Today.AddDays(-30);
        dtpTo.Value = DateTime.Today;
    }

    private async void btnTimKiem_Click(object sender, EventArgs e)
    {
        if (dtpFrom.Value > dtpTo.Value)
        {
            MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        btnTimKiem.Enabled = false;
        btnXuatPDF.Enabled = false;
        try
        {
            var data = await ServiceContainer.BaoCaoService.GetThongKeAsync(dtpFrom.Value, dtpTo.Value);
            _currentData = data.ToList();
            LoadChart();
            LoadGrid();
            lblTongSo.Text = $"Tổng số chẩn đoán: {_currentData.Count}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnTimKiem.Enabled = true;
            btnXuatPDF.Enabled = _currentData.Count > 0;
        }
    }

    private void LoadChart()
    {
        var byDay = _currentData
            .GroupBy(r => r.NgayChanDoan.Date)
            .OrderBy(g => g.Key)
            .Select(g => (Date: g.Key, Count: g.Count()))
            .ToList();

        if (byDay.Count == 0)
        {
            chart.Series = Array.Empty<ISeries>();
            chart.XAxes = new Axis[] { new Axis { Labels = Array.Empty<string>() } };
            return;
        }

        chart.Series = new ISeries[]
        {
            new ColumnSeries<int>
            {
                Values = byDay.Select(d => d.Count).ToArray(),
                Name = "Số chẩn đoán",
                Fill = new SolidColorPaint(new SKColor(0, 120, 215, 200)),
                MaxBarWidth = 40,
            }
        };

        chart.XAxes = new Axis[]
        {
            new Axis
            {
                Labels = byDay.Select(d => d.Date.ToString("dd/MM")).ToArray(),
                LabelsRotation = 45,
                Name = "Ngày",
                TextSize = 11,
            }
        };

        chart.YAxes = new Axis[]
        {
            new Axis
            {
                Name = "Số lượng",
                MinLimit = 0,
                TextSize = 11,
            }
        };
    }

    private void LoadGrid()
    {
        dgvRecords.DataSource = null;
        dgvRecords.DataSource = _currentData.Select(r => new
        {
            r.Id,
            NgayChanDoan = r.NgayChanDoan.ToString("dd/MM/yyyy HH:mm"),
            BenhNhanId = r.BenhNhanId,
            KetQuaBenh = r.KetQuaBenh,
            ThuocGoiY = r.ThuocGoiY ?? "",
        }).ToList();

        if (dgvRecords.Columns.Count > 0)
        {
            dgvRecords.Columns["Id"].HeaderText = "ID";
            dgvRecords.Columns["NgayChanDoan"].HeaderText = "Ngày chẩn đoán";
            dgvRecords.Columns["BenhNhanId"].HeaderText = "Bệnh nhân #";
            dgvRecords.Columns["KetQuaBenh"].HeaderText = "Kết quả";
            dgvRecords.Columns["ThuocGoiY"].HeaderText = "Thuốc gợi ý";
            dgvRecords.Columns["Id"].Width = 50;
            dgvRecords.Columns["NgayChanDoan"].Width = 140;
            dgvRecords.Columns["BenhNhanId"].Width = 90;
            dgvRecords.Columns["KetQuaBenh"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRecords.Columns["ThuocGoiY"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }

    private async void btnXuatPDF_Click(object sender, EventArgs e)
    {
        if (_currentData.Count == 0) return;

        using var dlg = new SaveFileDialog
        {
            Title = "Lưu báo cáo PDF",
            Filter = "PDF files (*.pdf)|*.pdf",
            FileName = $"BaoCao_{dtpFrom.Value:yyyyMMdd}_{dtpTo.Value:yyyyMMdd}.pdf",
        };

        if (dlg.ShowDialog() != DialogResult.OK) return;

        btnXuatPDF.Enabled = false;
        try
        {
            await ServiceContainer.BaoCaoService.XuatPDFAsync(
                dtpFrom.Value, dtpTo.Value, _currentData, dlg.FileName);

            var result = MessageBox.Show(
                $"Đã xuất báo cáo thành công!\n{dlg.FileName}\n\nMở file ngay?",
                "Thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = dlg.FileName,
                    UseShellExecute = true
                });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi xuất PDF: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnXuatPDF.Enabled = true;
        }
    }
}
