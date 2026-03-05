-- =============================================
-- BATTLEGAME Database - SQL Server
-- Database First Approach
-- =============================================

CREATE DATABASE BATTLEGAME;
GO

USE BATTLEGAME;
GO

-- =============================================
-- TABLE: Player
-- =============================================
CREATE TABLE Player (
    PlayerId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PlayerName NVARCHAR(64) NOT NULL,
    FullName NVARCHAR(128),
    Age NVARCHAR(10),
    [Level] INT,
    Email NVARCHAR(64)
);
GO

-- =============================================
-- TABLE: Asset
-- =============================================
CREATE TABLE Asset (
    AssetId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    AssetName NVARCHAR(64) NOT NULL,
    LevelRequire INT
);
GO

-- =============================================
-- TABLE: PlayerAsset
-- =============================================
CREATE TABLE PlayerAsset (
    PlayerId UNIQUEIDENTIFIER NOT NULL,
    AssetId UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT PK_PlayerAsset PRIMARY KEY (PlayerId, AssetId),

    CONSTRAINT FK_PlayerAsset_Player 
        FOREIGN KEY (PlayerId) REFERENCES Player(PlayerId),

    CONSTRAINT FK_PlayerAsset_Asset 
        FOREIGN KEY (AssetId) REFERENCES Asset(AssetId)
);
GO

-- =============================================
-- Insert Sample Data
-- =============================================

-- Insert Sample Players
INSERT INTO Player (PlayerName, FullName, Age, [Level], Email) VALUES 
('Player1', 'John Doe', '20', 10, 'player1@example.com'),
('Player2', 'Jane Smith', '19', 3, 'player2@example.com'),
('Player3', 'Bob Wilson', '23', 10, 'player3@example.com');
GO

-- Insert Sample Assets
INSERT INTO Asset (AssetName, LevelRequire) VALUES 
('Hero 1', 1),
('Hero 2', 5),
('Sword', 1),
('Shield', 2),
('Potion', 1);
GO

-- Insert Sample PlayerAssets (Many-to-Many relationship)
DECLARE @Player1Id UNIQUEIDENTIFIER = (SELECT PlayerId FROM Player WHERE PlayerName = 'Player1');
DECLARE @Player2Id UNIQUEIDENTIFIER = (SELECT PlayerId FROM Player WHERE PlayerName = 'Player2');
DECLARE @Player3Id UNIQUEIDENTIFIER = (SELECT PlayerId FROM Player WHERE PlayerName = 'Player3');
DECLARE @Hero1Id UNIQUEIDENTIFIER = (SELECT AssetId FROM Asset WHERE AssetName = 'Hero 1');
DECLARE @Hero2Id UNIQUEIDENTIFIER = (SELECT AssetId FROM Asset WHERE AssetName = 'Hero 2');

INSERT INTO PlayerAsset (PlayerId, AssetId) VALUES 
(@Player1Id, @Hero1Id),
(@Player2Id, @Hero2Id),
(@Player3Id, @Hero1Id);
GO

-- =============================================
-- Stored Procedure: GetAssetsByPlayer
-- =============================================
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'GetAssetsByPlayer')
    DROP PROCEDURE GetAssetsByPlayer;
GO

CREATE PROCEDURE GetAssetsByPlayer
AS
BEGIN
    SELECT 
        ROW_NUMBER() OVER (ORDER BY p.PlayerName) AS No,
        p.PlayerName,
        p.[Level] AS Level,
        p.Age,
        a.AssetName
    FROM Player p
    INNER JOIN PlayerAsset pa ON p.PlayerId = pa.PlayerId
    INNER JOIN Asset a ON pa.AssetId = a.AssetId
    ORDER BY p.PlayerName;
END
GO

-- Execute the stored procedure
EXEC GetAssetsByPlayer;
GO

PRINT 'Database BATTLEGAME created successfully!';
