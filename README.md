# Hệ Chuyên Gia Thuốc Bệnh

Ứng dụng desktop hệ chuyên gia chẩn đoán bệnh và gợi ý thuốc điều trị.  
Đồ án môn Công nghệ Phần mềm (CNPM) — .NET 8 WinForms + SQL Server Express.

---

## Yêu cầu

| Phần mềm | Phiên bản |
|---|---|
| Windows | 10 / 11 (64-bit) |
| .NET SDK | 8.0+ |
| SQL Server Express | 2019+ |
| SQL Server Management Studio | Tùy chọn (để chạy script DB) |

---

## Cài đặt & Chạy lần đầu

### 1. Clone repository

```bash
git clone https://github.com/tuantran2409/HeChuyenGiaThuocBenhDemo.git
cd HeChuyenGiaThuocBenhDemo
```

### 2. Thiết lập cơ sở dữ liệu

Mở **SSMS** → kết nối `localhost\SQLEXPRESS`, rồi chạy theo thứ tự:

```sql
-- Bước 1: Tạo schema (8 bảng)
docs/database/schema.sql

-- Bước 2: Nạp dữ liệu mẫu
docs/database/seed_data.sql
```

Kiểm tra:
```sql
SELECT Username FROM Users;
-- Kết quả: admin, bacsi1, bacsi2, duocsi1
```

### 3. Chạy ứng dụng

```bash
dotnet run --project src/HeChuyenGiaThuocBenh.UI
```

---

## Tài khoản mặc định

| Username | Password | Vai trò |
|---|---|---|
| `admin` | `Admin@123` | Quản trị viên |
| `bacsi1` | `BacSi@123` | Bác sĩ |
| `bacsi2` | `BacSi@123` | Bác sĩ |
| `duocsi1` | `DuocSi@123` | Dược sĩ |

---

## Cấu trúc dự án

```
HeChuyenGiaThuocBenh/
├── src/
│   ├── HeChuyenGiaThuocBenh.Models     ← Entities, Enums
│   ├── HeChuyenGiaThuocBenh.DAL        ← Dapper repositories
│   ├── HeChuyenGiaThuocBenh.BLL        ← Business logic, services
│   └── HeChuyenGiaThuocBenh.UI         ← WinForms (.NET 8)
├── tests/
│   ├── HeChuyenGiaThuocBenh.Tests.Unit         ← Unit tests (MSTest)
│   └── HeChuyenGiaThuocBenh.Tests.Integration  ← Integration tests (real DB)
├── docs/
│   ├── database/
│   │   ├── schema.sql
│   │   └── seed_data.sql
│   ├── test-cases.md       ← 30 manual UI test cases
│   ├── user-guide.md       ← Hướng dẫn sử dụng
│   └── technical-guide.md  ← Tài liệu kỹ thuật
├── docker-compose.yml      ← SQL Server container
├── publish.ps1             ← Build EXE self-contained
└── handoff.md              ← Sprint tracking & context
```

---

## Chức năng

| Chức năng | Vai trò |
|---|---|
| 🔬 Chẩn đoán bệnh — forward chaining engine | Tất cả |
| 💊 Tra cứu thuốc + kiểm tra tương tác | Tất cả |
| 👤 Hồ sơ bệnh nhân + lịch sử chẩn đoán | Tất cả |
| 📊 Báo cáo thống kê + xuất PDF | Tất cả |
| 💊 Quản lý thuốc (CRUD) | Admin |
| 🏥 Quản lý bệnh + tập luật IF-THEN | Admin |
| 👥 Quản lý người dùng | Admin |

---

## Chạy Tests

```bash
# Unit tests — không cần DB
dotnet test tests/HeChuyenGiaThuocBenh.Tests.Unit/

# Integration tests — cần DB với seed data
dotnet test tests/HeChuyenGiaThuocBenh.Tests.Integration/
```

---

## Build bản phát hành

```powershell
# Tạo file EXE self-contained (win-x64)
.\publish.ps1

# Output: publish\win-x64\HeChuyenGiaThuocBenh.UI.exe
```

---

## Triển khai SQL Server bằng Docker

```bash
docker compose up -d
```

SQL Server 2022 chạy tại `localhost:1433`  
SA password: `HcGtb@2024!`

Sau đó cập nhật `src/HeChuyenGiaThuocBenh.UI/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=HeChuyenGiaThuocBenh;User Id=sa;Password=HcGtb@2024!;TrustServerCertificate=True;"
  }
}
```

---

## Stack

| Layer | Technology |
|---|---|
| Database | SQL Server Express — `Vietnamese_CI_AS` |
| Data Access | Dapper + Microsoft.Data.SqlClient |
| Business Logic | .NET 8, BCrypt.Net-Next, QuestPDF |
| UI | .NET 8 WinForms + LiveChartsCore.SkiaSharp |
| Tests | MSTest v4 |

---

## Branches

| Branch | Nội dung |
|---|---|
| `main` | Production-ready |
| `develop` | Integration branch |
| `feature/sprint1-setup` | DB + Auth + UI shell |
| `feature/sprint2-inference` | Inference engine + drug lookup |
| `feature/sprint3-admin-patient` | Admin CRUD + patient records |
| `feature/sprint4-reports` | Reports dashboard + PDF export |
| `feature/sprint5-tests` | Tests + docs + deployment |
