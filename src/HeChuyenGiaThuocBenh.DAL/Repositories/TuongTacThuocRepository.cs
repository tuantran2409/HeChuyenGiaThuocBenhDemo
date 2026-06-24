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
        var rows = await conn.QueryAsync<TuongTacThuoc, Thuoc, Thuoc, TuongTacThuoc>(@"
            SELECT tt.*, t1.*, t2.*
            FROM TuongTacThuoc tt
            INNER JOIN Thuoc t1 ON t1.Id = tt.ThuocId1
            INNER JOIN Thuoc t2 ON t2.Id = tt.ThuocId2
            WHERE tt.ThuocId1 IN @Ids AND tt.ThuocId2 IN @Ids
            ORDER BY tt.MucDo DESC",
            (tt, t1, t2) => { tt.Thuoc1 = t1; tt.Thuoc2 = t2; return tt; },
            new { Ids = idList },
            splitOn: "Id,Id");
        return rows;
    }
}
