using Dapper;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public class BenhRepository : IBenhRepository
{
    private readonly DbConnectionFactory _factory;

    public BenhRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<Benh>> GetAllAsync()
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<Benh>(
            "SELECT * FROM Benh WHERE IsActive=1 ORDER BY Ten");
    }

    public async Task<Benh?> GetByIdWithDetailsAsync(int id)
    {
        using var conn = _factory.CreateConnection();
        var benh = await conn.QueryFirstOrDefaultAsync<Benh>(
            "SELECT * FROM Benh WHERE Id=@Id", new { Id = id });

        if (benh == null) return null;

        benh.TrieuChungList = (await conn.QueryAsync<TrieuChung>(@"
            SELECT tc.* FROM TrieuChung tc
            INNER JOIN BenhTrieuChung btc ON btc.TrieuChungId = tc.Id
            WHERE btc.BenhId=@BenhId", new { BenhId = id })).ToList();

        benh.ThuocDieuTriList = (await conn.QueryAsync<Thuoc>(@"
            SELECT t.* FROM Thuoc t
            INNER JOIN BenhThuoc bt ON bt.ThuocId = t.Id
            WHERE bt.BenhId=@BenhId ORDER BY bt.ThuTu", new { BenhId = id })).ToList();

        return benh;
    }

    public async Task<IEnumerable<Benh>> GetByTrieuChungIdsAsync(IEnumerable<int> trieuChungIds)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<Benh>(@"
            SELECT DISTINCT b.* FROM Benh b
            INNER JOIN BenhTrieuChung btc ON btc.BenhId = b.Id
            WHERE btc.TrieuChungId IN @Ids AND b.IsActive=1",
            new { Ids = trieuChungIds });
    }

    public async Task<IEnumerable<BenhTrieuChung>> GetBenhTrieuChungAsync(int benhId)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<BenhTrieuChung>(
            "SELECT * FROM BenhTrieuChung WHERE BenhId=@BenhId",
            new { BenhId = benhId });
    }

    public async Task<int> CreateAsync(Benh benh)
    {
        using var conn = _factory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(@"
            INSERT INTO Benh (Ten, MoTa, NhomBenh, IsActive)
            OUTPUT INSERTED.Id
            VALUES (@Ten, @MoTa, @NhomBenh, @IsActive)",
            benh);
    }

    public async Task UpdateAsync(Benh benh)
    {
        using var conn = _factory.CreateConnection();
        await conn.ExecuteAsync(
            "UPDATE Benh SET Ten=@Ten, MoTa=@MoTa, NhomBenh=@NhomBenh WHERE Id=@Id",
            benh);
    }

    public async Task DeleteAsync(int id)
    {
        using var conn = _factory.CreateConnection();
        await conn.ExecuteAsync("UPDATE Benh SET IsActive=0 WHERE Id=@Id", new { Id = id });
    }
}
