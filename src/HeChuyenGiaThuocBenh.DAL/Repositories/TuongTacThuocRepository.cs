using Dapper;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public class TuongTacThuocRepository : ITuongTacThuocRepository
{
    private readonly DbConnectionFactory _factory;

    public TuongTacThuocRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<TuongTacThuoc>> GetAllAsync()
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<TuongTacThuoc>(
            "SELECT * FROM TuongTacThuoc ORDER BY MucDo DESC");
    }

    public async Task<TuongTacThuoc?> CheckInteractionAsync(int thuocId1, int thuocId2)
    {
        int id1 = Math.Min(thuocId1, thuocId2);
        int id2 = Math.Max(thuocId1, thuocId2);
        using var conn = _factory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TuongTacThuoc>(@"
            SELECT * FROM TuongTacThuoc
            WHERE ThuocId1=@Id1 AND ThuocId2=@Id2",
            new { Id1 = id1, Id2 = id2 });
    }

    public async Task<IEnumerable<TuongTacThuoc>> CheckMultipleInteractionsAsync(IEnumerable<int> thuocIds)
    {
        var idList = thuocIds.ToList();
        if (idList.Count < 2) return [];

        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<TuongTacThuoc>(@"
            SELECT * FROM TuongTacThuoc
            WHERE ThuocId1 IN @Ids AND ThuocId2 IN @Ids
            ORDER BY MucDo DESC",
            new { Ids = idList });
    }
}
