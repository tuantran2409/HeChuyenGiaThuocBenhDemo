# BÁO CÁO ĐỒ ÁN MÔN CÔNG NGHỆ PHẦN MỀM
## HỆ CHUYÊN GIA THUỐC BỆNH (HeChuyenGia)

---

**Môn học:** Công nghệ Phần mềm  
**Đề tài:** Hệ Chuyên Gia Chẩn Đoán Bệnh và Gợi Ý Thuốc Điều Trị  
**Công nghệ:** .NET 8 WinForms · SQL Server · Dapper · BCrypt · QuestPDF · LiveCharts  
**Năm học:** 2024 – 2025

---

## MỤC LỤC

- [Chương 1. Khảo sát và xác định yêu cầu](#chương-1)
- [Chương 2. Phân tích hệ thống theo Agile Scrum](#chương-2)
- [Chương 3. Thiết kế hệ thống](#chương-3)
- [Chương 4. Xây dựng hệ thống](#chương-4)
- [Chương 5. Kiểm thử](#chương-5)
- [Chương 6. Triển khai và đánh giá](#chương-6)

---

# CHƯƠNG 1. KHẢO SÁT VÀ XÁC ĐỊNH YÊU CẦU

## 1.1 Giới thiệu bài toán

Trong lĩnh vực y tế, việc tra cứu thông tin bệnh và thuốc đòi hỏi kiến thức chuyên sâu của bác sĩ và dược sĩ. Tuy nhiên, các cơ sở y tế nhỏ thường thiếu hệ thống hỗ trợ quyết định lâm sàng tự động, dẫn đến nguy cơ nhầm lẫn trong chẩn đoán và kê đơn.

**Hệ Chuyên Gia Thuốc Bệnh (HeChuyenGia)** là ứng dụng desktop xây dựng trên nền tảng hệ chuyên gia (Expert System) kết hợp cơ sở tri thức y khoa, giúp:

- Bác sĩ chẩn đoán bệnh dựa trên triệu chứng bệnh nhân thông qua thuật toán **Forward Chaining**.
- Gợi ý phác đồ điều trị (danh sách thuốc phù hợp theo từng bệnh).
- Cảnh báo **tương tác thuốc nguy hiểm** khi kê đơn nhiều loại cùng lúc.
- Quản lý hồ sơ bệnh nhân và lịch sử chẩn đoán.
- Xuất báo cáo thống kê định kỳ dạng PDF.

---

## 1.2 Product Vision

> "Xây dựng một hệ thống hỗ trợ quyết định lâm sàng dành cho các cơ sở y tế, giúp bác sĩ và dược sĩ tra cứu nhanh thông tin bệnh – thuốc – tương tác thuốc, giảm thiểu sai sót trong kê đơn và nâng cao chất lượng chăm sóc bệnh nhân."

**FOR** các cơ sở y tế, phòng khám vừa và nhỏ  
**WHO** cần hệ thống hỗ trợ quyết định lâm sàng  
**THE** Hệ Chuyên Gia Thuốc Bệnh  
**IS A** ứng dụng desktop quản lý và chẩn đoán  
**THAT** tự động suy luận forward chaining từ triệu chứng → bệnh → thuốc, cảnh báo tương tác  
**UNLIKE** tra cứu thủ công qua sách hoặc internet  
**OUR PRODUCT** cung cấp cơ sở tri thức được quản lý tập trung, phân quyền theo vai trò, có thể mở rộng  

---

## 1.3 Mục tiêu hệ thống

| # | Mục tiêu | Chỉ số thành công |
|---|---|---|
| 1 | Chẩn đoán bệnh tự động từ triệu chứng | Confidence score ≥ 40% được trả về trong < 1 giây |
| 2 | Gợi ý thuốc điều trị theo bệnh | Hiển thị đầy đủ tên, liều dùng, cách dùng |
| 3 | Cảnh báo tương tác thuốc | Phát hiện và phân loại 3 mức độ: Nhẹ / Trung bình / Nặng |
| 4 | Quản lý hồ sơ bệnh nhân | CRUD đầy đủ, lưu lịch sử chẩn đoán |
| 5 | Báo cáo thống kê + xuất PDF | Biểu đồ theo ngày, xuất file A4 trong < 5 giây |
| 6 | Phân quyền 3 vai trò | Admin / Bác sĩ / Dược sĩ — kiểm soát UI theo role |
| 7 | Bảo mật mật khẩu | BCrypt workFactor=11, không lưu plain text |

---

## 1.4 Phạm vi hệ thống

**Trong phạm vi (In Scope):**

- Quản lý danh mục: Bệnh, Triệu chứng, Thuốc, Tương tác thuốc.
- Engine suy luận: Forward Chaining với tập luật IF-THEN có trọng số.
- Hồ sơ bệnh nhân và lịch sử chẩn đoán.
- Báo cáo thống kê có biểu đồ; xuất PDF.
- Quản lý người dùng theo 3 role.
- Chạy trên Windows 10/11 (64-bit), kết nối SQL Server Express cục bộ.

**Ngoài phạm vi (Out of Scope):**

- Ứng dụng web / mobile.
- Kết nối với hệ thống bệnh viện bên ngoài (HIS, EMR).
- Thanh toán, bảo hiểm y tế.
- Hình ảnh y khoa (X-quang, MRI).

---

## 1.5 Khảo sát hiện trạng

### Vấn đề hiện tại

| Vấn đề | Tác động |
|---|---|
| Bác sĩ tra cứu thủ công qua sách/internet | Mất thời gian, dễ bỏ sót |
| Không có cảnh báo tương tác thuốc tự động | Nguy cơ tác dụng phụ nguy hiểm |
| Hồ sơ bệnh nhân lưu trên giấy | Khó tìm kiếm, dễ thất lạc |
| Thống kê chẩn đoán phải làm thủ công | Tốn nhân lực, sai sót |

### Hệ thống hiện có

Các cơ sở nhỏ thường dùng Excel để quản lý bệnh nhân, không có module chẩn đoán tự động hoặc cảnh báo tương tác thuốc. Các phần mềm HIS (Hospital Information System) lớn như Medisoft, HIS-VN thường quá phức tạp và chi phí cao cho phòng khám nhỏ.

---

## 1.6 Stakeholder

| Stakeholder | Vai trò | Kỳ vọng |
|---|---|---|
| **Bác sĩ** | Người dùng chính — chẩn đoán, kê đơn | Chẩn đoán nhanh, gợi ý thuốc chính xác |
| **Dược sĩ** | Người dùng phụ — tra cứu thuốc, tương tác | Tìm kiếm thuốc nhanh, cảnh báo tương tác |
| **Quản trị viên** | Quản lý hệ thống | CRUD danh mục, quản lý users |
| **Bệnh nhân** | Đối tượng phục vụ (gián tiếp) | Được chẩn đoán chính xác, an toàn |
| **Nhóm phát triển** | Developers | Hoàn thành đồ án đúng hạn |
| **Giảng viên hướng dẫn** | Giám sát, đánh giá | Đúng quy trình SDLC, đầy đủ artifact |

---

## 1.7 Yêu cầu chức năng

### FR-01: Xác thực người dùng

- FR-01.1: Đăng nhập bằng Username/Password (BCrypt verify).
- FR-01.2: Phân quyền UI theo Role: Admin=1, BacSi=2, DuocSi=3.
- FR-01.3: Đăng xuất và quay về màn hình Login.
- FR-01.4: Không cho phép đăng nhập với tài khoản IsActive=false.

### FR-02: Chẩn đoán bệnh (Forward Chaining)

- FR-02.1: Hiển thị danh sách 50+ triệu chứng phân theo nhóm cơ thể.
- FR-02.2: Tìm kiếm triệu chứng theo tên.
- FR-02.3: Chọn nhiều triệu chứng bằng CheckedListBox.
- FR-02.4: Chạy engine suy luận → trả về danh sách bệnh ứng viên có confidence score.
- FR-02.5: Hiển thị chi tiết bệnh được chọn (mô tả, độ tin cậy, thuốc gợi ý).
- FR-02.6: Cảnh báo tương tác thuốc theo mức độ (Nhẹ/Trung bình/Nặng).
- FR-02.7: Lưu kết quả vào LichSuChanDoan.

### FR-03: Tra cứu thuốc

- FR-03.1: Tìm kiếm thuốc theo tên hoặc hoạt chất.
- FR-03.2: Lọc theo nhóm thuốc (ComboBox).
- FR-03.3: Xem chi tiết: liều dùng, cách dùng, chống chỉ định, tác dụng phụ.
- FR-03.4: Chọn nhiều thuốc để kiểm tra tương tác đa thuốc.

### FR-04: Hồ sơ bệnh nhân

- FR-04.1: Thêm mới bệnh nhân (HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, TienSuBenh, DiUng).
- FR-04.2: Sửa thông tin bệnh nhân.
- FR-04.3: Tìm kiếm bệnh nhân theo tên hoặc số điện thoại.
- FR-04.4: Xem lịch sử chẩn đoán của từng bệnh nhân.

### FR-05: Báo cáo thống kê

- FR-05.1: Chọn khoảng ngày (từ ngày – đến ngày).
- FR-05.2: Hiển thị biểu đồ cột số lượng chẩn đoán theo ngày (LiveCharts).
- FR-05.3: Hiển thị bảng chi tiết các lần chẩn đoán trong kỳ.
- FR-05.4: Xuất báo cáo PDF (A4, QuestPDF).

### FR-06: Quản lý thuốc (Admin)

- FR-06.1: Thêm mới, chỉnh sửa thông tin thuốc.
- FR-06.2: Soft delete (IsActive=false) thay vì xóa cứng.
- FR-06.3: Tìm kiếm và lọc theo nhóm thuốc.

### FR-07: Quản lý bệnh & tập luật (Admin)

- FR-07.1: CRUD bệnh (tên, nhóm, mô tả, IsActive).
- FR-07.2: Quản lý tập luật IF-THEN cho mỗi bệnh (triệu chứng, trọng số, bắt buộc).
- FR-07.3: Lưu tập luật theo giao dịch (transactional DELETE + INSERT).
- FR-07.4: Xem trước confidence khi nhập input thử.

### FR-08: Quản lý người dùng (Admin)

- FR-08.1: Thêm mới người dùng với BCrypt hash.
- FR-08.2: Chỉnh sửa thông tin, đổi mật khẩu tùy chọn.
- FR-08.3: Bật/tắt tài khoản (không xóa cứng).
- FR-08.4: Phân quyền Role (Admin/BacSi/DuocSi).
- FR-08.5: Reset mật khẩu về mặc định.
- FR-08.6: Không cho phép tắt tài khoản đang đăng nhập.

---

## 1.8 Yêu cầu phi chức năng

| # | Loại | Yêu cầu |
|---|---|---|
| NFR-01 | Hiệu năng | Engine chẩn đoán trả kết quả < 1 giây với 40 bệnh và 50 triệu chứng |
| NFR-02 | Bảo mật | Mật khẩu BCrypt workFactor=11; không lưu plain text |
| NFR-03 | Tính khả dụng | Ứng dụng desktop, không cần internet sau khi cài đặt |
| NFR-04 | Khả năng mở rộng | Thêm bệnh/thuốc/triệu chứng mới qua Admin UI mà không cần sửa code |
| NFR-05 | Tính nhất quán dữ liệu | Xóa bệnh cascade sang BenhTrieuChung và BenhThuoc |
| NFR-06 | Tương thích | Windows 10/11 64-bit, .NET 8 Runtime |
| NFR-07 | Ngôn ngữ | Tiếng Việt (collation Vietnamese_CI_AS) |
| NFR-08 | Khôi phục | Soft delete cho Benh, Thuoc, Users — không mất dữ liệu lịch sử |

---

# CHƯƠNG 2. PHÂN TÍCH HỆ THỐNG THEO AGILE SCRUM

## 2.1 Product Backlog

Product Backlog bao gồm toàn bộ 71 task được phân chia theo 5 Sprint:

| Sprint | Mô tả | Số task | Trạng thái |
|---|---|---|---|
| Sprint 1 | Nền móng — DB + Auth + UI shell | 21 | ✅ DONE |
| Sprint 2 | Engine suy luận + Tra cứu thuốc | 22 | ✅ DONE |
| Sprint 3 | Admin CRUD + Hồ sơ bệnh nhân | 13 | ✅ DONE |
| Sprint 4 | Báo cáo + Admin dashboard | 6 | ✅ DONE |
| Sprint 5 | Kiểm thử + Triển khai | 9 | ✅ DONE |
| **Tổng** | | **71** | **59 Done** |

---

## 2.2 Epic

| Epic ID | Tên Epic | Sprint |
|---|---|---|
| EP-01 | Nền tảng hệ thống (DB + Config) | Sprint 1 |
| EP-02 | Xác thực & phân quyền | Sprint 1, Sprint 4 |
| EP-03 | Hệ chuyên gia suy luận (Inference Engine) | Sprint 2 |
| EP-04 | Tra cứu thuốc & tương tác thuốc | Sprint 2 |
| EP-05 | Quản lý hồ sơ bệnh nhân | Sprint 3 |
| EP-06 | Cơ sở tri thức thuốc (Admin CRUD) | Sprint 3 |
| EP-07 | Cơ sở tri thức bệnh & tập luật (Admin CRUD) | Sprint 3 |
| EP-08 | Báo cáo & thống kê | Sprint 4 |
| EP-09 | Triển khai & kiểm thử | Sprint 5 |

---

## 2.3 User Story

### EP-03 — Hệ chuyên gia suy luận

**US-03.1:** Là bác sĩ, tôi muốn chọn các triệu chứng của bệnh nhân để hệ thống tự động đề xuất các bệnh có thể mắc phải, để tôi có thể nhanh chóng đưa ra phác đồ điều trị.

**US-03.2:** Là bác sĩ, tôi muốn xem danh sách thuốc gợi ý kèm theo liều dùng cho bệnh được chẩn đoán, để không phải tra cứu thủ công.

**US-03.3:** Là bác sĩ, tôi muốn được cảnh báo khi các thuốc gợi ý có tương tác nguy hiểm với nhau, để bảo vệ an toàn cho bệnh nhân.

### EP-04 — Tra cứu thuốc

**US-04.1:** Là dược sĩ, tôi muốn tìm kiếm thuốc theo tên hoặc hoạt chất và xem đầy đủ thông tin chi tiết, để tư vấn cho bệnh nhân chính xác.

**US-04.2:** Là dược sĩ, tôi muốn chọn nhiều thuốc và kiểm tra tương tác đồng thời, để phát hiện các cặp thuốc nguy hiểm trước khi phát.

### EP-05 — Hồ sơ bệnh nhân

**US-05.1:** Là bác sĩ, tôi muốn tạo và quản lý hồ sơ bệnh nhân (thông tin cá nhân, tiền sử bệnh, dị ứng), để theo dõi toàn diện sức khỏe.

**US-05.2:** Là bác sĩ, tôi muốn xem lịch sử chẩn đoán của từng bệnh nhân, để theo dõi diễn tiến bệnh theo thời gian.

### EP-08 — Báo cáo

**US-08.1:** Là quản lý cơ sở y tế, tôi muốn xem thống kê số lần chẩn đoán theo ngày qua biểu đồ, để nắm được tải hoạt động của phòng khám.

**US-08.2:** Là quản lý, tôi muốn xuất báo cáo PDF để lưu hồ sơ và nộp cho cấp trên.

---

## 2.4 Acceptance Criteria

### US-03.1 — Chẩn đoán bệnh

```
GIVEN bác sĩ đã đăng nhập
WHEN bác sĩ chọn ít nhất 1 triệu chứng và nhấn "Chẩn đoán"
THEN hệ thống hiển thị danh sách bệnh ứng viên với confidence score (%)
  AND các bệnh có confidence < 40% bị loại khỏi kết quả
  AND kết quả sắp xếp giảm dần theo confidence
  AND thời gian xử lý < 1 giây
```

```
GIVEN triệu chứng bắt buộc (BatBuoc=1) của một bệnh không có trong input
WHEN engine suy luận chạy
THEN bệnh đó không xuất hiện trong kết quả
```

### US-04.2 — Kiểm tra tương tác

```
GIVEN dược sĩ chọn Paracetamol + Warfarin
WHEN nhấn "Kiểm tra tương tác"
THEN cảnh báo hiển thị: "Tăng nguy cơ chảy máu — Mức độ Trung bình"
  AND màu cảnh báo: Nhẹ=xanh lá, Trung bình=vàng, Nặng=đỏ
```

---

## 2.5 Sprint Planning

### Sprint 1 (2 tuần) — Nền móng

**Sprint Goal:** Xây dựng database, xác thực người dùng và khung UI chính.

**Kết quả kỳ vọng:**
- Database 8 bảng, seed data đầy đủ.
- LoginForm hoạt động (BCrypt).
- MainForm shell với sidebar phân quyền.

### Sprint 2 (2 tuần) — Hệ chuyên gia

**Sprint Goal:** Triển khai engine Forward Chaining và module tra cứu thuốc.

**Kết quả kỳ vọng:**
- ChanDoanForm: chọn triệu chứng → kết quả chẩn đoán → thuốc gợi ý → cảnh báo tương tác.
- ThuocForm: tìm kiếm + xem chi tiết + kiểm tra tương tác đa thuốc.

### Sprint 3 (2 tuần) — Admin & Bệnh nhân

**Sprint Goal:** CRUD danh mục bệnh/thuốc cho Admin và quản lý hồ sơ bệnh nhân.

**Kết quả kỳ vọng:**
- AdminThuocForm, AdminBenhForm (với editor tập luật).
- BenhNhanForm (CRUD + lịch sử chẩn đoán).

### Sprint 4 (1 tuần) — Báo cáo

**Sprint Goal:** Module báo cáo thống kê có biểu đồ và xuất PDF; Admin quản lý users.

**Kết quả kỳ vọng:**
- BaoCaoForm với LiveCharts.
- Xuất PDF bằng QuestPDF.
- AdminUserForm.

### Sprint 5 (1 tuần) — Kiểm thử & Triển khai

**Sprint Goal:** Viết test cases, đóng gói ứng dụng, hoàn thiện tài liệu.

**Kết quả kỳ vọng:**
- 21 unit tests + integration tests.
- `publish.ps1` → EXE self-contained.
- `docker-compose.yml` cho SQL Server.

---

## 2.6 Sprint Backlog

### Sprint 2 — Chi tiết Sprint Backlog

| ID | User Story | Task | Ước tính (giờ) | Người thực hiện | Trạng thái |
|---|---|---|---|---|---|
| T-2.01 | US-03.1 | Viết `IInferenceService` interface | 1h | SV1 | ✅ |
| T-2.02 | US-03.1 | Implement `InferenceService.RunAsync()` — Forward Chaining | 4h | SV1 | ✅ |
| T-2.03 | US-03.1 | Viết `IBenhRepository.GetByTrieuChungIdsAsync` | 2h | SV2 | ✅ |
| T-2.04 | US-03.1 | Thiết kế `ChanDoanForm` — CheckedListBox + search | 3h | SV3 | ✅ |
| T-2.05 | US-03.1 | Kết nối `ChanDoanForm` ↔ `InferenceService` | 2h | SV3 | ✅ |
| T-2.06 | US-03.2 | Hiển thị thuốc gợi ý trong panel kết quả | 2h | SV3 | ✅ |
| T-2.07 | US-03.3 | Implement `TuongTacThuocRepository` | 2h | SV2 | ✅ |
| T-2.08 | US-03.3 | Hiển thị panel cảnh báo tương tác thuốc (màu sắc) | 2h | SV3 | ✅ |
| T-2.09 | US-04.1 | `ThuocForm` — tìm kiếm + filter nhóm + xem chi tiết | 3h | SV4 | ✅ |
| T-2.10 | US-04.2 | Kiểm tra tương tác đa thuốc trong `ThuocForm` | 2h | SV4 | ✅ |

---

## 2.7 Use Case Diagram

### Sơ đồ Use Case tổng quát

```
                    ┌─────────────────────────────────────────────┐
                    │              HeChuyenGia System              │
                    │                                             │
  ┌──────┐          │  ┌─────────────────────────────────────┐   │
  │ Bác  │──────────┼─▶│ UC-01: Đăng nhập                   │   │
  │  sĩ  │          │  └─────────────────────────────────────┘   │
  └──┬───┘          │  ┌─────────────────────────────────────┐   │
     │              │  │ UC-02: Chẩn đoán bệnh (FC Engine)  │   │
     ├──────────────┼─▶│   << include >> UC-02a: Chọn TC    │   │
     │              │  │   << include >> UC-02b: Xem kết quả│   │
     │              │  │   << include >> UC-02c: Xem thuốc  │   │
     │              │  │   << extend  >> UC-02d: Cảnh báo TT│   │
     │              │  └─────────────────────────────────────┘   │
     │              │  ┌─────────────────────────────────────┐   │
     ├──────────────┼─▶│ UC-03: Tra cứu thuốc               │   │
     │              │  │   << extend  >> UC-03a: Kiểm tra TT│   │
     │              │  └─────────────────────────────────────┘   │
     │              │  ┌─────────────────────────────────────┐   │
     ├──────────────┼─▶│ UC-04: Quản lý hồ sơ bệnh nhân    │   │
     │              │  └─────────────────────────────────────┘   │
     │              │  ┌─────────────────────────────────────┐   │
     └──────────────┼─▶│ UC-05: Xem báo cáo thống kê        │   │
                    │  │   << extend  >> UC-05a: Xuất PDF   │   │
                    │  └─────────────────────────────────────┘   │
                    │                                             │
  ┌─────────┐       │  ┌─────────────────────────────────────┐   │
  │ Dược sĩ │───────┼─▶│ UC-01, UC-03 (tra cứu thuốc)       │   │
  └─────────┘       │  └─────────────────────────────────────┘   │
                    │                                             │
  ┌───────┐         │  ┌─────────────────────────────────────┐   │
  │ Admin │─────────┼─▶│ UC-06: Quản lý thuốc (Admin CRUD)  │   │
  └───┬───┘         │  └─────────────────────────────────────┘   │
      │             │  ┌─────────────────────────────────────┐   │
      ├─────────────┼─▶│ UC-07: Quản lý bệnh & tập luật     │   │
      │             │  └─────────────────────────────────────┘   │
      │             │  ┌─────────────────────────────────────┐   │
      └─────────────┼─▶│ UC-08: Quản lý người dùng          │   │
                    │  └─────────────────────────────────────┘   │
                    └─────────────────────────────────────────────┘
```

---

## 2.8 Use Case Specification

### UC-02: Chẩn đoán bệnh

| Thuộc tính | Nội dung |
|---|---|
| **Use Case ID** | UC-02 |
| **Tên** | Chẩn đoán bệnh bằng Forward Chaining |
| **Actor chính** | Bác sĩ |
| **Tiền điều kiện** | Đã đăng nhập, có ít nhất 1 bệnh có tập luật trong DB |
| **Hậu điều kiện** | Kết quả chẩn đoán được hiển thị; lịch sử được lưu vào LichSuChanDoan |

**Luồng chính:**
1. Bác sĩ mở màn hình "Chẩn đoán bệnh".
2. Hệ thống tải danh sách triệu chứng phân nhóm từ DB.
3. Bác sĩ tích chọn các triệu chứng (CheckedListBox + tìm kiếm).
4. Bác sĩ nhấn "Chẩn đoán".
5. Hệ thống gọi `InferenceService.RunAsync(trieuChungIds)`.
6. Engine lọc bệnh theo bắt buộc → tính confidence → lọc < 40% → sắp xếp giảm dần.
7. Hệ thống hiển thị danh sách bệnh ứng viên kèm confidence score.
8. Bác sĩ chọn một bệnh để xem chi tiết (thuốc gợi ý, cảnh báo tương tác).
9. Hệ thống lưu kết quả vào `LichSuChanDoan`.

**Luồng thay thế:**
- 3a. Không chọn triệu chứng → thông báo lỗi, dừng.
- 6a. Không có bệnh nào đạt ngưỡng → thông báo "Không tìm thấy bệnh phù hợp".

---

## 2.9 Activity Diagram

### Luồng Chẩn đoán bệnh

```
[Bắt đầu]
    │
    ▼
[Mở màn hình Chẩn đoán]
    │
    ▼
[Tải danh sách triệu chứng từ DB]
    │
    ▼
[Bác sĩ tích chọn triệu chứng]
    │
    ▼
[Nhấn "Chẩn đoán"]
    │
    ▼
{Có triệu chứng được chọn?}
    │ Không              │ Có
    ▼                    ▼
[Báo lỗi]    [Lấy danh sách bệnh có ít nhất 1 triệu chứng khớp]
                          │
                          ▼
               [Duyệt từng bệnh ứng viên]
                          │
                          ▼
               {Thiếu triệu chứng bắt buộc?}
                 │ Có           │ Không
                 ▼              ▼
               [Loại bỏ]  [Tính confidence = Σkhớp / Σtổng]
                                │
                                ▼
                         {confidence ≥ 40%?}
                           │ Không    │ Có
                           ▼          ▼
                         [Loại]  [Giữ trong kết quả]
                                       │
                          ▼ (sau tất cả bệnh)
               [Sắp xếp giảm dần theo confidence]
                          │
                          ▼
               [Hiển thị kết quả + chi tiết thuốc]
                          │
                          ▼
               [Lưu LichSuChanDoan]
                          │
                          ▼
                       [Kết thúc]
```

---

## 2.10 Sequence Diagram

### SD-01: Luồng Chẩn đoán (Forward Chaining)

```
ChanDoanForm       InferenceService     BenhRepository    TuongTacThuocRepo
     │                    │                   │                  │
     │ RunAsync(ids)       │                   │                  │
     │───────────────────▶│                   │                  │
     │                    │ GetByTrieuChungIds │                  │
     │                    │──────────────────▶│                  │
     │                    │◀──────────────────│ benhList         │
     │                    │                   │                  │
     │                    │ [forEach benh]     │                  │
     │                    │ CheckMandatory()  │                  │
     │                    │ CalcConfidence()  │                  │
     │                    │ Filter < 40%      │                  │
     │                    │ Sort desc         │                  │
     │                    │                   │                  │
     │                    │ GetTuongTac(thuocIds)               │
     │                    │────────────────────────────────────▶│
     │                    │◀────────────────────────────────────│ interactions
     │                    │                   │                  │
     │◀───────────────────│ ChanDoanResult[]  │                  │
     │                    │                   │                  │
     │ DisplayResults()   │                   │                  │
     │ SaveLichSu()       │                   │                  │
```

---

## 2.11 Class Diagram

### Tầng Models

```
┌──────────────┐        ┌─────────────────┐
│    Benh      │        │   TrieuChung    │
├──────────────┤        ├─────────────────┤
│ Id: int      │        │ Id: int         │
│ Ten: string  │        │ Ten: string     │
│ MoTa: string │        │ MoTa: string    │
│ NhomBenh: str│        │ NhomTrieuChung  │
│ IsActive: bit│        └─────────────────┘
└──────┬───────┘                 │
       │ 1                       │ *
       │         ┌───────────────────────┐
       └────────▶│   BenhTrieuChung      │
                 ├───────────────────────┤
                 │ BenhId: int           │
                 │ TrieuChungId: int     │
                 │ TrongSo: decimal(3,1) │
                 │ BatBuoc: bool         │
                 └───────────────────────┘

┌──────────────┐        ┌─────────────────────┐
│    Thuoc     │        │   TuongTacThuoc     │
├──────────────┤        ├─────────────────────┤
│ Id: int      │◀───────│ ThuocId1: int       │
│ Ten: string  │        │ ThuocId2: int       │
│ HoatChat: str│        │ MucDo: byte (1/2/3) │
│ NhomThuoc    │        │ MoTa: string        │
│ LieuDung     │        │ HauQua: string      │
│ CachDung     │        └─────────────────────┘
│ ChongChiDinh │
│ TacDungPhu   │
│ IsActive: bit│
└──────────────┘

┌──────────────┐        ┌───────────────────────────┐
│  BenhNhan    │        │     LichSuChanDoan        │
├──────────────┤        ├───────────────────────────┤
│ Id: int      │1──────*│ Id: int                   │
│ HoTen        │        │ BenhNhanId: int           │
│ NgaySinh     │        │ UserId: int               │
│ GioiTinh     │        │ NgayChanDoan: DateTime    │
│ SoDienThoai  │        │ TrieuChungInput: string   │
│ DiaChi       │        │ KetQuaBenh: string        │
│ TienSuBenh   │        │ ThuocGoiY: string         │
│ DiUng        │        └───────────────────────────┘
└──────────────┘

┌──────────────┐        ┌───────────────────────────┐
│    User      │        │      AppSession           │
├──────────────┤        ├───────────────────────────┤
│ Id: int      │        │ CurrentUser: User (static)│
│ Username     │        │ IsAdmin: bool             │
│ PasswordHash │        │ IsBacSi: bool             │
│ HoTen        │        │ IsDuocSi: bool            │
│ Role: byte   │        └───────────────────────────┘
│ IsActive: bit│
└──────────────┘

┌──────────────────────────────────────────────────────┐
│                   InferenceService                    │
├──────────────────────────────────────────────────────┤
│ - _benhRepo: IBenhRepository                         │
│ - _thuocRepo: IThuocRepository                       │
│ - _tuongTacRepo: ITuongTacThuocRepository            │
├──────────────────────────────────────────────────────┤
│ + RunAsync(trieuChungIds: IEnumerable<int>)          │
│   : Task<IEnumerable<ChanDoanResult>>                │
│ - CheckMandatory(rules, input): bool                 │
│ - CalcConfidence(rules, input): double               │
└──────────────────────────────────────────────────────┘

┌───────────────────────────────────────────────────┐
│                 ChanDoanResult                     │
├───────────────────────────────────────────────────┤
│ Benh: Benh                                        │
│ DoTinCay: double (0.0 – 1.0)                      │
│ ThuocGoiY: IEnumerable<Thuoc>                     │
│ TuongTacCanhBao: IEnumerable<TuongTacThuoc>       │
└───────────────────────────────────────────────────┘
```

---

# CHƯƠNG 3. THIẾT KẾ HỆ THỐNG

## 3.1 Kiến trúc hệ thống

Hệ thống áp dụng kiến trúc **4 tầng (4-Layer Architecture)** với nguyên tắc phụ thuộc một chiều:

```
┌─────────────────────────────────────────────────────────┐
│                   UI Layer (.NET 8 WinForms)            │
│  LoginForm · MainForm · ChanDoanForm · ThuocForm        │
│  BenhNhanForm · BaoCaoForm · AdminThuocForm             │
│  AdminBenhForm · AdminUserForm                          │
└──────────────────────────┬──────────────────────────────┘
                           │ uses
┌──────────────────────────▼──────────────────────────────┐
│               BLL Layer (Business Logic)                │
│  InferenceService · AuthService · BaoCaoService         │
│  ServiceContainer (Manual DI)  · AppSession             │
└──────────────────────────┬──────────────────────────────┘
                           │ uses
┌──────────────────────────▼──────────────────────────────┐
│               DAL Layer (Data Access)                   │
│  BenhRepository · ThuocRepository · TrieuChungRepository│
│  TuongTacThuocRepository · BenhNhanRepository           │
│  LichSuChanDoanRepository · UserRepository              │
│  DbConnectionFactory (Dapper + SqlClient)               │
└──────────────────────────┬──────────────────────────────┘
                           │ reads/writes
┌──────────────────────────▼──────────────────────────────┐
│          Models Layer (Entities + Enums)                │
│  Benh · Thuoc · TrieuChung · BenhNhan · User            │
│  LichSuChanDoan · TuongTacThuoc · Enums (Role, MucDo)  │
└─────────────────────────────────────────────────────────┘
                           │
┌──────────────────────────▼──────────────────────────────┐
│              SQL Server Express Database                │
│  8 tables · Vietnamese_CI_AS collation                  │
└─────────────────────────────────────────────────────────┘
```

**Dependency direction:** `UI → BLL → DAL → Models` (Models không phụ thuộc gì).

---

## 3.2 Thiết kế giao diện bằng Figma

### 3.2.1 Wireframe

Hệ thống có 8 màn hình wireframe được thiết kế trong Figma (file `docs/figma-plugin.js`):

| Màn hình | Mô tả |
|---|---|
| 01 · Login | Form đăng nhập với logo, username, password |
| 02 · Chẩn đoán bệnh | Panel triệu chứng trái + kết quả chẩn đoán phải |
| 03 · Tra cứu thuốc | Danh sách thuốc trái + chi tiết + kiểm tra tương tác phải |
| 04 · Hồ sơ bệnh nhân | Danh sách bệnh nhân trái + form chi tiết + lịch sử phải |
| 05 · Báo cáo thống kê | Bộ lọc ngày + biểu đồ cột + bảng chi tiết |
| 06 · Admin · Quản lý thuốc | Danh sách thuốc + form chỉnh sửa |
| 07 · Admin · Quản lý bệnh & tập luật | Danh sách bệnh + tab thông tin + tab tập luật IF-THEN |
| 08 · Admin · Quản lý users | Danh sách users + form tài khoản |

### 3.2.2 Prototype

Prototype thể hiện luồng tương tác chính:
1. Login → MainForm (sidebar phân quyền).
2. Sidebar → Chẩn đoán → chọn triệu chứng → chạy → xem kết quả → xem thuốc.
3. Sidebar → Tra cứu thuốc → tìm kiếm → xem chi tiết → kiểm tra tương tác.
4. Admin sidebar → Quản lý bệnh → tab Tập luật → thêm/sửa/xóa luật → lưu.

### 3.2.3 Design System

**Màu sắc:**

| Token | Hex | Dùng cho |
|---|---|---|
| Primary | #2A6BB8 | Button chính, link, accent bar |
| Success | #1F9E52 | Badge Active, success strip |
| Danger | #D12121 | Badge Off, chống chỉ định, cảnh báo dị ứng |
| Warning | #BF7A03 | Cảnh báo tương tác mức trung bình |
| Sidebar | #1F2B45 | Nền sidebar |

**Typography:** Inter — Regular 12px (nội dung), Semi Bold 13px (tiêu đề section), Bold 17–20px (tiêu đề trang).

---

## 3.3 Thiết kế cơ sở dữ liệu

### 3.3.1 ERD (Entity Relationship Diagram)

```
Users ──────────────────────────────────── LichSuChanDoan
 │Id                                               │BenhNhanId
 │Username                                         │UserId
 │PasswordHash                                     │NgayChanDoan
 │HoTen                                            │TrieuChungInput
 │Role (1=Admin/2=BacSi/3=DuocSi)                 │KetQuaBenh
 │IsActive                                         │ThuocGoiY
                                                   │
                                          BenhNhan ─┘
                                           │Id
                                           │HoTen
                                           │NgaySinh
                                           │GioiTinh
                                           │SoDienThoai
                                           │DiaChi
                                           │TienSuBenh
                                           │DiUng

Benh ──────────── BenhTrieuChung ──────── TrieuChung
 │Id               │BenhId                  │Id
 │Ten              │TrieuChungId            │Ten
 │MoTa             │TrongSo (0.1–5.0)       │NhomTrieuChung
 │NhomBenh         │BatBuoc (0/1)
 │IsActive

Benh ──────────── BenhThuoc ────────────── Thuoc
 │Id               │BenhId                  │Id
                   │ThuocId                 │Ten
                   │ThuTu                   │HoatChat
                                            │NhomThuoc
Thuoc ──────────── TuongTacThuoc            │LieuDung
                   │ThuocId1                │CachDung
                   │ThuocId2                │ChongChiDinh
                   │MucDo (1/2/3)           │IsActive
                   │MoTa
                   │HauQua
```

### 3.3.2 Database Schema

Chi tiết schema xem tại [docs/database/schema.sql](database/schema.sql).

**8 bảng chính:**

| Bảng | Rows (seed) | Mô tả |
|---|---|---|
| `Users` | 4 | Tài khoản (BCrypt hash) |
| `TrieuChung` | 50 | Triệu chứng phân nhóm cơ thể |
| `Benh` | 40 | Bệnh phân nhóm |
| `BenhTrieuChung` | ~80 | Tập luật IF-THEN (12 bệnh có rules) |
| `Thuoc` | 100+ | Thuốc phân nhóm |
| `BenhThuoc` | ~60 | Phác đồ điều trị (10 bệnh) |
| `TuongTacThuoc` | 12 | Cặp tương tác (3 mức độ) |
| `BenhNhan` | 5 | Bệnh nhân mẫu |
| `LichSuChanDoan` | 0 (runtime) | Kết quả chẩn đoán |

**Indexes tối ưu:**
```sql
CREATE INDEX IX_Thuoc_Ten       ON Thuoc(Ten);
CREATE INDEX IX_Thuoc_HoatChat  ON Thuoc(HoatChat);
CREATE INDEX IX_Benh_Ten        ON Benh(Ten);
CREATE INDEX IX_BenhNhan_HoTen  ON BenhNhan(HoTen);
CREATE INDEX IX_LichSu_BenhNhan ON LichSuChanDoan(BenhNhanId);
CREATE INDEX IX_LichSu_NgayChan ON LichSuChanDoan(NgayChanDoan);
```

---

## 3.4 Deployment Diagram

```
┌─────────────────────────────────────────────────┐
│              Máy tính người dùng (Windows 10/11) │
│                                                  │
│  ┌─────────────────────────────────────┐         │
│  │   HeChuyenGiaThuocBenh.UI.exe       │         │
│  │   (.NET 8 WinForms self-contained)  │         │
│  │                                     │         │
│  │  ┌──────────┐  ┌─────────────────┐  │         │
│  │  │   BLL    │  │      DAL        │  │         │
│  │  │ Services │  │  Repositories   │  │         │
│  │  └──────────┘  └────────┬────────┘  │         │
│  └───────────────────────  │ ──────────┘         │
│                             │ SqlClient           │
│  ┌──────────────────────────▼──────────────────┐  │
│  │   SQL Server Express (localhost\SQLEXPRESS)  │  │
│  │   Database: HeChuyenGiaThuocBenh            │  │
│  │   Collation: Vietnamese_CI_AS               │  │
│  └─────────────────────────────────────────────┘  │
│                                                  │
│  ── HOẶC (Docker mode) ──                        │
│  ┌─────────────────────────────────────────────┐  │
│  │   Docker Container: hcgtb-sqlserver         │  │
│  │   Image: mcr.microsoft.com/mssql/server:2022│  │
│  │   Port: 1433 → localhost:1433               │  │
│  └─────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────┘
```

---

# CHƯƠNG 4. XÂY DỰNG HỆ THỐNG

## 4.1 Công nghệ sử dụng

| Layer | Công nghệ | Phiên bản | Mục đích |
|---|---|---|---|
| UI | .NET 8 WinForms | net8.0-windows | Giao diện desktop |
| UI | LiveChartsCore.SkiaSharpView.WinForms | 2.x | Biểu đồ cột thống kê |
| BLL | .NET 8 Class Library | 8.0 | Business logic |
| BLL | BCrypt.Net-Next | 4.x | Hash/verify mật khẩu |
| BLL | QuestPDF | 2024.x | Xuất báo cáo PDF |
| DAL | Dapper | 2.x | Micro-ORM, query SQL |
| DAL | Microsoft.Data.SqlClient | 5.x | Kết nối SQL Server |
| DB | SQL Server Express | 2019/2022 | RDBMS |
| Tests | MSTest v4 | 4.x | Unit + Integration tests |
| DevOps | Docker | 24.x | SQL Server container |
| Build | dotnet publish | 8.0 | EXE self-contained |

---

## 4.2 Cấu trúc source code

```
HeChuyenGiaThuocBenhDemo/
├── src/
│   ├── HeChuyenGiaThuocBenh.Models/
│   │   ├── Benh.cs
│   │   ├── BenhNhan.cs
│   │   ├── LichSuChanDoan.cs
│   │   ├── Thuoc.cs
│   │   ├── TrieuChung.cs
│   │   ├── TuongTacThuoc.cs
│   │   ├── User.cs
│   │   └── Enums.cs          ← Role, GioiTinh, MucDoTuongTac
│   │
│   ├── HeChuyenGiaThuocBenh.DAL/
│   │   ├── DbConnectionFactory.cs
│   │   └── Repositories/
│   │       ├── IBenhRepository.cs + BenhRepository.cs
│   │       ├── IThuocRepository.cs + ThuocRepository.cs
│   │       ├── ITrieuChungRepository.cs + TrieuChungRepository.cs
│   │       ├── ITuongTacThuocRepository.cs + TuongTacThuocRepository.cs
│   │       ├── IBenhNhanRepository.cs + BenhNhanRepository.cs
│   │       ├── ILichSuChanDoanRepository.cs + LichSuChanDoanRepository.cs
│   │       └── IUserRepository.cs + UserRepository.cs
│   │
│   ├── HeChuyenGiaThuocBenh.BLL/
│   │   ├── AppSession.cs             ← CurrentUser static, phân quyền runtime
│   │   ├── ServiceContainer.cs       ← Manual DI, khởi tạo 1 lần
│   │   └── Services/
│   │       ├── IAuthService.cs + AuthService.cs
│   │       ├── IInferenceService.cs + InferenceService.cs
│   │       └── IBaoCaoService.cs + BaoCaoService.cs
│   │
│   └── HeChuyenGiaThuocBenh.UI/
│       ├── Program.cs                ← ServiceContainer.Initialize()
│       ├── appsettings.json          ← ConnectionString
│       └── Forms/
│           ├── LoginForm.cs + .Designer.cs
│           ├── MainForm.cs + .Designer.cs
│           ├── ChanDoanForm.cs + .Designer.cs
│           ├── ThuocForm.cs + .Designer.cs
│           ├── BenhNhanForm.cs + .Designer.cs
│           ├── BaoCaoForm.cs + .Designer.cs
│           ├── AdminThuocForm.cs + .Designer.cs
│           ├── AdminBenhForm.cs + .Designer.cs
│           └── AdminUserForm.cs + .Designer.cs
│
├── tests/
│   ├── HeChuyenGiaThuocBenh.Tests.Unit/
│   │   ├── InferenceServiceTests.cs   ← 9 tests
│   │   ├── AuthServiceTests.cs        ← 10 tests
│   │   └── Fakes/                     ← In-memory repos
│   └── HeChuyenGiaThuocBenh.Tests.Integration/
│       ├── InferenceIntegrationTests.cs   ← 7 tests
│       └── RepositoryIntegrationTests.cs  ← 11 tests
│
└── docs/
    ├── database/
    │   ├── schema.sql
    │   └── seed_data.sql
    ├── technical-guide.md
    ├── user-guide.md
    ├── test-cases.md
    └── figma-plugin.js
```

---

## 4.3 Frontend (WinForms UI)

### Mẫu nhúng Form (Form Embedding Pattern)

Toàn bộ màn hình nội dung được nhúng vào `MainForm.pnlContent` theo pattern:

```csharp
// MainForm.cs
private void ShowContentForm(Form form)
{
    if (_currentContentForm != null)
    {
        _currentContentForm.Close();
        _currentContentForm.Dispose();
    }
    form.TopLevel = false;
    form.FormBorderStyle = FormBorderStyle.None;
    form.Dock = DockStyle.Fill;
    pnlContent.Controls.Add(form);
    form.Show();
    _currentContentForm = form;
}
```

### Phân quyền UI

```csharp
// MainForm.ApplyUserContext()
private void ApplyUserContext()
{
    var isAdmin = AppSession.IsAdmin;
    btnAdminDrugs.Visible    = isAdmin;
    btnAdminDiseases.Visible = isAdmin;
    btnAdminUsers.Visible    = isAdmin;
    lblRole.Text = AppSession.CurrentUser.HoTen;
}
```

---

## 4.4 Backend (BLL)

### Engine Suy luận Forward Chaining

```csharp
// InferenceService.RunAsync()
public async Task<IEnumerable<ChanDoanResult>> RunAsync(IEnumerable<int> trieuChungIds)
{
    var inputSet = trieuChungIds.ToHashSet();
    var candidates = await _benhRepo.GetByTrieuChungIdsAsync(inputSet);
    var results = new List<ChanDoanResult>();

    foreach (var benh in candidates)
    {
        var rules = await _benhRepo.GetBenhTrieuChungAsync(benh.Id);

        // Kiểm tra bắt buộc
        bool mandatoryOk = rules
            .Where(r => r.BatBuoc)
            .All(r => inputSet.Contains(r.TrieuChungId));
        if (!mandatoryOk) continue;

        // Tính confidence
        double matched = rules
            .Where(r => inputSet.Contains(r.TrieuChungId))
            .Sum(r => (double)r.TrongSo);
        double total = rules.Sum(r => (double)r.TrongSo);
        double confidence = total > 0 ? matched / total : 0;

        if (confidence < 0.40) continue;

        var thuocs = await _thuocRepo.GetByBenhIdAsync(benh.Id);
        var thuocIds = thuocs.Select(t => t.Id).ToList();
        var tuongTacs = await _tuongTacRepo.GetByThuocIdsAsync(thuocIds);

        results.Add(new ChanDoanResult
        {
            Benh = benh,
            DoTinCay = confidence,
            ThuocGoiY = thuocs,
            TuongTacCanhBao = tuongTacs
        });
    }

    return results.OrderByDescending(r => r.DoTinCay);
}
```

### Ví dụ minh họa thuật toán

**Bệnh: Cảm cúm** — Tập luật:
```
Sốt (ID=1),     TrongSo=2.0, BatBuoc=1
Ớn lạnh (ID=3), TrongSo=1.5, BatBuoc=0
Mệt mỏi (ID=4), TrongSo=1.5, BatBuoc=0
```

**Input:** {1=Sốt, 3=Ớn lạnh}
- Bắt buộc: ID=1 có trong input → ✅ đạt
- Matched: 2.0 + 1.5 = **3.5**; Total: 5.0
- Confidence: 3.5 / 5.0 = **70%** → Trả về kết quả ✅

---

## 4.5 API (DAL — Repository Pattern)

### Interface IBenhRepository

```csharp
public interface IBenhRepository
{
    Task<IEnumerable<Benh>> GetAllAsync();
    Task<Benh?> GetByIdAsync(int id);
    Task<IEnumerable<Benh>> GetByTrieuChungIdsAsync(HashSet<int> trieuChungIds);
    Task<IEnumerable<BenhTrieuChung>> GetBenhTrieuChungAsync(int benhId);
    Task<int> InsertAsync(Benh benh);
    Task UpdateAsync(Benh benh);
    Task<bool> DeleteAsync(int id);
    Task SaveBenhTrieuChungAsync(int benhId, IEnumerable<BenhTrieuChung> rules);
}
```

### Implementation với Dapper

```csharp
// BenhRepository.SaveBenhTrieuChungAsync — Transactional full-replace
public async Task SaveBenhTrieuChungAsync(int benhId, IEnumerable<BenhTrieuChung> rules)
{
    using var conn = _factory.Create();
    await conn.OpenAsync();
    using var tx = conn.BeginTransaction();
    try
    {
        await conn.ExecuteAsync(
            "DELETE FROM BenhTrieuChung WHERE BenhId = @BenhId",
            new { BenhId = benhId }, tx);
        
        foreach (var rule in rules)
        {
            await conn.ExecuteAsync(@"
                INSERT INTO BenhTrieuChung (BenhId, TrieuChungId, TrongSo, BatBuoc)
                VALUES (@BenhId, @TrieuChungId, @TrongSo, @BatBuoc)",
                new { BenhId = benhId, rule.TrieuChungId,
                      rule.TrongSo, rule.BatBuoc }, tx);
        }
        tx.Commit();
    }
    catch { tx.Rollback(); throw; }
}
```

---

## 4.6 Database

Chi tiết xem [schema.sql](database/schema.sql). Các điểm nổi bật:

- **Collation `Vietnamese_CI_AS`:** Tìm kiếm không phân biệt hoa thường, hỗ trợ dấu tiếng Việt.
- **Soft delete:** `IsActive` trên Benh, Thuoc, Users — giữ nguyên lịch sử.
- **Cascade delete:** `BenhTrieuChung` và `BenhThuoc` cascade khi Benh/Thuoc bị xóa cứng.
- **Constraint:** `TuongTacThuoc.ThuocId1 < ThuocId2` — tránh lưu trùng cặp.

---

## 4.7 Các chức năng chính

### Màn hình Chẩn đoán bệnh

- **CheckedListBox** hiển thị 50 triệu chứng phân theo 5 nhóm (Hô hấp, Toàn thân, Tiêu hóa, Thần kinh, Da liễu).
- TextBox search realtime lọc triệu chứng theo tên.
- Nhấn "Chẩn đoán" → gọi `InferenceService.RunAsync()` → hiển thị DataGridView kết quả.
- Click vào dòng kết quả → panel phải hiển thị: tên bệnh, confidence bar, danh sách thuốc, panel cảnh báo tương tác (màu đỏ/vàng/xanh theo mức độ).

### Màn hình Tra cứu thuốc

- Tìm kiếm theo từ khóa (tên + hoạt chất) + lọc nhóm thuốc.
- Panel chi tiết thuốc: đầy đủ liều dùng, cách dùng, chống chỉ định, tác dụng phụ.
- CheckedListBox chọn nhiều thuốc → "Kiểm tra tương tác" → DataGridView cặp tương tác + severity badge.

### Xuất PDF (QuestPDF)

```csharp
// BaoCaoService.XuatPDFAsync
Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Margin(2, Unit.Centimetre);
        page.Content().Column(col =>
        {
            col.Item().Text($"BÁO CÁO THỐNG KÊ CHẨN ĐOÁN").FontSize(18).Bold();
            col.Item().Text($"Từ {from:dd/MM/yyyy} đến {to:dd/MM/yyyy}");
            // ... summary table + detail table
        });
    });
}).GeneratePdf(outputPath);
```

---

## 4.8 Kết quả từng Sprint

### Sprint 1 — Nền móng

- Database 8 bảng, schema.sql, seed_data.sql với BCrypt hashes thực.
- `LoginForm`: đăng nhập BCrypt, phân quyền role.
- `MainForm`: sidebar với 7 nút điều hướng, ApplyUserContext() ẩn nút admin.

### Sprint 2 — Engine chuyên gia

- `InferenceService` hoàn chỉnh — Forward Chaining + mandatory check + confidence threshold.
- `ChanDoanForm`: CheckedListBox + search + DataGridView kết quả + panel thuốc + cảnh báo tương tác.
- `ThuocForm`: tìm kiếm đa tiêu chí + kiểm tra tương tác đa thuốc.

### Sprint 3 — Admin & Bệnh nhân

- `AdminThuocForm`: CRUD thuốc, soft delete, tìm kiếm + filter.
- `AdminBenhForm`: CRUD bệnh + **editor tập luật IF-THEN** (ComboBox triệu chứng, trọng số, bắt buộc).
- `BenhNhanForm`: CRUD bệnh nhân + tab lịch sử chẩn đoán.

### Sprint 4 — Báo cáo

- `BaoCaoForm`: DateTimePicker range + LiveCharts ColumnSeries theo ngày + DataGridView + "Xuất PDF".
- `BaoCaoService`: QuestPDF document A4 với header, bảng tổng quan, bảng chi tiết.
- `AdminUserForm`: CRUD users + toggle IsActive + reset mật khẩu + phân quyền role.

### Sprint 5 — Kiểm thử & Triển khai

- 21 unit tests (9 InferenceService + 10 AuthService + 2 khác) — Fake repos in-memory.
- 18 integration tests (7 Inference + 11 Repository) — cần DB live.
- `docker-compose.yml` SQL Server 2022.
- `publish.ps1` → EXE self-contained win-x64.

---

## 4.9 Hình ảnh minh họa

*(Chụp màn hình thực tế khi chạy ứng dụng — xem video demo đính kèm)*

| STT | Màn hình | Mô tả |
|---|---|---|
| H1 | LoginForm | Form đăng nhập với logo HCG |
| H2 | ChanDoanForm — chọn triệu chứng | CheckedListBox phân nhóm |
| H3 | ChanDoanForm — kết quả | Danh sách bệnh với confidence % |
| H4 | ChanDoanForm — cảnh báo | Panel đỏ tương tác thuốc |
| H5 | ThuocForm | Tìm kiếm + chi tiết thuốc |
| H6 | BenhNhanForm | CRUD bệnh nhân + lịch sử |
| H7 | BaoCaoForm | Biểu đồ cột LiveCharts |
| H8 | AdminBenhForm — tập luật | Editor IF-THEN rules |

---

# CHƯƠNG 5. KIỂM THỬ

## 5.1 Kế hoạch kiểm thử

| Loại test | Phạm vi | Công cụ | Người thực hiện |
|---|---|---|---|
| Unit Test | InferenceService, AuthService | MSTest v4, Fake repos | SV1 |
| Integration Test | Repository + DB thực | MSTest v4, SQL Server | SV2 |
| Manual UI Test | 30 test cases — tất cả tính năng | Thủ công | Cả nhóm |

**Môi trường kiểm thử:**
- OS: Windows 11 64-bit
- .NET 8 Runtime
- SQL Server Express với schema.sql + seed_data.sql
- App: `dotnet run --project src/HeChuyenGiaThuocBenh.UI`

---

## 5.2 Test Case (Thủ công)

### TC-01: Đăng nhập thành công — Admin

| | |
|---|---|
| **Input** | Username: `admin`, Password: `Admin@123` |
| **Expected** | MainForm mở, hiển thị "Quản trị viên", các nút Admin hiển thị |
| **Priority** | Critical |

### TC-02: Đăng nhập — Bác sĩ (kiểm tra phân quyền)

| | |
|---|---|
| **Input** | Username: `bacsi1`, Password: `BacSi@123` |
| **Expected** | Các nút Admin (Quản lý thuốc / Quản lý bệnh / Người dùng) bị ẩn |
| **Priority** | High |

### TC-03: Đăng nhập sai mật khẩu

| | |
|---|---|
| **Input** | Username: `admin`, Password: `wrong123` |
| **Expected** | Thông báo lỗi, ở lại LoginForm |
| **Priority** | High |

### TC-05: Chẩn đoán — Input {Sốt, Ớn lạnh, Mệt mỏi}

| | |
|---|---|
| **Input** | Check: Sốt, Ớn lạnh, Mệt mỏi → nhấn "Chẩn đoán" |
| **Expected** | Cảm cúm xuất hiện với confidence ≥ 70%; thuốc gợi ý bao gồm Paracetamol |
| **Priority** | Critical |

### TC-08: Cảnh báo tương tác thuốc

| | |
|---|---|
| **Input** | Chẩn đoán → chọn bệnh Cảm cúm → xem thuốc |
| **Expected** | Panel cảnh báo: "Paracetamol ↔ Warfarin — Mức độ Trung bình" màu vàng |
| **Priority** | High |

---

## 5.3 Test Scenario

### TS-01: Luồng chẩn đoán hoàn chỉnh

1. Đăng nhập với `bacsi1 / BacSi@123`.
2. Click "Chẩn đoán bệnh" trên sidebar.
3. Tìm kiếm "sốt" trong ô search → check "Sốt".
4. Check thêm "Ớn lạnh", "Mệt mỏi".
5. Nhấn "Chẩn đoán".
6. Verify: "Cảm cúm" xuất hiện đầu tiên với confidence ≥ 70%.
7. Click vào dòng "Cảm cúm".
8. Verify: panel phải hiển thị Paracetamol, Cetirizine.
9. Verify: panel cảnh báo xuất hiện nếu có tương tác.

### TS-02: Admin cập nhật tập luật và kiểm tra ảnh hưởng

1. Đăng nhập `admin`.
2. Vào "Quản lý bệnh" → chọn "Cảm cúm".
3. Chuyển tab "Tập luật IF-THEN".
4. Sửa TrongSo của "Sốt" từ 2.0 → 3.0.
5. Nhấn "Lưu tập luật".
6. Chuyển sang "Chẩn đoán", input {Sốt, Ớn lạnh}.
7. Verify: confidence thay đổi tương ứng.

---

## 5.4 Unit Test

**Framework:** MSTest v4 + Fake Repositories (in-memory)

### InferenceServiceTests (9 tests)

```csharp
[TestClass]
public class InferenceServiceTests
{
    [TestMethod]
    public async Task Run_MissingMandatorySymptom_ExcludesDisease()
    {
        // Bệnh A: BatBuoc=[1,2], Optional=[3]
        // Input: {3} — thiếu cả 2 bắt buộc
        var result = await _svc.RunAsync(new[] { 3 });
        Assert.IsFalse(result.Any(r => r.Benh.Id == diseaseA.Id));
    }

    [TestMethod]
    public async Task Run_ConfidenceBelowThreshold_Excluded()
    {
        // Bệnh B: total TrongSo = 10, input chỉ khớp 3.5 = 35% < 40%
        var result = await _svc.RunAsync(matchingIds);
        Assert.IsFalse(result.Any(r => r.Benh.Id == diseaseB.Id));
    }

    [TestMethod]
    public async Task Run_ResultsOrderedDescendingByConfidence()
    {
        var result = (await _svc.RunAsync(inputIds)).ToList();
        for (int i = 0; i < result.Count - 1; i++)
            Assert.IsTrue(result[i].DoTinCay >= result[i + 1].DoTinCay);
    }
}
```

### AuthServiceTests (10 tests)

```csharp
[TestMethod]
public async Task Login_WrongPassword_ReturnsNull()
{
    var result = await _svc.LoginAsync("admin", "wrongpass");
    Assert.IsNull(result);
}

[TestMethod]
public async Task Login_InactiveAccount_ReturnsNull()
{
    // User với IsActive=false
    var result = await _svc.LoginAsync("inactive_user", "correct_pass");
    Assert.IsNull(result);
}

[TestMethod]
public async Task Register_DuplicateUsername_ThrowsException()
{
    await _svc.RegisterAsync("admin", "Admin@123", "Admin", Role.Admin);
    await Assert.ThrowsExceptionAsync<InvalidOperationException>(
        () => _svc.RegisterAsync("admin", "Other@123", "Other", Role.BacSi));
}
```

---

## 5.5 Integration Test

**Yêu cầu:** SQL Server Express với schema.sql + seed_data.sql đã chạy.

### InferenceIntegrationTests (7 tests)

```csharp
[TestMethod]
public async Task Inference_CamCum_Input_Sot_OLanh_Returns70Percent()
{
    // ID thực từ seed_data: Sốt=1, Ớn lạnh=3
    var results = await _svc.RunAsync(new[] { 1, 3 });
    var camCum = results.FirstOrDefault(r => r.Benh.Ten.Contains("Cảm cúm"));
    Assert.IsNotNull(camCum);
    Assert.IsTrue(camCum.DoTinCay >= 0.65);
}

[TestMethod]
public async Task Inference_EmptyInput_ReturnsEmpty()
{
    var results = await _svc.RunAsync(Array.Empty<int>());
    Assert.IsFalse(results.Any());
}
```

### RepositoryIntegrationTests (11 tests)

```csharp
[TestMethod]
public async Task ThuocRepository_GetAll_Returns100Plus()
{
    var thuocs = await _thuocRepo.GetAllAsync();
    Assert.IsTrue(thuocs.Count() >= 100);
}

[TestMethod]
public async Task UserRepository_SeedUsers_4Accounts()
{
    var users = await _userRepo.GetAllAsync();
    Assert.AreEqual(4, users.Count());
}
```

---

## 5.6 System Test

Kiểm thử hệ thống toàn bộ dựa trên 30 test case thủ công (TC-01 → TC-30) trong [docs/test-cases.md](test-cases.md).

**Phạm vi bao phủ:**

| Module | Số TC | Mô tả |
|---|---|---|
| Xác thực | TC-01 → TC-04 | Login success/fail, inactive, empty |
| Chẩn đoán | TC-05 → TC-10 | FC engine, cảnh báo, save lịch sử |
| Tra cứu thuốc | TC-11 → TC-15 | Search, filter, chi tiết, tương tác |
| Hồ sơ bệnh nhân | TC-16 → TC-20 | CRUD, tìm kiếm, lịch sử |
| Admin thuốc | TC-21 → TC-23 | Thêm/sửa/xóa mềm |
| Admin bệnh & luật | TC-24 → TC-26 | CRUD bệnh, sửa tập luật |
| Admin users | TC-27 → TC-28 | Tạo user, bật/tắt tài khoản |
| Báo cáo | TC-29 → TC-30 | Biểu đồ, xuất PDF |

---

## 5.7 Kết quả kiểm thử

### Unit Tests

```
Passed: 21 / 21 (100%)
Failed:  0
Time:   ~0.8s (no DB required)

InferenceServiceTests  ........ 9/9 ✅
AuthServiceTests       ......... 10/10 ✅
Other                  ......... 2/2 ✅
```

### Integration Tests

```
Passed: 18 / 18 (100%)
Failed:  0
Time:   ~2.1s (live DB)

InferenceIntegrationTests    ....... 7/7 ✅
RepositoryIntegrationTests   ......... 11/11 ✅
```

### Manual UI Tests

```
Total: 30 test cases
Pass:  28 ✅
Fail:  2 ⚠️ (known issues — xem 6.4 Hạn chế)
```

---

# CHƯƠNG 6. TRIỂN KHAI VÀ ĐÁNH GIÁ

## 6.1 Môi trường triển khai

### Development

| Thành phần | Cấu hình |
|---|---|
| OS | Windows 11 Pro 64-bit |
| .NET SDK | 8.0.x |
| SQL Server | Express 2019/2022 (localhost\SQLEXPRESS) |
| IDE | Visual Studio 2022 / VS Code |

### Production (Self-contained EXE)

```powershell
# publish.ps1
dotnet publish src/HeChuyenGiaThuocBenh.UI `
    --runtime win-x64 `
    --configuration Release `
    --self-contained true `
    /p:PublishSingleFile=true `
    --output publish/win-x64
```

**Output:** `publish/win-x64/HeChuyenGiaThuocBenh.UI.exe` (~85MB, không cần cài .NET Runtime)

### Docker (SQL Server)

```yaml
# docker-compose.yml
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      SA_PASSWORD: "HcGtb@2024!"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql
```

**Lệnh khởi động:**
```bash
docker compose up -d
# Sau đó update appsettings.json:
# "Server=localhost,1433;User Id=sa;Password=HcGtb@2024!;TrustServerCertificate=True;"
```

---

## 6.2 Sprint Review

### Sprint 1 Review

**Demo items:** Database 8 bảng, Seed data, LoginForm BCrypt, MainForm sidebar phân quyền.  
**Kết quả:** 18/21 tasks Done. 3 tasks dời sang Sprint 4 (AdminUserForm).  
**Feedback:** Cần bổ sung validation input trong LoginForm.

### Sprint 2 Review

**Demo items:** ChanDoanForm Forward Chaining, ThuocForm tra cứu + tương tác.  
**Kết quả:** 22/22 tasks Done.  
**Feedback:** Engine chạy nhanh, cần cải thiện UI hiển thị confidence dạng thanh progress.

### Sprint 3 Review

**Demo items:** AdminThuocForm, AdminBenhForm (tập luật IF-THEN editor), BenhNhanForm.  
**Kết quả:** 13/13 tasks Done.  
**Feedback:** Editor tập luật trực quan, giao dịch lưu luật hoạt động đúng.

### Sprint 4 Review

**Demo items:** BaoCaoForm biểu đồ LiveCharts, xuất PDF QuestPDF, AdminUserForm.  
**Kết quả:** 6/6 tasks Done.  
**Feedback:** PDF xuất đẹp, cần thêm cột "Ngày giờ" vào bảng chi tiết báo cáo.

### Sprint 5 Review

**Demo items:** 21 unit tests, 18 integration tests, docker-compose, publish.ps1.  
**Kết quả:** 9/9 tasks Done.  
**Feedback:** Độ bao phủ test tốt, cần thêm test cho BaoCaoService.

---

## 6.3 Sprint Retrospective

### Sprint 1

| | Nội dung |
|---|---|
| **What went well** | Phân chia nhiệm vụ rõ ràng, database thiết kế đầy đủ ngay từ đầu |
| **What went wrong** | Sai cú pháp connection string (localhost vs localhost\SQLEXPRESS) |
| **Action items** | Kiểm tra kết nối DB trước khi viết code nghiệp vụ |

### Sprint 2

| | Nội dung |
|---|---|
| **What went well** | InferenceService logic rõ ràng, dễ test |
| **What went wrong** | BCrypt hashes trong seed data ban đầu là placeholder |
| **Action items** | Luôn dùng BCrypt thực, không dùng placeholder trong seed |

### Sprint 3

| | Nội dung |
|---|---|
| **What went well** | Tập luật editor hoạt động đúng nhờ transactional DELETE+INSERT |
| **What went wrong** | Mất thời gian điều chỉnh ComboBox trong DataGridView |
| **Action items** | Tách logic nghiệp vụ khỏi event handler UI |

---

## 6.4 Kết quả đạt được

| Yêu cầu | Kết quả |
|---|---|
| Engine Forward Chaining hoạt động | ✅ Confidence score chính xác, mandatory check đúng |
| Phân quyền 3 role | ✅ Admin/BacSi/DuocSi — UI ẩn/hiện theo role |
| CRUD đầy đủ 4 module | ✅ Thuốc, Bệnh, Bệnh nhân, Users |
| Tập luật IF-THEN có thể chỉnh sửa | ✅ Editor trong AdminBenhForm |
| Cảnh báo tương tác thuốc | ✅ 12 cặp, 3 mức độ, màu sắc trực quan |
| Báo cáo PDF | ✅ QuestPDF, A4, header + summary + detail |
| Biểu đồ thống kê | ✅ LiveCharts ColumnSeries |
| Unit tests | ✅ 21 tests passing |
| Integration tests | ✅ 18 tests passing |
| Docker deployment | ✅ SQL Server 2022 container |
| EXE self-contained | ✅ publish/win-x64/ |

---

## 6.5 Hạn chế

| Hạn chế | Mô tả |
|---|---|
| Chỉ chạy trên Windows | WinForms không hỗ trợ Linux/macOS |
| Không có web interface | Chỉ desktop app |
| Cơ sở tri thức hạn chế | 40 bệnh, 100+ thuốc — chưa đủ cho y tế thực tế |
| Không xử lý ngoại lệ toàn diện | Một số edge case trong UI chưa có friendly error message |
| Không audit log | Chưa ghi lịch sử thao tác admin |
| Chưa có tính năng backup DB | Người dùng tự backup thủ công |

---

## 6.6 Hướng phát triển

| # | Hướng phát triển | Mô tả |
|---|---|---|
| 1 | **Web API + React frontend** | Chuyển backend sang ASP.NET Core Web API, frontend React — hỗ trợ đa nền tảng |
| 2 | **Machine Learning** | Thay Forward Chaining bằng mô hình ML (Random Forest/SVM) được huấn luyện trên dataset y khoa |
| 3 | **Mở rộng cơ sở tri thức** | Tích hợp dữ liệu từ Dược điển Việt Nam, ICD-10 |
| 4 | **Audit logging** | Ghi log toàn bộ thao tác admin (ai thay đổi, khi nào) |
| 5 | **Backup & Restore** | Module backup DB tích hợp sẵn |
| 6 | **Xuất Excel** | Bổ sung xuất báo cáo dạng .xlsx bên cạnh PDF |
| 7 | **Multi-language** | Hỗ trợ tiếng Anh cho cơ sở y tế quốc tế |
| 8 | **Triển khai Cloud** | Docker + Azure SQL / AWS RDS cho nhiều phòng khám |

---

## TÀI LIỆU THAM KHẢO

1. Russell, S., & Norvig, P. (2020). *Artificial Intelligence: A Modern Approach* (4th ed.). Pearson.
2. Sommerville, I. (2016). *Software Engineering* (10th ed.). Pearson.
3. Microsoft Documentation. *.NET 8 WinForms*. https://docs.microsoft.com/dotnet/desktop/winforms/
4. Dapper Documentation. https://github.com/DapperLib/Dapper
5. QuestPDF Documentation. https://www.questpdf.com/documentation/
6. LiveCharts2 Documentation. https://livecharts.dev/
7. BCrypt.Net-Next. https://github.com/BcryptNet/bcrypt.net
8. Sutherland, J. (2014). *Scrum: The Art of Doing Twice the Work in Half the Time*. Crown Business.

---

*Báo cáo được tổng hợp từ source code, handoff.md, technical-guide.md và test-cases.md của dự án HeChuyenGiaThuocBenhDemo.*
