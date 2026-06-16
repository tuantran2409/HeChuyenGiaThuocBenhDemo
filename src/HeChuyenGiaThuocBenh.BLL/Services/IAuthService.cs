using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.BLL.Services;

public interface IAuthService
{
    Task<User?> LoginAsync(string username, string password);
    Task<bool> RegisterAsync(User user, string plainPassword);
    Task ChangePasswordAsync(int userId, string newPassword);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}
