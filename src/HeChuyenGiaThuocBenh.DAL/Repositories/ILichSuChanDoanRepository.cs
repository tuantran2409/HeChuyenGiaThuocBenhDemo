using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public interface ILichSuChanDoanRepository
{
    Task<IEnumerable<LichSuChanDoan>> GetByBenhNhanIdAsync(int benhNhanId);
    Task<IEnumerable<LichSuChanDoan>> GetByDateRangeAsync(DateTime from, DateTime to);
    Task<LichSuChanDoan?> GetByIdAsync(int id);
    Task<int> CreateAsync(LichSuChanDoan lichSu);
    Task<int> CountByDateAsync(DateTime date);
}
