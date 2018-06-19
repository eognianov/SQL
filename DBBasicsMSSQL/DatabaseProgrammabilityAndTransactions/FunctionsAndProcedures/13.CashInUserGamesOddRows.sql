CREATE FUNCTION ufn_CashInUsersGames
(@gameName VARCHAR(50)
)
RETURNS TABLE
AS
     RETURN
(
    SELECT    SUM(e.Cash) AS [SumCash]
    FROM
(
    SELECT g.Id,
           ug.Cash,
           ROW_NUMBER() OVER(ORDER BY ug.Cash DESC) AS [RowNumber]
    FROM   Games AS g
           JOIN UsersGames AS ug ON ug.GameId = g.Id
    WHERE  @gameName = g.Name
) AS e
    WHERE e.RowNumber % 2 = 1
);