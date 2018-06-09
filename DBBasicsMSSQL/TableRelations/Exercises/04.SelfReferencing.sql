CREATE TABLE Teachers(
	TeacherID INT IDENTITY(101,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	ManagerID INT

	CONSTRAINT PK_Teachers
	PRIMARY KEY (TeacherID),
	CONSTRAINT FK_Teachers_ManagerID
	FOREIGN KEY (ManagerID)
	REFERENCES Teachers(TeacherID)
)


INSERT INTO Teachers([Name], ManagerID)
VALUES
('John', NULL),
('Maya', 106),
('Silvia', 106),
('Ted', 105),
('Mark', 101),
('Greta', 101)
