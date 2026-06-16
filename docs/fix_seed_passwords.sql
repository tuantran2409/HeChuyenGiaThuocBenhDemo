-- Chạy file này SAU KHI đã chạy seed_data.sql
-- Passwords: admin/bacsi/duocsi đều là các hash BCrypt workFactor=11
-- Tạo hash bằng cách chạy đoạn C# sau trong LINQPad hoặc dotnet-script:
--
--   using BCrypt.Net;
--   Console.WriteLine(BCrypt.HashPassword("Admin@123", 11));   // cho admin
--   Console.WriteLine(BCrypt.HashPassword("BacSi@123", 11));   // cho bacsi1, bacsi2
--   Console.WriteLine(BCrypt.HashPassword("DuocSi@123", 11));  // cho duocsi1
--
-- Sau đó thay các giá trị HASH_PLACEHOLDER bên dưới:

USE HeChuyenGiaThuocBenh;

UPDATE Users SET MatKhauHash = 'HASH_PLACEHOLDER_ADMIN'  WHERE TenDangNhap = 'admin';
UPDATE Users SET MatKhauHash = 'HASH_PLACEHOLDER_BACSI'  WHERE TenDangNhap = 'bacsi1';
UPDATE Users SET MatKhauHash = 'HASH_PLACEHOLDER_BACSI'  WHERE TenDangNhap = 'bacsi2';
UPDATE Users SET MatKhauHash = 'HASH_PLACEHOLDER_DUOCSI' WHERE TenDangNhap = 'duocsi1';
