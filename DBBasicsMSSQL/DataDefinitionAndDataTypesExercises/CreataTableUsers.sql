CREATE TABLE Users(
	Id BIGINT PRIMARY KEY IDENTITY,
	Username VARCHAR(30) NOT NULL UNIQUE,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX),
	LastLoginTime DATETIME,
	IsDeleted BIT 
)

INSERT INTO Users (Username, [Password], ProfilePicture, LastLoginTime, IsDeleted) VALUES 
('Stamat', '123', NULL, CONVERT(datetime,'22-05-2018',103), 0),
('Gosho', '12312323', NULL, CONVERT(datetime,'12-06-2018',103), 0),
('Pesho', '2313123', NULL, CONVERT(datetime,'14-05-2018',103), 0),
('Ivan', '132123', NULL, CONVERT(datetime,'22-05-2018',103), 0),
('Emo', '13213123', NULL, CONVERT(datetime,'22-05-2018',103), 0)


ALTER TABLE Users
ADD CONSTRAINT CHK_ProfilePicture CHECK (DATALENGTH(ProfilePicture)<=900*1024)