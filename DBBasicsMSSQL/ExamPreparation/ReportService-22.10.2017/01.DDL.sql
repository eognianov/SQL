CREATE DATABASE ReportService
GO

USE ReportService
GO

CREATE TABLE Users(
	Id INT IDENTITY NOT NULL,
	Username NVARCHAR(30) UNIQUE NOT NULL,
	[Password] NVARCHAR(50) NOT NULL,
	[Name] NVARCHAR(50),
	Gender CHAR(1) CHECK(Gender IN('M','F')),
	BirthDate DATETIME,
	Age INT,
	Email NVARCHAR(50) NOT NULL

	CONSTRAINT PK_Users
	PRIMARY KEY (Id)
)

CREATE TABLE Departments(
	Id INT IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL

	CONSTRAINT PK_Departaments
	PRIMARY KEY (Id)
)

CREATE TABLE Employees (
	Id INT IDENTITY NOT NULL,
	FirstName NVARCHAR(25),
	LastName NVARCHAR (25),
	Gender CHAR(1) CHECK(Gender IN ('M','F')),
	BirthDate DATETIME,
	Age INT,
	DepartmentId INT NOT NULL


	CONSTRAINT PK_Employees
	PRIMARY KEY (Id),

	CONSTRAINT FK_Employees_Departments
	FOREIGN KEY (DepartmentId)
	REFERENCES Departments(Id)
)

CREATE TABLE Categories(
	Id INT IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	DepartmentId INT NOT NULL,

	CONSTRAINT PK_Categories
	PRIMARY KEY (Id),

	CONSTRAINT FK_Categories_Departments
	FOREIGN KEY (DepartmentId)
	REFERENCES Departments(Id)
)

CREATE TABLE [Status](
	Id INT IDENTITY NOT NULL,
	Label VARCHAR(30) NOT NULL

	CONSTRAINT PK_Status
	PRIMARY KEY (Id)
)

CREATE TABLE Reports(
	Id INT IDENTITY NOT NULL,
	CategoryId INT NOT NULL,
	StatusId INT NOT NULL,
	OpenDate DATETIME NOT NULL,
	CloseDate DATETIME,
	[Description] NVARCHAR(200),
	UserId INT NOT NULL,
	EmployeeId INT

	
	CONSTRAINT PK_Reports
	PRIMARY KEY (Id),

	CONSTRAINT FK_Repots_Categories
	FOREIGN KEY (CategoryId)
	REFERENCES Categories(Id),

	CONSTRAINT FK_Reports_Status
	FOREIGN KEY (StatusId)
	REFERENCES [Status](Id),

	CONSTRAINT FK_Reports_Users
	FOREIGN KEY (UserId)
	REFERENCES Users(Id),
	
	CONSTRAINT FK_Reports_Employees
	FOREIGN KEY (EmployeeId)
	REFERENCES Employees(Id) 
)