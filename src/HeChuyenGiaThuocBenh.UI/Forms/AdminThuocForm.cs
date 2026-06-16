using HeChuyenGiaThuocBenh.BLL;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.UI.Forms;

public partial class AdminThuocForm : Form
{
    private List<Thuoc> _allThuoc = new();
    private Thuoc? _selected;
    private bool _isNew = false;

    public AdminThuocForm()
    {
        InitializeComponent();
        _ = LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            lblStatus.Text = "Đang tải...";
            var all = (await ServiceContainer.ThuocRepository.GetAllAsync()).ToList();
            _allThuoc = all;
            PopulateNhomFilter();
            RefreshGrid(_allThuoc);
            lblStatus.Text = $"Tổng: {_allThuoc.Count} thuốc đang hoạt động";
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Lỗi: {ex.Message}";
        }
    }

    private void PopulateNhomFilter()
    {
        var nhoms = _allThuoc.Select(t => t.NhomThuoc ?? "Khác").Distinct().OrderBy(n => n).ToList();
        cboNhom.Items.Clear();
        cboNhom.Items.Add("Tất cả nhóm");
        foreach (var n in nhoms) cboNhom.Items.Add(n);
        cboNhom.SelectedIndex = 0;
    }

    private void RefreshGrid(IEnumerable<Thuoc> list)
    {
        _allThuoc = list.ToList();
        dgvThuoc.Rows.Clear();
        foreach (var t in _allThuoc)
            dgvThuoc.Rows.Add(t.Ten, t.HoatChat, t.NhomThuoc ?? "—");
    }

    private void dgvThuoc_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvThuoc.SelectedRows.Count == 0) return;
        int idx = dgvThuoc.SelectedRows[0].Index;
        if (idx < 0 || idx >= _allThuoc.Count) return;
        _selected = _allThuoc[idx];
        _isNew = false;
        FillEditForm(_selected);
    }

    private void FillEditForm(Thuoc t)
    {
        txtTen.Text = t.Ten;
        txtHoatChat.Text = t.HoatChat;
        txtNhomThuoc.Text = t.NhomThuoc ?? string.Empty;
        txtLieuDung.Text = t.LieuDung ?? string.Empty;
        txtCachDung.Text = t.CachDung ?? string.Empty;
        txtChongChiDinh.Text = t.ChongChiDinh ?? string.Empty;
        txtTacDungPhu.Text = t.TacDungPhu ?? string.Empty;
        txtMoTa.Text = t.MoTa ?? string.Empty;
        chkIsActive.Checked = t.IsActive;
        pnlEdit.Visible = true;
        lblEditTitle.Text = $"Chỉnh sửa: {t.Ten}";
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        _selected = null;
        _isNew = true;
        dgvThuoc.ClearSelection();
        txtTen.Clear(); txtHoatChat.Clear(); txtNhomThuoc.Clear();
        txtLieuDung.Clear(); txtCachDung.Clear();
        txtChongChiDinh.Clear(); txtTacDungPhu.Clear(); txtMoTa.Clear();
        chkIsActive.Checked = true;
        lblEditTitle.Text = "Thêm thuốc mới";
        pnlEdit.Visible = true;
        txtTen.Focus();
    }

    private async void btnLuu_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtTen.Text))
        {
            MessageBox.Show("Tên thuốc không được để trống.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTen.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtHoatChat.Text))
        {
            MessageBox.Show("Hoạt chất không được để trống.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtHoatChat.Focus();
            return;
        }

        btnLuu.Enabled = false;
        try
        {
            var thuoc = _isNew ? new Thuoc() : (_selected ?? new Thuoc());
            thuoc.Ten = txtTen.Text.Trim();
            thuoc.HoatChat = txtHoatChat.Text.Trim();
            thuoc.NhomThuoc = NullIfEmpty(txtNhomThuoc.Text);
            thuoc.LieuDung = NullIfEmpty(txtLieuDung.Text);
            thuoc.CachDung = NullIfEmpty(txtCachDung.Text);
            thuoc.ChongChiDinh = NullIfEmpty(txtChongChiDinh.Text);
            thuoc.TacDungPhu = NullIfEmpty(txtTacDungPhu.Text);
            thuoc.MoTa = NullIfEmpty(txtMoTa.Text);
            thuoc.IsActive = chkIsActive.Checked;

            if (_isNew)
            {
                int newId = await ServiceContainer.ThuocRepository.CreateAsync(thuoc);
                thuoc.Id = newId;
                _isNew = false;
                _selected = thuoc;
            }
            else
            {
                await ServiceContainer.ThuocRepository.UpdateAsync(thuoc);
            }

            await LoadDataAsync();

            int savedId = thuoc.Id;
            for (int i = 0; i < _allThuoc.Count; i++)
            {
                if (_allThuoc[i].Id == savedId)
                {
                    dgvThuoc.Rows[i].Selected = true;
                    dgvThuoc.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }

            MessageBox.Show("Đã lưu thông tin thuốc.", "Thành công",
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

    private async void btnXoa_Click(object sender, EventArgs e)
    {
        if (_selected == null || _isNew)
        {
            MessageBox.Show("Chọn một thuốc trong danh sách để xóa.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var result = MessageBox.Show(
            $"Xóa thuốc \"{_selected.Ten}\"?\n(Thuốc sẽ bị ẩn khỏi hệ thống, không xóa vĩnh viễn)",
            "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        if (result != DialogResult.Yes) return;

        try
        {
            await ServiceContainer.ThuocRepository.DeleteAsync(_selected.Id);
            pnlEdit.Visible = false;
            _selected = null;
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnTimKiem_Click(object sender, EventArgs e) => ApplyFilter();
    private void txtSearch_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) ApplyFilter(); }
    private void cboNhom_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilter();

    private void ApplyFilter()
    {
        string kw = txtSearch.Text.Trim();
        string nhom = cboNhom.SelectedIndex > 0 ? cboNhom.SelectedItem!.ToString()! : string.Empty;

        var result = _allThuoc.AsEnumerable();
        if (!string.IsNullOrEmpty(kw))
            result = result.Where(t =>
                t.Ten.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                t.HoatChat.Contains(kw, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(nhom))
            result = result.Where(t => (t.NhomThuoc ?? "Khác") == nhom);

        var list = result.ToList();
        RefreshGrid(list);
        lblStatus.Text = $"Tìm thấy: {list.Count} thuốc";
    }

    private async void btnXemTatCa_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        await LoadDataAsync();
    }

    private static string? NullIfEmpty(string s)
        => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
}
