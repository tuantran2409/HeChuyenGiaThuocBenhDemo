using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.BLL.Services;

public class ChanDoanResult
{
    public Benh Benh { get; set; } = null!;
    public double DoTinCay { get; set; }
    public List<Thuoc> ThuocGoiY { get; set; } = new();
    public List<TuongTacThuoc> CanhBao { get; set; } = new();
}

public interface IInferenceService
{
    Task<List<ChanDoanResult>> ChanDoanAsync(IEnumerable<int> trieuChungIds);
}
