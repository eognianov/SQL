SELECT TOP (5) c.Id, c.[Name] AS City,c.CountryCode AS Country, COUNT(a.Id) AS Accounts
  FROM Cities AS c
  JOIN Accounts AS a ON a.CityId = c.Id
  GROUP BY c.[Name], c.CountryCode, c.Id
  ORDER BY Accounts DESC