namespace HeChuyenGiaThuocBenh.Models;

public class TrieuChung
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public string? MoTa { get; set; }
    public string? NhomTrieuChung { get; set; }
}

public class BenhTrieuChung
{
    public int BenhId { get; set; }
    public int TrieuChungId { get; set; }
    public decimal TrongSo { get; set; } = 1.0m;
    public bool BatBuoc { get; set; } = false;
}

public class BenhThuoc
{
    public int BenhId { get; set; }
    public int ThuocId { get; set; }
    public string? GhiChu { get; set; }
    public int ThuTu { get; set; } = 1;
}
