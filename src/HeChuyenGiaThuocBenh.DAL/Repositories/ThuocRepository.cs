using Dapper;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public class ThuocRepository : IThuocRepository
{
    private readonly DbConnectionFactory _factory;

    public ThuocRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<Thuoc>> GetAllAsync()
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<Thuoc>(
            "SELECT * FROM Thuoc WHERE IsActive=1 ORDER BY Ten");
    }

    public async Task<Thuoc?> GetByIdAsync(int id)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Thuoc>(
            "SELECT * FROM Thuoc WHERE Id=@Id", new { Id = id });
    }

    public async Task<IEnumerable<Thuoc>> SearchAsync(string keyword)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<Thuoc>(@"
            SELECT * FROM Thuoc
            WHERE IsActive=1
              AND (Ten LIKE @kw OR HoatChat LIKE @kw OR NhomThuoc LIKE @kw)
            ORDER BY Ten",
            new { kw = $"%{keyword}%" });
    }

    public async Task<IEnumerable<Thuoc>> GetByBenhIdAsync(int benhId)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<Thuoc>(@"
            SELECT t.* FROM Thuoc t
            INNER JOIN BenhThuoc bt ON bt.ThuocId = t.Id
            WHERE bt.BenhId=@BenhId AND t.IsActive=1
            ORDER BY bt.ThuTu",
            new { BenhId = benhId });
    }

    public async Task<int> CreateAsync(Thuoc thuoc)
    {
        using var conn = _factory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(@"
            INSERT INTO Thuoc (Ten, HoatChat, NhomThuoc, LieuDung, CachDung, ChongChiDinh, TacDungPhu, MoTa, IsActive)
            OUTPUT INSERTED.Id
            VALUES (@Ten, @HoatChat, @NhomThuoc, @LieuDung, @CachDung, @ChongChiDinh, @TacDungPhu, @MoTa, @IsActive)",
            thuoc);
    }

    public async Task UpdateAsync(Thuoc thuoc)
    {
        using var conn = _factory.CreateConnection();
        await conn.ExecuteAsync(@"
            UPDATE Thuoc SET Ten=@Ten, HoatChat=@HoatChat, NhomThuoc=@NhomThuoc,
            LieuDung=@LieuDung, CachDung=@CachDung, ChongChiDinh=@ChongChiDinh,
            TacDungPhu=@TacDungPhu, MoTa=@MoTa WHERE Id=@Id",
            thuoc);
    }

    public async Task DeleteAsync(int id)
    {
        using var conn = _factory.CreateConnection();
        await conn.ExecuteAsync("UPDATE Thuoc SET IsActive=0 WHERE Id=@Id", new { Id = id });
    }
}
