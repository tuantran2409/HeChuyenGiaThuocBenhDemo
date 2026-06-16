namespace HeChuyenGiaThuocBenh.Models;

public class Thuoc
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public string HoatChat { get; set; } = string.Empty;
    public string? NhomThuoc { get; set; }
    public string? LieuDung { get; set; }
    public string? CachDung { get; set; }
    public string? ChongChiDinh { get; set; }
    public string? TacDungPhu { get; set; }
    public string? MoTa { get; set; }
    public bool IsActive { get; set; } = true;
}
