-- Fix TuongTacThuoc: seed_data.sql had 3 rows violating CHECK (ThuocId1 < ThuocId2),
-- causing the entire INSERT to fail and leaving the table empty.
-- Run this script once on any existing database that ran the original seed_data.sql.

USE HeChuyenGiaThuocBenh;
GO

IF NOT EXISTS (SELECT 1 FROM TuongTacThuoc)
BEGIN
    INSERT INTO TuongTacThuoc (ThuocId1, ThuocId2, MucDo, MoTa, HauQua) VALUES
    (2, 3, 3, N'Ibuprofen + Aspirin', N'Tăng nguy cơ xuất huyết tiêu hóa, giảm hiệu quả chống kết tập tiểu cầu của Aspirin'),
    (3, 4, 2, N'Aspirin + Diclofenac', N'Tăng tác dụng phụ tiêu hóa, nguy cơ xuất huyết'),
    (8, 11, 2, N'Ciprofloxacin + Metronidazole', N'Tăng nguy cơ tác dụng phụ thần kinh'),
    (13, 14, 1, N'Omeprazole + Pantoprazole', N'Không nên dùng 2 PPI cùng lúc, không có lợi thêm'),
    (27, 28, 1, N'Amlodipine + Enalapril', N'Hạ huyết áp cộng thêm - cần điều chỉnh liều'),
    (31, 32, 2, N'Metformin + Glipizide', N'Tăng nguy cơ hạ đường huyết, cần theo dõi chặt'),
    (36, 38, 2, N'Fluoxetine + Diazepam', N'Tăng tác dụng an thần của Diazepam'),
    (2, 31, 2, N'Ibuprofen + Metformin', N'NSAIDs có thể làm giảm tác dụng của Metformin, tăng nguy cơ suy thận'),
    (30, 31, 1, N'Metoprolol + Metformin', N'Metoprolol che lấp triệu chứng hạ đường huyết'),
    (29, 31, 1, N'Atorvastatin + Metformin', N'Atorvastatin có thể tăng nhẹ đường huyết'),
    (7, 12, 1, N'Azithromycin + Clarithromycin', N'Không dùng 2 Macrolide cùng lúc, không tăng hiệu quả'),
    (8, 9, 2, N'Ciprofloxacin + Doxycycline', N'Tương tác kháng khuẩn, không nên phối hợp thường quy');

    PRINT 'TuongTacThuoc: 12 rows inserted.';
END
ELSE
BEGIN
    PRINT 'TuongTacThuoc already has data - skipped.';
END
GO

-- Fix BenhTrieuChung: over-strict mandatory flags causing diagnosis to return no results.
-- Cảm cúm  (BenhId=1): Ho (TrieuChungId=7) was mandatory - patients often lack cough early.
-- Viêm họng (BenhId=3): Viêm họng symptom (Id=14) same name as disease - confusing as mandatory.
-- Viêm phổi (BenhId=4): Khó thở (Id=9) removed from mandatory - 3 mandatory was too strict.
UPDATE BenhTrieuChung SET BatBuoc=0 WHERE BenhId=1 AND TrieuChungId=7;
UPDATE BenhTrieuChung SET BatBuoc=0 WHERE BenhId=3 AND TrieuChungId=14;
UPDATE BenhTrieuChung SET BatBuoc=0 WHERE BenhId=4 AND TrieuChungId=9;
PRINT 'BenhTrieuChung: mandatory flags updated.';
GO
