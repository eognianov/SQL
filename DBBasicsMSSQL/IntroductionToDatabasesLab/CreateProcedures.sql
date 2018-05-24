CREATE PROCEDURE p_AddAccount @Id INT, @ClientId INT, @AccountType INT AS
INSERT INTO Accounts(Id, ClientId,AccountTypeId)
VALUES (@Id, @ClientId,@AccountType)
GO

--p_AddAccount 5,2,2

CREATE PROC p_Deposit @AccountId INT, @Amount DECIMAL (15,2) AS
UPDATE Accounts
SET Balance+=@Amount
WHERE Id = @AccountId
GO

--p_Deposit 2, 100

CREATE PROC p_Withdraw @AccountId INT, @Amount DECIMAL (15,2) AS
BEGIN
	DECLARE @OldBalance DECIMAL(15,2)
	SELECT @OldBalance = Balance FROM Accounts WHERE Id = @AccountId
	IF (@OldBalance - @Amount>=0)
	BEGIN
		UPDATE Accounts
		SET Balance -= @Amount
		WHERE Id = @AccountId
	END
	ELSE
	BEGIN
		RAISERROR('Insufficient funds', 10,1)
	END
END

p_Withdraw 5, 150