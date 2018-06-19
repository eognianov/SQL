CREATE TABLE Logs
(LogId     INT IDENTITY NOT NULL,
 AccountId INT NOT NULL,
 OldSum    DECIMAL(15, 2),
 NewSum    DECIMAL(15, 2)

 CONSTRAINT PK_Logs
 PRIMARY KEY (LogId),

 CONSTRAINT FK_Logs_Accounts
 FOREIGN KEY (AccountId)
 REFERENCES dbo.Accounts(Id)
)
;
GO


CREATE TRIGGER tr_AddToLogs
ON Accounts
AFTER UPDATE
AS 
BEGIN
	DECLARE @accountId INT = (
	SELECT Id FROM INSERTED ins)
	DECLARE @oldSum DECIMAL = ( SELECT del.Balance FROM DELETED del)
	DECLARE @newSum DECIMAL = (SELECT ins.Balance FROM INSERTED ins)

	INSERT INTO Logs
	(
	    --LogId - this column value is auto-generated
	    AccountId,
	    OldSum,
	    NewSum
	)
	VALUES
	(
	    @accountId,
		@oldSum,
		@newSum
	)
END


