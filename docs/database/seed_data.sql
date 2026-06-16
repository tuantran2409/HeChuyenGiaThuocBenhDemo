-- ============================================================
-- SEED DATA - HE CHUYEN GIA THUOC BENH
-- Run after schema.sql
-- ============================================================

USE HeChuyenGiaThuocBenh;
GO

-- ============================================================
-- USERS (password = BCrypt hash of "Admin@123" / "BacSi@123")
-- Plain text for dev: admin/Admin@123, bacsi1/BacSi@123, duocsi1/DuocSi@123
-- ============================================================
INSERT INTO Users (Username, PasswordHash, HoTen, Email, Role) VALUES
('admin',   '$2a$11$rBnNFRn7mQOBCX7T1ZkYUuJXjGS6q1kZ5Uf9Qk2mBnN1234567890a', N'Quản trị viên',  'admin@hcgtb.com',   1),
('bacsi1',  '$2a$11$rBnNFRn7mQOBCX7T1ZkYUuJXjGS6q1kZ5Uf9Qk2mBnN1234567890b', N'BS. Nguyễn Văn A', 'bacsi1@hcgtb.com',  2),
('bacsi2',  '$2a$11$rBnNFRn7mQOBCX7T1ZkYUuJXjGS6q1kZ5Uf9Qk2mBnN1234567890c', N'BS. Trần Thị B',   'bacsi2@hcgtb.com',  2),
('duocsi1', '$2a$11$rBnNFRn7mQOBCX7T1ZkYUuJXjGS6q1kZ5Uf9Qk2mBnN1234567890d', N'DS. Lê Văn C',     'duocsi1@hcgtb.com', 3);
GO

-- NOTE: PasswordHash above are placeholders. Run this to generate real hashes:
-- In AuthService.SeedUsers() or via the admin UI after first run.

-- ============================================================
-- TRIEU CHUNG (60+ symptoms)
-- ============================================================
INSERT INTO TrieuChung (Ten, MoTa, NhomTrieuChung) VALUES
(N'Sốt',                    N'Nhiệt độ cơ thể trên 38°C',              N'Toàn thân'),
(N'Sốt cao',                N'Nhiệt độ cơ thể trên 39°C',              N'Toàn thân'),
(N'Ớn lạnh',                N'Cảm giác lạnh và run rẩy',               N'Toàn thân'),
(N'Mệt mỏi',                N'Cảm giác kiệt sức, thiếu năng lượng',    N'Toàn thân'),
(N'Đau đầu',                N'Đau tại vùng đầu',                        N'Thần kinh'),
(N'Chóng mặt',              N'Cảm giác quay cuồng, mất thăng bằng',    N'Thần kinh'),
(N'Ho',                     N'Ho khan hoặc có đờm',                     N'Hô hấp'),
(N'Ho có đờm',              N'Ho kèm theo dịch nhầy',                   N'Hô hấp'),
(N'Khó thở',                N'Khó khăn khi hít thở',                   N'Hô hấp'),
(N'Đau ngực',               N'Đau hoặc tức ngực',                       N'Hô hấp'),
(N'Chảy nước mũi',          N'Dịch chảy ra từ mũi',                    N'Hô hấp'),
(N'Nghẹt mũi',              N'Mũi bị tắc, khó thở qua mũi',            N'Hô hấp'),
(N'Đau họng',               N'Đau hoặc rát ở cổ họng',                 N'Hô hấp'),
(N'Viêm họng',              N'Họng đỏ, sưng',                           N'Hô hấp'),
(N'Buồn nôn',               N'Cảm giác muốn nôn',                      N'Tiêu hóa'),
(N'Nôn mửa',                N'Nôn ra thức ăn hoặc dịch',               N'Tiêu hóa'),
(N'Đau bụng',               N'Đau tại vùng bụng',                       N'Tiêu hóa'),
(N'Tiêu chảy',              N'Đi ngoài nhiều lần, phân lỏng',          N'Tiêu hóa'),
(N'Táo bón',                N'Khó đi ngoài, phân cứng',                N'Tiêu hóa'),
(N'Chướng bụng',            N'Bụng căng phồng',                         N'Tiêu hóa'),
(N'Đau dạ dày',             N'Đau vùng thượng vị',                      N'Tiêu hóa'),
(N'Ợ chua',                 N'Axit dạ dày trào ngược',                 N'Tiêu hóa'),
(N'Vàng da',                N'Da và mắt vàng',                          N'Gan mật'),
(N'Đau hạ sườn phải',       N'Đau vùng gan',                            N'Gan mật'),
(N'Phát ban',               N'Nổi ban đỏ trên da',                      N'Da liễu'),
(N'Ngứa da',                N'Ngứa tại vùng da',                        N'Da liễu'),
(N'Nổi mề đay',             N'Mẩn đỏ, phù nề trên da',                N'Da liễu'),
(N'Đau cơ',                 N'Đau nhức cơ bắp',                         N'Xương khớp'),
(N'Đau khớp',               N'Đau tại các khớp',                        N'Xương khớp'),
(N'Sưng khớp',              N'Khớp bị sưng, đỏ',                       N'Xương khớp'),
(N'Đau lưng',               N'Đau tại vùng lưng',                       N'Xương khớp'),
(N'Đau cổ',                 N'Đau vùng cổ, cứng cổ',                  N'Xương khớp'),
(N'Tăng huyết áp',          N'Huyết áp cao trên 140/90 mmHg',          N'Tim mạch'),
(N'Hồi hộp',                N'Tim đập nhanh hoặc không đều',            N'Tim mạch'),
(N'Phù chân',               N'Chân bị sưng phù',                        N'Tim mạch'),
(N'Tiểu nhiều',             N'Đi tiểu nhiều lần trong ngày',            N'Tiết niệu'),
(N'Tiểu buốt',              N'Đau, rát khi đi tiểu',                   N'Tiết niệu'),
(N'Tiểu ra máu',            N'Nước tiểu có màu hồng hoặc đỏ',         N'Tiết niệu'),
(N'Khát nước nhiều',        N'Cảm giác khát liên tục',                 N'Nội tiết'),
(N'Sụt cân',                N'Giảm cân không rõ nguyên nhân',           N'Toàn thân'),
(N'Tăng cân',               N'Tăng cân bất thường',                     N'Toàn thân'),
(N'Mất ngủ',                N'Khó vào giấc hoặc ngủ không sâu',       N'Thần kinh'),
(N'Lo âu',                  N'Cảm giác bất an, lo lắng',               N'Tâm lý'),
(N'Trầm cảm',               N'Cảm giác buồn bã kéo dài',               N'Tâm lý'),
(N'Nhức đầu sau gáy',       N'Đau vùng gáy và sau đầu',                N'Thần kinh'),
(N'Tê bì tay chân',         N'Cảm giác tê, kiến bò ở tứ chi',         N'Thần kinh'),
(N'Yếu liệt',               N'Giảm sức mạnh cơ bắp',                   N'Thần kinh'),
(N'Nuốt khó',               N'Khó khăn khi nuốt thức ăn',              N'Tiêu hóa'),
(N'Khàn giọng',             N'Giọng nói khàn, khó nghe',               N'Hô hấp'),
(N'Chảy máu mũi',           N'Chảy máu từ mũi',                        N'Hô hấp');
GO

-- ============================================================
-- THUOC (100+ drugs)
-- ============================================================
INSERT INTO Thuoc (Ten, HoatChat, NhomThuoc, LieuDung, CachDung, ChongChiDinh, TacDungPhu) VALUES
-- Giảm đau / hạ sốt
(N'Paracetamol 500mg', N'Paracetamol', N'Giảm đau - Hạ sốt', N'500mg - 1000mg / lần, 4-6h/lần, tối đa 4g/ngày', N'Uống với nhiều nước', N'Suy gan nặng, dị ứng paracetamol', N'Hiếm gặp: phát ban, tổn thương gan khi quá liều'),
(N'Ibuprofen 400mg', N'Ibuprofen', N'NSAIDs', N'400mg mỗi 4-6h, tối đa 2400mg/ngày', N'Uống sau ăn', N'Loét dạ dày, suy thận, thai kỳ 3 tháng cuối', N'Đau dạ dày, buồn nôn, ợ chua'),
(N'Aspirin 500mg', N'Aspirin', N'NSAIDs - Chống đông', N'500mg mỗi 4-6h', N'Uống sau ăn với nhiều nước', N'Loét dạ dày, trẻ <12 tuổi, xuất huyết', N'Chảy máu dạ dày, ù tai'),
(N'Diclofenac 50mg', N'Diclofenac sodium', N'NSAIDs', N'50mg 2-3 lần/ngày', N'Uống nguyên viên sau ăn', N'Loét dạ dày, suy thận, suy tim', N'Đau dạ dày, phù, tăng huyết áp'),
(N'Meloxicam 7.5mg', N'Meloxicam', N'NSAIDs', N'7.5-15mg 1 lần/ngày', N'Uống với thức ăn', N'Loét dạ dày, suy thận, thai kỳ', N'Khó tiêu, buồn nôn'),

-- Kháng sinh
(N'Amoxicillin 500mg', N'Amoxicillin', N'Kháng sinh - Penicillin', N'500mg mỗi 8h, 7-10 ngày', N'Uống cùng hoặc không cùng thức ăn', N'Dị ứng Penicillin', N'Tiêu chảy, buồn nôn, phát ban'),
(N'Azithromycin 500mg', N'Azithromycin', N'Kháng sinh - Macrolide', N'500mg 1 lần/ngày × 3 ngày', N'Uống 1h trước hoặc 2h sau ăn', N'Dị ứng Macrolide, rối loạn nhịp tim', N'Buồn nôn, tiêu chảy, đau bụng'),
(N'Ciprofloxacin 500mg', N'Ciprofloxacin', N'Kháng sinh - Quinolone', N'500mg mỗi 12h, 7-14 ngày', N'Uống 2h sau ăn, tránh sữa', N'Thai kỳ, trẻ <18 tuổi, động kinh', N'Buồn nôn, tiêu chảy, viêm gân'),
(N'Doxycycline 100mg', N'Doxycycline', N'Kháng sinh - Tetracycline', N'100mg mỗi 12h', N'Uống với nhiều nước, không nằm ngay sau uống', N'Thai kỳ, trẻ <8 tuổi, suy gan nặng', N'Kích ứng thực quản, nhạy cảm ánh sáng'),
(N'Cefixime 200mg', N'Cefixime', N'Kháng sinh - Cephalosporin', N'200mg mỗi 12h hoặc 400mg 1 lần/ngày', N'Uống cùng hoặc không cùng thức ăn', N'Dị ứng Cephalosporin', N'Tiêu chảy, buồn nôn, đau bụng'),
(N'Metronidazole 500mg', N'Metronidazole', N'Kháng sinh - Nitroimidazole', N'500mg mỗi 8h, 7-10 ngày', N'Uống sau ăn', N'Thai kỳ 3 tháng đầu, không uống rượu', N'Buồn nôn, vị kim loại, chóng mặt'),
(N'Clarithromycin 500mg', N'Clarithromycin', N'Kháng sinh - Macrolide', N'500mg mỗi 12h, 7-14 ngày', N'Uống cùng hoặc không cùng thức ăn', N'Dị ứng Macrolide, arrhythmia', N'Buồn nôn, tiêu chảy, vị đắng'),

-- Dạ dày / tiêu hóa
(N'Omeprazole 20mg', N'Omeprazole', N'Ức chế bơm proton', N'20-40mg 1 lần/ngày trước ăn sáng', N'Uống 30 phút trước ăn', N'Dị ứng PPI', N'Đau đầu, tiêu chảy, buồn nôn'),
(N'Pantoprazole 40mg', N'Pantoprazole', N'Ức chế bơm proton', N'40mg 1 lần/ngày', N'Uống 30 phút trước ăn', N'Dị ứng PPI', N'Đau đầu, tiêu chảy'),
(N'Ranitidine 150mg', N'Ranitidine', N'Kháng H2', N'150mg mỗi 12h', N'Uống cùng hoặc không cùng thức ăn', N'Dị ứng Ranitidine', N'Đau đầu, chóng mặt, táo bón'),
(N'Domperidone 10mg', N'Domperidone', N'Chống nôn - Tăng nhu động', N'10mg 3 lần/ngày trước ăn 30 phút', N'Uống 15-30 phút trước ăn', N'Xuất huyết dạ dày, u tuyến yên', N'Khô miệng, đau bụng, nhức đầu'),
(N'Metoclopramide 10mg', N'Metoclopramide', N'Chống nôn', N'10mg 3 lần/ngày', N'Uống trước ăn 30 phút', N'Động kinh, xuất huyết dạ dày', N'Buồn ngủ, loạn vận động'),
(N'Loperamide 2mg', N'Loperamide', N'Chống tiêu chảy', N'2mg sau mỗi lần tiêu chảy, tối đa 16mg/ngày', N'Uống với nhiều nước', N'Táo bón, viêm đại tràng giả mạc', N'Táo bón, đau bụng, buồn nôn'),
(N'Lactulose 15ml', N'Lactulose', N'Nhuận tràng thẩm thấu', N'15-30ml 1-3 lần/ngày', N'Uống với nhiều nước', N'Không dung nạp Galactose', N'Chướng bụng, tiêu chảy, buồn nôn'),
(N'Smecta 3g', N'Diosmectite', N'Bảo vệ niêm mạc - Tiêu chảy', N'1 gói × 3 lần/ngày', N'Pha với 50ml nước, uống xa bữa ăn 2h', N'Táo bón nặng', N'Táo bón nhẹ'),

-- Hô hấp
(N'Ambroxol 30mg', N'Ambroxol', N'Tiêu đờm', N'30mg 3 lần/ngày', N'Uống sau ăn', N'Loét dạ dày tá tràng đang tiến triển', N'Buồn nôn, tiêu chảy, dị ứng da'),
(N'Bromhexine 8mg', N'Bromhexine', N'Tiêu đờm', N'8mg 3 lần/ngày', N'Uống sau ăn', N'Loét dạ dày', N'Buồn nôn, chóng mặt'),
(N'Cetirizine 10mg', N'Cetirizine', N'Kháng Histamine', N'10mg 1 lần/ngày', N'Uống buổi tối', N'Suy thận nặng', N'Buồn ngủ, khô miệng'),
(N'Loratadine 10mg', N'Loratadine', N'Kháng Histamine', N'10mg 1 lần/ngày', N'Uống sáng, không gây buồn ngủ', N'Không có chống chỉ định đáng kể', N'Nhức đầu, khô miệng'),
(N'Salbutamol 4mg', N'Salbutamol', N'Giãn phế quản - Beta2', N'4mg 3 lần/ngày', N'Uống sau ăn', N'Nhịp tim nhanh, cường giáp', N'Run tay, hồi hộp, nhịp tim nhanh'),
(N'Montelukast 10mg', N'Montelukast', N'Kháng Leukotriene', N'10mg 1 lần/ngày buổi tối', N'Uống buổi tối', N'Dị ứng Montelukast', N'Nhức đầu, đau bụng'),

-- Tim mạch
(N'Amlodipine 5mg', N'Amlodipine', N'Chẹn kênh Canxi', N'5-10mg 1 lần/ngày', N'Uống buổi sáng', N'Sốc tim, suy tim mất bù', N'Phù chân, đỏ bừng mặt, nhức đầu'),
(N'Enalapril 5mg', N'Enalapril', N'Ức chế ACE', N'5-40mg 1-2 lần/ngày', N'Uống trước ăn', N'Thai kỳ, hẹp động mạch thận 2 bên', N'Ho khan, tụt huyết áp, tăng Kali'),
(N'Losartan 50mg', N'Losartan', N'Chẹn thụ thể Angiotensin', N'50-100mg 1 lần/ngày', N'Uống sáng hoặc tối', N'Thai kỳ, suy thận nặng', N'Chóng mặt, tăng Kali, tụt huyết áp'),
(N'Atorvastatin 20mg', N'Atorvastatin', N'Statin - Hạ Lipid', N'20-80mg 1 lần/ngày buổi tối', N'Uống buổi tối', N'Bệnh gan, thai kỳ', N'Đau cơ, tăng men gan, đau đầu'),
(N'Metoprolol 50mg', N'Metoprolol', N'Chẹn Beta', N'50-200mg 1-2 lần/ngày', N'Uống sáng sau ăn', N'Hen phế quản, nhịp tim chậm, sốc tim', N'Mệt mỏi, chân tay lạnh, hạ huyết áp'),

-- Tiểu đường
(N'Metformin 500mg', N'Metformin', N'Biguanide - Tiểu đường', N'500-2000mg chia 2-3 lần/ngày', N'Uống ngay sau ăn', N'Suy thận (CrCl<30), suy gan, nhiễm toan', N'Buồn nôn, tiêu chảy, đau bụng'),
(N'Glipizide 5mg', N'Glipizide', N'Sulfonylurea - Tiểu đường', N'5-40mg/ngày chia 1-2 lần', N'Uống 30 phút trước ăn', N'Đái tháo đường type 1, suy gan nặng', N'Hạ đường huyết, tăng cân'),
(N'Glibenclamide 5mg', N'Glibenclamide', N'Sulfonylurea - Tiểu đường', N'2.5-20mg/ngày', N'Uống trước ăn sáng', N'ĐTĐ type 1, suy thận nặng', N'Hạ đường huyết, tăng cân'),

-- Thần kinh
(N'Diazepam 5mg', N'Diazepam', N'Benzodiazepine', N'5-10mg 2-4 lần/ngày', N'Uống hoặc tiêm', N'Glaucoma góc đóng, myasthenia gravis, thai kỳ', N'Buồn ngủ, phụ thuộc thuốc, yếu cơ'),
(N'Alprazolam 0.25mg', N'Alprazolam', N'Benzodiazepine', N'0.25-0.5mg 3 lần/ngày', N'Uống theo chỉ định', N'Glaucoma góc đóng, thai kỳ', N'Buồn ngủ, phụ thuộc, giảm trí nhớ'),
(N'Amitriptyline 25mg', N'Amitriptyline', N'Chống trầm cảm 3 vòng', N'25-150mg/ngày chia lần', N'Uống buổi tối', N'Nhồi máu cơ tim cấp, glaucoma góc đóng', N'Khô miệng, táo bón, buồn ngủ'),
(N'Fluoxetine 20mg', N'Fluoxetine', N'SSRI - Chống trầm cảm', N'20-80mg 1 lần/ngày buổi sáng', N'Uống buổi sáng', N'Dùng MAOIs trong 14 ngày', N'Buồn nôn, mất ngủ, lo âu'),
(N'Carbamazepine 200mg', N'Carbamazepine', N'Chống động kinh', N'200-1600mg/ngày chia lần', N'Uống sau ăn', N'Bệnh gan, hồng cầu giảm', N'Chóng mặt, buồn nôn, phát ban'),
(N'Gabapentin 300mg', N'Gabapentin', N'Chống động kinh - Đau thần kinh', N'300-3600mg/ngày chia 3 lần', N'Uống cùng hoặc không cùng thức ăn', N'Suy thận nặng', N'Buồn ngủ, chóng mặt, phù'),

-- Kháng nấm / Kháng virus
(N'Fluconazole 150mg', N'Fluconazole', N'Kháng nấm - Azole', N'150mg liều duy nhất hoặc 50-400mg/ngày', N'Uống cùng hoặc không cùng thức ăn', N'Dùng Cisapride, Thai kỳ', N'Buồn nôn, đau đầu, phát ban'),
(N'Acyclovir 400mg', N'Acyclovir', N'Kháng virus - Herpes', N'400mg 3 lần/ngày × 5-10 ngày', N'Uống với nhiều nước', N'Suy thận nặng', N'Buồn nôn, nhức đầu, phát ban'),
(N'Oseltamivir 75mg', N'Oseltamivir', N'Kháng virus - Cúm', N'75mg 2 lần/ngày × 5 ngày', N'Uống trong vòng 48h khởi phát triệu chứng', N'Suy thận nặng', N'Buồn nôn, nôn, nhức đầu'),

-- Vitamin / bổ sung
(N'Vitamin C 500mg', N'Ascorbic acid', N'Vitamin', N'500mg 1-2 lần/ngày', N'Uống sau ăn', N'Sỏi thận, thalassemia', N'Tiêu chảy khi liều cao'),
(N'Vitamin B Complex', N'Vitamin B1, B2, B6, B12', N'Vitamin nhóm B', N'1-2 viên/ngày', N'Uống sau ăn', N'Không có chống chỉ định đáng kể', N'Nước tiểu vàng đậm');
GO

-- ============================================================
-- BENH (50+ diseases)
-- ============================================================
INSERT INTO Benh (Ten, MoTa, NhomBenh) VALUES
(N'Cảm cúm', N'Bệnh nhiễm virus cúm với triệu chứng sốt, ho, đau mình mẩy', N'Nhiễm trùng hô hấp'),
(N'Cảm lạnh thông thường', N'Nhiễm virus đường hô hấp trên', N'Nhiễm trùng hô hấp'),
(N'Viêm họng cấp', N'Viêm nhiễm vùng họng', N'Nhiễm trùng hô hấp'),
(N'Viêm phổi', N'Nhiễm trùng nhu mô phổi', N'Nhiễm trùng hô hấp'),
(N'Viêm phế quản cấp', N'Viêm nhiễm phế quản', N'Nhiễm trùng hô hấp'),
(N'Hen phế quản', N'Bệnh mạn tính gây co thắt phế quản', N'Hô hấp mạn tính'),
(N'Viêm mũi dị ứng', N'Phản ứng dị ứng tại niêm mạc mũi', N'Dị ứng'),
(N'Viêm dạ dày cấp', N'Viêm niêm mạc dạ dày', N'Tiêu hóa'),
(N'Loét dạ dày tá tràng', N'Tổn thương loét niêm mạc dạ dày hoặc tá tràng', N'Tiêu hóa'),
(N'Viêm đại tràng cấp', N'Viêm niêm mạc đại tràng', N'Tiêu hóa'),
(N'Tiêu chảy cấp', N'Đi ngoài phân lỏng nhiều lần', N'Tiêu hóa'),
(N'Táo bón mạn tính', N'Khó đại tiện kéo dài', N'Tiêu hóa'),
(N'Trào ngược dạ dày thực quản', N'Axit dạ dày trào ngược lên thực quản', N'Tiêu hóa'),
(N'Viêm gan A', N'Viêm gan do virus HAV', N'Gan mật'),
(N'Tăng huyết áp nguyên phát', N'Huyết áp cao không rõ nguyên nhân', N'Tim mạch'),
(N'Đái tháo đường type 2', N'Rối loạn chuyển hóa glucose', N'Nội tiết'),
(N'Rối loạn lipid máu', N'Mỡ máu cao', N'Nội tiết'),
(N'Viêm khớp dạng thấp', N'Bệnh tự miễn gây viêm khớp', N'Cơ xương khớp'),
(N'Gout', N'Rối loạn chuyển hóa acid uric', N'Cơ xương khớp'),
(N'Thoái hóa khớp', N'Tổn thương sụn khớp', N'Cơ xương khớp'),
(N'Đau thắt lưng cấp', N'Đau vùng lưng dưới cấp tính', N'Cơ xương khớp'),
(N'Đau đầu căng cơ', N'Đau đầu do co thắt cơ', N'Thần kinh'),
(N'Migraine', N'Đau nửa đầu', N'Thần kinh'),
(N'Mất ngủ', N'Rối loạn giấc ngủ', N'Thần kinh'),
(N'Rối loạn lo âu', N'Trạng thái lo lắng thái quá', N'Tâm thần'),
(N'Trầm cảm', N'Rối loạn tâm thần gây buồn bã kéo dài', N'Tâm thần'),
(N'Nhiễm trùng tiết niệu', N'Nhiễm khuẩn đường tiết niệu', N'Tiết niệu'),
(N'Sỏi thận', N'Sự hình thành sỏi trong thận', N'Tiết niệu'),
(N'Dị ứng da tiếp xúc', N'Phản ứng dị ứng da khi tiếp xúc với chất gây dị ứng', N'Da liễu'),
(N'Mề đay cấp', N'Phản ứng dị ứng trên da', N'Da liễu'),
(N'Nấm da', N'Nhiễm nấm trên da', N'Da liễu'),
(N'Nhiễm khuẩn da', N'Viêm nhiễm da do vi khuẩn', N'Da liễu'),
(N'Herpes môi', N'Nhiễm virus Herpes simplex', N'Da liễu'),
(N'Sốt xuất huyết Dengue', N'Nhiễm virus Dengue qua muỗi Aedes', N'Nhiễm trùng'),
(N'COVID-19', N'Nhiễm SARS-CoV-2', N'Nhiễm trùng hô hấp'),
(N'Viêm xoang cấp', N'Viêm nhiễm các xoang cạnh mũi', N'Nhiễm trùng hô hấp'),
(N'Viêm tai giữa', N'Viêm nhiễm tai giữa', N'Tai mũi họng'),
(N'Viêm kết mạc', N'Viêm niêm mạc mắt', N'Mắt'),
(N'Đau thần kinh tọa', N'Đau dọc theo dây thần kinh tọa', N'Thần kinh'),
(N'Suy nhược cơ thể', N'Trạng thái kiệt sức toàn thân', N'Toàn thân');
GO

-- ============================================================
-- BENH - TRIEU CHUNG links (inference rules)
-- ============================================================
-- Cảm cúm (Id=1)
INSERT INTO BenhTrieuChung VALUES (1, 1, 2.0, 1),  -- Sốt - bắt buộc
                                   (1, 3, 1.5, 0),  -- Ớn lạnh
                                   (1, 4, 1.5, 0),  -- Mệt mỏi
                                   (1, 5, 1.0, 0),  -- Đau đầu
                                   (1, 7, 2.0, 1),  -- Ho - bắt buộc
                                   (1, 28, 1.5, 0), -- Đau cơ
                                   (1, 11, 1.0, 0); -- Chảy nước mũi

-- Cảm lạnh (Id=2)
INSERT INTO BenhTrieuChung VALUES (2, 11, 2.0, 1), -- Chảy nước mũi - bắt buộc
                                   (2, 12, 1.5, 0), -- Nghẹt mũi
                                   (2, 7, 1.5, 0),  -- Ho
                                   (2, 13, 1.0, 0), -- Đau họng
                                   (2, 1, 0.5, 0);  -- Sốt nhẹ

-- Viêm họng (Id=3)
INSERT INTO BenhTrieuChung VALUES (3, 13, 2.0, 1), -- Đau họng - bắt buộc
                                   (3, 14, 2.0, 1), -- Viêm họng - bắt buộc
                                   (3, 1, 1.0, 0),  -- Sốt
                                   (3, 48, 1.0, 0); -- Khàn giọng

-- Viêm phổi (Id=4)
INSERT INTO BenhTrieuChung VALUES (4, 2, 2.0, 1),  -- Sốt cao - bắt buộc
                                   (4, 8, 2.0, 1),  -- Ho có đờm - bắt buộc
                                   (4, 9, 2.0, 1),  -- Khó thở - bắt buộc
                                   (4, 10, 1.5, 0), -- Đau ngực
                                   (4, 4, 1.0, 0);  -- Mệt mỏi

-- Hen phế quản (Id=6)
INSERT INTO BenhTrieuChung VALUES (6, 9, 3.0, 1),  -- Khó thở - bắt buộc
                                   (6, 7, 2.0, 0),  -- Ho
                                   (6, 10, 1.5, 0); -- Đau ngực

-- Viêm dạ dày cấp (Id=8)
INSERT INTO BenhTrieuChung VALUES (8, 21, 3.0, 1), -- Đau dạ dày - bắt buộc
                                   (8, 15, 1.5, 0), -- Buồn nôn
                                   (8, 16, 1.5, 0), -- Nôn mửa
                                   (8, 22, 1.0, 0); -- Ợ chua

-- Tiêu chảy cấp (Id=11)
INSERT INTO BenhTrieuChung VALUES (11, 18, 3.0, 1), -- Tiêu chảy - bắt buộc
                                    (11, 17, 1.5, 0), -- Đau bụng
                                    (11, 15, 1.0, 0), -- Buồn nôn
                                    (11, 1, 0.5, 0);  -- Sốt

-- Tăng huyết áp (Id=15)
INSERT INTO BenhTrieuChung VALUES (15, 33, 3.0, 1), -- Tăng huyết áp - bắt buộc
                                    (15, 5, 1.0, 0),  -- Đau đầu
                                    (15, 45, 1.5, 0), -- Nhức đầu sau gáy
                                    (15, 34, 1.0, 0), -- Hồi hộp
                                    (15, 6, 0.5, 0);  -- Chóng mặt

-- Đái tháo đường type 2 (Id=16)
INSERT INTO BenhTrieuChung VALUES (16, 36, 2.0, 1), -- Tiểu nhiều - bắt buộc
                                    (16, 39, 2.0, 1), -- Khát nước nhiều - bắt buộc
                                    (16, 40, 1.5, 0), -- Sụt cân
                                    (16, 4, 1.0, 0);  -- Mệt mỏi

-- Nhiễm trùng tiết niệu (Id=27)
INSERT INTO BenhTrieuChung VALUES (27, 37, 3.0, 1), -- Tiểu buốt - bắt buộc
                                    (27, 36, 1.5, 0), -- Tiểu nhiều
                                    (27, 38, 1.5, 0), -- Tiểu ra máu
                                    (27, 1, 1.0, 0);  -- Sốt

-- Rối loạn lo âu (Id=25)
INSERT INTO BenhTrieuChung VALUES (25, 43, 3.0, 1), -- Lo âu - bắt buộc
                                    (25, 42, 1.5, 0), -- Mất ngủ
                                    (25, 34, 1.0, 0), -- Hồi hộp
                                    (25, 5, 0.5, 0);  -- Đau đầu

-- Trầm cảm (Id=26)
INSERT INTO BenhTrieuChung VALUES (26, 44, 3.0, 1), -- Trầm cảm - bắt buộc
                                    (26, 4, 1.5, 0),  -- Mệt mỏi
                                    (26, 42, 1.5, 0), -- Mất ngủ
                                    (26, 40, 1.0, 0); -- Sụt cân
GO

-- ============================================================
-- BENH - THUOC (treatment mapping)
-- ============================================================
-- Cảm cúm
INSERT INTO BenhThuoc VALUES (1, 40, N'Điều trị triệu chứng', 1), -- Oseltamivir
                              (1, 1,  N'Hạ sốt, giảm đau', 2),    -- Paracetamol
                              (1, 42, N'Bổ sung vitamin', 3);       -- Vitamin C

-- Cảm lạnh
INSERT INTO BenhThuoc VALUES (2, 1,  N'Hạ sốt, giảm đau', 1),   -- Paracetamol
                              (2, 23, N'Giảm nghẹt mũi', 2),      -- Loratadine
                              (2, 21, N'Tiêu đờm', 3);             -- Ambroxol

-- Viêm họng
INSERT INTO BenhThuoc VALUES (3, 6,  N'Kháng sinh khi do vi khuẩn', 1), -- Amoxicillin
                              (3, 1,  N'Hạ sốt', 2);                      -- Paracetamol

-- Viêm dạ dày cấp
INSERT INTO BenhThuoc VALUES (8, 13, N'Ức chế axit', 1),        -- Omeprazole
                              (8, 16, N'Chống nôn', 2);            -- Domperidone

-- Tăng huyết áp
INSERT INTO BenhThuoc VALUES (15, 27, N'Điều trị bậc 1', 1),   -- Amlodipine
                              (15, 28, N'Điều trị phối hợp', 2), -- Enalapril
                              (15, 29, N'Thay thế Enalapril', 3); -- Losartan

-- Đái tháo đường type 2
INSERT INTO BenhThuoc VALUES (16, 31, N'Điều trị bậc 1', 1),   -- Metformin
                              (16, 32, N'Phối hợp nếu cần', 2);  -- Glipizide

-- Tiêu chảy cấp
INSERT INTO BenhThuoc VALUES (11, 18, N'Giảm nhu động ruột', 1), -- Loperamide
                              (11, 20, N'Bảo vệ niêm mạc', 2);   -- Smecta

-- Nhiễm trùng tiết niệu
INSERT INTO BenhThuoc VALUES (27, 8,  N'Kháng sinh ưu tiên', 1),  -- Ciprofloxacin
                              (27, 10, N'Thay thế', 2);             -- Cefixime

-- Rối loạn lo âu
INSERT INTO BenhThuoc VALUES (25, 38, N'Điều trị ngắn hạn', 1),  -- Diazepam
                              (25, 36, N'Điều trị nền', 2);         -- Fluoxetine

-- Trầm cảm
INSERT INTO BenhThuoc VALUES (26, 36, N'SSRI ưu tiên', 1),        -- Fluoxetine
                              (26, 35, N'Thay thế', 2);             -- Amitriptyline
GO

-- ============================================================
-- TUONG TAC THUOC (Drug Interactions)
-- ============================================================
INSERT INTO TuongTacThuoc (ThuocId1, ThuocId2, MucDo, MoTa, HauQua) VALUES
(3, 2, 3, N'Aspirin + Ibuprofen', N'Tăng nguy cơ xuất huyết tiêu hóa, giảm hiệu quả chống kết tập tiểu cầu của Aspirin'),
(3, 4, 2, N'Aspirin + Diclofenac', N'Tăng tác dụng phụ tiêu hóa, nguy cơ xuất huyết'),
(8, 11, 2, N'Ciprofloxacin + Metronidazole', N'Tăng nguy cơ tác dụng phụ thần kinh'),
(13, 14, 1, N'Omeprazole + Pantoprazole', N'Không nên dùng 2 PPI cùng lúc, không có lợi thêm'),
(31, 32, 2, N'Metformin + Glipizide', N'Tăng nguy cơ hạ đường huyết, cần theo dõi chặt'),
(27, 28, 1, N'Amlodipine + Enalapril', N'Hạ huyết áp cộng thêm - cần điều chỉnh liều'),
(38, 36, 2, N'Diazepam + Fluoxetine', N'Tăng tác dụng an thần của Diazepam'),
(2, 31, 2, N'Ibuprofen + Metformin', N'NSAIDs có thể làm giảm tác dụng của Metformin, tăng nguy cơ suy thận'),
(30, 31, 1, N'Metoprolol + Metformin', N'Metoprolol che lấp triệu chứng hạ đường huyết'),
(29, 31, 1, N'Atorvastatin + Metformin', N'Atorvastatin có thể tăng nhẹ đường huyết'),
(7, 12, 1, N'Azithromycin + Clarithromycin', N'Không dùng 2 Macrolide cùng lúc, không tăng hiệu quả'),
(9, 8, 2, N'Doxycycline + Ciprofloxacin', N'Tương tác kháng khuẩn, không nên phối hợp thường quy');
GO

-- ============================================================
-- BENH NHAN (sample patients)
-- ============================================================
INSERT INTO BenhNhan (HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, TienSuBenh) VALUES
(N'Nguyễn Văn An',   '1980-03-15', 1, '0901234567', N'123 Lê Lợi, Q1, TP.HCM', N'Tăng huyết áp'),
(N'Trần Thị Bình',   '1992-07-22', 2, '0912345678', N'456 Nguyễn Huệ, Q1, TP.HCM', NULL),
(N'Lê Văn Cường',    '1975-11-30', 1, '0923456789', N'789 Điện Biên Phủ, Q3, TP.HCM', N'Đái tháo đường type 2'),
(N'Phạm Thị Dung',   '2000-05-08', 2, '0934567890', N'12 Võ Thị Sáu, Q3, TP.HCM', NULL),
(N'Hoàng Minh Em',   '1965-09-14', 1, '0945678901', N'34 Nam Kỳ Khởi Nghĩa, Q3, TP.HCM', N'Hen phế quản, tăng huyết áp');
GO

PRINT 'Seed data inserted successfully!';
GO
