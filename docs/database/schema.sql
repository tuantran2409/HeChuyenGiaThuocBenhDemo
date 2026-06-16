-- ============================================================
-- HE CHUYEN GIA THUOC BENH - DATABASE SCHEMA
-- SQL Server 2019+
-- ============================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'HeChuyenGiaThuocBenh')
    DROP DATABASE HeChuyenGiaThuocBenh;
GO

CREATE DATABASE HeChuyenGiaThuocBenh
    COLLATE Vietnamese_CI_AS;
GO

USE HeChuyenGiaThuocBenh;
GO

-- ============================================================
-- USERS
-- ============================================================
CREATE TABLE Users (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Username    NVARCHAR(50)  NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    HoTen       NVARCHAR(100) NOT NULL,
    Email       NVARCHAR(100) NULL,
    Role        TINYINT       NOT NULL DEFAULT 2,  -- 1=Admin, 2=BacSi, 3=DuocSi
    IsActive    BIT           NOT NULL DEFAULT 1,
    CreatedAt   DATETIME      NOT NULL DEFAULT GETDATE()
);

-- ============================================================
-- THUOC (DRUGS)
-- ============================================================
CREATE TABLE Thuoc (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    Ten             NVARCHAR(200) NOT NULL,
    HoatChat        NVARCHAR(200) NOT NULL,
    NhomThuoc       NVARCHAR(100) NULL,
    LieuDung        NVARCHAR(500) NULL,
    CachDung        NVARCHAR(500) NULL,
    ChongChiDinh    NVARCHAR(1000) NULL,
    TacDungPhu      NVARCHAR(1000) NULL,
    MoTa            NVARCHAR(MAX) NULL,
    IsActive        BIT           NOT NULL DEFAULT 1
);

-- ============================================================
-- BENH (DISEASES)
-- ============================================================
CREATE TABLE Benh (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Ten         NVARCHAR(200) NOT NULL,
    MoTa        NVARCHAR(MAX) NULL,
    NhomBenh    NVARCHAR(100) NULL,
    IsActive    BIT           NOT NULL DEFAULT 1
);

-- ============================================================
-- TRIEU CHUNG (SYMPTOMS)
-- ============================================================
CREATE TABLE TrieuChung (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    Ten             NVARCHAR(200) NOT NULL,
    MoTa            NVARCHAR(500) NULL,
    NhomTrieuChung  NVARCHAR(100) NULL
);

-- ============================================================
-- BENH - TRIEU CHUNG (many-to-many)
-- ============================================================
CREATE TABLE BenhTrieuChung (
    BenhId      INT           NOT NULL,
    TrieuChungId INT          NOT NULL,
    TrongSo     DECIMAL(3,1)  NOT NULL DEFAULT 1.0,
    BatBuoc     BIT           NOT NULL DEFAULT 0,
    PRIMARY KEY (BenhId, TrieuChungId),
    FOREIGN KEY (BenhId)       REFERENCES Benh(Id)       ON DELETE CASCADE,
    FOREIGN KEY (TrieuChungId) REFERENCES TrieuChung(Id) ON DELETE CASCADE
);

-- ============================================================
-- BENH - THUOC (many-to-many, treatment mapping)
-- ============================================================
CREATE TABLE BenhThuoc (
    BenhId      INT           NOT NULL,
    ThuocId     INT           NOT NULL,
    GhiChu      NVARCHAR(500) NULL,
    ThuTu       INT           NOT NULL DEFAULT 1,
    PRIMARY KEY (BenhId, ThuocId),
    FOREIGN KEY (BenhId)  REFERENCES Benh(Id)  ON DELETE CASCADE,
    FOREIGN KEY (ThuocId) REFERENCES Thuoc(Id) ON DELETE CASCADE
);

-- ============================================================
-- TUONG TAC THUOC (DRUG INTERACTIONS)
-- ============================================================
CREATE TABLE TuongTacThuoc (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    ThuocId1    INT           NOT NULL,
    ThuocId2    INT           NOT NULL,
    MucDo       TINYINT       NOT NULL DEFAULT 1,  -- 1=Nhe, 2=TrungBinh, 3=Nang
    MoTa        NVARCHAR(1000) NOT NULL,
    HauQua      NVARCHAR(1000) NULL,
    FOREIGN KEY (ThuocId1) REFERENCES Thuoc(Id),
    FOREIGN KEY (ThuocId2) REFERENCES Thuoc(Id),
    CONSTRAINT CHK_TuongTac_Order CHECK (ThuocId1 < ThuocId2)
);

-- ============================================================
-- BENH NHAN (PATIENTS)
-- ============================================================
CREATE TABLE BenhNhan (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,
    NgaySinh    DATE          NOT NULL,
    GioiTinh    TINYINT       NOT NULL DEFAULT 1,  -- 1=Nam, 2=Nu, 3=Khac
    SoDienThoai NVARCHAR(15)  NULL,
    DiaChi      NVARCHAR(500) NULL,
    TienSuBenh  NVARCHAR(MAX) NULL,
    DiUng       NVARCHAR(500) NULL,
    CreatedAt   DATETIME      NOT NULL DEFAULT GETDATE()
);

-- ============================================================
-- LICH SU CHAN DOAN (DIAGNOSIS HISTORY)
-- ============================================================
CREATE TABLE LichSuChanDoan (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    BenhNhanId      INT           NOT NULL,
    UserId          INT           NOT NULL,
    NgayChanDoan    DATETIME      NOT NULL DEFAULT GETDATE(),
    TrieuChungInput NVARCHAR(MAX) NOT NULL,
    KetQuaBenh      NVARCHAR(MAX) NOT NULL,
    ThuocGoiY       NVARCHAR(MAX) NULL,
    GhiChu          NVARCHAR(MAX) NULL,
    FOREIGN KEY (BenhNhanId) REFERENCES BenhNhan(Id),
    FOREIGN KEY (UserId)     REFERENCES Users(Id)
);

-- ============================================================
-- INDEXES
-- ============================================================
CREATE INDEX IX_Thuoc_Ten        ON Thuoc(Ten);
CREATE INDEX IX_Thuoc_HoatChat   ON Thuoc(HoatChat);
CREATE INDEX IX_Benh_Ten         ON Benh(Ten);
CREATE INDEX IX_BenhNhan_HoTen   ON BenhNhan(HoTen);
CREATE INDEX IX_LichSu_BenhNhan  ON LichSuChanDoan(BenhNhanId);
CREATE INDEX IX_LichSu_NgayChan  ON LichSuChanDoan(NgayChanDoan);
GO
