CREATE TABLE NotificationEmails (
	Id INT IDENTITY NOT NULL,
	Recipient INT NOT NULL,
	[Subject] NVARCHAR(MAX) NOT NULL,
	[Body] NVARCHAR(MAX) NOT NULL


	CONSTRAINT PK_NotifiactionEmail
	PRIMARY KEY (Id),

	CONSTRAINT FK_NotificationsEmails_Accounts
	FOREIGN KEY (Recipient)
	REFERENCES dbo.Accounts(Id)
)
GO



CREATE TRIGGER tr_CreataEmail ON Logs
AFTER INSERT
AS
     BEGIN
         INSERT INTO NotificationEmails
(Recipient,
 Subject,
 Body
)
                SELECT i.AccountId,
                       CONCAT('Balance change for account: ', i.AccountId),
                       CONCAT('On ', GETDATE(), ' your balance was changed from ', i.OldSum, ' to ', i.NewSum, '.')
                FROM   INSERTED i;
     END;



