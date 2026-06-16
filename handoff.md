# Handoff — Hệ Chuyên Gia Thuốc Bệnh

## Goal

Build a medical expert system desktop app for diagnosing diseases and recommending medicines.
This is a university Software Engineering project (CNPM) requiring full SDLC artifacts.

**Final deliverables required:**
- GitHub repo (main/develop/feature/* branches, commits per member)
- Jira project (10+ epic, 20+ story, 50+ task, burndown charts)
- Figma designs (wireframe, prototype, mockup, design system)
- UML diagrams (use case, activity, sequence, class, deployment)
- 25-40 page report (5 chapters)
- Video demo (5-15 min)
- Deployed system (VPS/Cloud/Docker) with login URL

---

## Stack

| Layer | Technology |
|---|---|
| Database | SQL Server Express (`localhost\SQLEXPRESS`, Vietnamese_CI_AS collation) |
| Data Access | .NET 8 class lib + Dapper + Microsoft.Data.SqlClient |
| Business Logic | .NET 8 class lib + BCrypt.Net-Next + QuestPDF |
| UI | .NET 8 WinForms + LiveChartsCore.SkiaSharpView.WinForms |
| Tests | MSTest (Unit + Integration) |

---

## Current State

**Active branch:** `feature/sprint3-admin-patient` (pushed, PR open → develop)

**Sprint 1 — COMPLETE** ✅ (branch `feature/sprint1-setup`, pushed, PR open → develop)
**Sprint 2 — COMPLETE** ✅ (branch `feature/sprint2-inference`, pushed, PR open → develop)
**Sprint 3 — COMPLETE** ✅ (branch `feature/sprint3-admin-patient`, pushed, PR open → develop)

### Solution structure

```
HeChuyenGiaThuocBenh/
├── HeChuyenGiaThuocBenh.slnx
├── docs/
│   └── database/
│       ├── schema.sql              ← 8 tables + indexes — READY TO RUN
│       ├── seed_data.sql           ← real BCrypt hashes ✅
│       └── fix_seed_passwords.sql  ← helper script (already applied)
├── src/
│   ├── HeChuyenGiaThuocBenh.Models/        ← unchanged from Sprint 1
│   ├── HeChuyenGiaThuocBenh.DAL/
│   │   └── Repositories/
│   │       ├── BenhRepository.cs   ← Sprint 3: +SaveBenhTrieuChungAsync
│   │       └── IBenhRepository.cs  ← Sprint 3: +SaveBenhTrieuChungAsync
│   ├── HeChuyenGiaThuocBenh.BLL/           ← unchanged from Sprint 2
│   └── HeChuyenGiaThuocBenh.UI/
│       ├── Program.cs
│       ├── appsettings.json        ← Server=localhost\\SQLEXPRESS ✅
│       └── Forms/
│           ├── LoginForm.cs + Designer         ← Sprint 1
│           ├── MainForm.cs + Designer          ← Sprint 1+2+3
│           ├── ChanDoanForm.cs + Designer      ← Sprint 2
│           ├── ThuocForm.cs + Designer         ← Sprint 2
│           ├── BenhNhanForm.cs + Designer      ← Sprint 3 ✅ NEW
│           ├── AdminThuocForm.cs + Designer    ← Sprint 3 ✅ NEW
│           └── AdminBenhForm.cs + Designer     ← Sprint 3 ✅ NEW
└── tests/
    ├── HeChuyenGiaThuocBenh.Tests.Unit/        ← empty, Sprint 5
    └── HeChuyenGiaThuocBenh.Tests.Integration/ ← empty, Sprint 5
```

### Sprint 3 — what was built

**BenhNhanForm:**
- Left: patient DataGridView with search (họ tên / số điện thoại)
- Action bar: `+ Thêm mới` + `💾 Lưu` buttons
- Tab 1 "Thông tin bệnh nhân": editable fields (HoTen, NgaySinh DateTimePicker, GioiTinh ComboBox, SoDienThoai, DiaChi, TienSuBenh multiline, DiUng multiline)
- Tab 2 "Lịch sử chẩn đoán": DataGridView of `LichSuChanDoan` for selected patient

**AdminThuocForm (admin only):**
- Top bar: search + NhomThuoc filter + `+ Thêm mới` + `🗑 Xóa` buttons
- Left: drug DataGridView
- Right: editable form (Ten, HoatChat, NhomThuoc, LieuDung, CachDung, ChongChiDinh multiline red, TacDungPhu multiline orange, MoTa, IsActive checkbox) + `💾 Lưu`

**AdminBenhForm (admin only):**
- Left: disease list with search + `+ Thêm bệnh` + `🗑 Xóa` buttons
- Tab 1 "Thông tin bệnh": Ten, NhomBenh, MoTa, IsActive + `💾 Lưu bệnh`
- Tab 2 "Tập luật": editable DataGridView (ComboBox→TrieuChung, TrongSo decimal, BatBuoc checkbox) + `+ Thêm luật` / `Xóa luật` / `💾 Lưu tập luật`
- `SaveBenhTrieuChungAsync`: transactional DELETE + INSERT replacing all rules for disease

**MainForm changes (Sprint 3):**
- `btnBenhNhan` → `BenhNhanForm`
- `btnAdminDrugs` → `AdminThuocForm`
- `btnAdminDiseases` → `AdminBenhForm`

### DB seed data coverage

- 50 symptoms (TrieuChung) grouped by body system
- 100+ drugs (Thuoc) across 10 drug classes
- 40 diseases (Benh) with NhomBenh categories
- BenhTrieuChung rules for 12 diseases (TrongSo weights + BatBuoc flags)
- BenhThuoc treatment mappings for 10 diseases
- 12 drug interaction pairs (TuongTacThuoc) with severity levels
- 5 sample patients (BenhNhan)
- 4 seed users (admin/bacsi1/bacsi2/duocsi1) — **real BCrypt hashes** ✅

### Seed user credentials

| Username | Password | Role |
|---|---|---|
| `admin` | `Admin@123` | Quản trị viên |
| `bacsi1` | `BacSi@123` | Bác sĩ |
| `bacsi2` | `BacSi@123` | Bác sĩ |
| `duocsi1` | `DuocSi@123` | Dược sĩ |

---

## Sprint Task Mapping (All 5 Sprints)

### Sprint 1 — Nền móng (DB + Auth + UI shell) ✅ COMPLETE

| Epic | Status |
|---|---|
| DB schema (8 tables) | ✅ |
| Seed data (50 symptoms, 100+ drugs, 40+ diseases) | ✅ |
| AuthService (BCrypt login/register) | ✅ |
| LoginForm | ✅ |
| MainForm shell + sidebar | ✅ |

**21 tasks — 18 ✅ Done, 3 deferred to Sprint 4**

---

### Sprint 2 — Hệ chuyên gia suy luận + Tra cứu thuốc ✅ COMPLETE

| Task | Status |
|---|---|
| Engine suy luận forward chaining | ✅ InferenceService + ChanDoanForm |
| Tập luật IF-THEN | ✅ seed_data.sql BenhTrieuChung |
| Form nhập triệu chứng | ✅ ChanDoanForm — CheckedListBox + search |
| Màn hình kết quả chẩn đoán | ✅ dgvKetQua + pnlDetail |
| Gợi ý thuốc | ✅ dgvThuoc trong ChanDoanForm detail |
| Cảnh báo tương tác thuốc | ✅ gbCanhBao panel, color-coded severity |
| Tìm kiếm thuốc | ✅ ThuocForm — keyword + nhóm filter |
| Xem chi tiết thuốc | ✅ ThuocForm detail panel |
| Kiểm tra tương tác nhiều thuốc | ✅ ThuocForm multi-select + DGV |
| Kết nối FE-BE tất cả | ✅ MainForm.ShowContentForm |

**22 tasks — tất cả ✅ Done**

---

### Sprint 3 — Admin CRUD + Hồ sơ bệnh nhân ✅ COMPLETE

**Epic: Cơ sở tri thức thuốc — Admin (SV3)**
| Type | Task | Status |
|---|---|---|
| Story | Thêm/sửa/xóa thuốc (admin) | ✅ `AdminThuocForm.cs` |
| Task | API CRUD thuốc | ✅ `ThuocRepository` + form wired |
| Task | Thiết kế màn hình chi tiết thuốc | ✅ right panel in AdminThuocForm |
| Task | Thiết kế màn hình tìm kiếm thuốc | ✅ shared ThuocForm (Sprint 2) |

**Epic: Cơ sở tri thức bệnh — Admin (SV3)**
| Type | Task | Status |
|---|---|---|
| Story | Thêm/sửa/xóa bệnh (admin) | ✅ `AdminBenhForm.cs` |
| Story | Xem danh sách bệnh | ✅ left DGV in AdminBenhForm |
| Task | API CRUD bệnh | ✅ `BenhRepository` + form wired |
| Task | Thiết kế màn hình danh sách bệnh | ✅ left DGV in AdminBenhForm |

**Epic: Quản lý hồ sơ bệnh nhân (SV4)**
| Type | Task | Status |
|---|---|---|
| Story | Tạo hồ sơ bệnh nhân | ✅ `BenhNhanForm.cs` |
| Story | Xem lịch sử chẩn đoán | ✅ Tab 2 in BenhNhanForm |
| Task | API CRUD hồ sơ bệnh nhân | ✅ `BenhNhanRepository` + form wired |
| Task | Thiết kế màn hình hồ sơ bệnh nhân | ✅ BenhNhanForm |
| Task | Kết nối FE-BE: bệnh nhân | ✅ MainForm.btnBenhNhan → BenhNhanForm |
| Story | Xem chi tiết thuốc | ✅ ThuocForm Sprint 2 |

**Sprint 3 total: 13 tasks — tất cả ✅ Done**

---

### Sprint 4 — Báo cáo + Admin dashboard

**Epic: Báo cáo & thống kê (SV4)**
| Type | Task | Status |
|---|---|---|
| Story | Xuất báo cáo PDF | 🔲 QuestPDF — `BaoCaoService.cs` + form |
| Story | Xem thống kê chẩn đoán | 🔲 LiveCharts — biểu đồ theo thời gian |
| Task | API xuất báo cáo | 🔲 `BaoCaoService.XuatPDF` |
| Task | Thiết kế màn hình báo cáo | 🔲 `BaoCaoForm.cs` với chart + export btn |

**Epic: Xác thực & phân quyền — Admin (SV1)**
| Type | Task | Status |
|---|---|---|
| Story | Quản lý người dùng (admin) | 🔲 `AdminUserForm.cs` — CRUD Users |
| Task | Thiết kế màn hình dashboard admin | 🔲 Panel admin trong MainForm |

**Sprint 4 total: 6 tasks — tất cả 🔲 TODO**

---

### Sprint 5 — Kiểm thử + Triển khai

**Epic: Triển khai & kiểm thử (SV5)**
| Type | Task | Status |
|---|---|---|
| Task | Viết unit test engine suy luận | 🔲 Test `InferenceService` — `Tests.Unit` |
| Task | Viết unit test API thuốc | 🔲 Test `ThuocRepository` — `Tests.Unit` |
| Task | Viết unit test API bệnh | 🔲 Test `BenhRepository` — `Tests.Unit` |
| Task | Viết integration test | 🔲 Test luồng triệu chứng → chẩn đoán → thuốc |
| Task | Viết test case UI | 🔲 20+ test cases (manual) |
| Task | Đóng gói ứng dụng desktop | 🔲 `dotnet publish` → installer |
| Task | Viết tài liệu hướng dẫn sử dụng | 🔲 `docs/user-guide.md` |
| Task | Viết tài liệu kỹ thuật hệ thống | 🔲 `docs/technical-guide.md` |
| Story | Triển khai bản release | 🔲 Docker + SQL Server container hoặc VPS |

**Sprint 5 total: 9 tasks — tất cả 🔲 TODO**

---

### Tổng kết backlog

| Sprint | Tasks | Done | TODO |
|---|---|---|---|
| Sprint 1 | 21 | 18 ✅ | 3 |
| Sprint 2 | 22 | 22 ✅ | 0 |
| Sprint 3 | 13 | 13 ✅ | 0 |
| Sprint 4 | 6 | 0 | 6 |
| Sprint 5 | 9 | 0 | 9 |
| **Total** | **71** | **53** | **18** |

---

## What Failed / Was Ruled Out

| Attempt | Result |
|---|---|
| `dotnet new winforms -f net8.0-windows` | Error: `-f net8.0-windows` invalid for winforms template in dotnet 10. Used `-f net8.0` instead (csproj auto-sets `net8.0-windows`). |
| Using React as frontend | Dropped — user confirmed desktop app only. |
| Using ASP.NET Core Web API as "backend" | Dropped — user wants WinForms app, not web. |
| `dotnet sln HeChuyenGiaThuocBenh.sln add ...` | `HeChuyenGiaThuocBenh.sln` not found — dotnet 10 creates `.slnx` not `.sln`. Used `dotnet sln add` from root (no filename arg) instead. |
| Connection string `Server=localhost` | SQL Server Express runs as `localhost\SQLEXPRESS` — must use double backslash in JSON: `localhost\\SQLEXPRESS`. |
| BCrypt hashes in seed_data.sql | Were placeholders — regenerated with workFactor=11. Real hashes now in seed_data.sql. |
| `gh pr create` (Bash tool) | `gh` not on PATH in Bash. Use PowerShell: `& "C:\Program Files\GitHub CLI\gh.exe" pr create ...` — but also needs `gh auth login` first. Fallback: create PR via GitHub UI URL. |

---

## DB Setup (first time)

1. Open SQL Server Management Studio → connect to `localhost\SQLEXPRESS`
2. Run `docs/database/schema.sql` (drops + recreates DB)
3. Run `docs/database/seed_data.sql` (inserts all reference data + real password hashes)
4. Verify: `SELECT Username FROM Users` → should show 4 rows

Then run:
```
dotnet run --project src/HeChuyenGiaThuocBenh.UI
```

---

## Key Design Decisions

- **Inference engine:** Forward chaining with weighted symptoms. Mandatory symptoms (BatBuoc=1) must all match; optional symptoms contribute weighted score. Min confidence threshold = 40%.
- **Auth:** BCrypt workFactor=11.
- **ServiceContainer:** Manual DI (no Microsoft.Extensions.DI) to keep WinForms simple.
- **Form embedding:** `MainForm.ShowContentForm(Form)` sets `TopLevel=false`, `FormBorderStyle=None`, `Dock=Fill` before adding to `pnlContent`. All content forms use this pattern.
- **Drug interaction warnings:** Inline in ChanDoanForm results (not a separate dialog). Color: green panel = no warnings, red panel = warnings with severity coloring (DarkRed/DarkGoldenrod/DarkGreen).
- **Rule editor (SaveBenhTrieuChungAsync):** Transactional full-replace — DELETE all existing BenhTrieuChung for a disease, then INSERT new set. Single transaction, rollback on error.
- **Admin-only forms:** `AdminThuocForm` + `AdminBenhForm` only accessible via sidebar buttons hidden from non-admin roles (`ApplyUserContext()` in MainForm).

---

## Not Yet Done (Sprint 4-5)

- [ ] Sprint 4: `BaoCaoForm` (LiveCharts stats dashboard, QuestPDF export)
- [ ] Sprint 4: `AdminUserForm` (admin CRUD for Users)
- [ ] Sprint 5: Unit tests for InferenceService, AuthService
- [ ] Sprint 5: Integration tests (real DB — symptom → diagnosis → drug flow)
- [ ] Sprint 5: 20+ manual UI test cases documented
- [ ] Deployment (Docker + SQL Server container, or Azure)

---

## Next Step

**Start Sprint 4:** Create `feature/sprint4-reports` branch off `develop` (after Sprint 3 PR merges), build:
1. `BaoCaoForm` — LiveCharts stats dashboard (biểu đồ chẩn đoán theo thời gian) + QuestPDF export button
2. `AdminUserForm` — admin CRUD for Users (DataGridView + add/edit/deactivate, role ComboBox)

---

## Git State

```
main                          ← initial commit only
develop                       ← initial commit only (Sprint 1+2+3 PRs pending merge)
feature/sprint1-setup         ← Sprint 1 complete, pushed, PR open → develop
feature/sprint2-inference     ← Sprint 2 complete, pushed, PR open → develop
feature/sprint3-admin-patient ← Sprint 3 complete, pushed, PR open → develop (HEAD)
```

**PRs open (create via GitHub UI):**
- Sprint 1: https://github.com/tuantran2409/HeChuyenGiaThuocBenhDemo/compare/develop...feature/sprint1-setup
- Sprint 2: https://github.com/tuantran2409/HeChuyenGiaThuocBenhDemo/compare/develop...feature/sprint2-inference
- Sprint 3: https://github.com/tuantran2409/HeChuyenGiaThuocBenhDemo/compare/develop...feature/sprint3-admin-patient

**`gh` CLI:** installed at `C:\Program Files\GitHub CLI\gh.exe` but needs `gh auth login` before `gh pr create` works.
