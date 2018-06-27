SELECT 
CASE 
	WHEN (MiddleName IS NULL) THEN CONCAT(FirstName, ' ', LastName)
	ELSE CONCAT(FirstName,' ', MiddleName,' ', LastName) 
END AS [Full Name], YEAR(BirthDate) AS BirthYear
 FROM Accounts
WHERE YEAR(BirthDate) > 1991
ORDER BY YEAR(BirthDate) DESC, [Full Name]