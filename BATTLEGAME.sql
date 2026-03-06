-- Tạo database
CREATE DATABASE BATTLEGAME;
GO

-- Sử dụng database
USE BATTLEGAME;
GO

-- Bảng Player
CREATE TABLE Player (
    PlayerId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PlayerName NVARCHAR(64),
    FullName NVARCHAR(128),
    Age NVARCHAR(10),
    [Level] INT,
    Email NVARCHAR(64)
);
GO

-- Bảng Asset
CREATE TABLE Asset (
    AssetId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    AssetName NVARCHAR(64),
    LevelRequire INT
);
GO

-- Bảng trung gian PlayerAsset
CREATE TABLE PlayerAsset (
    PlayerId UNIQUEIDENTIFIER,
    AssetId UNIQUEIDENTIFIER,

    CONSTRAINT PK_PlayerAsset PRIMARY KEY (PlayerId, AssetId),

    CONSTRAINT FK_PlayerAsset_Player
        FOREIGN KEY (PlayerId)
        REFERENCES Player(PlayerId),

    CONSTRAINT FK_PlayerAsset_Asset
        FOREIGN KEY (AssetId)
        REFERENCES Asset(AssetId)
);
GO