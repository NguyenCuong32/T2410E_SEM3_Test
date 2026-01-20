CREATE DATABASE ComicSystem;
GO

USE ComicSystem;
GO
CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(15) NOT NULL,
    RegistrationDate DATETIME NOT NULL DEFAULT GETDATE()
);
CREATE TABLE ComicBooks (
    ComicBookID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255) NOT NULL,
    PricePerDay DECIMAL(10,2) NOT NULL
);
CREATE TABLE Rentals (
    RentalID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    RentalDate DATETIME NOT NULL,
    ReturnDate DATETIME NULL,
    Status NVARCHAR(50) NOT NULL,

    CONSTRAINT FK_Rentals_Customers
        FOREIGN KEY (CustomerID)
        REFERENCES Customers(CustomerID)
);
CREATE TABLE RentalDetails (
    RentalDetailID INT IDENTITY(1,1) PRIMARY KEY,
    RentalID INT NOT NULL,
    ComicBookID INT NOT NULL,
    Quantity INT NOT NULL,
    PricePerDay DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_RentalDetails_Rentals
        FOREIGN KEY (RentalID)
        REFERENCES Rentals(RentalID),

    CONSTRAINT FK_RentalDetails_ComicBooks
        FOREIGN KEY (ComicBookID)
        REFERENCES ComicBooks(ComicBookID)
);


INSERT INTO Customers (FullName, PhoneNumber, RegistrationDate)
VALUES ('Nguyen Hung', '0123456789', '2024-01-01');


INSERT INTO ComicBooks (Title, Author, PricePerDay)
VALUES 
    ('Conan', 'Gosho Aoyama', 5000),
    ('Doraemon', 'Fujiko F. Fujio', 4000);


INSERT INTO Rentals (CustomerID, RentalDate, ReturnDate, Status)
VALUES 
    (1, '2024-10-01', '2024-10-10', 'Completed'),
    (1, '2024-10-01', '2024-10-20', 'Completed');

INSERT INTO RentalDetails (RentalID, ComicBookID, Quantity, PricePerDay)
VALUES 
    (1, 1, 1, 5000),  -- Conan, Quantity 1
    (2, 2, 3, 4000);  -- Doraemon, Quantity 3

