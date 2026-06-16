# Test Cases — Hệ Chuyên Gia Thuốc Bệnh

Manual UI test cases for acceptance testing.  
**Precondition:** schema.sql + seed_data.sql applied, app running, login as admin unless stated.

---

## TC-01: Đăng nhập thành công — Admin

| | |
|---|---|
| **Input** | Username: `admin`, Password: `Admin@123` |
| **Expected** | MainForm opens, welcome label shows "Quản trị viên", admin sidebar buttons visible |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-02: Đăng nhập thành công — Bác sĩ

| | |
|---|---|
| **Input** | Username: `bacsi1`, Password: `BacSi@123` |
| **Expected** | MainForm opens, admin section buttons hidden (Người dùng / Quản lý thuốc / Quản lý bệnh) |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-03: Đăng nhập sai mật khẩu

| | |
|---|---|
| **Input** | Username: `admin`, Password: `wrong` |
| **Expected** | Error message shown, stay on LoginForm |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-04: Đăng nhập — trường rỗng

| | |
|---|---|
| **Input** | Username: `""`, Password: `""` |
| **Expected** | Validation error, stay on LoginForm |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-05: Chẩn đoán — chọn triệu chứng và chạy

| | |
|---|---|
| **Input** | Click "Chẩn đoán", check "Sốt", "Ớn lạnh", "Mệt mỏi", click "Chẩn đoán" |
| **Expected** | Kết quả: Cảm cúm xuất hiện với confidence ≥ 40%, có gợi ý thuốc |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-06: Chẩn đoán — không chọn triệu chứng

| | |
|---|---|
| **Input** | Click "Chẩn đoán" without selecting any symptom |
| **Expected** | Warning message "Vui lòng chọn ít nhất một triệu chứng" |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-07: Chẩn đoán — xem chi tiết bệnh

| | |
|---|---|
| **Input** | Run diagnosis, click on a disease row in results table |
| **Expected** | Right panel shows: disease name, confidence bar, drug recommendations |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-08: Chẩn đoán — cảnh báo tương tác thuốc

| | |
|---|---|
| **Input** | Select symptoms that trigger a disease with drug interactions in seed data |
| **Expected** | Warning panel turns red, lists drug pairs with severity (Nặng/Trung bình/Nhẹ) |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-09: Chẩn đoán — tìm kiếm triệu chứng

| | |
|---|---|
| **Input** | Type "đau" in search box on ChanDoanForm |
| **Expected** | Symptom list filters to only show symptoms containing "đau" |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-10: Tra cứu thuốc — tìm theo từ khóa

| | |
|---|---|
| **Input** | Click "Tra cứu thuốc", type "para" in search box, click "Tìm" |
| **Expected** | Drug list shows Paracetamol-based drugs |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-11: Tra cứu thuốc — lọc theo nhóm

| | |
|---|---|
| **Input** | Select "Kháng sinh" from NhomThuoc dropdown |
| **Expected** | List filtered to antibiotic drugs only |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-12: Tra cứu thuốc — xem chi tiết

| | |
|---|---|
| **Input** | Click a drug row in ThuocForm |
| **Expected** | Right panel shows: tên, hoạt chất, nhóm thuốc, liều dùng, cách dùng, chống chỉ định (red), tác dụng phụ (orange) |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-13: Kiểm tra tương tác nhiều thuốc

| | |
|---|---|
| **Input** | Multi-select 2 drugs known to interact, click "Kiểm tra tương tác" |
| **Expected** | Interaction table shown with severity level |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-14: Hồ sơ bệnh nhân — tạo mới

| | |
|---|---|
| **Input** | Click "Hồ sơ bệnh nhân", click "+ Thêm mới", fill HoTen/NgaySinh/SoDienThoai, click "💾 Lưu" |
| **Expected** | New patient appears in left DataGridView |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-15: Hồ sơ bệnh nhân — tìm theo tên

| | |
|---|---|
| **Input** | Type patient name in search box |
| **Expected** | List filters to matching patients |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-16: Hồ sơ bệnh nhân — tìm theo số điện thoại

| | |
|---|---|
| **Input** | Type phone number in search box |
| **Expected** | List filters to matching patient |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-17: Hồ sơ bệnh nhân — xem lịch sử chẩn đoán

| | |
|---|---|
| **Input** | Select a patient, click "Lịch sử chẩn đoán" tab |
| **Expected** | Tab 2 shows DataGridView of past diagnoses for that patient |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-18: Admin — Quản lý thuốc: thêm mới

| | |
|---|---|
| **Login** | admin |
| **Input** | Click "💊 Quản lý thuốc", click "+ Thêm mới", fill Tên/HoạtChất/NhómThuốc, click "💾 Lưu" |
| **Expected** | New drug appears in list |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-19: Admin — Quản lý thuốc: sửa

| | |
|---|---|
| **Login** | admin |
| **Input** | Select a drug, change Liều dùng field, click "💾 Lưu" |
| **Expected** | Updated value persists after re-selecting the drug |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-20: Admin — Quản lý thuốc: xóa

| | |
|---|---|
| **Login** | admin |
| **Input** | Select a drug, click "🗑 Xóa", confirm |
| **Expected** | Drug removed from list (soft delete — IsActive=0) |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-21: Admin — Quản lý bệnh: thêm bệnh mới với tập luật

| | |
|---|---|
| **Login** | admin |
| **Input** | Click "🏥 Quản lý bệnh", click "+ Thêm bệnh", enter Tên bệnh, click "💾 Lưu bệnh", add 2 rules in tab "Tập luật", click "💾 Lưu tập luật" |
| **Expected** | Disease added; rules saved; re-selecting disease shows saved rules |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-22: Admin — Quản lý bệnh: sửa trọng số luật

| | |
|---|---|
| **Login** | admin |
| **Input** | Select existing disease with rules, edit TrongSo in rule table, click "💾 Lưu tập luật" |
| **Expected** | New weight values saved; confirm by re-selecting disease |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-23: Admin — Quản lý người dùng: tạo mới

| | |
|---|---|
| **Login** | admin |
| **Input** | Click "👥 Người dùng", click "+ Thêm mới", fill Username/HoTen/Email/Role, Password, click "💾 Lưu" |
| **Expected** | New user appears in left grid |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-24: Admin — Quản lý người dùng: bật/tắt tài khoản

| | |
|---|---|
| **Login** | admin |
| **Input** | Select a non-admin user, click "🔒 Bật/Tắt", confirm |
| **Expected** | TrangThai column toggles between "Hoạt động" and "Vô hiệu" |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-25: Admin — Quản lý người dùng: reset mật khẩu

| | |
|---|---|
| **Login** | admin |
| **Input** | Select a user, click "🔑 Reset mật khẩu", confirm |
| **Expected** | Success message; user can login with `Admin@123` |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-26: Báo cáo — truy vấn theo khoảng ngày

| | |
|---|---|
| **Input** | Click "📊 Báo cáo", set Từ = 30 days ago, Đến = today, click "🔍 Tìm kiếm" |
| **Expected** | Chart shows bars per day, table shows diagnosis records, total count label updates |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-27: Báo cáo — xuất PDF

| | |
|---|---|
| **Input** | After TC-26, click "📄 Xuất PDF", choose save path |
| **Expected** | PDF created; dialog asks to open; PDF contains header, stats table, detail table |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-28: Đăng xuất

| | |
|---|---|
| **Input** | Click "Đăng xuất" button in header |
| **Expected** | LoginForm shown; MainForm closed |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-29: Phân quyền — bác sĩ không thấy menu admin

| | |
|---|---|
| **Login** | bacsi1 |
| **Expected** | Sidebar buttons "Người dùng", "Quản lý thuốc", "Quản lý bệnh" are hidden |
| **Status** | [ ] Pass / [ ] Fail |

---

## TC-30: Phân quyền — dược sĩ không thấy menu admin

| | |
|---|---|
| **Login** | duocsi1 / DuocSi@123 |
| **Expected** | Same as TC-29 — admin section hidden |
| **Status** | [ ] Pass / [ ] Fail |

---

*Total: 30 test cases*
