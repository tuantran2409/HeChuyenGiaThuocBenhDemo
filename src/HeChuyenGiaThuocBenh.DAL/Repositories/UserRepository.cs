using Dapper;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbConnectionFactory _factory;

    public UserRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Username = @Username AND IsActive = 1",
            new { Username = username });
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Id = @Id",
            new { Id = id });
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<User>("SELECT * FROM Users ORDER BY HoTen");
    }

    public async Task<int> CreateAsync(User user)
    {
        using var conn = _factory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(@"
            INSERT INTO Users (Username, PasswordHash, HoTen, Email, Role, IsActive, CreatedAt)
            OUTPUT INSERTED.Id
            VALUES (@Username, @PasswordHash, @HoTen, @Email, @Role, @IsActive, @CreatedAt)",
            user);
    }

    public async Task UpdateAsync(User user)
    {
        using var conn = _factory.CreateConnection();
        await conn.ExecuteAsync(@"
            UPDATE Users SET HoTen=@HoTen, Email=@Email, Role=@Role
            WHERE Id=@Id",
            user);
    }

    public async Task SetActiveAsync(int id, bool isActive)
    {
        using var conn = _factory.CreateConnection();
        await conn.ExecuteAsync(
            "UPDATE Users SET IsActive=@IsActive WHERE Id=@Id",
            new { Id = id, IsActive = isActive });
    }
}
