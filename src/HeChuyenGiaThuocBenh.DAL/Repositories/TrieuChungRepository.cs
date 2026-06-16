using Dapper;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public class TrieuChungRepository : ITrieuChungRepository
{
    private readonly DbConnectionFactory _factory;

    public TrieuChungRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<TrieuChung>> GetAllAsync()
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<TrieuChung>(
            "SELECT * FROM TrieuChung ORDER BY NhomTrieuChung, Ten");
    }

    public async Task<IEnumerable<TrieuChung>> GetByNhomAsync(string nhom)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<TrieuChung>(
            "SELECT * FROM TrieuChung WHERE NhomTrieuChung=@Nhom ORDER BY Ten",
            new { Nhom = nhom });
    }

    public async Task<TrieuChung?> GetByIdAsync(int id)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TrieuChung>(
            "SELECT * FROM TrieuChung WHERE Id=@Id", new { Id = id });
    }
}
