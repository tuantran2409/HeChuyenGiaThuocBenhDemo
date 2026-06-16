using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public interface IBenhRepository
{
    Task<IEnumerable<Benh>> GetAllAsync();
    Task<Benh?> GetByIdWithDetailsAsync(int id);
    Task<IEnumerable<Benh>> GetByTrieuChungIdsAsync(IEnumerable<int> trieuChungIds);
    Task<IEnumerable<BenhTrieuChung>> GetBenhTrieuChungAsync(int benhId);
    Task SaveBenhTrieuChungAsync(int benhId, IEnumerable<BenhTrieuChung> rules);
    Task<int> CreateAsync(Benh benh);
    Task UpdateAsync(Benh benh);
    Task DeleteAsync(int id);
}
