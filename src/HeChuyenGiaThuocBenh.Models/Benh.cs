namespace HeChuyenGiaThuocBenh.Models;

public class Benh
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public string? MoTa { get; set; }
    public string? NhomBenh { get; set; }
    public bool IsActive { get; set; } = true;

    public List<TrieuChung> TrieuChungList { get; set; } = new();
    public List<Thuoc> ThuocDieuTriList { get; set; } = new();
}
