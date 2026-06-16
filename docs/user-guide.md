# Hướng dẫn sử dụng — Hệ Chuyên Gia Thuốc Bệnh

## Yêu cầu hệ thống

- Windows 10/11 (64-bit)
- .NET 8.0 Runtime (hoặc dùng bản self-contained, không cần cài)
- SQL Server Express 2019+ tại `localhost\SQLEXPRESS`

---

## Cài đặt lần đầu

### 1. Thiết lập cơ sở dữ liệu

1. Mở **SQL Server Management Studio (SSMS)**
2. Kết nối tới `localhost\SQLEXPRESS`
3. Chạy file `docs/database/schema.sql` (tạo DB + 8 bảng)
4. Chạy file `docs/database/seed_data.sql` (dữ liệu mẫu)
5. Kiểm tra: `SELECT COUNT(*) FROM Users` → phải trả về 4

### 2. Chạy ứng dụng

```
dotnet run --project src/HeChuyenGiaThuocBenh.UI
```

Hoặc chạy file `.exe` nếu dùng bản đã publish.

---

## Tài khoản mặc định

| Tên đăng nhập | Mật khẩu | Vai trò |
|---|---|---|
| `admin` | `Admin@123` | Quản trị viên |
| `bacsi1` | `BacSi@123` | Bác sĩ |
| `bacsi2` | `BacSi@123` | Bác sĩ |
| `duocsi1` | `DuocSi@123` | Dược sĩ |

---

## Các chức năng chính

### Đăng nhập

- Nhập tên đăng nhập và mật khẩu, nhấn **Đăng nhập** hoặc **Enter**
- Nếu sai thông tin: hiện thông báo lỗi
- Sau khi đăng nhập: chuyển sang màn hình chính

---

### Chẩn đoán bệnh (🔬)

1. Nhấn **Chẩn đoán** trong menu bên trái
2. Tìm kiếm triệu chứng bằng ô tìm kiếm hoặc cuộn danh sách
3. Tích chọn các triệu chứng bệnh nhân đang có
4. Nhấn nút **Chẩn đoán**
5. Xem kết quả:
   - Danh sách bệnh theo độ tin cậy (cao → thấp)
   - Click vào bệnh để xem thuốc gợi ý
   - **Panel xanh**: không có tương tác thuốc nguy hiểm
   - **Panel đỏ**: có cảnh báo tương tác thuốc — kiểm tra kỹ trước khi kê

---

### Tra cứu thuốc (💊)

1. Nhấn **Tra cứu thuốc** trong menu
2. Tìm theo tên hoặc hoạt chất, lọc theo nhóm thuốc
3. Click vào thuốc để xem chi tiết: liều dùng, cách dùng, chống chỉ định, tác dụng phụ
4. **Kiểm tra tương tác**: chọn nhiều thuốc (Ctrl+Click) → nhấn **Kiểm tra tương tác**

---

### Hồ sơ bệnh nhân (👤)

1. Nhấn **Hồ sơ bệnh nhân**
2. **Thêm mới**: nhấn `+ Thêm mới`, điền thông tin, nhấn `💾 Lưu`
3. **Tìm kiếm**: gõ họ tên hoặc số điện thoại vào ô tìm kiếm
4. **Lịch sử chẩn đoán**: chọn bệnh nhân → chuyển sang tab "Lịch sử chẩn đoán"

---

### Báo cáo & Thống kê (📊) — Admin / Bác sĩ

1. Nhấn **Báo cáo**
2. Chọn khoảng ngày (Từ / Đến)
3. Nhấn **🔍 Tìm kiếm** → biểu đồ cột hiện số chẩn đoán theo ngày, bảng hiện danh sách
4. Nhấn **📄 Xuất PDF** → chọn nơi lưu → mở PDF ngay nếu muốn

---

### Quản lý thuốc (Admin) (💊)

1. Nhấn **Quản lý thuốc** (chỉ hiện với admin)
2. **Thêm**: nhấn `+ Thêm mới`, điền thông tin, nhấn `💾 Lưu`
3. **Sửa**: chọn thuốc trong danh sách, chỉnh thông tin bên phải, nhấn `💾 Lưu`
4. **Xóa**: chọn thuốc, nhấn `🗑 Xóa`, xác nhận

---

### Quản lý bệnh & Tập luật (Admin) (🏥)

1. Nhấn **Quản lý bệnh**
2. **Thêm bệnh**: nhấn `+ Thêm bệnh`, điền Tên/Nhóm/Mô tả, nhấn `💾 Lưu bệnh`
3. **Tập luật**: chuyển tab "Tập luật" → thêm luật (chọn triệu chứng, trọng số, bắt buộc hay không), nhấn `💾 Lưu tập luật`
4. **Lưu ý**: trọng số (TrongSo) là số dương, triệu chứng "Bắt buộc" phải xuất hiện để bệnh được xét đến

---

### Quản lý người dùng (Admin) (👥)

1. Nhấn **Người dùng**
2. **Thêm**: nhấn `+ Thêm mới`, điền thông tin + mật khẩu (≥ 6 ký tự), nhấn `💾 Lưu`
3. **Sửa**: chọn người dùng, chỉnh thông tin (để trống mật khẩu nếu không đổi), nhấn `💾 Lưu`
4. **Bật/Tắt**: chọn người dùng, nhấn `🔒 Bật/Tắt`, xác nhận
5. **Reset mật khẩu**: nhấn `🔑 Reset mật khẩu` → mật khẩu đặt lại về `Admin@123`

---

## Ghi chú bảo mật

- Chỉ admin mới thấy và truy cập được 3 menu quản trị
- Không thể vô hiệu hóa tài khoản đang đăng nhập
- Mật khẩu lưu dưới dạng BCrypt hash (workFactor=11) — không có khả năng giải mã ngược
