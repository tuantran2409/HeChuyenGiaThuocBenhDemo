using HeChuyenGiaThuocBenh.DAL.Repositories;
using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.BLL.Services;

/// <summary>
/// Forward chaining inference engine.
/// Scores each disease by matching input symptoms against rules (BenhTrieuChung).
/// Rules with BatBuoc=true must match; others contribute weighted score.
/// </summary>
public class InferenceService : IInferenceService
{
    private readonly IBenhRepository _benhRepo;
    private readonly IThuocRepository _thuocRepo;
    private readonly ITuongTacThuocRepository _tuongTacRepo;

    private const double MIN_CONFIDENCE = 0.4;

    public InferenceService(
        IBenhRepository benhRepo,
        IThuocRepository thuocRepo,
        ITuongTacThuocRepository tuongTacRepo)
    {
        _benhRepo = benhRepo;
        _thuocRepo = thuocRepo;
        _tuongTacRepo = tuongTacRepo;
    }

    public async Task<List<ChanDoanResult>> ChanDoanAsync(IEnumerable<int> trieuChungIds)
    {
        var inputSet = trieuChungIds.ToHashSet();
        var candidateDiseases = await _benhRepo.GetByTrieuChungIdsAsync(inputSet);

        var results = new List<ChanDoanResult>();

        foreach (var benh in candidateDiseases)
        {
            var rules = (await _benhRepo.GetBenhTrieuChungAsync(benh.Id)).ToList();

            // Bắt buộc: tất cả triệu chứng bắt buộc phải có trong input
            bool mandatoryMet = rules
                .Where(r => r.BatBuoc)
                .All(r => inputSet.Contains(r.TrieuChungId));

            if (!mandatoryMet) continue;

            double totalWeight = rules.Sum(r => (double)r.TrongSo);
            double matchedWeight = rules
                .Where(r => inputSet.Contains(r.TrieuChungId))
                .Sum(r => (double)r.TrongSo);

            double confidence = totalWeight > 0 ? matchedWeight / totalWeight : 0;
            if (confidence < MIN_CONFIDENCE) continue;

            var thuocList = (await _thuocRepo.GetByBenhIdAsync(benh.Id)).ToList();
            var thuocIds = thuocList.Select(t => t.Id).ToList();
            var canhBao = (await _tuongTacRepo.CheckMultipleInteractionsAsync(thuocIds)).ToList();

            var benhDetails = await _benhRepo.GetByIdWithDetailsAsync(benh.Id);

            results.Add(new ChanDoanResult
            {
                Benh = benhDetails ?? benh,
                DoTinCay = confidence,
                ThuocGoiY = thuocList,
                CanhBao = canhBao
            });
        }

        return results.OrderByDescending(r => r.DoTinCay).ToList();
    }
}
