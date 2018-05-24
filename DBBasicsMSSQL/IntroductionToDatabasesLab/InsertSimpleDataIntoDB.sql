INSERT INTO Clients(Id, FirstName, LastName)
VALUES
(1, 'Ivan', 'Ivanov'),
(2, 'Pesho', 'Petrov'),
(3, 'Merry', 'Ivanov')

INSERT INTO AccountTypes(Id, Name)
VALUES
(1,'Savings'),
(2, 'Checkings')

INSERT INTO Accounts (Id, AccountTypeId, ClientId, Balance)
VALUES
(1, 1, 2, 120),
(2, 2, 3, 50.2)
