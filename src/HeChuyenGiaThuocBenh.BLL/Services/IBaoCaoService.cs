using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.BLL.Services;

public interface IBaoCaoService
{
    Task<IEnumerable<LichSuChanDoan>> GetThongKeAsync(DateTime from, DateTime to);
    Task XuatPDFAsync(DateTime from, DateTime to, IEnumerable<LichSuChanDoan> data, string outputPath);
}
