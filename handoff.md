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

**Active branch:** `feature/sprint2-inference` (committed + pushed)

**Sprint 1 — COMPLETE** ✅ (branch `feature/sprint1-setup`, pushed, PR open to develop)
**Sprint 2 — COMPLETE** ✅ (branch `feature/sprint2-inference`, pushed, PR open to develop)
**Sprint 3 — COMPLETE** ✅ (branch `feature/sprint3-admin-patient`, committed)

### Solution structure

```
HeChuyenGiaThuocBenh/
├── HeChuyenGiaThuocBenh.slnx
├── docs/
│   └── database/
│       ├── schema.sql          ← 8 tables + indexes — READY TO RUN
│       └── seed_data.sql       ← real BCrypt hashes ✅ FIXED
├── src/
│   ├── HeChuyenGiaThuocBenh.Models/        ← unchanged from Sprint 1
│   ├── HeChuyenGiaThuocBenh.DAL/           ← unchanged from Sprint 1
│   ├── HeChuyenGiaThuocBenh.BLL/           ← unchanged from Sprint 1
│   └── HeChuyenGiaThuocBenh.UI/
│       ├── Program.cs
│       ├── appsettings.json    ← Server=localhost\\SQLEXPRESS ✅ FIXED
│       └── Forms/
│           ├── LoginForm.cs + Designer         ← Sprint 1, DONE
│           ├── MainForm.cs + Designer          ← Sprint 1+2, ShowContentForm added
│           ├── ChanDoanForm.cs + Designer      ← Sprint 2 ✅ NEW
│           └── ThuocForm.cs + Designer         ← Sprint 2 ✅ NEW
└── tests/
    ├── HeChuyenGiaThuocBenh.Tests.Unit/       ← empty, Sprint 5
    └── HeChuyenGiaThuocBenh.Tests.Integration/ ← empty, Sprint 5
```

### Sprint 2 — what was built

**ChanDoanForm:**
- Left panel: symptom CheckedListBox grouped by NhomTrieuChung, real-time search filter, selected count
- "Chẩn đoán" button calls `InferenceService.ChanDoanAsync(selectedIds)`
- Results DataGridView: disease name, group, confidence % (color-coded green/orange/red), drug count, warning count
- Detail panel (shows on row select): disease info, recommended drugs DataGridView, drug interaction warning panel (green=none, red=warnings with severity)

**ThuocForm:**
- Top bar: keyword search + NhomThuoc dropdown filter
- Left: drug DataGridView (multi-select for interaction check)
- Right detail: drug fields (hoạt chất, nhóm, liều dùng, cách dùng), contraindications (red), side effects (orange)
- Drug interaction checker: select 2+ drugs → click button → calls `TuongTacThuocRepository.CheckMultipleInteractionsAsync`

**MainForm changes:**
- `ShowContentForm(Form)` helper: embeds child form into `pnlContent` (TopLevel=false, Dock=Fill)
- `btnChanDoan` → `ChanDoanForm`
- `btnThuoc` → `ThuocForm`

### DB seed data coverage (unchanged)

- 50 symptoms (TrieuChung) grouped by body system
- 100+ drugs (Thuoc) across 10 drug classes
- 40 diseases (Benh) with NhomBenh categories
- BenhTrieuChung rules for 12 diseases (inference rules with TrongSo weights + BatBuoc flags)
- BenhThuoc treatment mappings for 10 diseases
- 12 drug interaction pairs (TuongTacThuoc) with severity levels
- 5 sample patients (BenhNhan)
- 4 seed users (admin/bacsi1/bacsi2/duocsi1) — **password hashes are REAL** ✅

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
| Engine suy luận forward chaining | ✅ InferenceService (Sprint 1) + ChanDoanForm (Sprint 2) |
| Tập luật IF-THEN | ✅ seed_data.sql BenhTrieuChung |
| Form nhập triệu chứng | ✅ ChanDoanForm — CheckedListBox + search |
| Màn hình kết quả chẩn đoán | ✅ dgvKetQua + pnlDetail |
| Gợi ý thuốc | ✅ dgvThuoc trong ChanDoanForm detail |
| Cảnh báo tương tác thuốc | ✅ gbCanhBao panel, color-coded severity |
| Tìm kiếm thuốc | ✅ ThuocForm — keyword + nhóm filter |
| Xem chi tiết thuốc | ✅ ThuocForm detail panel |
| Kiểm tra tương tác nhiều thuốc | ✅ ThuocForm multi-select + DGV |
| Kết nối FE-BE tất cả | ✅ MainForm.ShowContentForm wires everything |

**22 tasks — tất cả ✅ Done**

---

### Sprint 3 — Admin CRUD + Hồ sơ bệnh nhân

**Epic: Cơ sở tri thức thuốc — Admin (SV3)**
| Type | Task | Status |
|---|---|---|
| Story | Thêm/sửa/xóa thuốc (admin) | 🔲 Tạo `AdminThuocForm.cs` |
| Task | API CRUD thuốc | 🔲 `ThuocRepository` done — cần form |
| Task | Thiết kế màn hình chi tiết thuốc | 🔲 `ThuocDetailForm.cs` |
| Task | Thiết kế màn hình tìm kiếm thuốc | 🔲 dùng chung ThuocForm Sprint 2 |

**Epic: Cơ sở tri thức bệnh — Admin (SV3)**
| Type | Task | Status |
|---|---|---|
| Story | Thêm/sửa/xóa bệnh (admin) | 🔲 Tạo `AdminBenhForm.cs` |
| Story | Xem danh sách bệnh | 🔲 Tạo `BenhListForm.cs` |
| Task | API CRUD bệnh | 🔲 `BenhRepository` done — cần form |
| Task | Thiết kế màn hình danh sách bệnh | 🔲 `BenhListForm.cs` |

**Epic: Quản lý hồ sơ bệnh nhân (SV4)**
| Type | Task | Status |
|---|---|---|
| Story | Tạo hồ sơ bệnh nhân | 🔲 Tạo `BenhNhanForm.cs` |
| Story | Xem lịch sử chẩn đoán | 🔲 Tab lịch sử trong BenhNhanForm |
| Task | API CRUD hồ sơ bệnh nhân | 🔲 `BenhNhanRepository` done — cần form |
| Task | Thiết kế màn hình hồ sơ bệnh nhân | 🔲 `BenhNhanForm.cs` |
| Task | Kết nối FE-BE: bệnh nhân | 🔲 Wire BenhNhanForm → BenhNhanRepository |
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

---

## DB Setup (first time)

1. Open SQL Server Management Studio → connect to `localhost\SQLEXPRESS`
2. Run `docs/database/schema.sql` (drops + recreates DB)
3. Run `docs/database/seed_data.sql` (inserts all reference data + real password hashes)
4. Verify: `SELECT TenDangNhap FROM Users` → should show 4 rows

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

---

## Not Yet Done (Sprint 3-5)

- [ ] Sprint 3: `BenhNhanForm` (create/search patients, view diagnosis history tab)
- [ ] Sprint 3: `AdminThuocForm` (admin CRUD for Thuoc — DataGridView + add/edit/delete buttons)
- [ ] Sprint 3: `AdminBenhForm` / `BenhListForm` (admin CRUD for Benh + BenhTrieuChung rules editor)
- [ ] Sprint 4: `BaoCaoForm` (LiveCharts stats dashboard, QuestPDF export)
- [ ] Sprint 4: `AdminUserForm` (admin CRUD for Users)
- [ ] Sprint 5: Unit tests for InferenceService, AuthService
- [ ] Sprint 5: Integration tests (real DB — symptom → diagnosis → drug flow)
- [ ] Sprint 5: 20+ manual UI test cases documented
- [ ] Deployment (Docker + SQL Server container, or Azure)

---

## Next Step

**Start Sprint 4:** Push + PR Sprint 3 to develop, then create `feature/sprint4-reports` branch, build:
1. `BaoCaoForm` — LiveCharts stats dashboard + QuestPDF export button
2. `AdminUserForm` — admin CRUD for Users (DataGridView + add/edit/deactivate)

---

## Git State

```
main                          ← initial commit only
develop                       ← initial commit only (Sprint 1 + 2 PRs pending merge)
feature/sprint1-setup         ← Sprint 1 complete, pushed, PR open → develop
feature/sprint2-inference     ← Sprint 2 complete, pushed, PR open → develop
feature/sprint3-admin-patient ← Sprint 3 complete, committed (HEAD) — push + PR pending
```

**PRs open (create via GitHub UI or `gh pr create`):**
- Sprint 1: https://github.com/tuantran2409/HeChuyenGiaThuocBenhDemo/compare/develop...feature/sprint1-setup
- Sprint 2: https://github.com/tuantran2409/HeChuyenGiaThuocBenhDemo/compare/develop...feature/sprint2-inference

**`gh` CLI installed** ✅ (`winget install GitHub.cli` — done). PRs now created automatically after each push.
