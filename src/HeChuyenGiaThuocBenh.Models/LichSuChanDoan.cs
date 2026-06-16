namespace HeChuyenGiaThuocBenh.Models;

public class LichSuChanDoan
{
    public int Id { get; set; }
    public int BenhNhanId { get; set; }
    public int UserId { get; set; }
    public DateTime NgayChanDoan { get; set; } = DateTime.Now;
    public string TrieuChungInput { get; set; } = string.Empty;
    public string KetQuaBenh { get; set; } = string.Empty;
    public string? ThuocGoiY { get; set; }
    public string? GhiChu { get; set; }

    public BenhNhan? BenhNhan { get; set; }
    public User? User { get; set; }
}
