using HeChuyenGiaThuocBenh.BLL.Services;
using HeChuyenGiaThuocBenh.DAL;
using HeChuyenGiaThuocBenh.DAL.Repositories;

namespace HeChuyenGiaThuocBenh.BLL;

/// <summary>
/// Simple service locator / DI root. Wired manually to avoid adding a DI framework.
/// </summary>
public static class ServiceContainer
{
    private static DbConnectionFactory? _factory;

    public static void Initialize(string connectionString)
    {
        _factory = new DbConnectionFactory(connectionString);
    }

    public static IAuthService AuthService
        => new AuthService(new UserRepository(_factory!));

    public static IInferenceService InferenceService
        => new InferenceService(
            new BenhRepository(_factory!),
            new ThuocRepository(_factory!),
            new TuongTacThuocRepository(_factory!));

    public static IUserRepository UserRepository => new UserRepository(_factory!);
    public static IThuocRepository ThuocRepository => new ThuocRepository(_factory!);
    public static IBenhRepository BenhRepository => new BenhRepository(_factory!);
    public static ITrieuChungRepository TrieuChungRepository => new TrieuChungRepository(_factory!);
    public static IBenhNhanRepository BenhNhanRepository => new BenhNhanRepository(_factory!);
    public static ILichSuChanDoanRepository LichSuChanDoanRepository => new LichSuChanDoanRepository(_factory!);
    public static ITuongTacThuocRepository TuongTacThuocRepository => new TuongTacThuocRepository(_factory!);
    public static IBaoCaoService BaoCaoService => new BaoCaoService(new LichSuChanDoanRepository(_factory!));
}
