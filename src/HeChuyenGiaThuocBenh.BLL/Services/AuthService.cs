using BCrypt.Net;
using HeChuyenGiaThuocBenh.DAL.Repositories;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.BLL.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;

    public AuthService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<User?> LoginAsync(string username, string password)
    {
        var user = await _userRepo.GetByUsernameAsync(username);
        if (user == null) return null;
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;
        return user;
    }

    public async Task<bool> RegisterAsync(User user, string plainPassword)
    {
        var existing = await _userRepo.GetByUsernameAsync(user.Username);
        if (existing != null) return false;

        user.PasswordHash = HashPassword(plainPassword);
        await _userRepo.CreateAsync(user);
        return true;
    }

    public async Task ChangePasswordAsync(int userId, string newPassword)
    {
        var user = await _userRepo.GetByIdAsync(userId);
        if (user == null) throw new InvalidOperationException("User not found");

        user.PasswordHash = HashPassword(newPassword);
        await _userRepo.UpdateAsync(user);
    }

    public string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password, workFactor: 11);

    public bool VerifyPassword(string password, string hash)
        => BCrypt.Net.BCrypt.Verify(password, hash);
}
