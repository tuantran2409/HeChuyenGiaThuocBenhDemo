using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public interface ITrieuChungRepository
{
    Task<IEnumerable<TrieuChung>> GetAllAsync();
    Task<IEnumerable<TrieuChung>> GetByNhomAsync(string nhom);
    Task<TrieuChung?> GetByIdAsync(int id);
}
