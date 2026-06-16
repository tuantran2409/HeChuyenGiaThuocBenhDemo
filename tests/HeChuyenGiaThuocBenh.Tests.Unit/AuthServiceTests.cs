using HeChuyenGiaThuocBenh.BLL.Services;
using HeChuyenGiaThuocBenh.Models;
using HeChuyenGiaThuocBenh.Tests.Unit.Fakes;

namespace HeChuyenGiaThuocBenh.Tests.Unit;

[TestClass]
public class AuthServiceTests
{
    private static AuthService MakeService(List<User>? users = null)
        => new AuthService(new FakeUserRepository(users));

    private static User MakeUser(int id, string username, string plainPassword, bool active = true) => new()
    {
        Id = id,
        Username = username,
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword, workFactor: 4), // low factor for speed in tests
        HoTen = "Test User",
        IsActive = active,
        Role = UserRole.BacSi,
    };

    [TestMethod]
    public async Task Login_CorrectCredentials_ReturnsUser()
    {
        var svc = MakeService(new() { MakeUser(1, "bacsi1", "BacSi@123") });

        var result = await svc.LoginAsync("bacsi1", "BacSi@123");

        Assert.IsNotNull(result);
        Assert.AreEqual("bacsi1", result.Username);
    }

    [TestMethod]
    public async Task Login_WrongPassword_ReturnsNull()
    {
        var svc = MakeService(new() { MakeUser(1, "bacsi1", "BacSi@123") });

        var result = await svc.LoginAsync("bacsi1", "WrongPassword");

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Login_UnknownUsername_ReturnsNull()
    {
        var svc = MakeService(new() { MakeUser(1, "bacsi1", "BacSi@123") });

        var result = await svc.LoginAsync("unknown", "BacSi@123");

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Login_InactiveUser_ReturnsNull()
    {
        var svc = MakeService(new() { MakeUser(1, "bacsi1", "BacSi@123", active: false) });

        var result = await svc.LoginAsync("bacsi1", "BacSi@123");

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Register_NewUsername_ReturnsTrue()
    {
        var svc = MakeService(new());
        var newUser = new User { Username = "newdoc", HoTen = "New Doctor" };

        var result = await svc.RegisterAsync(newUser, "Password123");

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task Register_DuplicateUsername_ReturnsFalse()
    {
        var svc = MakeService(new() { MakeUser(1, "bacsi1", "BacSi@123") });
        var duplicate = new User { Username = "bacsi1", HoTen = "Duplicate" };

        var result = await svc.RegisterAsync(duplicate, "SomePassword");

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task Register_HashesPasswordBeforeStore()
    {
        var repo = new FakeUserRepository(new());
        var svc = new AuthService(repo);
        var newUser = new User { Username = "testuser", HoTen = "Test" };

        await svc.RegisterAsync(newUser, "MyPassword");

        var stored = await repo.GetByUsernameAsync("testuser");
        Assert.IsNotNull(stored);
        Assert.AreNotEqual("MyPassword", stored.PasswordHash, "Should store hash, not plain text");
        Assert.IsTrue(BCrypt.Net.BCrypt.Verify("MyPassword", stored.PasswordHash));
    }

    [TestMethod]
    public void HashPassword_ProducesVerifiableHash()
    {
        var svc = MakeService();
        const string plain = "Test@Password123";

        var hash = svc.HashPassword(plain);

        Assert.AreNotEqual(plain, hash);
        Assert.IsTrue(svc.VerifyPassword(plain, hash));
    }

    [TestMethod]
    public void VerifyPassword_WrongPassword_ReturnsFalse()
    {
        var svc = MakeService();
        var hash = svc.HashPassword("CorrectPassword");

        Assert.IsFalse(svc.VerifyPassword("WrongPassword", hash));
    }

    [TestMethod]
    public void HashPassword_TwoCalls_ProduceDifferentHashes()
    {
        var svc = MakeService();
        const string plain = "SamePassword";

        var hash1 = svc.HashPassword(plain);
        var hash2 = svc.HashPassword(plain);

        Assert.AreNotEqual(hash1, hash2, "BCrypt uses different salts each call");
        Assert.IsTrue(svc.VerifyPassword(plain, hash1));
        Assert.IsTrue(svc.VerifyPassword(plain, hash2));
    }
}
