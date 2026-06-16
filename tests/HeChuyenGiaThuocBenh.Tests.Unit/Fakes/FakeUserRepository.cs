using HeChuyenGiaThuocBenh.DAL.Repositories;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.Tests.Unit.Fakes;

internal class FakeUserRepository : IUserRepository
{
    private readonly List<User> _users;

    public FakeUserRepository(List<User>? users = null)
    {
        _users = users ?? new List<User>();
    }

    public Task<User?> GetByUsernameAsync(string username)
        => Task.FromResult(_users.FirstOrDefault(u => u.Username == username && u.IsActive));

    public Task<User?> GetByIdAsync(int id)
        => Task.FromResult(_users.FirstOrDefault(u => u.Id == id));

    public Task<IEnumerable<User>> GetAllAsync()
        => Task.FromResult<IEnumerable<User>>(_users);

    public Task<int> CreateAsync(User user)
    {
        int newId = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
        user.Id = newId;
        _users.Add(user);
        return Task.FromResult(newId);
    }

    public Task UpdateAsync(User user)
    {
        var existing = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existing != null)
        {
            existing.HoTen = user.HoTen;
            existing.Email = user.Email;
            existing.Role = user.Role;
            existing.PasswordHash = user.PasswordHash;
        }
        return Task.CompletedTask;
    }

    public Task SetActiveAsync(int id, bool isActive)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null) user.IsActive = isActive;
        return Task.CompletedTask;
    }

    public Task ResetPasswordAsync(int id, string newHash)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null) user.PasswordHash = newHash;
        return Task.CompletedTask;
    }
}
