using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public interface IBenhNhanRepository
{
    Task<IEnumerable<BenhNhan>> GetAllAsync();
    Task<BenhNhan?> GetByIdAsync(int id);
    Task<IEnumerable<BenhNhan>> SearchAsync(string keyword);
    Task<int> CreateAsync(BenhNhan benhNhan);
    Task UpdateAsync(BenhNhan benhNhan);
}
