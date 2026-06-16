using HeChuyenGiaThuocBenh.BLL;
using HeChuyenGiaThuocBenh.BLL.Services;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.UI.Forms;

public partial class ChanDoanForm : Form
{
    private readonly IInferenceService _inferenceService;
    private List<TrieuChung> _allTrieuChung = new();
    private List<TrieuChung> _displayedTrieuChung = new();
    private List<ChanDoanResult> _ketQua = new();

    public ChanDoanForm()
    {
        InitializeComponent();
        _inferenceService = ServiceContainer.InferenceService;
        _ = LoadTrieuChungAsync();
    }

    private async Task LoadTrieuChungAsync()
    {
        try
        {
            lblStatus.Text = "Đang tải danh sách triệu chứng...";
            _allTrieuChung = (await ServiceContainer.TrieuChungRepository.GetAllAsync()).ToList();
            FilterAndDisplay(string.Empty);
            lblStatus.Text = $"Tải {_allTrieuChung.Count} triệu chứng thành công.";
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Lỗi tải dữ liệu: {ex.Message}";
        }
    }

    private void FilterAndDisplay(string keyword)
    {
        var checkedIds = GetCheckedIds();

        _displayedTrieuChung = (string.IsNullOrWhiteSpace(keyword)
            ? _allTrieuChung
            : _allTrieuChung.Where(t =>
                t.Ten.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                (t.NhomTrieuChung ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase)))
            .OrderBy(t => t.NhomTrieuChung)
            .ThenBy(t => t.Ten)
            .ToList();

        clbTrieuChung.Items.Clear();
        for (int i = 0; i < _displayedTrieuChung.Count; i++)
        {
            var tc = _displayedTrieuChung[i];
            clbTrieuChung.Items.Add($"[{tc.NhomTrieuChung ?? "Khác"}]  {tc.Ten}");
            clbTrieuChung.SetItemChecked(i, checkedIds.Contains(tc.Id));
        }

        UpdateSelectedCount();
    }

    private HashSet<int> GetCheckedIds()
    {
        var ids = new HashSet<int>();
        for (int i = 0; i < _displayedTrieuChung.Count; i++)
        {
            if (i < clbTrieuChung.Items.Count && clbTrieuChung.GetItemChecked(i))
                ids.Add(_displayedTrieuChung[i].Id);
        }
        return ids;
    }

    private void UpdateSelectedCount()
    {
        int count = 0;
        for (int i = 0; i < clbTrieuChung.Items.Count; i++)
            if (clbTrieuChung.GetItemChecked(i)) count++;
        lblSelectedCount.Text = $"Đã chọn: {count} triệu chứng";
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
        => FilterAndDisplay(txtSearch.Text);

    private void clbTrieuChung_ItemCheck(object sender, ItemCheckEventArgs e)
        => BeginInvoke(UpdateSelectedCount);

    private void btnXoa_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        for (int i = 0; i < clbTrieuChung.Items.Count; i++)
            clbTrieuChung.SetItemChecked(i, false);
        FilterAndDisplay(string.Empty);
        ClearResults();
        lblStatus.Text = string.Empty;
    }

    private async void btnChanDoan_Click(object sender, EventArgs e)
    {
        var selectedIds = GetCheckedIds();
        if (selectedIds.Count == 0)
        {
            lblStatus.Text = "Vui lòng chọn ít nhất một triệu chứng.";
            return;
        }

        btnChanDoan.Enabled = false;
        lblStatus.Text = "Đang suy luận...";
        ClearResults();

        try
        {
            _ketQua = await _inferenceService.ChanDoanAsync(selectedIds);

            if (_ketQua.Count == 0)
            {
                lblStatus.Text = "Không tìm được bệnh phù hợp. Thử chọn thêm triệu chứng.";
                return;
            }

            foreach (var r in _ketQua)
            {
                int rowIdx = dgvKetQua.Rows.Add(
                    r.Benh.Ten,
                    r.Benh.NhomBenh ?? "—",
                    $"{r.DoTinCay:P1}",
                    r.ThuocGoiY.Count,
                    r.CanhBao.Count > 0 ? $"⚠ {r.CanhBao.Count}" : "✓");

                var confCell = dgvKetQua.Rows[rowIdx].Cells[2];
                confCell.Style.ForeColor = r.DoTinCay >= 0.75 ? Color.FromArgb(0, 128, 0)
                    : r.DoTinCay >= 0.5 ? Color.FromArgb(180, 100, 0)
                    : Color.FromArgb(180, 0, 0);
                confCell.Style.Font = new Font(dgvKetQua.Font, FontStyle.Bold);
            }

            lblStatus.Text = $"Tìm thấy {_ketQua.Count} bệnh phù hợp với {selectedIds.Count} triệu chứng.";
            dgvKetQua.ClearSelection();
            dgvKetQua.Rows[0].Selected = true;
            ShowDetail(_ketQua[0]);
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Lỗi: {ex.Message}";
        }
        finally
        {
            btnChanDoan.Enabled = true;
        }
    }

    private void dgvKetQua_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvKetQua.SelectedRows.Count == 0) return;
        int idx = dgvKetQua.SelectedRows[0].Index;
        if (idx >= 0 && idx < _ketQua.Count)
            ShowDetail(_ketQua[idx]);
    }

    private void ShowDetail(ChanDoanResult result)
    {
        lblBenhTen.Text = result.Benh.Ten;
        lblDoTinCay.Text = $"Độ tin cậy: {result.DoTinCay:P1}";
        lblNhomBenh.Text = $"Nhóm bệnh: {result.Benh.NhomBenh ?? "—"}";
        txtMoTaBenh.Text = result.Benh.MoTa ?? "Không có mô tả.";

        dgvThuoc.Rows.Clear();
        foreach (var t in result.ThuocGoiY)
            dgvThuoc.Rows.Add(t.Ten, t.HoatChat, t.LieuDung ?? "—", t.NhomThuoc ?? "—");

        dgvCanhBao.Rows.Clear();
        if (result.CanhBao.Count == 0)
        {
            pnlCanhBaoHeader.BackColor = Color.FromArgb(220, 255, 220);
            lblCanhBaoTitle.Text = "✓  Không có cảnh báo tương tác thuốc";
            lblCanhBaoTitle.ForeColor = Color.FromArgb(0, 120, 0);
            dgvCanhBao.Visible = false;
        }
        else
        {
            pnlCanhBaoHeader.BackColor = Color.FromArgb(255, 235, 235);
            lblCanhBaoTitle.Text = $"⚠  {result.CanhBao.Count} cảnh báo tương tác thuốc";
            lblCanhBaoTitle.ForeColor = Color.FromArgb(180, 0, 0);
            dgvCanhBao.Visible = true;

            foreach (var cb in result.CanhBao)
            {
                string mucDo = cb.MucDo switch
                {
                    MucDoTuongTac.Nang => "NGUY HIỂM",
                    MucDoTuongTac.TrungBinh => "Trung bình",
                    _ => "Nhẹ"
                };
                int row = dgvCanhBao.Rows.Add(
                    cb.Thuoc1?.Ten ?? $"Thuốc #{cb.ThuocId1}",
                    cb.Thuoc2?.Ten ?? $"Thuốc #{cb.ThuocId2}",
                    mucDo,
                    cb.MoTa);

                var mucDoCell = dgvCanhBao.Rows[row].Cells[2];
                mucDoCell.Style.ForeColor = cb.MucDo switch
                {
                    MucDoTuongTac.Nang => Color.DarkRed,
                    MucDoTuongTac.TrungBinh => Color.DarkGoldenrod,
                    _ => Color.DarkGreen
                };
                mucDoCell.Style.Font = new Font(dgvCanhBao.Font, FontStyle.Bold);
            }
        }

        pnlDetail.Visible = true;
    }

    private void ClearResults()
    {
        dgvKetQua.Rows.Clear();
        pnlDetail.Visible = false;
    }
}
