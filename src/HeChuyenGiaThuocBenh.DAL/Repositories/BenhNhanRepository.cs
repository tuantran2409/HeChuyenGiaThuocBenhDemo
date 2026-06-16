using Dapper;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public class BenhNhanRepository : IBenhNhanRepository
{
    private readonly DbConnectionFactory _factory;

    public BenhNhanRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<BenhNhan>> GetAllAsync()
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<BenhNhan>(
            "SELECT * FROM BenhNhan ORDER BY HoTen");
    }

    public async Task<BenhNhan?> GetByIdAsync(int id)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<BenhNhan>(
            "SELECT * FROM BenhNhan WHERE Id=@Id", new { Id = id });
    }

    public async Task<IEnumerable<BenhNhan>> SearchAsync(string keyword)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<BenhNhan>(@"
            SELECT * FROM BenhNhan
            WHERE HoTen LIKE @kw OR SoDienThoai LIKE @kw
            ORDER BY HoTen",
            new { kw = $"%{keyword}%" });
    }

    public async Task<int> CreateAsync(BenhNhan benhNhan)
    {
        using var conn = _factory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(@"
            INSERT INTO BenhNhan (HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, TienSuBenh, DiUng, CreatedAt)
            OUTPUT INSERTED.Id
            VALUES (@HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @DiaChi, @TienSuBenh, @DiUng, @CreatedAt)",
            benhNhan);
    }

    public async Task UpdateAsync(BenhNhan benhNhan)
    {
        using var conn = _factory.CreateConnection();
        await conn.ExecuteAsync(@"
            UPDATE BenhNhan SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh,
            SoDienThoai=@SoDienThoai, DiaChi=@DiaChi, TienSuBenh=@TienSuBenh, DiUng=@DiUng
            WHERE Id=@Id",
            benhNhan);
    }
}
