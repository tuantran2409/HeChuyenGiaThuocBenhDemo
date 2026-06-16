using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.BLL;

public static class AppSession
{
    public static User? CurrentUser { get; private set; }
    public static bool IsLoggedIn => CurrentUser != null;

    public static void Login(User user) => CurrentUser = user;
    public static void Logout() => CurrentUser = null;

    public static bool IsAdmin => CurrentUser?.Role == UserRole.Admin;
    public static bool IsBacSi => CurrentUser?.Role == UserRole.BacSi;
    public static bool IsDuocSi => CurrentUser?.Role == UserRole.DuocSi;
}
