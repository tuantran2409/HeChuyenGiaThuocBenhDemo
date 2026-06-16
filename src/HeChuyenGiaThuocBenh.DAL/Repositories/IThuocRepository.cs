using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public interface IThuocRepository
{
    Task<IEnumerable<Thuoc>> GetAllAsync();
    Task<Thuoc?> GetByIdAsync(int id);
    Task<IEnumerable<Thuoc>> SearchAsync(string keyword);
    Task<IEnumerable<Thuoc>> GetByBenhIdAsync(int benhId);
    Task<int> CreateAsync(Thuoc thuoc);
    Task UpdateAsync(Thuoc thuoc);
    Task DeleteAsync(int id);
}
