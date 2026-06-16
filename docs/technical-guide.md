# Tài liệu kỹ thuật — Hệ Chuyên Gia Thuốc Bệnh

## Kiến trúc tổng thể

```
HeChuyenGiaThuocBenh/
├── src/
│   ├── HeChuyenGiaThuocBenh.Models        ← Entities, Enums (no deps)
│   ├── HeChuyenGiaThuocBenh.DAL           ← Dapper repositories + DbConnectionFactory
│   ├── HeChuyenGiaThuocBenh.BLL           ← Services (AuthService, InferenceService, BaoCaoService)
│   └── HeChuyenGiaThuocBenh.UI            ← WinForms (net8.0-windows)
├── tests/
│   ├── HeChuyenGiaThuocBenh.Tests.Unit    ← Pure logic tests (no DB)
│   └── HeChuyenGiaThuocBenh.Tests.Integration ← Real DB tests
└── docs/
    └── database/
        ├── schema.sql
        └── seed_data.sql
```

**Dependency direction:** UI → BLL → DAL → Models

---

## Stack

| Layer | Technology |
|---|---|
| Database | SQL Server Express — collation `Vietnamese_CI_AS` |
| Data Access | Dapper + `Microsoft.Data.SqlClient` |
| Business Logic | .NET 8 class lib, BCrypt.Net-Next (workFactor=11), QuestPDF |
| UI | .NET 8 WinForms + LiveChartsCore.SkiaSharpView.WinForms |
| Tests | MSTest v4 |

---

## Cơ sở dữ liệu

### 8 bảng chính

| Bảng | Mô tả |
|---|---|
| `Users` | Người dùng — Username, PasswordHash (BCrypt), HoTen, Role, IsActive |
| `TrieuChung` | Triệu chứng — Ten, MoTa, NhomTrieuChung |
| `Benh` | Bệnh — Ten, MoTa, NhomBenh, IsActive |
| `BenhTrieuChung` | Tập luật IF-THEN — BenhId, TrieuChungId, TrongSo (decimal), BatBuoc (bit) |
| `Thuoc` | Thuốc — Ten, HoatChat, NhomThuoc, LieuDung, CachDung, ChongChiDinh, TacDungPhu |
| `BenhThuoc` | Thuốc điều trị theo bệnh — BenhId, ThuocId, ThuTu |
| `TuongTacThuoc` | Tương tác thuốc — ThuocId1, ThuocId2, MucDo (1/2/3), MoTa, HauQua |
| `BenhNhan` | Hồ sơ bệnh nhân — HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, TienSuBenh, DiUng |
| `LichSuChanDoan` | Kết quả chẩn đoán — BenhNhanId, UserId, NgayChanDoan, TrieuChungInput, KetQuaBenh, ThuocGoiY |

### Xóa mềm (Soft delete)
- `Benh.IsActive = 0` — bệnh bị ẩn khỏi chẩn đoán nhưng giữ lại lịch sử
- `Thuoc.IsActive = 0` — tương tự
- `Users.IsActive = 0` — vô hiệu hóa tài khoản (không xóa)

---

## Hệ suy luận (Forward Chaining)

**File:** `InferenceService.cs`

### Thuật toán

1. Tìm các bệnh có ít nhất một triệu chứng khớp với input (`GetByTrieuChungIdsAsync`)
2. Với mỗi bệnh ứng viên:
   - **Kiểm tra bắt buộc**: tất cả `BatBuoc=1` rules phải có trong input → nếu thiếu, loại bỏ
   - **Tính độ tin cậy**: `confidence = Σ(TrongSo khớp) / Σ(TrongSo tất cả rules)`
   - Nếu `confidence < 40%` → loại bỏ
3. Kết quả sắp xếp giảm dần theo `DoTinCay`

### Ví dụ

Bệnh: Cảm cúm — rules:
```
TrieuChungId=1 (Sốt),     TrongSo=2.0, BatBuoc=1
TrieuChungId=3 (Ớn lạnh), TrongSo=1.5, BatBuoc=0
TrieuChungId=4 (Mệt mỏi), TrongSo=1.5, BatBuoc=0
```

Input: {1, 3} (Sốt + Ớn lạnh)
- Bắt buộc: triệu chứng 1 có trong input → đạt
- Khớp: 2.0 + 1.5 = 3.5; Tổng: 5.0
- Confidence = 3.5 / 5.0 = **70%** → trả về

---

## Dependency Injection (Manual)

`ServiceContainer.cs` (BLL) — static service locator, khởi tạo một lần khi app start:

```csharp
ServiceContainer.Initialize(connectionString); // Program.cs
// Sau đó dùng:
ServiceContainer.InferenceService
ServiceContainer.AuthService
ServiceContainer.UserRepository
// ...
```

Mỗi property tạo instance mới (no singleton) — đủ cho WinForms desktop.

---

## Form Embedding Pattern

`MainForm.ShowContentForm(Form f)`:
- Set `f.TopLevel = false`, `f.FormBorderStyle = None`, `f.Dock = Fill`
- Add vào `pnlContent`, gọi `f.Show()`
- Dispose form cũ trước khi thêm mới

---

## Authentication & Authorization

- **Login**: BCrypt.Verify(inputPassword, storedHash)
- **Roles**: Admin=1, BacSi=2, DuocSi=3
- **Phân quyền UI**: `MainForm.ApplyUserContext()` ẩn các button admin nếu `AppSession.IsAdmin == false`
- **Không dùng JWT/session token** — đây là desktop app, `AppSession.CurrentUser` lưu trong static property

---

## QuestPDF (BaoCaoService)

- License: `QuestPDF.Settings.License = LicenseType.Community`
- Cấu hình PDF: A4, margin 2cm, font Arial
- Flow: `GetThongKeAsync` → `XuatPDFAsync` → `Document.Create(...).GeneratePdf(path)`

---

## LiveCharts (BaoCaoForm)

```csharp
chart.Series = new ISeries[]
{
    new ColumnSeries<int> { Values = perDayCounts, Fill = ... }
};
chart.XAxes = new Axis[] { new Axis { Labels = dateLabels } };
chart.YAxes = new Axis[] { new Axis { MinLimit = 0 } };
```

---

## Chạy tests

```bash
# Unit tests (không cần DB):
dotnet test tests/HeChuyenGiaThuocBenh.Tests.Unit/

# Integration tests (cần DB với seed data):
dotnet test tests/HeChuyenGiaThuocBenh.Tests.Integration/
```

---

## Build & Deploy

### Development

```bash
dotnet run --project src/HeChuyenGiaThuocBenh.UI
```

### Production (self-contained EXE)

```powershell
.\publish.ps1          # win-x64 (default)
.\publish.ps1 -Runtime win-x86
```

Output: `publish/win-x64/HeChuyenGiaThuocBenh.UI.exe`

### SQL Server via Docker

```bash
docker compose up -d
# SA password: HcGtb@2024!
# Port: 1433
# Sau đó update appsettings.json: Server=localhost;User Id=sa;Password=HcGtb@2024!
```

Chạy schema.sql + seed_data.sql qua SSMS hoặc sqlcmd:
```bash
docker exec -it hcgtb-sqlserver /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P "HcGtb@2024!" -i /path/to/schema.sql
```

---

## Các lưu ý quan trọng

| Issue | Giải pháp |
|---|---|
| Connection string Windows Auth | `Trusted_Connection=True;TrustServerCertificate=True` |
| SQL Express instance | `Server=localhost\\SQLEXPRESS` (double backslash in JSON) |
| WinForms trên .NET 8 | `<UseWindowsForms>true</UseWindowsForms>` + `net8.0-windows` |
| dotnet publish WinForms | Không hỗ trợ `--self-contained` với ReadyToRun trên một số configs — dùng `PublishSingleFile=true` |
| Vietnamese collation | `Vietnamese_CI_AS` trong CREATE DATABASE — quan trọng cho tìm kiếm không phân biệt hoa thường |
