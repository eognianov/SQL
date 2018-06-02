SELECT CountryName AS [Country Name], IsoCode AS [Iso Code] 
FROM Countries
--full string letters minus replaced string letters(after replacement with '')
WHERE LEN(CountryName) - LEN(REPLACE(CountryName, 'a','')) >= 3
ORDER BY [Iso Code]