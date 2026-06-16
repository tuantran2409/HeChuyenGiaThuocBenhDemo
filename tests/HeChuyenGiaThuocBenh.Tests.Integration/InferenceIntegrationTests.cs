using HeChuyenGiaThuocBenh.BLL.Services;
using HeChuyenGiaThuocBenh.DAL;
using HeChuyenGiaThuocBenh.DAL.Repositories;

namespace HeChuyenGiaThuocBenh.Tests.Integration;

/// <summary>
/// Requires live HeChuyenGiaThuocBenh DB with seed data applied.
/// Symptom IDs match seed_data.sql INSERT order (1-based, sequential).
/// </summary>
[TestClass]
public class InferenceIntegrationTests
{
    private static InferenceService _svc = null!;

    [ClassInitialize]
    public static void ClassInit(TestContext _)
    {
        var factory = new DbConnectionFactory(TestConfig.ConnectionString);
        _svc = new InferenceService(
            new BenhRepository(factory),
            new ThuocRepository(factory),
            new TuongTacThuocRepository(factory));
    }

    [TestMethod]
    public async Task ChanDoan_CamCum_SymptomSot_ReturnsResult()
    {
        // TrieuChung id=1 (Sốt) is mandatory for Cảm cúm (BenhId=1)
        var results = await _svc.ChanDoanAsync(new[] { 1 });

        Assert.IsTrue(results.Count > 0, "Expected at least one diagnosis result");
        bool hasCamCum = results.Any(r => r.Benh.Id == 1);
        Assert.IsTrue(hasCamCum, "Cảm cúm (Id=1) should be in results when Sốt (Id=1) is input");
    }

    [TestMethod]
    public async Task ChanDoan_CamCum_ConfidenceAboveThreshold()
    {
        // Sốt (1) is mandatory+weight 2.0; total weight 5.0 → confidence = 2/5 = 40% (on threshold)
        // Include ớn lạnh (3) + mệt mỏi (4) → confidence = 5/5 = 100%
        var results = await _svc.ChanDoanAsync(new[] { 1, 3, 4 });

        var camCum = results.FirstOrDefault(r => r.Benh.Id == 1);
        Assert.IsNotNull(camCum, "Cảm cúm should be returned");
        Assert.IsTrue(camCum.DoTinCay >= 0.4, $"Confidence {camCum.DoTinCay:P0} should be >= 40%");
    }

    [TestMethod]
    public async Task ChanDoan_TieuChay_SymptomTieuChay_ReturnsResult()
    {
        // TrieuChung id=18 (Tiêu chảy) is mandatory for Tiêu chảy cấp (BenhId=11)
        var results = await _svc.ChanDoanAsync(new[] { 18 });

        Assert.IsTrue(results.Count > 0);
        bool hasTieuChay = results.Any(r => r.Benh.Id == 11);
        Assert.IsTrue(hasTieuChay, "Tiêu chảy cấp (Id=11) should be in results");
    }

    [TestMethod]
    public async Task ChanDoan_ViemHong_BothMandatoryRequired()
    {
        // Viêm họng (Id=3): mandatory = đau họng (13) AND viêm họng (14)
        // Only one mandatory → should NOT match
        var onlyDauHong = await _svc.ChanDoanAsync(new[] { 13 });
        bool hasViemHong = onlyDauHong.Any(r => r.Benh.Id == 3);
        Assert.IsFalse(hasViemHong, "Viêm họng should NOT match with only one mandatory symptom");

        // Both mandatory present → should match
        var bothMandatory = await _svc.ChanDoanAsync(new[] { 13, 14 });
        Assert.IsTrue(bothMandatory.Any(r => r.Benh.Id == 3),
            "Viêm họng should match when both mandatory symptoms present");
    }

    [TestMethod]
    public async Task ChanDoan_EmptySymptoms_ReturnsNoResults()
    {
        var results = await _svc.ChanDoanAsync(Array.Empty<int>());

        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    public async Task ChanDoan_ResultsIncludeThuocGoiY()
    {
        // Seed data has BenhThuoc mappings for 10 diseases; Cảm cúm (1) should have drugs
        var results = await _svc.ChanDoanAsync(new[] { 1, 3, 4 });

        var camCum = results.FirstOrDefault(r => r.Benh.Id == 1);
        Assert.IsNotNull(camCum);
        Assert.IsTrue(camCum.ThuocGoiY.Count > 0, "Cảm cúm should have associated drugs");
    }

    [TestMethod]
    public async Task ChanDoan_OrderedByDoTinCayDescending()
    {
        // Input symptoms hitting multiple diseases — verify ordering
        var results = await _svc.ChanDoanAsync(new[] { 1, 3, 4, 5, 7, 11, 12, 13, 14 });

        for (int i = 0; i < results.Count - 1; i++)
        {
            Assert.IsTrue(results[i].DoTinCay >= results[i + 1].DoTinCay,
                $"Result[{i}].DoTinCay ({results[i].DoTinCay:P0}) should >= Result[{i + 1}].DoTinCay ({results[i + 1].DoTinCay:P0})");
        }
    }
}
