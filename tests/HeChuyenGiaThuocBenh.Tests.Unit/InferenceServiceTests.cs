using HeChuyenGiaThuocBenh.BLL.Services;
using HeChuyenGiaThuocBenh.Models;
using HeChuyenGiaThuocBenh.Tests.Unit.Fakes;

namespace HeChuyenGiaThuocBenh.Tests.Unit;

[TestClass]
public class InferenceServiceTests
{
    // Disease 1: mandatory symptom 10, optional symptom 20 (weight 2.0)
    // Total weight = 3.0 + 2.0 = 5.0; confidence with only mandatory = 3.0/5.0 = 60%
    private static readonly Benh _disease1 = new() { Id = 1, Ten = "Bệnh A", IsActive = true };
    private static readonly List<BenhTrieuChung> _rulesD1 = new()
    {
        new() { BenhId = 1, TrieuChungId = 10, TrongSo = 3.0m, BatBuoc = true },
        new() { BenhId = 1, TrieuChungId = 20, TrongSo = 2.0m, BatBuoc = false },
    };

    // Disease 2: two mandatory symptoms (10 and 30), total weight 6.0
    private static readonly Benh _disease2 = new() { Id = 2, Ten = "Bệnh B", IsActive = true };
    private static readonly List<BenhTrieuChung> _rulesD2 = new()
    {
        new() { BenhId = 2, TrieuChungId = 10, TrongSo = 3.0m, BatBuoc = true },
        new() { BenhId = 2, TrieuChungId = 30, TrongSo = 3.0m, BatBuoc = true },
    };

    // Disease 3: no mandatory, but very low optional weight → confidence < 40%
    private static readonly Benh _disease3 = new() { Id = 3, Ten = "Bệnh C", IsActive = true };
    private static readonly List<BenhTrieuChung> _rulesD3 = new()
    {
        new() { BenhId = 3, TrieuChungId = 10, TrongSo = 1.0m, BatBuoc = false },
        new() { BenhId = 3, TrieuChungId = 99, TrongSo = 9.0m, BatBuoc = false }, // input won't have this
    };

    private static InferenceService MakeService(
        List<Benh> diseases,
        Dictionary<int, List<BenhTrieuChung>> rules,
        Dictionary<int, List<Thuoc>>? thuoc = null,
        List<TuongTacThuoc>? interactions = null)
    {
        return new InferenceService(
            new FakeBenhRepository(diseases, rules),
            new FakeThuocRepository(thuoc),
            new FakeTuongTacThuocRepository(interactions));
    }

    [TestMethod]
    public async Task ChanDoan_EmptyInput_ReturnsEmptyList()
    {
        var svc = MakeService(new() { _disease1 }, new() { { 1, _rulesD1 } });

        var result = await svc.ChanDoanAsync(Array.Empty<int>());

        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public async Task ChanDoan_MandatorySymptomMissing_ExcludesDisease()
    {
        var svc = MakeService(new() { _disease1 }, new() { { 1, _rulesD1 } });

        // symptom 20 present but NOT 10 (mandatory)
        var result = await svc.ChanDoanAsync(new[] { 20 });

        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public async Task ChanDoan_AllMandatoryMatched_ReturnsDisease()
    {
        var svc = MakeService(new() { _disease1 }, new() { { 1, _rulesD1 } });

        // symptom 10 = mandatory, confidence = 3/5 = 60% > 40%
        var result = await svc.ChanDoanAsync(new[] { 10 });

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(1, result[0].Benh.Id);
    }

    [TestMethod]
    public async Task ChanDoan_AllSymptomsMatched_ConfidenceIs100Percent()
    {
        var svc = MakeService(new() { _disease1 }, new() { { 1, _rulesD1 } });

        var result = await svc.ChanDoanAsync(new[] { 10, 20 });

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(1.0, result[0].DoTinCay, 1e-9);
    }

    [TestMethod]
    public async Task ChanDoan_ConfidenceBelowThreshold_ExcludesDisease()
    {
        // D3: only symptom 10 matched (weight 1.0), total 10.0 → confidence = 10% < 40%
        var svc = MakeService(new() { _disease3 }, new() { { 3, _rulesD3 } });

        var result = await svc.ChanDoanAsync(new[] { 10 });

        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public async Task ChanDoan_OneMandatoryMissing_ExcludesDisease()
    {
        var svc = MakeService(new() { _disease2 }, new() { { 2, _rulesD2 } });

        // only symptom 10 present, missing mandatory 30
        var result = await svc.ChanDoanAsync(new[] { 10 });

        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public async Task ChanDoan_MultipleDiseases_OrderedByConfidenceDesc()
    {
        // D1: input {10,20} → confidence 100%
        // D2: input {10,30} also → confidence 100% but D1 picked first (stable sort)
        // More interesting: D1 gets {10,20} = 100%, D3 gets {10} = 10% (filtered out)
        // So only D1 should remain
        var diseases = new List<Benh> { _disease1, _disease2 };
        var rules = new Dictionary<int, List<BenhTrieuChung>>
        {
            { 1, _rulesD1 }, // mandatory 10, optional 20
            { 2, _rulesD2 }, // mandatory 10 AND 30
        };
        var svc = MakeService(diseases, rules);

        // input has 10 and 20 but NOT 30 → D1 matches (confidence 100%), D2 excluded (missing 30)
        var result = await svc.ChanDoanAsync(new[] { 10, 20 });

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(1, result[0].Benh.Id);
    }

    [TestMethod]
    public async Task ChanDoan_TwoDiseases_HigherConfidenceFirst()
    {
        // Disease 4: mandatory 10 (weight 2), optional 20 (weight 2) → total 4
        // Disease 5: mandatory 10 (weight 3) → total 3, confidence 100% with input {10,20}
        // D4 with input {10,20} → confidence = 4/4 = 100%
        // D5 with input {10,20} → confidence = 3/3 = 100%
        // Make D4 lower by tweaking: D4 mandatory 10 (3), optional 20 (7) → with {10} only = 3/10 = 30% excluded
        // D5: mandatory 10 (3) only → with {10} = 100%
        var d4 = new Benh { Id = 4, Ten = "D4", IsActive = true };
        var d5 = new Benh { Id = 5, Ten = "D5", IsActive = true };
        var r4 = new List<BenhTrieuChung>
        {
            new() { BenhId = 4, TrieuChungId = 10, TrongSo = 3.0m, BatBuoc = true },
            new() { BenhId = 4, TrieuChungId = 20, TrongSo = 2.0m, BatBuoc = false },
        };
        var r5 = new List<BenhTrieuChung>
        {
            new() { BenhId = 5, TrieuChungId = 10, TrongSo = 3.0m, BatBuoc = true },
        };
        var svc = MakeService(
            new() { d4, d5 },
            new() { { 4, r4 }, { 5, r5 } });

        // input {10}: D4 confidence = 3/5 = 60%, D5 = 3/3 = 100%
        var result = await svc.ChanDoanAsync(new[] { 10 });

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(5, result[0].Benh.Id, "D5 should be first (higher confidence)");
        Assert.AreEqual(4, result[1].Benh.Id);
        Assert.IsTrue(result[0].DoTinCay > result[1].DoTinCay);
    }

    [TestMethod]
    public async Task ChanDoan_ReturnsAssociatedThuoc()
    {
        var thuocList = new List<Thuoc>
        {
            new() { Id = 100, Ten = "Thuoc X", HoatChat = "X" }
        };
        var svc = MakeService(
            new() { _disease1 },
            new() { { 1, _rulesD1 } },
            new Dictionary<int, List<Thuoc>> { { 1, thuocList } });

        var result = await svc.ChanDoanAsync(new[] { 10 });

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(1, result[0].ThuocGoiY.Count);
        Assert.AreEqual(100, result[0].ThuocGoiY[0].Id);
    }

    [TestMethod]
    public async Task ChanDoan_ReturnsInteractionWarnings()
    {
        var thuocList = new List<Thuoc>
        {
            new() { Id = 101, Ten = "T1", HoatChat = "A" },
            new() { Id = 102, Ten = "T2", HoatChat = "B" },
        };
        var interaction = new TuongTacThuoc
        {
            ThuocId1 = 101, ThuocId2 = 102,
            MoTa = "Tương tác nghiêm trọng",
            MucDo = MucDoTuongTac.Nang
        };
        var svc = MakeService(
            new() { _disease1 },
            new() { { 1, _rulesD1 } },
            new Dictionary<int, List<Thuoc>> { { 1, thuocList } },
            new List<TuongTacThuoc> { interaction });

        var result = await svc.ChanDoanAsync(new[] { 10 });

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(1, result[0].CanhBao.Count);
        Assert.AreEqual(MucDoTuongTac.Nang, result[0].CanhBao[0].MucDo);
    }
}
