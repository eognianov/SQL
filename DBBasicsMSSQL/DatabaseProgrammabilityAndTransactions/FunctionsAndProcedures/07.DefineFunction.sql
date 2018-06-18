CREATE FUNCTION ufn_IsWordComprised
(@setOfLetters VARCHAR(50),
 @word         VARCHAR(50)
)
RETURNS BIT
AS
     BEGIN
         DECLARE @count INT= 1;
         DECLARE @currentLetter CHAR;
         WHILE(LEN(@word) >= @count)
             BEGIN
                 SET @currentLetter = SUBSTRING(@word, @count, 1);
                 DECLARE @matchIndex INT= CHARINDEX(@currentLetter, @setOfLetters);
                 IF(@matchIndex = 0)
                     BEGIN
                         RETURN 0;
                     END;
                 SET @count = @count + 1;
             END;
         RETURN 1;
     END;


