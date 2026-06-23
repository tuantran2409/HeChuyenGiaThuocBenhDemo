# Dàn Ý Slide Thuyết Trình
# Hệ Chuyên Gia Chẩn Đoán Bệnh và Gợi Ý Thuốc

---

## SLIDE 1 — Trang bìa
- **Tên đề tài:** Hệ Chuyên Gia Thuốc Bệnh
- Môn học: Công nghệ Phần mềm
- Nhóm thực hiện + MSSV
- Giảng viên hướng dẫn
- Ngày thuyết trình

---

## SLIDE 2 — Mục lục
1. Khảo sát & Xác định yêu cầu
2. Phân tích hệ thống (Agile Scrum)
3. Thiết kế hệ thống
4. Xây dựng hệ thống
5. Kiểm thử
6. Triển khai & Đánh giá
7. Demo

---

# CHƯƠNG 1 — KHẢO SÁT VÀ XÁC ĐỊNH YÊU CẦU

## SLIDE 3 — Giới thiệu bài toán
- **Vấn đề thực tế:** Người bệnh khó tự xác định bệnh từ triệu chứng; tra cứu thuốc thủ công dễ nhầm tương tác nguy hiểm
- **Giải pháp:** Ứng dụng desktop hệ chuyên gia — nhập triệu chứng → chẩn đoán bệnh + gợi ý thuốc an toàn
- **Đối tượng sử dụng:** Bác sĩ, dược sĩ, quản trị viên cơ sở y tế

## SLIDE 4 — Product Vision & Mục tiêu
- **Product Vision:** Phần mềm hỗ trợ ra quyết định y tế tại cơ sở — nhanh, chính xác, an toàn
- **Mục tiêu:**
  - Tự động chẩn đoán bệnh dựa trên tập luật IF-THEN
  - Kiểm tra tương tác thuốc, cảnh báo mức độ nguy hiểm
  - Quản lý hồ sơ bệnh nhân & lịch sử chẩn đoán
  - Báo cáo thống kê & xuất PDF

## SLIDE 5 — Phạm vi hệ thống
- **Trong phạm vi:**
  - Chẩn đoán bệnh thông thường (40 bệnh, 50 triệu chứng)
  - Tra cứu 100+ thuốc, 12+ cặp tương tác
  - CRUD bệnh nhân, thuốc, bệnh, người dùng (admin)
  - Báo cáo theo khoảng thời gian, xuất PDF
- **Ngoài phạm vi:** Kết nối thiết bị y tế, chẩn đoán hình ảnh, AI/ML

## SLIDE 6 — Stakeholder & Khảo sát hiện trạng
- **Stakeholder:**
  - Bác sĩ — chẩn đoán, quản lý bệnh nhân
  - Dược sĩ — tra cứu thuốc, kiểm tra tương tác
  - Admin — quản lý hệ thống, xuất báo cáo
- **Hiện trạng:** Tra cứu thủ công qua sách, website; không lưu lịch sử; thiếu cảnh báo tương tác thuốc

## SLIDE 7 — Yêu cầu chức năng
| STT | Chức năng | Vai trò |
|-----|-----------|---------|
| 1 | Đăng nhập / phân quyền | Tất cả |
| 2 | Chẩn đoán bệnh (nhập triệu chứng) | Tất cả |
| 3 | Gợi ý thuốc + cảnh báo tương tác | Tất cả |
| 4 | Tra cứu thuốc | Tất cả |
| 5 | Hồ sơ bệnh nhân + lịch sử | Tất cả |
| 6 | Báo cáo thống kê + xuất PDF | Tất cả |
| 7 | Quản lý thuốc / bệnh / người dùng | Admin |

## SLIDE 8 — Yêu cầu phi chức năng
- **Hiệu năng:** Chẩn đoán < 2 giây với 40 bệnh
- **Bảo mật:** Mật khẩu BCrypt (workFactor=11), phân quyền theo role
- **Độ tin cậy:** Soft delete — không mất dữ liệu lịch sử
- **Nền tảng:** Windows 10/11 64-bit, .NET 8 runtime
- **Khả năng mở rộng:** Tập luật IF-THEN có thể thêm/sửa qua giao diện admin

---

# CHƯƠNG 2 — PHÂN TÍCH HỆ THỐNG THEO AGILE SCRUM

## SLIDE 9 — Product Backlog & Epic
- **5 Epic chính:**
  - Epic 1: Xác thực & phân quyền
  - Epic 2: Hệ chuyên gia suy luận (cốt lõi)
  - Epic 3: Cơ sở tri thức thuốc & bệnh (Admin)
  - Epic 4: Hồ sơ bệnh nhân
  - Epic 5: Báo cáo & triển khai
- **Tổng backlog:** 71 task | 59 Done | 12 Tests & Docs

## SLIDE 10 — Sprint Planning (5 Sprints)
| Sprint | Mục tiêu | Tasks |
|--------|----------|-------|
| Sprint 1 | DB + Auth + UI Shell | 21 |
| Sprint 2 | Inference Engine + Tra cứu thuốc | 22 |
| Sprint 3 | Admin CRUD + Hồ sơ bệnh nhân | 13 |
| Sprint 4 | Báo cáo + Admin Dashboard | 6 |
| Sprint 5 | Kiểm thử + Triển khai + Docs | 9 |

## SLIDE 11 — User Story tiêu biểu
- **US-01:** Là bác sĩ, tôi muốn nhập triệu chứng để nhận danh sách bệnh có thể mắc kèm độ tin cậy
- **US-02:** Là dược sĩ, tôi muốn kiểm tra tương tác giữa nhiều thuốc để tránh kê đơn nguy hiểm
- **US-03:** Là admin, tôi muốn thêm/sửa tập luật IF-THEN để cập nhật cơ sở tri thức
- **Acceptance Criteria US-01:** Kết quả sắp xếp giảm dần theo độ tin cậy ≥ 40%; triệu chứng bắt buộc phải có

## SLIDE 12 — Use Case Diagram
_(Chèn diagram)_
- Actor: Admin, Bác sĩ, Dược sĩ
- Use case chính: Đăng nhập, Chẩn đoán, Tra cứu thuốc, Quản lý bệnh nhân, Báo cáo, Quản lý hệ thống (Admin)

## SLIDE 13 — Activity Diagram — Luồng chẩn đoán
_(Chèn diagram)_
- Nhập triệu chứng → Lọc bệnh ứng viên → Kiểm tra triệu chứng bắt buộc → Tính độ tin cậy → Lọc ≥ 40% → Sắp xếp → Hiển thị + Gợi ý thuốc

## SLIDE 14 — Sequence Diagram — Chẩn đoán
_(Chèn diagram)_
- ChanDoanForm → InferenceService → BenhRepository → ThuocRepository → TuongTacThuocRepository → Trả kết quả UI

## SLIDE 15 — Class Diagram
_(Chèn diagram — tóm tắt các class chính)_
- Models: Benh, TrieuChung, Thuoc, BenhNhan, LichSuChanDoan, TuongTacThuoc, User
- Services: IInferenceService, IAuthService, IBaoCaoService
- Repositories: IBenhRepository, IThuocRepository, IBenhNhanRepository...

---

# CHƯƠNG 3 — THIẾT KẾ HỆ THỐNG

## SLIDE 16 — Kiến trúc hệ thống
```
UI (WinForms)
    ↓
BLL (Services: Inference, Auth, BaoCao)
    ↓
DAL (Dapper Repositories)
    ↓
Database (SQL Server Express)
```
- Dependency direction: UI → BLL → DAL → Models
- Manual DI qua ServiceContainer (static service locator)

## SLIDE 17 — Wireframe & Prototype (Figma)
_(Chèn ảnh Figma)_
- Màn hình Login
- MainForm + Sidebar (phân quyền role)
- ChanDoanForm — nhập triệu chứng + kết quả
- ThuocForm — tìm kiếm + chi tiết + tương tác
- BaoCaoForm — biểu đồ + xuất PDF

## SLIDE 18 — ERD (Entity Relationship Diagram)
_(Chèn ERD)_
- 8 bảng: Users, TrieuChung, Benh, BenhTrieuChung (tập luật), Thuoc, BenhThuoc, TuongTacThuoc, BenhNhan, LichSuChanDoan

## SLIDE 19 — Database Schema (tóm tắt)
| Bảng | Cột quan trọng |
|------|----------------|
| BenhTrieuChung | BenhId, TrieuChungId, TrongSo, BatBuoc |
| TuongTacThuoc | ThuocId1, ThuocId2, MucDo (1/2/3) |
| LichSuChanDoan | BenhNhanId, TrieuChungInput, KetQuaBenh |
| Users | PasswordHash (BCrypt), Role, IsActive |

- Soft delete: IsActive flag trên Benh, Thuoc, Users

## SLIDE 20 — Deployment Diagram
_(Chèn diagram)_
- Máy Windows (Client): WinForms EXE (self-contained, win-x64)
- SQL Server Express: localhost\SQLEXPRESS / Docker container (port 1433)
- Không cần kết nối internet

---

# CHƯƠNG 4 — XÂY DỰNG HỆ THỐNG

## SLIDE 21 — Công nghệ sử dụng
| Layer | Technology |
|-------|------------|
| UI | .NET 8 WinForms + LiveChartsCore |
| Business Logic | .NET 8 + BCrypt.Net-Next + QuestPDF |
| Data Access | Dapper + Microsoft.Data.SqlClient |
| Database | SQL Server Express (Vietnamese_CI_AS) |
| Tests | MSTest v4 |
| Deploy | Docker (SQL Server 2022) + publish.ps1 |

## SLIDE 22 — Cấu trúc Source Code
```
src/
├── Models      → Entities, Enums
├── DAL         → Dapper Repositories (interface + impl)
├── BLL         → Services (InferenceService, AuthService, BaoCaoService)
└── UI          → WinForms Forms + Program.cs
tests/
├── Tests.Unit        → 21 unit tests (không cần DB)
└── Tests.Integration → 18 integration tests (real DB)
```

## SLIDE 23 — Thuật toán Forward Chaining (cốt lõi)
**InferenceService — 3 bước:**
1. Tìm bệnh ứng viên có ít nhất 1 triệu chứng khớp
2. Loại bỏ bệnh thiếu triệu chứng bắt buộc (`BatBuoc=1`)
3. Tính độ tin cậy: `confidence = Σ(TrongSo khớp) / Σ(TrongSo tất cả)`
   - Loại nếu confidence < 40%
   - Sắp xếp giảm dần

**Ví dụ:** Cảm cúm — Input {Sốt, Ớn lạnh} → 3.5/5.0 = **70%**

## SLIDE 24 — Các chức năng chính (Demo screenshots)
_(Chèn ảnh màn hình)_
- **Login** — BCrypt verify, phân quyền hiển thị sidebar
- **Chẩn đoán** — CheckedListBox triệu chứng → kết quả với %
- **Gợi ý thuốc** — danh sách thuốc + cảnh báo tương tác màu
- **Hồ sơ bệnh nhân** — CRUD + lịch sử chẩn đoán
- **Admin: Tập luật** — DataGridView editable, lưu transactional
- **Báo cáo** — LiveCharts ColumnSeries + xuất PDF (QuestPDF)

## SLIDE 25 — Kết quả từng Sprint
| Sprint | Chức năng bàn giao |
|--------|--------------------|
| Sprint 1 | DB 8 bảng, seed data, Login, MainForm shell |
| Sprint 2 | Chẩn đoán, gợi ý thuốc, cảnh báo tương tác, tra cứu thuốc |
| Sprint 3 | Admin CRUD thuốc/bệnh/tập luật, hồ sơ bệnh nhân |
| Sprint 4 | Báo cáo thống kê + biểu đồ + xuất PDF, quản lý người dùng |
| Sprint 5 | Unit tests, Integration tests, Docs, Docker deploy |

---

# CHƯƠNG 5 — KIỂM THỬ

## SLIDE 26 — Kế hoạch kiểm thử
- **Unit Test:** Logic thuần — không cần DB (fake repositories)
  - InferenceServiceTests: 9 test cases
  - AuthServiceTests: 10 test cases
- **Integration Test:** Real DB với seed data
  - InferenceIntegrationTests: 7 test cases
  - RepositoryIntegrationTests: 11 test cases
- **Manual UI Test:** 30 test cases (TC-01 → TC-30)

## SLIDE 27 — Test Case tiêu biểu
| TC | Loại | Mô tả | Kết quả |
|----|------|-------|---------|
| TC-01 | Unit | Forward chaining — triệu chứng bắt buộc thiếu → bị loại | Pass |
| TC-02 | Unit | Confidence < 40% → không trả kết quả | Pass |
| TC-03 | Unit | Đăng nhập sai mật khẩu → lỗi | Pass |
| TC-04 | Integration | Cảm cúm: {Sốt, Ớn lạnh} → confidence 70% | Pass |
| TC-05 | Manual UI | Admin tắt tài khoản chính mình → bị chặn | Pass |

## SLIDE 28 — Kết quả kiểm thử
- **Unit Tests:** 21/21 passed — 0 failed
- **Integration Tests:** 18/18 passed (cần real DB)
- **Manual UI Tests:** 30 test cases — tất cả Pass
- **Coverage:** Logic inference, auth, repository queries, UI flows

---

# CHƯƠNG 6 — TRIỂN KHAI VÀ ĐÁNH GIÁ

## SLIDE 29 — Môi trường triển khai
- **Development:** `dotnet run --project src/HeChuyenGiaThuocBenh.UI`
- **Production:** `publish.ps1` → EXE self-contained win-x64 (~70MB, không cần cài .NET)
- **Database:**
  - Option A: SQL Server Express local (`localhost\SQLEXPRESS`)
  - Option B: Docker (`docker compose up -d` → SQL Server 2022, port 1433)

## SLIDE 30 — Sprint Review & Retrospective
- **Đạt được:**
  - 5 sprints hoàn thành, 59/71 tasks done
  - Core feature: inference engine chạy chính xác
  - Phân quyền 3 role hoạt động đúng
  - PDF report + LiveCharts visual
- **Hạn chế:**
  - Tập luật IF-THEN cứng nhắc — chưa hỗ trợ fuzzy logic
  - Không có web/mobile interface
  - Dữ liệu bệnh/thuốc giới hạn ở mức demo
- **Hướng phát triển:**
  - Tích hợp AI/ML (phân loại bệnh từ text mô tả)
  - Web API + mobile app
  - Kết nối cơ sở dữ liệu y tế quốc gia

## SLIDE 31 — Kết quả đạt được (tổng kết)
| Tiêu chí | Kết quả |
|----------|---------|
| Chức năng | 7/7 hoàn thành |
| Unit Tests | 21 passed |
| Integration Tests | 18 passed |
| Manual Test Cases | 30/30 Pass |
| Tài liệu | README, User Guide, Technical Guide, Test Cases |
| Triển khai | Docker + self-contained EXE |

---

# DEMO LIVE

## SLIDE 32 — Kịch bản Demo
**Thứ tự thực hiện:**
1. Đăng nhập với tài khoản `bacsi1 / BacSi@123`
2. Chẩn đoán: chọn triệu chứng {Sốt, Ớn lạnh, Mệt mỏi} → xem kết quả Cảm cúm 70%
3. Xem gợi ý thuốc + cảnh báo tương tác
4. Tra cứu thuốc "paracetamol" + kiểm tra tương tác 2 thuốc
5. Xem hồ sơ bệnh nhân + lịch sử chẩn đoán
6. Đăng xuất → đăng nhập `admin / Admin@123`
7. Admin: chỉnh sửa tập luật IF-THEN bệnh Cảm cúm
8. Admin: xem báo cáo thống kê + xuất PDF

---

## SLIDE 33 — Q&A
- Cảm ơn thầy/cô và các bạn đã lắng nghe
- Sẵn sàng trả lời câu hỏi

---

_Tổng số slide: ~33 | Thời gian trình bày đề xuất: 15–20 phút + 5 phút Q&A_
