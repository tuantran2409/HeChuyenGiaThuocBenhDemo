using HeChuyenGiaThuocBenh.DAL.Repositories;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.Tests.Unit.Fakes;

internal class FakeTuongTacThuocRepository : ITuongTacThuocRepository
{
    private readonly List<TuongTacThuoc> _interactions;

    public FakeTuongTacThuocRepository(List<TuongTacThuoc>? interactions = null)
    {
        _interactions = interactions ?? new List<TuongTacThuoc>();
    }

    public Task<IEnumerable<TuongTacThuoc>> CheckMultipleInteractionsAsync(IEnumerable<int> thuocIds)
    {
        var idList = thuocIds.ToList();
        var found = _interactions.Where(t =>
            idList.Contains(t.ThuocId1) && idList.Contains(t.ThuocId2));
        return Task.FromResult(found);
    }

    public Task<TuongTacThuoc?> CheckInteractionAsync(int thuocId1, int thuocId2)
        => throw new NotImplementedException();
    public Task<IEnumerable<TuongTacThuoc>> GetAllAsync()
        => throw new NotImplementedException();
}
