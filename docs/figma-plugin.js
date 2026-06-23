// ============================================================
// HeChuyenGia – Figma Wireframe Generator
// Cách chạy: Figma → Plugins → Development → New Plugin
//            → chọn "Run once" → paste toàn bộ file này vào code.js → Run
// Hoặc: dùng plugin "Scripter" (Community) → paste & Run
// ============================================================

async function main() {
  // ── Fonts ──────────────────────────────────────────────────
  await figma.loadFontAsync({ family: "Inter", style: "Regular" });
  await figma.loadFontAsync({ family: "Inter", style: "Medium" });
  await figma.loadFontAsync({ family: "Inter", style: "Semi Bold" });
  await figma.loadFontAsync({ family: "Inter", style: "Bold" });

  // ── Constants ──────────────────────────────────────────────
  const W = 1280, H = 800, GAP = 120;
  const SIDEBAR_W = 220;

  // ── Color palette ──────────────────────────────────────────
  const C = {
    bg:        { r: 0.95, g: 0.96, b: 0.98 },
    white:     { r: 1,    g: 1,    b: 1    },
    primary:   { r: 0.16, g: 0.42, b: 0.72 },
    primaryLt: { r: 0.90, g: 0.94, b: 0.99 },
    sidebar:   { r: 0.12, g: 0.17, b: 0.27 },
    sidebarAct:{ r: 0.15, g: 0.38, b: 0.68 },
    sidebarTxt:{ r: 0.72, g: 0.80, b: 0.92 },
    dark:      { r: 0.13, g: 0.13, b: 0.15 },
    gray:      { r: 0.50, g: 0.52, b: 0.55 },
    border:    { r: 0.85, g: 0.86, b: 0.88 },
    rowAlt:    { r: 0.975,g: 0.975,b: 0.98 },
    success:   { r: 0.12, g: 0.62, b: 0.32 },
    successLt: { r: 0.90, g: 0.97, b: 0.93 },
    danger:    { r: 0.82, g: 0.13, b: 0.13 },
    dangerLt:  { r: 0.99, g: 0.93, b: 0.93 },
    warn:      { r: 0.75, g: 0.48, b: 0.02 },
    warnLt:    { r: 0.99, g: 0.95, b: 0.88 },
    purple:    { r: 0.55, g: 0.20, b: 0.90 },
    teal:      { r: 0.10, g: 0.60, b: 0.55 },
  };

  // ── Helpers ────────────────────────────────────────────────

  function solid(color, a = 1) {
    return [{ type: "SOLID", color, opacity: a }];
  }

  function mkFrame(name, x, y, w = W, h = H, fillColor = C.bg) {
    const f = figma.createFrame();
    f.name = name;
    f.x = x; f.y = y;
    f.resize(w, h);
    f.fills = solid(fillColor);
    figma.currentPage.appendChild(f);
    return f;
  }

  function mkRect(parent, x, y, w, h, color, name = "rect", radius = 0) {
    const r = figma.createRectangle();
    r.name = name;
    r.x = x; r.y = y;
    // Figma throws if width or height is 0 or negative
    r.resize(Math.max(w, 1), Math.max(h, 1));
    r.fills = solid(color);
    if (radius) r.cornerRadius = radius;
    parent.appendChild(r);
    return r;
  }

  function border(node, color = C.border, weight = 1) {
    node.strokes = solid(color);
    node.strokeWeight = weight;
    node.strokeAlign = "INSIDE";
    return node;
  }

  function shadow(node) {
    node.effects = [{
      type: "DROP_SHADOW",
      color: { r: 0, g: 0, b: 0, a: 0.08 },
      offset: { x: 0, y: 4 },
      radius: 16,
      visible: true, blendMode: "NORMAL",
    }];
    return node;
  }

  function mkText(parent, x, y, content, size, color, style = "Regular", maxW = 0) {
    const t = figma.createText();
    t.fontName = { family: "Inter", style };
    t.fontSize = size;
    t.fills = solid(color);
    t.characters = String(content);
    if (maxW > 0) {
      // Correct order: set width first, then enable auto-height
      t.resize(Math.max(maxW, 1), 200);
      t.textAutoResize = "HEIGHT";
    }
    t.x = x; t.y = y;
    parent.appendChild(t);
    return t;
  }

  function mkInput(parent, x, y, w, label, value, highlight = false) {
    mkText(parent, x, y, label, 11, C.gray, "Semi Bold");
    const bg = mkRect(parent, x, y + 17, w, 34, highlight ? C.primaryLt : C.bg, "input", 4);
    border(bg, highlight ? C.primary : C.border);
    mkText(parent, x + 10, y + 26, value, 12, highlight ? C.dark : C.gray);
    return bg;
  }

  function mkButton(parent, x, y, w, h, label, bgColor, textColor, radius = 4) {
    const btn = mkRect(parent, x, y, w, h, bgColor, "btn", radius);
    const t = figma.createText();
    t.fontName = { family: "Inter", style: "Medium" };
    t.fontSize = 12;
    t.fills = solid(textColor);
    t.characters = String(label);
    parent.appendChild(t);
    // Center text inside button — read width/height AFTER appending
    const tw = t.width  || 60;
    const th = t.height || 16;
    t.x = x + Math.round((w - tw) / 2);
    t.y = y + Math.round((h - th) / 2);
    return btn;
  }

  function mkDivider(parent, x, y, w, color = C.border) {
    mkRect(parent, x, y, w, 1, color, "divider");
  }

  // ── Sidebar ────────────────────────────────────────────────
  const NAV_ITEMS = [
    { icon: "🔬", label: "Chẩn đoán bệnh" },
    { icon: "💊", label: "Tra cứu thuốc" },
    { icon: "👤", label: "Hồ sơ bệnh nhân" },
    { icon: "📊", label: "Báo cáo thống kê" },
    { icon: "─", label: "─────────────" },
    { icon: "🔧", label: "Quản lý thuốc" },
    { icon: "🏥", label: "Quản lý bệnh" },
    { icon: "👥", label: "Quản lý users" },
  ];

  function drawSidebar(parent, activeIdx = 0, role = "Bác sĩ", username = "bacsi1") {
    mkRect(parent, 0, 0, SIDEBAR_W, H, C.sidebar, "sidebar");
    mkText(parent, 18, 18, "HeChuyenGia", 15, C.white, "Bold");
    mkText(parent, 18, 40, "Hệ Chuyên Gia Thuốc Bệnh", 9, C.sidebarTxt);
    mkDivider(parent, 12, 62, SIDEBAR_W - 24, C.sidebarAct);

    NAV_ITEMS.forEach((item, i) => {
      if (item.icon === "─") {
        mkDivider(parent, 12, 76 + i * 46 + 18, SIDEBAR_W - 24, C.sidebarAct);
        return;
      }
      const iy = 76 + i * 46;
      if (i === activeIdx) {
        const hl = mkRect(parent, 0, iy - 2, SIDEBAR_W, 42, C.sidebarAct, "nav-active", 0);
        mkRect(parent, 0, iy - 2, 3, 42, C.white, "nav-accent");
      }
      mkText(parent, 16, iy + 8, `${item.icon}  ${item.label}`, 12,
        i === activeIdx ? C.white : C.sidebarTxt,
        i === activeIdx ? "Semi Bold" : "Regular");
    });

    mkDivider(parent, 12, H - 58, SIDEBAR_W - 24, C.sidebarAct);
    mkText(parent, 18, H - 48, username, 12, C.white, "Semi Bold");
    mkText(parent, 18, H - 30, `${role}  ·  Đăng xuất`, 10, C.sidebarTxt);
  }

  // ── Content header ─────────────────────────────────────────
  function drawContentHeader(parent, title, subtitle = "") {
    const CX = SIDEBAR_W + 16;
    mkRect(parent, SIDEBAR_W, 0, W - SIDEBAR_W, 64, C.white, "topbar");
    mkDivider(parent, SIDEBAR_W, 64, W - SIDEBAR_W);
    mkText(parent, CX, 14, title, 20, C.dark, "Bold");
    if (subtitle) mkText(parent, CX, 40, subtitle, 11, C.gray);
  }

  function card(parent, x, y, w, h, name = "card", radius = 6) {
    const c = mkRect(parent, x, y, w, h, C.white, name, radius);
    border(c);
    shadow(c);
    return c;
  }

  // ── Table helpers ──────────────────────────────────────────
  function drawTableHeader(parent, x, y, w, cols) {
    mkRect(parent, x, y, w, 34, { r: 0.93, g: 0.94, b: 0.97 }, "thead");
    let cx = x + 12;
    cols.forEach(({ label, w: cw }) => {
      mkText(parent, cx, y + 10, label, 11, C.gray, "Semi Bold");
      cx += cw;
    });
  }

  function drawTableRow(parent, x, y, w, cells, cols, isSelected = false, isAlt = false) {
    if (isSelected) mkRect(parent, x, y, w, 40, C.primaryLt, "row-sel");
    else if (isAlt)  mkRect(parent, x, y, w, 40, C.rowAlt, "row-alt");
    let cx = x + 12;
    cells.forEach((cell, i) => {
      const color = isSelected && i === 0 ? C.primary : C.dark;
      const style = isSelected && i === 0 ? "Semi Bold" : "Regular";
      mkText(parent, cx, y + 12, cell, 12, color, style);
      cx += cols[i].w;
    });
    mkDivider(parent, x, y + 40, w);
  }

  function badge(parent, x, y, label, bgColor, textColor) {
    const b = mkRect(parent, x, y, 58, 22, bgColor, "badge", 4);
    mkText(parent, x + 8, y + 4, label, 10, textColor, "Medium");
  }

  // ══════════════════════════════════════════════════════════
  // SCREEN 1 — LOGIN
  // ══════════════════════════════════════════════════════════
  {
    const f = mkFrame("01 · Login", 0, 0, W, H, C.bg);

    // Decorative gradient blobs
    const b1 = mkRect(f, -60, -60, 400, 400, C.primary, "blob1", 200);
    b1.opacity = 0.06;
    const b2 = mkRect(f, W - 300, H - 300, 400, 400, { r: 0.10, g: 0.60, b: 0.55 }, "blob2", 200);
    b2.opacity = 0.06;

    const cardX = (W - 400) / 2, cardY = (H - 480) / 2;
    const loginCard = mkRect(f, cardX, cardY, 400, 480, C.white, "login-card", 12);
    shadow(loginCard);

    // Logo
    const logoBox = mkRect(f, cardX + 150, cardY + 32, 100, 100, C.primary, "logo", 50);
    mkText(f, cardX + 166, cardY + 54, "HCG", 22, C.white, "Bold");
    mkText(f, cardX + 162, cardY + 82, "THUỐC BỆNH", 8, { r: 0.7, g: 0.85, b: 1 }, "Semi Bold");

    mkText(f, cardX + 60, cardY + 152, "Hệ Chuyên Gia Thuốc Bệnh", 17, C.dark, "Bold");
    mkText(f, cardX + 116, cardY + 178, "Đăng nhập hệ thống", 12, C.gray);

    mkDivider(f, cardX + 24, cardY + 202, 352);

    mkInput(f, cardX + 28, cardY + 218, 344, "Tên đăng nhập", "bacsi1", false);
    mkInput(f, cardX + 28, cardY + 284, 344, "Mật khẩu", "••••••••", false);

    mkButton(f, cardX + 28, cardY + 370, 344, 44, "Đăng nhập", C.primary, C.white, 6);

    mkText(f, cardX + 28, cardY + 426, "Tài khoản mặc định:", 10, C.gray, "Semi Bold");
    mkText(f, cardX + 28, cardY + 442, "admin / Admin@123   ·   bacsi1 / BacSi@123   ·   duocsi1 / DuocSi@123", 9, C.gray);
    mkText(f, cardX + 120, cardY + 460, "© 2024 HeChuyenGia System", 9, { r: 0.75, g: 0.76, b: 0.78 });
  }

  // ══════════════════════════════════════════════════════════
  // SCREEN 2 — CHẨN ĐOÁN
  // ══════════════════════════════════════════════════════════
  {
    const f = mkFrame("02 · Chẩn đoán bệnh", W + GAP, 0);
    drawSidebar(f, 0);
    drawContentHeader(f, "Chẩn đoán bệnh", "Chọn triệu chứng → hệ thống phân tích forward chaining → kết quả + gợi ý thuốc");

    const CX = SIDEBAR_W + 16;
    const TOP = 80;
    const LEFT_W = 340;
    const RIGHT_X = CX + LEFT_W + 12;
    const RIGHT_W = W - RIGHT_X - 16;

    // ── Left: symptom panel ──
    card(f, CX, TOP, LEFT_W, H - TOP - 16, "symptom-panel");

    mkText(f, CX + 12, TOP + 12, "Chọn triệu chứng", 13, C.dark, "Semi Bold");
    mkText(f, CX + 12, TOP + 30, "3 triệu chứng đã chọn", 10, C.primary);

    const sb = mkRect(f, CX + 12, TOP + 50, LEFT_W - 24, 32, C.bg, "search", 4);
    border(sb);
    mkText(f, CX + 22, TOP + 60, "🔍  Tìm triệu chứng...", 11, C.gray);

    const groups = [
      { name: "HÔ HẤP", items: [
        { label: "Sốt",        checked: true  },
        { label: "Ớn lạnh",   checked: true  },
        { label: "Ho khan",    checked: false },
        { label: "Khó thở",   checked: false },
        { label: "Chảy nước mũi", checked: false },
      ]},
      { name: "TOÀN THÂN", items: [
        { label: "Mệt mỏi",   checked: true  },
        { label: "Đau đầu",   checked: false },
        { label: "Chóng mặt", checked: false },
        { label: "Ớn lạnh",   checked: false },
      ]},
      { name: "TIÊU HÓA", items: [
        { label: "Buồn nôn",  checked: false },
        { label: "Đau bụng",  checked: false },
        { label: "Tiêu chảy", checked: false },
      ]},
    ];

    let gy = TOP + 92;
    groups.forEach(g => {
      mkText(f, CX + 12, gy, g.name, 9, C.gray, "Bold");
      gy += 18;
      g.items.forEach(item => {
        if (item.checked) {
          const hl = mkRect(f, CX + 8, gy - 2, LEFT_W - 16, 26, C.primaryLt, "checked-row", 3);
          mkText(f, CX + 16, gy + 3, `☑  ${item.label}`, 12, C.primary, "Medium");
        } else {
          mkText(f, CX + 16, gy + 3, `☐  ${item.label}`, 12, C.dark);
        }
        gy += 28;
      });
      gy += 6;
    });

    mkButton(f, CX + 12, H - 60, LEFT_W - 24, 40, "🔬  Chẩn đoán (3 triệu chứng)", C.primary, C.white, 6);

    // ── Right: results ──
    mkText(f, RIGHT_X, TOP + 12, "Kết quả chẩn đoán", 13, C.dark, "Semi Bold");

    const diseases = [
      { name: "Cảm cúm",          conf: 70, color: C.success },
      { name: "Viêm họng cấp",    conf: 55, color: C.warn   },
      { name: "COVID-19",          conf: 42, color: C.danger  },
    ];

    diseases.forEach((d, i) => {
      const dy = TOP + 36 + i * 82;
      const dw = RIGHT_W;
      const dc = card(f, RIGHT_X, dy, dw, 74, `disease-${i}`);
      mkRect(f, RIGHT_X, dy, 4, 74, d.color, "accent-bar", 0);

      mkText(f, RIGHT_X + 14, dy + 10, d.name, 14, C.dark, "Semi Bold");
      mkText(f, RIGHT_X + 14, dy + 32, "Độ tin cậy:", 10, C.gray);

      const barW = dw - 140;
      const barBg = mkRect(f, RIGHT_X + 85, dy + 36, barW, 8, C.border, "bar-bg", 4);
      mkRect(f, RIGHT_X + 85, dy + 36, Math.round(barW * d.conf / 100), 8, d.color, "bar-fill", 4);

      mkText(f, RIGHT_X + dw - 56, dy + 26, `${d.conf}%`, 18, d.color, "Bold");
      mkText(f, RIGHT_X + 14, dy + 54, "Xem thuốc gợi ý  →", 10, C.primary);
    });

    // Drug suggestions panel
    const drugY = TOP + 36 + diseases.length * 82 + 8;
    const drugH = H - drugY - 16;
    card(f, RIGHT_X, drugY, RIGHT_W, drugH, "drug-panel");

    mkText(f, RIGHT_X + 14, drugY + 12, "💊  Thuốc gợi ý — Cảm cúm", 12, C.dark, "Semi Bold");
    mkDivider(f, RIGHT_X + 8, drugY + 34, RIGHT_W - 16);

    ["Paracetamol 500mg — uống 1–2 viên/lần, tối đa 4g/ngày",
     "Cetirizine 10mg — kháng histamine, uống 1 viên/ngày",
     "Ambroxol 30mg — long đờm, uống 3 lần/ngày"].forEach((s, i) => {
      mkText(f, RIGHT_X + 14, drugY + 46 + i * 24, `•  ${s}`, 11, C.dark);
    });

    // Warning strip
    const warnY = drugY + drugH - 62;
    const warnBg = mkRect(f, RIGHT_X + 8, warnY, RIGHT_W - 16, 50, C.warnLt, "warn-strip", 4);
    border(warnBg, { r: 0.85, g: 0.68, b: 0.20 });
    mkText(f, RIGHT_X + 20, warnY + 8,  "⚠  Cảnh báo tương tác thuốc (1 cặp)", 11, C.warn, "Semi Bold");
    mkText(f, RIGHT_X + 20, warnY + 28, "Paracetamol ↔ Warfarin: Tăng nguy cơ chảy máu — Mức độ Trung bình", 10, C.warn);
  }

  // ══════════════════════════════════════════════════════════
  // SCREEN 3 — TRA CỨU THUỐC
  // ══════════════════════════════════════════════════════════
  {
    const f = mkFrame("03 · Tra cứu thuốc", (W + GAP) * 2, 0);
    drawSidebar(f, 1);
    drawContentHeader(f, "Tra cứu thuốc", "Tìm kiếm 100+ thuốc · kiểm tra tương tác đa thuốc");

    const CX = SIDEBAR_W + 16;
    const TOP = 80;

    // Search bar
    const searchBg = mkRect(f, CX, TOP, 360, 36, C.white, "search", 4);
    border(searchBg);
    mkText(f, CX + 12, TOP + 10, "🔍  Tìm thuốc — tên, hoạt chất...", 12, C.gray);

    const filterBg = mkRect(f, CX + 372, TOP, 170, 36, C.white, "filter", 4);
    border(filterBg);
    mkText(f, CX + 382, TOP + 10, "Nhóm: Tất cả  ▾", 12, C.gray);

    mkButton(f, CX + 554, TOP, 100, 36, "Tìm kiếm", C.primary, C.white);

    const LEFT_W = 360;
    const RIGHT_X = CX + LEFT_W + 12;
    const RIGHT_W = W - RIGHT_X - 16;
    const LIST_TOP = TOP + 48;

    // Drug list
    card(f, CX, LIST_TOP, LEFT_W, H - LIST_TOP - 16, "drug-list");

    const listCols = [
      { label: "Tên thuốc", w: 200 },
      { label: "Nhóm", w: 130 },
      { label: "Tình trạng", w: 30 },
    ];
    drawTableHeader(f, CX, LIST_TOP, LEFT_W, listCols);

    const drugRows = [
      { name: "Paracetamol 500mg",  group: "Giảm đau, hạ sốt",  active: true,  sel: true  },
      { name: "Amoxicillin 500mg",  group: "Kháng sinh",         active: true,  sel: false },
      { name: "Cetirizine 10mg",    group: "Kháng histamine",    active: true,  sel: false },
      { name: "Ibuprofen 400mg",    group: "NSAIDs",             active: false, sel: false },
      { name: "Omeprazole 20mg",    group: "Ức chế bơm proton",  active: true,  sel: false },
      { name: "Metformin 500mg",    group: "Đái tháo đường",     active: true,  sel: false },
      { name: "Atorvastatin 20mg",  group: "Hạ lipid máu",       active: true,  sel: false },
      { name: "Losartan 50mg",      group: "Hạ huyết áp",        active: true,  sel: false },
    ];

    drugRows.forEach((d, i) => {
      const ry = LIST_TOP + 34 + i * 40;
      if (d.sel) {
        mkRect(f, CX, ry, LEFT_W, 40, C.primaryLt, "sel-row");
        mkText(f, CX + 12, ry + 12, d.name,  12, C.primary, "Semi Bold");
      } else {
        if (i % 2 === 1) mkRect(f, CX, ry, LEFT_W, 40, C.rowAlt, "alt-row");
        mkText(f, CX + 12, ry + 12, d.name,  12, C.dark);
      }
      mkText(f, CX + 212, ry + 12, d.group, 11, C.gray);
      badge(f, CX + LEFT_W - 68, ry + 9, d.active ? "Active" : "Off",
        d.active ? C.successLt : C.dangerLt,
        d.active ? C.success : C.danger);
      mkDivider(f, CX, ry + 40, LEFT_W);
    });

    // Drug detail
    card(f, RIGHT_X, LIST_TOP, RIGHT_W, H - LIST_TOP - 16, "drug-detail");

    mkText(f, RIGHT_X + 16, LIST_TOP + 12, "Paracetamol 500mg", 17, C.dark, "Bold");
    mkText(f, RIGHT_X + 16, LIST_TOP + 36, "Hoạt chất: Acetaminophen  ·  Nhóm: Giảm đau, hạ sốt", 10, C.gray);
    mkDivider(f, RIGHT_X + 8, LIST_TOP + 58, RIGHT_W - 16);

    const detailFields = [
      ["Liều dùng",       "500–1000 mg / lần · tối đa 4 g / ngày"],
      ["Cách dùng",       "Uống sau ăn, kèm nhiều nước"],
      ["Chống chỉ định",  "Dị ứng Acetaminophen · Suy gan nặng (Child-Pugh C)"],
      ["Tác dụng phụ",   "Buồn nôn, phát ban da (hiếm gặp)"],
      ["Bảo quản",        "Nhiệt độ phòng, tránh ẩm, ánh sáng trực tiếp"],
    ];

    let fy = LIST_TOP + 68;
    detailFields.forEach(([label, val]) => {
      mkText(f, RIGHT_X + 16, fy,      label, 10, C.gray, "Semi Bold");
      mkText(f, RIGHT_X + 16, fy + 14, val,   12, C.dark, "Regular", RIGHT_W - 32);
      fy += 50;
    });

    mkDivider(f, RIGHT_X + 8, fy, RIGHT_W - 16);
    fy += 12;

    mkText(f, RIGHT_X + 16, fy, "Kiểm tra tương tác thuốc", 13, C.dark, "Semi Bold");
    fy += 22;
    mkText(f, RIGHT_X + 16, fy, "Chọn nhiều thuốc để kiểm tra:", 10, C.gray);
    fy += 18;

    ["☑  Paracetamol 500mg", "☑  Warfarin 5mg", "☐  Aspirin 81mg", "☐  Amoxicillin 500mg"].forEach(s => {
      const isChecked = s.startsWith("☑");
      mkText(f, RIGHT_X + 16, fy, s, 12, isChecked ? C.primary : C.dark);
      fy += 24;
    });

    mkButton(f, RIGHT_X + 16, fy + 6, 180, 34, "Kiểm tra tương tác", C.primary, C.white);
    fy += 54;

    // Interaction result
    const intBg = mkRect(f, RIGHT_X + 8, fy, RIGHT_W - 16, 72, C.warnLt, "interaction", 4);
    border(intBg, { r: 0.85, g: 0.68, b: 0.2 });
    mkText(f, RIGHT_X + 20, fy + 8,  "⚠  Paracetamol ↔ Warfarin", 12, C.warn, "Semi Bold");
    mkText(f, RIGHT_X + 20, fy + 28, "Mức độ: Trung bình (2/3)", 11, C.warn);
    mkText(f, RIGHT_X + 20, fy + 46, "Tăng nguy cơ chảy máu. Theo dõi INR thường xuyên.", 10, C.warn, "Regular", RIGHT_W - 40);
  }

  // ══════════════════════════════════════════════════════════
  // SCREEN 4 — HỒ SƠ BỆNH NHÂN
  // ══════════════════════════════════════════════════════════
  {
    const f = mkFrame("04 · Hồ sơ bệnh nhân", 0, H + GAP);
    drawSidebar(f, 2);
    drawContentHeader(f, "Hồ sơ bệnh nhân", "Quản lý hồ sơ · xem lịch sử chẩn đoán theo bệnh nhân");

    const CX = SIDEBAR_W + 16;
    const TOP = 80;

    mkButton(f, CX, TOP, 110, 34, "+ Thêm mới", C.primary, C.white);

    const sbBg = mkRect(f, CX + 122, TOP, 270, 34, C.white, "search", 4);
    border(sbBg);
    mkText(f, CX + 134, TOP + 10, "🔍  Tìm tên / số điện thoại...", 11, C.gray);

    const LEFT_W = 310;
    const RIGHT_X = CX + LEFT_W + 12;
    const RIGHT_W = W - RIGHT_X - 16;
    const LIST_TOP = TOP + 46;

    // Patient list
    card(f, CX, LIST_TOP, LEFT_W, H - LIST_TOP - 16, "patient-list");
    mkText(f, CX + 12, LIST_TOP + 10, "Danh sách bệnh nhân (5)", 12, C.dark, "Semi Bold");
    mkDivider(f, CX, LIST_TOP + 32, LEFT_W);

    const patients = [
      { name: "Nguyễn Văn An",  info: "15/03/1985 · Nam",   sel: true  },
      { name: "Trần Thị Bình",  info: "22/07/1992 · Nữ",    sel: false },
      { name: "Lê Minh Cường",  info: "08/11/1978 · Nam",   sel: false },
      { name: "Phạm Thị Dung",  info: "14/02/1965 · Nữ",    sel: false },
      { name: "Hoàng Văn Em",   info: "30/09/2001 · Nam",   sel: false },
    ];

    patients.forEach((p, i) => {
      const py = LIST_TOP + 36 + i * 58;
      if (p.sel) mkRect(f, CX, py, LEFT_W, 58, C.primaryLt, "sel-patient");
      mkText(f, CX + 12, py + 8,  p.name, 13, p.sel ? C.primary : C.dark, p.sel ? "Semi Bold" : "Regular");
      mkText(f, CX + 12, py + 30, p.info, 10, C.gray);
      mkDivider(f, CX, py + 57, LEFT_W);
    });

    // Detail panel
    card(f, RIGHT_X, LIST_TOP, RIGHT_W, H - LIST_TOP - 16, "patient-detail");

    mkText(f, RIGHT_X + 16, LIST_TOP + 12, "Nguyễn Văn An", 17, C.dark, "Bold");

    // Tabs
    ["Thông tin bệnh nhân", "Lịch sử chẩn đoán"].forEach((tab, i) => {
      const tx = RIGHT_X + 16 + i * 185;
      const ty = LIST_TOP + 40;
      if (i === 0) {
        mkRect(f, tx, ty, 176, 30, C.primary, "tab-active", 4);
        mkText(f, tx + 12, ty + 8, tab, 11, C.white, "Semi Bold");
      } else {
        const tb = mkRect(f, tx, ty, 176, 30, C.bg, "tab", 4);
        border(tb);
        mkText(f, tx + 12, ty + 8, tab, 11, C.gray);
      }
    });
    mkDivider(f, RIGHT_X, LIST_TOP + 78, RIGHT_W);

    // Form – row 1
    const col1 = RIGHT_X + 16;
    const col2 = RIGHT_X + (RIGHT_W / 2) + 8;
    const colW = (RIGHT_W / 2) - 24;

    mkInput(f, col1, LIST_TOP + 90, colW, "Họ và tên",     "Nguyễn Văn An",           true);
    mkInput(f, col2, LIST_TOP + 90, colW, "Ngày sinh",     "15/03/1985",               false);
    mkInput(f, col1, LIST_TOP + 152, colW, "Giới tính",    "Nam  ▾",                   false);
    mkInput(f, col2, LIST_TOP + 152, colW, "Số điện thoại","0901 234 567",             false);
    mkInput(f, col1, LIST_TOP + 214, RIGHT_W - 32, "Địa chỉ", "123 Nguyễn Huệ, Q.1, TP.HCM", false);

    // Tiền sử bệnh
    mkText(f, col1, LIST_TOP + 278, "Tiền sử bệnh", 11, C.gray, "Semi Bold");
    const tsb = mkRect(f, col1, LIST_TOP + 296, RIGHT_W - 32, 56, C.bg, "tien-su", 4);
    border(tsb);
    mkText(f, col1 + 10, LIST_TOP + 308, "Tiểu đường type 2 (2018) · Tăng huyết áp (2020)", 12, C.dark);

    // Dị ứng
    mkText(f, col1, LIST_TOP + 364, "Dị ứng", 11, C.danger, "Semi Bold");
    const diung = mkRect(f, col1, LIST_TOP + 382, RIGHT_W - 32, 44, C.dangerLt, "di-ung", 4);
    border(diung, { r: 0.9, g: 0.75, b: 0.75 });
    mkText(f, col1 + 10, LIST_TOP + 394, "⚠  Penicillin — phản ứng nặng", 12, C.danger);

    mkButton(f, RIGHT_X + RIGHT_W - 110, H - 54, 100, 36, "💾  Lưu", C.primary, C.white);
  }

  // ══════════════════════════════════════════════════════════
  // SCREEN 5 — BÁO CÁO THỐNG KÊ
  // ══════════════════════════════════════════════════════════
  {
    const f = mkFrame("05 · Báo cáo thống kê", W + GAP, H + GAP);
    drawSidebar(f, 3);
    drawContentHeader(f, "Báo cáo thống kê", "Thống kê chẩn đoán · biểu đồ theo ngày · xuất PDF");

    const CX = SIDEBAR_W + 16;
    const TOP = 80;

    // Date range bar
    const fromBg = mkRect(f, CX, TOP, 155, 34, C.white, "from", 4);
    border(fromBg);
    mkText(f, CX + 10, TOP + 10, "Từ: 01/06/2024", 12, C.dark);

    const toBg = mkRect(f, CX + 167, TOP, 155, 34, C.white, "to", 4);
    border(toBg);
    mkText(f, CX + 177, TOP + 10, "Đến: 17/06/2024", 12, C.dark);

    mkButton(f, CX + 334, TOP, 110, 34, "🔍  Tìm kiếm", C.primary, C.white);
    mkButton(f, CX + 456, TOP, 110, 34, "📄  Xuất PDF", C.success, C.white);

    // Stat cards
    const statCards = [
      { label: "Tổng chẩn đoán",      val: "47",       color: C.primary },
      { label: "Bệnh nhân khác nhau", val: "12",       color: C.success },
      { label: "Bệnh phổ biến nhất",  val: "Cảm cúm", color: C.purple  },
    ];
    const statW = Math.floor((W - CX - 16 - 24) / 3);
    statCards.forEach((s, i) => {
      const sx = CX + i * (statW + 12);
      const sc = card(f, sx, TOP + 46, statW, 72, `stat-${i}`);
      mkRect(f, sx, TOP + 46, statW, 4, s.color, "accent-top", 0);
      mkText(f, sx + 14, TOP + 58, s.label, 10, C.gray);
      mkText(f, sx + 14, TOP + 72, s.val,   20, s.color, "Bold");
    });

    // Chart panel
    const chartY = TOP + 130;
    const chartPanel = card(f, CX, chartY, W - CX - 16, 240, "chart-panel");
    mkText(f, CX + 16, chartY + 14, "Số lượng chẩn đoán theo ngày", 13, C.dark, "Semi Bold");
    mkText(f, CX + 16, chartY + 32, "Tháng 6/2024 — LiveChartsCore ColumnSeries", 10, C.gray);

    // Simulated bar chart
    const bars = [3, 5, 2, 7, 4, 8, 3, 6, 5, 4, 7, 3, 6, 5, 4, 7, 5];
    const maxBar = 8, chartH = 158, barW = 22, barGap = 6;
    const bStartX = CX + 44, bBaseY = chartY + 210;

    // Y axis labels
    [0, 2, 4, 6, 8].forEach(v => {
      const ly = bBaseY - Math.round(v / maxBar * chartH);
      mkText(f, CX + 20, ly - 6, String(v), 9, C.gray);
      mkRect(f, bStartX - 4, ly, bars.length * (barW + barGap) + 8, 1,
        { r: 0.9, g: 0.9, b: 0.92 }, "grid-line");
    });

    bars.forEach((val, i) => {
      const bh = Math.round(val / maxBar * chartH);
      mkRect(f, bStartX + i * (barW + barGap), bBaseY - bh, barW, bh, C.primary, `bar-${i}`, 2);
      const day = String(i + 1).padStart(2, "0");
      mkText(f, bStartX + i * (barW + barGap) + 4, bBaseY + 4, day, 8, C.gray);
    });
    mkRect(f, bStartX - 4, bBaseY, bars.length * (barW + barGap) + 8, 1, C.border, "x-axis");

    // Table
    const tableY = chartY + 252;
    const tableH = H - tableY - 16;
    card(f, CX, tableY, W - CX - 16, tableH, "detail-table");

    mkText(f, CX + 16, tableY + 12, "Chi tiết chẩn đoán", 13, C.dark, "Semi Bold");

    const tCols = [
      { label: "STT",             w:  40 },
      { label: "Ngày",            w: 100 },
      { label: "Bệnh nhân",      w: 160 },
      { label: "Bệnh chẩn đoán", w: 200 },
      { label: "Độ tin cậy",     w:  90 },
      { label: "Bác sĩ",          w: 110 },
    ];
    drawTableHeader(f, CX, tableY + 34, W - CX - 16, tCols);

    const tRows = [
      ["1", "17/06/2024", "Nguyễn Văn An",  "Cảm cúm",       "70%", "bacsi1"],
      ["2", "17/06/2024", "Trần Thị Bình",  "Viêm họng cấp", "65%", "bacsi1"],
      ["3", "16/06/2024", "Lê Minh Cường",  "Tiêu chảy cấp", "80%", "bacsi2"],
      ["4", "15/06/2024", "Phạm Thị Dung",  "Tăng huyết áp", "72%", "bacsi1"],
    ];
    tRows.forEach((row, i) => {
      drawTableRow(f, CX, tableY + 68 + i * 40, W - CX - 16, row, tCols,
        false, i % 2 === 1);
    });
  }

  // ══════════════════════════════════════════════════════════
  // SCREEN 6 — ADMIN: QUẢN LÝ THUỐC
  // ══════════════════════════════════════════════════════════
  {
    const f = mkFrame("06 · Admin · Quản lý thuốc", (W + GAP) * 2, H + GAP);
    drawSidebar(f, 5, "Quản trị viên", "admin");
    drawContentHeader(f, "Quản lý thuốc (Admin)", "CRUD thuốc · soft delete · phân nhóm");

    const CX = SIDEBAR_W + 16;
    const TOP = 80;

    mkButton(f, CX,       TOP, 108, 34, "+ Thêm mới", C.primary, C.white);
    mkButton(f, CX + 120, TOP,  80, 34, "🗑  Xóa",   C.danger,  C.white);

    const sbBg = mkRect(f, CX + 212, TOP, 230, 34, C.white, "search", 4);
    border(sbBg);
    mkText(f, CX + 222, TOP + 10, "🔍  Tìm thuốc...", 11, C.gray);

    const filterBg = mkRect(f, CX + 454, TOP, 160, 34, C.white, "filter", 4);
    border(filterBg);
    mkText(f, CX + 464, TOP + 10, "Nhóm: Tất cả  ▾", 11, C.gray);

    const LEFT_W = 380;
    const RIGHT_X = CX + LEFT_W + 12;
    const RIGHT_W = W - RIGHT_X - 16;
    const LIST_TOP = TOP + 46;

    card(f, CX, LIST_TOP, LEFT_W, H - LIST_TOP - 16, "drug-list");

    const lCols = [
      { label: "Tên thuốc", w: 190 },
      { label: "Nhóm",      w: 140 },
      { label: "Trạng thái",w:  50 },
    ];
    drawTableHeader(f, CX, LIST_TOP, LEFT_W, lCols);

    const adminDrugs = [
      { name: "Paracetamol 500mg",  group: "Giảm đau, hạ sốt",  active: true,  sel: true  },
      { name: "Amoxicillin 500mg",  group: "Kháng sinh",         active: true,  sel: false },
      { name: "Cetirizine 10mg",    group: "Kháng histamine",    active: true,  sel: false },
      { name: "Ibuprofen 400mg",    group: "NSAIDs",             active: false, sel: false },
      { name: "Omeprazole 20mg",    group: "Ức chế bơm proton",  active: true,  sel: false },
      { name: "Metformin 500mg",    group: "Đái tháo đường",     active: true,  sel: false },
    ];

    adminDrugs.forEach((d, i) => {
      const ry = LIST_TOP + 34 + i * 42;
      if (d.sel) {
        mkRect(f, CX, ry, LEFT_W, 42, C.primaryLt, "sel-row");
        mkText(f, CX + 12, ry + 13, d.name,  12, C.primary, "Semi Bold");
      } else {
        if (i % 2 === 1) mkRect(f, CX, ry, LEFT_W, 42, C.rowAlt);
        mkText(f, CX + 12, ry + 13, d.name,  12, C.dark);
      }
      mkText(f, CX + 202, ry + 13, d.group, 11, C.gray);
      badge(f, CX + LEFT_W - 68, ry + 10, d.active ? "Active" : "Off",
        d.active ? C.successLt : C.dangerLt,
        d.active ? C.success : C.danger);
      mkDivider(f, CX, ry + 42, LEFT_W);
    });

    // Edit form
    card(f, RIGHT_X, LIST_TOP, RIGHT_W, H - LIST_TOP - 16, "edit-panel");
    mkText(f, RIGHT_X + 16, LIST_TOP + 12, "Chi tiết / Chỉnh sửa thuốc", 13, C.dark, "Semi Bold");
    mkDivider(f, RIGHT_X + 8, LIST_TOP + 34, RIGHT_W - 16);

    let ey = LIST_TOP + 44;
    [
      ["Tên thuốc",   "Paracetamol 500mg"],
      ["Hoạt chất",   "Acetaminophen"],
      ["Nhóm thuốc",  "Giảm đau, hạ sốt  ▾"],
      ["Liều dùng",   "500–1000 mg/lần · tối đa 4 g/ngày"],
      ["Cách dùng",   "Uống sau ăn, kèm nhiều nước"],
    ].forEach(([label, val]) => {
      mkInput(f, RIGHT_X + 16, ey, RIGHT_W - 32, label, val, label === "Tên thuốc");
      ey += 58;
    });

    // Chống chỉ định (red field)
    mkText(f, RIGHT_X + 16, ey, "Chống chỉ định", 11, C.danger, "Semi Bold");
    const contra = mkRect(f, RIGHT_X + 16, ey + 17, RIGHT_W - 32, 48, C.dangerLt, "contra", 4);
    border(contra, { r: 0.88, g: 0.74, b: 0.74 });
    mkText(f, RIGHT_X + 26, ey + 28, "Dị ứng Acetaminophen · Suy gan nặng (Child-Pugh C)", 11, C.danger, "Regular", RIGHT_W - 52);
    ey += 76;

    // IsActive
    mkText(f, RIGHT_X + 16, ey, "☑  Đang hoạt động (IsActive)", 12, C.dark);

    mkButton(f, RIGHT_X + RIGHT_W - 110, H - 54, 100, 36, "💾  Lưu", C.primary, C.white);
  }

  // ══════════════════════════════════════════════════════════
  // SCREEN 7 — ADMIN: QUẢN LÝ BỆNH + TẬP LUẬT
  // ══════════════════════════════════════════════════════════
  {
    const f = mkFrame("07 · Admin · Quản lý bệnh & tập luật", 0, (H + GAP) * 2);
    drawSidebar(f, 6, "Quản trị viên", "admin");
    drawContentHeader(f, "Quản lý bệnh & tập luật IF-THEN (Admin)", "CRUD bệnh · chỉnh sửa tập luật trọng số · forward chaining");

    const CX = SIDEBAR_W + 16;
    const TOP = 80;

    mkButton(f, CX,       TOP, 116, 34, "+ Thêm bệnh", C.primary, C.white);
    mkButton(f, CX + 128, TOP,  80, 34, "🗑  Xóa",    C.danger,  C.white);

    const sbBg = mkRect(f, CX + 220, TOP, 220, 34, C.white, "search", 4);
    border(sbBg);
    mkText(f, CX + 230, TOP + 10, "🔍  Tìm bệnh...", 11, C.gray);

    const LEFT_W = 250;
    const RIGHT_X = CX + LEFT_W + 12;
    const RIGHT_W = W - RIGHT_X - 16;
    const LIST_TOP = TOP + 46;

    card(f, CX, LIST_TOP, LEFT_W, H - LIST_TOP - 16, "disease-list");
    mkText(f, CX + 12, LIST_TOP + 10, "Danh sách bệnh (40)", 12, C.dark, "Semi Bold");
    mkDivider(f, CX, LIST_TOP + 30, LEFT_W);

    const diseaseList = [
      "Cảm cúm", "Viêm họng cấp", "Tiêu chảy cấp", "Viêm phổi",
      "Đái tháo đường type 2", "Tăng huyết áp", "Viêm dạ dày",
      "Sốt xuất huyết", "Viêm amidan", "Hen phế quản",
    ];
    diseaseList.forEach((d, i) => {
      const dy = LIST_TOP + 34 + i * 42;
      const isSel = i === 0;
      if (isSel) mkRect(f, CX, dy, LEFT_W, 42, C.primaryLt, "sel-row");
      else if (i % 2 === 1) mkRect(f, CX, dy, LEFT_W, 42, C.rowAlt);
      mkText(f, CX + 12, dy + 13, d, 12, isSel ? C.primary : C.dark, isSel ? "Semi Bold" : "Regular");
      mkDivider(f, CX, dy + 42, LEFT_W);
    });

    // Right panel
    card(f, RIGHT_X, LIST_TOP, RIGHT_W, H - LIST_TOP - 16, "right-panel");
    mkText(f, RIGHT_X + 16, LIST_TOP + 12, "Cảm cúm", 16, C.dark, "Bold");

    // Tabs
    ["Thông tin bệnh", "Tập luật IF-THEN"].forEach((tab, i) => {
      const tx = RIGHT_X + 16 + i * 175;
      const ty = LIST_TOP + 38;
      if (i === 1) {
        mkRect(f, tx, ty, 166, 30, C.primary, "tab-active", 4);
        mkText(f, tx + 10, ty + 8, tab, 11, C.white, "Semi Bold");
      } else {
        const tb = mkRect(f, tx, ty, 166, 30, C.bg, "tab", 4);
        border(tb);
        mkText(f, tx + 10, ty + 8, tab, 11, C.gray);
      }
    });
    mkDivider(f, RIGHT_X, LIST_TOP + 76, RIGHT_W);

    mkText(f, RIGHT_X + 16, LIST_TOP + 86, "Tập luật chẩn đoán Cảm cúm", 12, C.dark, "Semi Bold");
    mkText(f, RIGHT_X + 16, LIST_TOP + 104,"Điều chỉnh TrongSo & BatBuoc → ảnh hưởng trực tiếp confidence score", 10, C.gray);
    mkButton(f, RIGHT_X + RIGHT_W - 126, LIST_TOP + 88, 114, 28, "+ Thêm luật", C.primary, C.white, 4);

    // Rule table header
    const ruleCols = [
      { label: "Triệu chứng (ComboBox)",  w: 210 },
      { label: "Trọng số",               w:  90 },
      { label: "Bắt buộc",              w:  80 },
      { label: "",                        w:  60 },
    ];
    const ruleTableY = LIST_TOP + 122;
    drawTableHeader(f, RIGHT_X, ruleTableY, RIGHT_W, ruleCols);

    const rules = [
      { symptom: "Sốt (ID=1)",          weight: "2.0",  required: true  },
      { symptom: "Ớn lạnh (ID=3)",      weight: "1.5",  required: false },
      { symptom: "Mệt mỏi (ID=4)",      weight: "1.5",  required: false },
      { symptom: "Đau đầu (ID=5)",      weight: "1.0",  required: false },
      { symptom: "Sổ mũi (ID=8)",       weight: "0.8",  required: false },
    ];

    rules.forEach((rule, i) => {
      const ry = ruleTableY + 34 + i * 46;
      if (i % 2 === 1) mkRect(f, RIGHT_X, ry, RIGHT_W, 46, C.rowAlt);

      const comboBg = mkRect(f, RIGHT_X + 12, ry + 8, 195, 30, C.bg, "combo", 4);
      border(comboBg);
      mkText(f, RIGHT_X + 22, ry + 17, rule.symptom, 11, C.dark);

      const wBg = mkRect(f, RIGHT_X + 222, ry + 8, 74, 30, C.bg, "weight", 4);
      border(wBg);
      mkText(f, RIGHT_X + 232, ry + 17, rule.weight, 11, C.dark);

      mkText(f, RIGHT_X + 312, ry + 14, rule.required ? "☑" : "☐", 18,
        rule.required ? C.primary : C.dark);

      mkText(f, RIGHT_X + RIGHT_W - 52, ry + 16, "🗑 Xóa", 11, C.danger);

      mkDivider(f, RIGHT_X, ry + 46, RIGHT_W);
    });

    // Confidence preview
    const previewY = ruleTableY + 34 + rules.length * 46 + 12;
    const previewBg = mkRect(f, RIGHT_X + 12, previewY, RIGHT_W - 24, 52, C.primaryLt, "confidence-preview", 6);
    border(previewBg, C.primary);
    mkText(f, RIGHT_X + 24, previewY + 8,  "Xem trước confidence — Input {Sốt, Ớn lạnh}:", 11, C.primary, "Semi Bold");
    mkText(f, RIGHT_X + 24, previewY + 28, "3.5 / 5.0 = 70%  ✓  Vượt ngưỡng 40%  →  Trả về kết quả", 11, C.primary);

    mkButton(f, RIGHT_X + RIGHT_W - 162, H - 54, 150, 36, "💾  Lưu tập luật", C.primary, C.white);
  }

  // ══════════════════════════════════════════════════════════
  // SCREEN 8 — ADMIN: QUẢN LÝ USERS
  // ══════════════════════════════════════════════════════════
  {
    const f = mkFrame("08 · Admin · Quản lý users", W + GAP, (H + GAP) * 2);
    drawSidebar(f, 7, "Quản trị viên", "admin");
    drawContentHeader(f, "Quản lý người dùng (Admin)", "CRUD users · BCrypt password · phân quyền 3 role");

    const CX = SIDEBAR_W + 16;
    const TOP = 80;

    mkButton(f, CX,       TOP, 108, 34, "+ Thêm mới",   C.primary, C.white);
    mkButton(f, CX + 120, TOP, 130, 34, "🔒 Bật/Tắt TK", { r: 0.45, g: 0.45, b: 0.48 }, C.white);

    const LEFT_W = 430;
    const RIGHT_X = CX + LEFT_W + 12;
    const RIGHT_W = W - RIGHT_X - 16;
    const LIST_TOP = TOP + 46;

    card(f, CX, LIST_TOP, LEFT_W, H - LIST_TOP - 16, "user-list");

    const uCols = [
      { label: "Username", w: 105 },
      { label: "Họ tên",   w: 145 },
      { label: "Vai trò",  w: 100 },
      { label: "Trạng thái", w: 80 },
    ];
    drawTableHeader(f, CX, LIST_TOP, LEFT_W, uCols);

    const usersData = [
      { user: "admin",   name: "Administrator",   role: "Admin",       active: true,  sel: true,  roleColor: C.purple  },
      { user: "bacsi1",  name: "Nguyễn Văn An",  role: "Bác sĩ",      active: true,  sel: false, roleColor: C.primary },
      { user: "bacsi2",  name: "Trần Thị Bình",  role: "Bác sĩ",      active: true,  sel: false, roleColor: C.primary },
      { user: "duocsi1", name: "Lê Minh Cường",  role: "Dược sĩ",     active: false, sel: false, roleColor: C.teal    },
    ];

    usersData.forEach((u, i) => {
      const ry = LIST_TOP + 34 + i * 46;
      if (u.sel) {
        mkRect(f, CX, ry, LEFT_W, 46, C.primaryLt, "sel-row");
        mkText(f, CX + 12, ry + 15, u.user, 12, C.primary, "Semi Bold");
      } else {
        if (i % 2 === 1) mkRect(f, CX, ry, LEFT_W, 46, C.rowAlt);
        mkText(f, CX + 12, ry + 15, u.user, 12, C.dark);
      }
      mkText(f, CX + 117, ry + 15, u.name,  12, C.dark);
      mkText(f, CX + 262, ry + 15, u.role,  12, u.roleColor, "Medium");
      badge(f, CX + LEFT_W - 88, ry + 12, u.active ? "Active" : "Off",
        u.active ? C.successLt : C.dangerLt,
        u.active ? C.success : C.danger);
      mkDivider(f, CX, ry + 46, LEFT_W);
    });

    // Right edit panel
    card(f, RIGHT_X, LIST_TOP, RIGHT_W, H - LIST_TOP - 16, "edit-panel");
    mkText(f, RIGHT_X + 16, LIST_TOP + 12, "Thông tin tài khoản", 13, C.dark, "Semi Bold");
    mkDivider(f, RIGHT_X + 8, LIST_TOP + 34, RIGHT_W - 16);

    let ey = LIST_TOP + 44;
    const userFormFields = [
      { label: "Username (readonly khi sửa)", val: "admin",                 readOnly: true  },
      { label: "Họ và tên",                   val: "Administrator",          readOnly: false },
      { label: "Email",                        val: "admin@hospital.vn",     readOnly: false },
    ];

    userFormFields.forEach(({ label, val, readOnly }) => {
      mkInput(f, RIGHT_X + 16, ey, RIGHT_W - 32, label, val, !readOnly && label === "Họ và tên");
      ey += 58;
    });

    // Role
    mkText(f, RIGHT_X + 16, ey, "Vai trò", 11, C.gray, "Semi Bold");
    const roleBg = mkRect(f, RIGHT_X + 16, ey + 17, RIGHT_W - 32, 34, C.bg, "role", 4);
    border(roleBg);
    mkText(f, RIGHT_X + 26, ey + 28, "Quản trị viên (Admin)  ▾", 12, C.dark);
    ey += 62;

    // Password
    mkInput(f, RIGHT_X + 16, ey, RIGHT_W - 32, "Mật khẩu mới (để trống = không đổi)", "••••••••");
    ey += 62;

    // IsActive
    mkText(f, RIGHT_X + 16, ey, "☑  Tài khoản đang hoạt động (IsActive)", 12, C.dark);
    ey += 28;

    // Ghi chú
    const noteBg = mkRect(f, RIGHT_X + 16, ey, RIGHT_W - 32, 44, C.warnLt, "note", 4);
    border(noteBg, { r: 0.85, g: 0.7, b: 0.2 });
    mkText(f, RIGHT_X + 26, ey + 8,  "⚠  Không thể tắt tài khoản đang đăng nhập", 11, C.warn, "Semi Bold");
    mkText(f, RIGHT_X + 26, ey + 26, "Mật khẩu reset về Admin@123 khi dùng nút Reset", 10, C.warn);

    mkButton(f, RIGHT_X + RIGHT_W - 250, H - 54, 130, 36, "🔑  Reset mật khẩu",
      { r: 0.55, g: 0.40, b: 0.0 }, C.white);
    mkButton(f, RIGHT_X + RIGHT_W - 110, H - 54, 100, 36, "💾  Lưu", C.primary, C.white);
  }

  // ── Done ───────────────────────────────────────────────────
  if (figma.currentPage.children.length > 0) {
    figma.viewport.scrollAndZoomIntoView(figma.currentPage.children);
  }
  figma.closePlugin("✅ Tạo " + figma.currentPage.children.length + " màn hình wireframe thành công!");
}

main().catch(err => {
  const msg = err === undefined   ? "undefined thrown" :
              err === null        ? "null thrown" :
              typeof err === "string" ? err :
              err instanceof Error    ? (err.stack || err.message || String(err)) :
              JSON.stringify(err) || String(err);
  figma.closePlugin("❌ Lỗi: " + msg);
});
