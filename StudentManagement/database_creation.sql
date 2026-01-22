-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'StudentManagement')
BEGIN
    CREATE DATABASE StudentManagement;
END
GO

USE StudentManagement;
GO

-- Drop existing tables if they exist (in correct order due to foreign keys)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Enrollments]') AND type in (N'U'))
BEGIN
    DROP TABLE Enrollments;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Students]') AND type in (N'U'))
BEGIN
    DROP TABLE Students;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Courses]') AND type in (N'U'))
BEGIN
    DROP TABLE Courses;
END
GO

-- Create Student Table with single Name field
CREATE TABLE Students (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    DateOfBirth DATETIME2 NOT NULL,
    EnrollmentDate DATETIME2 NOT NULL
);
GO

-- Create Course Table
CREATE TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(200) NOT NULL,
    Credits INT NOT NULL,
    Department NVARCHAR(100)
);
GO

-- Create Enrollment Table
CREATE TABLE Enrollments (
    EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
    EnrollmentDate DATETIME2 NOT NULL,
    Grade DECIMAL(4, 2),
    StudentID INT NOT NULL,
    CourseID INT NOT NULL,
    CONSTRAINT FK_Enrollments_Students FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT FK_Enrollments_Courses FOREIGN KEY (CourseID) REFERENCES Courses(CourseID) ON DELETE CASCADE
);
GO

-- Insert Sample Data
INSERT INTO Students (Name, DateOfBirth, EnrollmentDate)
VALUES 
('Nguyen Van A', '2000-01-15', SYSDATETIME()),
('Tran Thi B', '2001-05-20', SYSDATETIME()),
('Pham Van C', '2002-03-10', SYSDATETIME());

INSERT INTO Courses (CourseName, Credits, Department)
VALUES 
('Mathematics', 3, 'Science'),
('History', 2, 'Social Studies'),
('Physics', 4, 'Science'),
('English', 3, 'Languages');

INSERT INTO Enrollments (EnrollmentDate, Grade, StudentID, CourseID)
VALUES 
(SYSDATETIME(), 8.5, 1, 1),
(SYSDATETIME(), 9.0, 2, 2),
(SYSDATETIME(), 7.5, 1, 2),
(SYSDATETIME(), NULL, 3, 3);
GO

-- Verify data
SELECT * FROM Students;
SELECT * FROM Courses;
SELECT * FROM Enrollments;
GO
