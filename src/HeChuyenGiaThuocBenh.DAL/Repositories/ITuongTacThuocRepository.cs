using HeChuyenGiaThuocBenh.Models;

namespace HeChuyenGiaThuocBenh.DAL.Repositories;

public interface ITuongTacThuocRepository
{
    Task<IEnumerable<TuongTacThuoc>> GetAllAsync();
    Task<TuongTacThuoc?> CheckInteractionAsync(int thuocId1, int thuocId2);
    Task<IEnumerable<TuongTacThuoc>> CheckMultipleInteractionsAsync(IEnumerable<int> thuocIds);
}
