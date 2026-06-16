using HeChuyenGiaThuocBenh.DAL.Repositories;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.Tests.Unit.Fakes;

internal class FakeBenhRepository : IBenhRepository
{
    private readonly List<Benh> _diseases;
    private readonly Dictionary<int, List<BenhTrieuChung>> _rules;

    public FakeBenhRepository(List<Benh> diseases, Dictionary<int, List<BenhTrieuChung>> rules)
    {
        _diseases = diseases;
        _rules = rules;
    }

    public Task<IEnumerable<Benh>> GetByTrieuChungIdsAsync(IEnumerable<int> trieuChungIds)
    {
        var idSet = trieuChungIds.ToHashSet();
        var result = _diseases.Where(d =>
            _rules.TryGetValue(d.Id, out var ruleList) &&
            ruleList.Any(r => idSet.Contains(r.TrieuChungId)));
        return Task.FromResult(result);
    }

    public Task<IEnumerable<BenhTrieuChung>> GetBenhTrieuChungAsync(int benhId)
    {
        _rules.TryGetValue(benhId, out var list);
        return Task.FromResult<IEnumerable<BenhTrieuChung>>(list ?? new List<BenhTrieuChung>());
    }

    public Task<Benh?> GetByIdWithDetailsAsync(int id)
        => Task.FromResult(_diseases.FirstOrDefault(d => d.Id == id));

    public Task<IEnumerable<Benh>> GetAllAsync()
        => Task.FromResult<IEnumerable<Benh>>(_diseases);

    public Task<int> CreateAsync(Benh benh) => throw new NotImplementedException();
    public Task UpdateAsync(Benh benh) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
    public Task SaveBenhTrieuChungAsync(int benhId, IEnumerable<BenhTrieuChung> rules) => throw new NotImplementedException();
}
