using HeChuyenGiaThuocBenh.DAL;
using HeChuyenGiaThuocBenh.DAL.Repositories;

namespace HeChuyenGiaThuocBenh.Tests.Integration;

/// <summary>
/// Basic smoke tests for DAL repositories against real seed DB.
/// Requires schema.sql + seed_data.sql applied to localhost\SQLEXPRESS.
/// </summary>
[TestClass]
public class RepositoryIntegrationTests
{
    private static DbConnectionFactory _factory = null!;

    [ClassInitialize]
    public static void ClassInit(TestContext _)
        => _factory = new DbConnectionFactory(TestConfig.ConnectionString);

    // ────────────── BenhRepository ──────────────

    [TestMethod]
    public async Task BenhRepository_GetAll_ReturnsActiveDisease()
    {
        var repo = new BenhRepository(_factory);
        var diseases = (await repo.GetAllAsync()).ToList();

        Assert.IsTrue(diseases.Count > 0, "Seed data should have active diseases");
        Assert.IsTrue(diseases.All(b => b.IsActive), "GetAllAsync should only return IsActive=1");
    }

    [TestMethod]
    public async Task BenhRepository_GetByIdWithDetails_ReturnsCamCum()
    {
        var repo = new BenhRepository(_factory);
        var benh = await repo.GetByIdWithDetailsAsync(1); // Cảm cúm

        Assert.IsNotNull(benh);
        Assert.AreEqual(1, benh.Id);
    }

    [TestMethod]
    public async Task BenhRepository_GetBenhTrieuChung_CamCumHasRules()
    {
        var repo = new BenhRepository(_factory);
        var rules = (await repo.GetBenhTrieuChungAsync(1)).ToList(); // Cảm cúm

        Assert.IsTrue(rules.Count > 0, "Cảm cúm should have symptom rules");
        Assert.IsTrue(rules.Any(r => r.BatBuoc), "At least one rule should be mandatory");
    }

    [TestMethod]
    public async Task BenhRepository_GetByTrieuChungIds_Sot_ReturnsCamCum()
    {
        var repo = new BenhRepository(_factory);
        var diseases = (await repo.GetByTrieuChungIdsAsync(new[] { 1 })).ToList();

        Assert.IsTrue(diseases.Any(b => b.Id == 1), "Sốt (id=1) should link to Cảm cúm (id=1)");
    }

    // ────────────── ThuocRepository ──────────────

    [TestMethod]
    public async Task ThuocRepository_GetAll_Returns100PlusDrugs()
    {
        var repo = new ThuocRepository(_factory);
        var drugs = (await repo.GetAllAsync()).ToList();

        Assert.IsTrue(drugs.Count >= 100, $"Expected 100+ drugs, got {drugs.Count}");
    }

    [TestMethod]
    public async Task ThuocRepository_GetById_ReturnsCorrectDrug()
    {
        var repo = new ThuocRepository(_factory);
        var drug = await repo.GetByIdAsync(1);

        Assert.IsNotNull(drug);
        Assert.AreEqual(1, drug.Id);
    }

    [TestMethod]
    public async Task ThuocRepository_Search_ByKeyword_ReturnsResults()
    {
        var repo = new ThuocRepository(_factory);
        var results = (await repo.SearchAsync("para")).ToList(); // Paracetamol

        Assert.IsTrue(results.Count > 0, "Search 'para' should find Paracetamol-based drugs");
    }

    [TestMethod]
    public async Task ThuocRepository_GetByBenhId_CamCumHasDrugs()
    {
        var repo = new ThuocRepository(_factory);
        var drugs = (await repo.GetByBenhIdAsync(1)).ToList(); // Cảm cúm

        Assert.IsTrue(drugs.Count > 0, "Cảm cúm should have associated drugs in seed data");
    }

    // ────────────── UserRepository ──────────────

    [TestMethod]
    public async Task UserRepository_GetByUsername_AdminExists()
    {
        var repo = new UserRepository(_factory);
        var user = await repo.GetByUsernameAsync("admin");

        Assert.IsNotNull(user);
        Assert.AreEqual("admin", user.Username);
        Assert.IsTrue(user.IsActive);
    }

    [TestMethod]
    public async Task UserRepository_GetAll_ReturnsFourSeedUsers()
    {
        var repo = new UserRepository(_factory);
        var users = (await repo.GetAllAsync()).ToList();

        Assert.IsTrue(users.Count >= 4, "Seed data has 4 users (admin/bacsi1/bacsi2/duocsi1)");
    }

    // ────────────── TrieuChungRepository ──────────────

    [TestMethod]
    public async Task TrieuChungRepository_GetAll_Returns50PlusSymptoms()
    {
        var repo = new TrieuChungRepository(_factory);
        var symptoms = (await repo.GetAllAsync()).ToList();

        Assert.IsTrue(symptoms.Count >= 50, $"Expected 50+ symptoms, got {symptoms.Count}");
    }
}
