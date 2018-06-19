CREATE TABLE Logs
(LogId     INT NOT NULL,
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

CREATE TRIGGER tr_ChangeBalance
ON Accounts
AFTER UPDATE
AS
BEGIN
    INSERT INTO Logs (AccountId, OldSum, NewSum)
         SELECT i.AccountHolderId,
                d.Balance,
                i.Balance
           FROM INSERTED i
     INNER JOIN DELETED d
             ON d.AccountHolderId = i.AccountHolderId
END