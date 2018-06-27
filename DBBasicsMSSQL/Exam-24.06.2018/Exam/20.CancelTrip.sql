CREATE TRIGGER tr_AccountsDelete ON Trips
INSTEAD OF DELETE
AS
UPDATE t SET CancelDate = GETDATE()
  FROM Trips AS t JOIN DELETED d 
    ON d.Id = t.Id
 WHERE t.CancelDate IS NULL