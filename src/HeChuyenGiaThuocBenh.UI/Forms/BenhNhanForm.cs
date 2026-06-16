using HeChuyenGiaThuocBenh.BLL;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.UI.Forms;

public partial class BenhNhanForm : Form
{
    private List<BenhNhan> _allBenhNhan = new();
    private BenhNhan? _selected;
    private bool _isNew = false;

    public BenhNhanForm()
    {
        InitializeComponent();
        _ = LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            lblStatus.Text = "Đang tải...";
            _allBenhNhan = (await ServiceContainer.BenhNhanRepository.GetAllAsync()).ToList();
            RefreshGrid(_allBenhNhan);
            lblStatus.Text = $"Tổng: {_allBenhNhan.Count} hồ sơ";
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Lỗi: {ex.Message}";
        }
    }

    private void RefreshGrid(IEnumerable<BenhNhan> list)
    {
        _allBenhNhan = list.ToList();
        dgvBenhNhan.Rows.Clear();
        foreach (var bn in _allBenhNhan)
            dgvBenhNhan.Rows.Add(bn.HoTen, bn.Tuoi, GioiTinhLabel(bn.GioiTinh), bn.SoDienThoai ?? "—");
    }

    private static string GioiTinhLabel(GioiTinh g) => g switch
    {
        GioiTinh.Nam => "Nam",
        GioiTinh.Nu => "Nữ",
        _ => "Khác"
    };

    private void dgvBenhNhan_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvBenhNhan.SelectedRows.Count == 0) return;
        int idx = dgvBenhNhan.SelectedRows[0].Index;
        if (idx < 0 || idx >= _allBenhNhan.Count) return;

        _selected = _allBenhNhan[idx];
        _isNew = false;
        FillFormFields(_selected);
        _ = LoadLichSuAsync(_selected.Id);
    }

    private void FillFormFields(BenhNhan bn)
    {
        txtHoTen.Text = bn.HoTen;
        dtpNgaySinh.Value = bn.NgaySinh == default ? DateTime.Today.AddYears(-30) : bn.NgaySinh;
        cboGioiTinh.SelectedIndex = (int)bn.GioiTinh - 1;
        txtSoDienThoai.Text = bn.SoDienThoai ?? string.Empty;
        txtDiaChi.Text = bn.DiaChi ?? string.Empty;
        txtTienSuBenh.Text = bn.TienSuBenh ?? string.Empty;
        txtDiUng.Text = bn.DiUng ?? string.Empty;
        tabControl.SelectedIndex = 0;
    }

    private async Task LoadLichSuAsync(int benhNhanId)
    {
        try
        {
            dgvLichSu.Rows.Clear();
            var list = (await ServiceContainer.LichSuChanDoanRepository
                .GetByBenhNhanIdAsync(benhNhanId)).ToList();

            foreach (var ls in list)
                dgvLichSu.Rows.Add(
                    ls.NgayChanDoan.ToString("dd/MM/yyyy HH:mm"),
                    ls.KetQuaBenh,
                    ls.TrieuChungInput,
                    ls.ThuocGoiY ?? "—",
                    ls.GhiChu ?? "—");

            lblLichSuCount.Text = $"{list.Count} bản ghi";
        }
        catch
        {
            lblLichSuCount.Text = string.Empty;
        }
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        _selected = null;
        _isNew = true;
        dgvBenhNhan.ClearSelection();
        txtHoTen.Clear();
        dtpNgaySinh.Value = DateTime.Today.AddYears(-30);
        cboGioiTinh.SelectedIndex = 0;
        txtSoDienThoai.Clear();
        txtDiaChi.Clear();
        txtTienSuBenh.Clear();
        txtDiUng.Clear();
        dgvLichSu.Rows.Clear();
        lblLichSuCount.Text = string.Empty;
        tabControl.SelectedIndex = 0;
        txtHoTen.Focus();
    }

    private async void btnLuu_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtHoTen.Text))
        {
            MessageBox.Show("Vui lòng nhập họ tên bệnh nhân.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtHoTen.Focus();
            return;
        }

        btnLuu.Enabled = false;
        try
        {
            var bn = _isNew ? new BenhNhan() : (_selected ?? new BenhNhan());
            bn.HoTen = txtHoTen.Text.Trim();
            bn.NgaySinh = dtpNgaySinh.Value.Date;
            bn.GioiTinh = (GioiTinh)(cboGioiTinh.SelectedIndex + 1);
            bn.SoDienThoai = NullIfEmpty(txtSoDienThoai.Text);
            bn.DiaChi = NullIfEmpty(txtDiaChi.Text);
            bn.TienSuBenh = NullIfEmpty(txtTienSuBenh.Text);
            bn.DiUng = NullIfEmpty(txtDiUng.Text);

            if (_isNew)
            {
                int newId = await ServiceContainer.BenhNhanRepository.CreateAsync(bn);
                bn.Id = newId;
                _isNew = false;
                _selected = bn;
            }
            else
            {
                await ServiceContainer.BenhNhanRepository.UpdateAsync(bn);
            }

            await LoadDataAsync();

            int savedId = bn.Id;
            for (int i = 0; i < _allBenhNhan.Count; i++)
            {
                if (_allBenhNhan[i].Id == savedId)
                {
                    dgvBenhNhan.Rows[i].Selected = true;
                    dgvBenhNhan.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }

            MessageBox.Show("Đã lưu hồ sơ bệnh nhân.", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi lưu: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnLuu.Enabled = true;
        }
    }

    private void txtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) _ = SearchAsync();
    }

    private void btnTimKiem_Click(object sender, EventArgs e) => _ = SearchAsync();

    private async Task SearchAsync()
    {
        string kw = txtSearch.Text.Trim();
        if (string.IsNullOrEmpty(kw))
        {
            await LoadDataAsync();
            return;
        }
        try
        {
            var result = (await ServiceContainer.BenhNhanRepository.SearchAsync(kw)).ToList();
            RefreshGrid(result);
            lblStatus.Text = $"Tìm thấy: {result.Count} hồ sơ";
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Lỗi: {ex.Message}";
        }
    }

    private async void btnXemTatCa_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        await LoadDataAsync();
    }

    private static string? NullIfEmpty(string s)
        => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
}
