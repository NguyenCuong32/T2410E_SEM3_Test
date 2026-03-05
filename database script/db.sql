CREATE DATABASE battlegame;

USE battlegame;
CREATE TABLE Player (
    PlayerId INT AUTO_INCREMENT PRIMARY KEY,
    PlayerName VARCHAR(50),
    FullName VARCHAR(100),
    Age INT,
    Level INT
);
CREATE TABLE Asset (
    AssetId INT AUTO_INCREMENT PRIMARY KEY,
    AssetName VARCHAR(100),
    AssetType VARCHAR(50)
);
CREATE TABLE PlayerAsset (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    PlayerId INT,
    AssetId INT,
    FOREIGN KEY (PlayerId) REFERENCES Player(PlayerId),
    FOREIGN KEY (AssetId) REFERENCES Asset(AssetId)
);