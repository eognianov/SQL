CREATE FUNCTION udf_GetAvailableRoom(@HotelId INT, @Date DATE, @People INT)
RETURNS NVARCHAR(200)
AS
BEGIN
	DECLARE @result VARCHAR (200) = (SELECT res.Result FROM (
													SELECT TOP (1) r.Id, r.[Type], r.Beds, (h.BaseRate+r.Price)*@People AS [Total Price], CONCAT('Room ',CAST(r.Id AS VARCHAR(10)),': ',r.[Type],' (',CAST(r.Beds AS VARCHAR(2)),' beds) - $',CAST((h.BaseRate+r.Price)*@People AS VARCHAR(20))) AS Result FROM Hotels AS h
													JOIN Rooms AS r ON r.HotelId = h.Id
													JOIN Trips AS t ON t.RoomId = r.Id
													WHERE r.Beds >= @People AND h.Id = @HotelId
													ORDER BY [Total Price] DESC) AS res)

	IF(@result IS NULL)
	BEGIN
		RETURN 'No rooms available'
	END

	RETURN @result
END

DECLARE @HotelId INT = 112;
DECLARE @Date DATE = '2011-12-17';
DECLARE @People INT = 2;

SELECT res.Result FROM (
SELECT TOP (1) r.Id, r.[Type], r.Beds, (h.BaseRate+r.Price)*@People AS [Total Price], CONCAT('Room ',CAST(r.Id AS VARCHAR(10)),': ',r.[Type],' (',CAST(r.Beds AS VARCHAR(2)),' beds) - $',CAST((h.BaseRate+r.Price)*@People AS VARCHAR(20))) AS Result FROM Hotels AS h
JOIN Rooms AS r ON r.HotelId = h.Id
JOIN Trips AS t ON t.RoomId = r.Id
WHERE r.Beds >= @People AND h.Id = @HotelId
ORDER BY [Total Price] DESC) AS res


SELECT dbo.udf_GetAvailableRoom(112, '2011-12-17', 2)


CONCAT('Room ',CAST(r.Id AS VARCHAR(10)),': ',r.[Type],' (',CAST(r.Beds AS VARCHAR(2)),' beds) - $',CAST((h.BaseRate+r.Price)*@People AS VARCHAR(20)),')')