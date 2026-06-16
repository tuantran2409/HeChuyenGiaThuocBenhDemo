namespace HeChuyenGiaThuocBenh.Models;

public class TuongTacThuoc
{
    public int Id { get; set; }
    public int ThuocId1 { get; set; }
    public int ThuocId2 { get; set; }
    public MucDoTuongTac MucDo { get; set; }
    public string MoTa { get; set; } = string.Empty;
    public string? HauQua { get; set; }

    public Thuoc? Thuoc1 { get; set; }
    public Thuoc? Thuoc2 { get; set; }
}
