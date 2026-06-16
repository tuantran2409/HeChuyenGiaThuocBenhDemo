using HeChuyenGiaThuocBenh.BLL;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.UI.Forms;

public partial class AdminBenhForm : Form
{
    private List<Benh> _allBenh = new();
    private List<TrieuChung> _allTrieuChung = new();
    private Benh? _selected;
    private bool _isNew = false;

    public AdminBenhForm()
    {
        InitializeComponent();
        _ = LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            lblStatus.Text = "Đang tải...";
            _allTrieuChung = (await ServiceContainer.TrieuChungRepository.GetAllAsync())
                .OrderBy(t => t.Ten).ToList();

            // Populate combobox column datasource
            colRuleTrieuChung.DataSource = _allTrieuChung.ToList();
            colRuleTrieuChung.DisplayMember = "Ten";
            colRuleTrieuChung.ValueMember = "Id";

            _allBenh = (await ServiceContainer.BenhRepository.GetAllAsync()).ToList();
            RefreshBenhGrid(_allBenh);
            lblStatus.Text = $"Tổng: {_allBenh.Count} bệnh";
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Lỗi: {ex.Message}";
        }
    }

    private void RefreshBenhGrid(IEnumerable<Benh> list)
    {
        _allBenh = list.ToList();
        dgvBenh.Rows.Clear();
        foreach (var b in _allBenh)
            dgvBenh.Rows.Add(b.Ten, b.NhomBenh ?? "—");
    }

    private void dgvBenh_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvBenh.SelectedRows.Count == 0) return;
        int idx = dgvBenh.SelectedRows[0].Index;
        if (idx < 0 || idx >= _allBenh.Count) return;

        _selected = _allBenh[idx];
        _isNew = false;
        FillBenhForm(_selected);
        _ = LoadRulesAsync(_selected.Id);
    }

    private void FillBenhForm(Benh b)
    {
        txtTen.Text = b.Ten;
        txtNhomBenh.Text = b.NhomBenh ?? string.Empty;
        txtMoTa.Text = b.MoTa ?? string.Empty;
        chkIsActive.Checked = b.IsActive;
        pnlBenhDetail.Visible = true;
        lblDetailTitle.Text = $"Bệnh: {b.Ten}";
    }

    private async Task LoadRulesAsync(int benhId)
    {
        try
        {
            dgvRules.Rows.Clear();
            var rules = (await ServiceContainer.BenhRepository.GetBenhTrieuChungAsync(benhId)).ToList();

            foreach (var r in rules)
            {
                int row = dgvRules.Rows.Add(r.TrieuChungId, r.TrongSo, r.BatBuoc);
                _ = row;
            }

            lblRuleCount.Text = $"{rules.Count} luật";
        }
        catch
        {
            lblRuleCount.Text = string.Empty;
        }
    }

    // ── Benh CRUD ───────────────────────────────────────────────────────────

    private void btnThemBenh_Click(object sender, EventArgs e)
    {
        _selected = null;
        _isNew = true;
        dgvBenh.ClearSelection();
        txtTen.Clear();
        txtNhomBenh.Clear();
        txtMoTa.Clear();
        chkIsActive.Checked = true;
        dgvRules.Rows.Clear();
        lblRuleCount.Text = string.Empty;
        lblDetailTitle.Text = "Thêm bệnh mới";
        pnlBenhDetail.Visible = true;
        tabRight.SelectedIndex = 0;
        txtTen.Focus();
    }

    private async void btnXoaBenh_Click(object sender, EventArgs e)
    {
        if (_selected == null || _isNew)
        {
            MessageBox.Show("Chọn một bệnh để xóa.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var result = MessageBox.Show(
            $"Xóa bệnh \"{_selected.Ten}\"?",
            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        if (result != DialogResult.Yes) return;

        try
        {
            await ServiceContainer.BenhRepository.DeleteAsync(_selected.Id);
            pnlBenhDetail.Visible = false;
            _selected = null;
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnLuuBenh_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtTen.Text))
        {
            MessageBox.Show("Tên bệnh không được để trống.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTen.Focus();
            return;
        }

        btnLuuBenh.Enabled = false;
        try
        {
            var benh = _isNew ? new Benh() : (_selected ?? new Benh());
            benh.Ten = txtTen.Text.Trim();
            benh.NhomBenh = string.IsNullOrWhiteSpace(txtNhomBenh.Text) ? null : txtNhomBenh.Text.Trim();
            benh.MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim();
            benh.IsActive = chkIsActive.Checked;

            if (_isNew)
            {
                int newId = await ServiceContainer.BenhRepository.CreateAsync(benh);
                benh.Id = newId;
                _isNew = false;
                _selected = benh;
            }
            else
            {
                await ServiceContainer.BenhRepository.UpdateAsync(benh);
            }

            await LoadDataAsync();

            int savedId = benh.Id;
            for (int i = 0; i < _allBenh.Count; i++)
            {
                if (_allBenh[i].Id == savedId)
                {
                    dgvBenh.Rows[i].Selected = true;
                    dgvBenh.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }

            MessageBox.Show("Đã lưu thông tin bệnh.", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnLuuBenh.Enabled = true;
        }
    }

    // ── Rule editor ─────────────────────────────────────────────────────────

    private void btnThemLuat_Click(object sender, EventArgs e)
    {
        if (_allTrieuChung.Count == 0) return;
        dgvRules.Rows.Add(_allTrieuChung[0].Id, 1.0m, false);
    }

    private void btnXoaLuat_Click(object sender, EventArgs e)
    {
        var selected = dgvRules.SelectedRows.Cast<DataGridViewRow>().ToList();
        foreach (var row in selected)
            if (!row.IsNewRow) dgvRules.Rows.Remove(row);
    }

    private async void btnLuuLuat_Click(object sender, EventArgs e)
    {
        if (_selected == null)
        {
            MessageBox.Show("Lưu thông tin bệnh trước khi lưu tập luật.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var rules = new List<BenhTrieuChung>();
        foreach (DataGridViewRow row in dgvRules.Rows)
        {
            if (row.IsNewRow) continue;

            var tcVal = row.Cells[0].Value;
            var tsVal = row.Cells[1].Value;
            var bbVal = row.Cells[2].Value;

            if (tcVal == null) continue;

            if (!int.TryParse(tcVal.ToString(), out int tcId)) continue;
            if (!decimal.TryParse(tsVal?.ToString() ?? "1", out decimal ts)) ts = 1.0m;
            bool bb = bbVal is bool b && b;

            rules.Add(new BenhTrieuChung
            {
                BenhId = _selected.Id,
                TrieuChungId = tcId,
                TrongSo = Math.Clamp(ts, 0.1m, 10m),
                BatBuoc = bb
            });
        }

        // Deduplicate by TrieuChungId
        rules = rules.GroupBy(r => r.TrieuChungId)
                     .Select(g => g.First())
                     .ToList();

        btnLuuLuat.Enabled = false;
        try
        {
            await ServiceContainer.BenhRepository.SaveBenhTrieuChungAsync(_selected.Id, rules);
            await LoadRulesAsync(_selected.Id);
            MessageBox.Show($"Đã lưu {rules.Count} luật cho bệnh \"{_selected.Ten}\".", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnLuuLuat.Enabled = true;
        }
    }

    private void txtSearchBenh_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) FilterBenh();
    }

    private void txtSearchBenh_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSearchBenh.Text))
            RefreshBenhGrid(_allBenh);
        else
            FilterBenh();
    }

    private void FilterBenh()
    {
        string kw = txtSearchBenh.Text.Trim();
        if (string.IsNullOrEmpty(kw)) { RefreshBenhGrid(_allBenh); return; }
        var filtered = _allBenh.Where(b =>
            b.Ten.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
            (b.NhomBenh ?? "").Contains(kw, StringComparison.OrdinalIgnoreCase)).ToList();
        RefreshBenhGrid(filtered);
    }
}
