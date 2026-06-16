using HeChuyenGiaThuocBenh.DAL.Repositories;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.Tests.Unit.Fakes;

internal class FakeThuocRepository : IThuocRepository
{
    private readonly Dictionary<int, List<Thuoc>> _byBenhId;

    public FakeThuocRepository(Dictionary<int, List<Thuoc>>? byBenhId = null)
    {
        _byBenhId = byBenhId ?? new Dictionary<int, List<Thuoc>>();
    }

    public Task<IEnumerable<Thuoc>> GetByBenhIdAsync(int benhId)
    {
        _byBenhId.TryGetValue(benhId, out var list);
        return Task.FromResult<IEnumerable<Thuoc>>(list ?? new List<Thuoc>());
    }

    public Task<IEnumerable<Thuoc>> GetAllAsync() => throw new NotImplementedException();
    public Task<Thuoc?> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task<IEnumerable<Thuoc>> SearchAsync(string keyword) => throw new NotImplementedException();
    public Task<int> CreateAsync(Thuoc thuoc) => throw new NotImplementedException();
    public Task UpdateAsync(Thuoc thuoc) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
}
