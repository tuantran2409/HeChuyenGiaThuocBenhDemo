using HeChuyenGiaThuocBenh.BLL;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.UI.Forms;

public partial class ThuocForm : Form
{
    private List<Thuoc> _allThuoc = new();
    private List<Thuoc> _displayedThuoc = new();

    public ThuocForm()
    {
        InitializeComponent();
        _ = LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            lblStatus.Text = "Đang tải danh sách thuốc...";

            _allThuoc = (await ServiceContainer.ThuocRepository.GetAllAsync())
                .Where(t => t.IsActive)
                .OrderBy(t => t.Ten)
                .ToList();

            // Populate NhomThuoc filter
            var nhomList = _allThuoc
                .Select(t => t.NhomThuoc ?? "Khác")
                .Distinct()
                .OrderBy(n => n)
                .ToList();

            cboNhom.Items.Clear();
            cboNhom.Items.Add("Tất cả nhóm");
            foreach (var n in nhomList)
                cboNhom.Items.Add(n);
            cboNhom.SelectedIndex = 0;

            DisplayThuoc(_allThuoc);
            lblStatus.Text = $"Tổng: {_allThuoc.Count} thuốc.";
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Lỗi: {ex.Message}";
        }
    }

    private void DisplayThuoc(IEnumerable<Thuoc> thuocList)
    {
        _displayedThuoc = thuocList.ToList();
        dgvThuoc.Rows.Clear();
        pnlDetail.Visible = false;

        foreach (var t in _displayedThuoc)
            dgvThuoc.Rows.Add(t.Ten, t.HoatChat, t.NhomThuoc ?? "—", t.LieuDung ?? "—");
    }

    private void btnTimKiem_Click(object sender, EventArgs e)
        => ApplyFilter();

    private void txtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) ApplyFilter();
    }

    private void cboNhom_SelectedIndexChanged(object sender, EventArgs e)
        => ApplyFilter();

    private void btnXemTatCa_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        cboNhom.SelectedIndex = 0;
        DisplayThuoc(_allThuoc);
        lblStatus.Text = $"Tổng: {_allThuoc.Count} thuốc.";
    }

    private void ApplyFilter()
    {
        string keyword = txtSearch.Text.Trim();
        string nhom = cboNhom.SelectedIndex > 0 ? cboNhom.SelectedItem!.ToString()! : string.Empty;

        var result = _allThuoc.AsEnumerable();

        if (!string.IsNullOrEmpty(keyword))
            result = result.Where(t =>
                t.Ten.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                t.HoatChat.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                (t.MoTa ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(nhom))
            result = result.Where(t => (t.NhomThuoc ?? "Khác") == nhom);

        var list = result.ToList();
        DisplayThuoc(list);
        lblStatus.Text = $"Tìm thấy: {list.Count} thuốc.";
    }

    private void dgvThuoc_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvThuoc.SelectedRows.Count == 0)
        {
            pnlDetail.Visible = false;
            return;
        }

        int idx = dgvThuoc.SelectedRows[0].Index;
        if (idx >= 0 && idx < _displayedThuoc.Count)
            ShowDetail(_displayedThuoc[idx]);
    }

    private void ShowDetail(Thuoc t)
    {
        lblTenThuoc.Text = t.Ten;
        lblHoatChatVal.Text = t.HoatChat;
        lblNhomVal.Text = t.NhomThuoc ?? "—";
        lblLieuDungVal.Text = t.LieuDung ?? "—";
        lblCachDungVal.Text = t.CachDung ?? "—";
        txtChongChiDinh.Text = string.IsNullOrWhiteSpace(t.ChongChiDinh) ? "Không có." : t.ChongChiDinh;
        txtTacDungPhu.Text = string.IsNullOrWhiteSpace(t.TacDungPhu) ? "Không ghi nhận." : t.TacDungPhu;

        dgvTuongTac.Rows.Clear();
        lblTuongTacStatus.Text = string.Empty;

        pnlDetail.Visible = true;
    }

    private async void btnKiemTraTuongTac_Click(object sender, EventArgs e)
    {
        var selectedIds = dgvThuoc.SelectedRows
            .Cast<DataGridViewRow>()
            .Select(r => r.Index)
            .Where(i => i >= 0 && i < _displayedThuoc.Count)
            .Select(i => _displayedThuoc[i].Id)
            .ToList();

        if (selectedIds.Count < 2)
        {
            lblTuongTacStatus.Text = "Chọn ít nhất 2 thuốc trong danh sách để kiểm tra.";
            lblTuongTacStatus.ForeColor = Color.DarkOrange;
            return;
        }

        btnKiemTraTuongTac.Enabled = false;
        lblTuongTacStatus.Text = "Đang kiểm tra...";
        dgvTuongTac.Rows.Clear();

        try
        {
            var interactions = (await ServiceContainer.TuongTacThuocRepository
                .CheckMultipleInteractionsAsync(selectedIds)).ToList();

            if (interactions.Count == 0)
            {
                lblTuongTacStatus.Text = "✓ Không phát hiện tương tác thuốc trong nhóm đã chọn.";
                lblTuongTacStatus.ForeColor = Color.FromArgb(0, 120, 0);
                return;
            }

            lblTuongTacStatus.Text = $"⚠ Phát hiện {interactions.Count} tương tác thuốc.";
            lblTuongTacStatus.ForeColor = Color.DarkRed;

            foreach (var cb in interactions)
            {
                string mucDo = cb.MucDo switch
                {
                    MucDoTuongTac.Nang => "NGUY HIỂM",
                    MucDoTuongTac.TrungBinh => "Trung bình",
                    _ => "Nhẹ"
                };
                int row = dgvTuongTac.Rows.Add(
                    cb.Thuoc1?.Ten ?? $"ID #{cb.ThuocId1}",
                    cb.Thuoc2?.Ten ?? $"ID #{cb.ThuocId2}",
                    mucDo,
                    cb.MoTa,
                    cb.HauQua ?? "—");

                var mucDoCell = dgvTuongTac.Rows[row].Cells[2];
                mucDoCell.Style.ForeColor = cb.MucDo switch
                {
                    MucDoTuongTac.Nang => Color.DarkRed,
                    MucDoTuongTac.TrungBinh => Color.DarkGoldenrod,
                    _ => Color.DarkGreen
                };
                mucDoCell.Style.Font = new Font(dgvTuongTac.Font, FontStyle.Bold);
            }
        }
        catch (Exception ex)
        {
            lblTuongTacStatus.Text = $"Lỗi: {ex.Message}";
            lblTuongTacStatus.ForeColor = Color.DarkRed;
        }
        finally
        {
            btnKiemTraTuongTac.Enabled = true;
        }
    }
}
