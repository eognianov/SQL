CREATE PROCEDURE usp_SwitchRoom(@TripId INT, @TargetRoomId INT)
AS
BEGIN
	DECLARE @OldHotelId INT = (SELECT r.HotelId FROM Trips AS t
JOIN Rooms AS r ON r.Id  = t.RoomId
WHERE t.Id = @TripId)
	DECLARE @NewHotelId INT = (SELECT r.HotelId FROM Rooms AS r WHERE r.Id = @TargetRoomId)
	DECLARE @NewRoomBedsCount INT = (SELECT r.Beds FROM Rooms AS r WHERE r.Id = @TargetRoomId)
	DECLARE @BedsNeeded INT = (SELECT COUNT(t.AccountId) FROM AccountsTrips AS t WHERE t.TripId = @TripId)
	IF(@OldHotelId <> @NewHotelId)
	BEGIN
		RAISERROR('Target room is in another hotel!', 16, 1)
        RETURN
	END

	IF(@NewRoomBedsCount < @BedsNeeded)
	BEGIN
		RAISERROR('Not enough beds in target room!', 16, 1)
        RETURN
	END

	UPDATE Trips
	SET RoomId = @TargetRoomId
	WHERE Id = @TripId
END

EXEC usp_SwitchRoom 10, 11

SELECT RoomId FROM Trips WHERE Id = 10