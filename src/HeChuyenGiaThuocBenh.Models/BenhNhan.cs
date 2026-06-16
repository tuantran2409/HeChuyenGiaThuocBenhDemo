namespace HeChuyenGiaThuocBenh.Models;

public class BenhNhan
{
    public int Id { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public DateTime NgaySinh { get; set; }
    public GioiTinh GioiTinh { get; set; }
    public string? SoDienThoai { get; set; }
    public string? DiaChi { get; set; }
    public string? TienSuBenh { get; set; }
    public string? DiUng { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int Tuoi => DateTime.Today.Year - NgaySinh.Year;
}
