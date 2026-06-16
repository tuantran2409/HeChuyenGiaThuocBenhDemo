using Dapper;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public class LichSuChanDoanRepository : ILichSuChanDoanRepository
{
    private readonly DbConnectionFactory _factory;

    public LichSuChanDoanRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<LichSuChanDoan>> GetByBenhNhanIdAsync(int benhNhanId)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<LichSuChanDoan>(@"
            SELECT ls.*, bn.HoTen as [BenhNhan.HoTen], u.HoTen as [User.HoTen]
            FROM LichSuChanDoan ls
            LEFT JOIN BenhNhan bn ON bn.Id = ls.BenhNhanId
            LEFT JOIN Users u ON u.Id = ls.UserId
            WHERE ls.BenhNhanId=@BenhNhanId
            ORDER BY ls.NgayChanDoan DESC",
            new { BenhNhanId = benhNhanId });
    }

    public async Task<IEnumerable<LichSuChanDoan>> GetByDateRangeAsync(DateTime from, DateTime to)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<LichSuChanDoan>(@"
            SELECT * FROM LichSuChanDoan
            WHERE NgayChanDoan BETWEEN @From AND @To
            ORDER BY NgayChanDoan DESC",
            new { From = from, To = to });
    }

    public async Task<LichSuChanDoan?> GetByIdAsync(int id)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<LichSuChanDoan>(
            "SELECT * FROM LichSuChanDoan WHERE Id=@Id", new { Id = id });
    }

    public async Task<int> CreateAsync(LichSuChanDoan lichSu)
    {
        using var conn = _factory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(@"
            INSERT INTO LichSuChanDoan (BenhNhanId, UserId, NgayChanDoan, TrieuChungInput, KetQuaBenh, ThuocGoiY, GhiChu)
            OUTPUT INSERTED.Id
            VALUES (@BenhNhanId, @UserId, @NgayChanDoan, @TrieuChungInput, @KetQuaBenh, @ThuocGoiY, @GhiChu)",
            lichSu);
    }

    public async Task<int> CountByDateAsync(DateTime date)
    {
        using var conn = _factory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(@"
            SELECT COUNT(*) FROM LichSuChanDoan
            WHERE CAST(NgayChanDoan AS DATE) = CAST(@Date AS DATE)",
            new { Date = date });
    }
}
