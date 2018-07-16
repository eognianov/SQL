--Script 3: Backup all non-system databases

--1. Variable declaration

DECLARE @path VARCHAR(500)
DECLARE @name VARCHAR(500)
DECLARE @filename VARCHAR(256)
DECLARE @time DATETIME
DECLARE @year VARCHAR(4)
DECLARE @month VARCHAR(2)
DECLARE @day VARCHAR(2)
DECLARE @hour VARCHAR(2)
DECLARE @minute VARCHAR(2)
DECLARE @second VARCHAR(2)
 
-- 2. Setting the backup path

SET @path = 'F:\Backup\'  

 -- 3. Getting the time values

SELECT @time = GETDATE()
SELECT @year   = (SELECT CONVERT(VARCHAR(4), DATEPART(yy, @time)))
SELECT @month  = (SELECT CONVERT(VARCHAR(2), FORMAT(DATEPART(mm,@time),'00')))
SELECT @day    = (SELECT CONVERT(VARCHAR(2), FORMAT(DATEPART(dd,@time),'00')))
SELECT @hour   = (SELECT CONVERT(VARCHAR(2), FORMAT(DATEPART(hh,@time),'00')))
SELECT @minute = (SELECT CONVERT(VARCHAR(2), FORMAT(DATEPART(mi,@time),'00')))
SELECT @second = (SELECT CONVERT(VARCHAR(2), FORMAT(DATEPART(ss,@time),'00')))

-- 4. Defining cursor operations
 
DECLARE db_cursor CURSOR FOR  
SELECT name 
FROM master.dbo.sysdatabases 
WHERE name NOT IN ('master','model','msdb','tempdb')  -- system databases are excluded

--5. Initializing cursor operations

OPEN db_cursor   
FETCH NEXT FROM db_cursor INTO @name   

WHILE @@FETCH_STATUS = 0   
BEGIN

-- 6. Defining the filename format
   
       SET @fileName = @path + @name + '_' + @year + @month + @day + @hour + 
@minute + @second + '.BAK'  
       BACKUP DATABASE @name TO DISK = @fileName  

 
       FETCH NEXT FROM db_cursor INTO @name   
END   
CLOSE db_cursor   
DEALLOCATE db_cursor