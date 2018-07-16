--Script 2: Include creation date in backup filenames

--1. Variable declaration

DECLARE @BackupFileName varchar(1000)

--2. Specifying backup path and filename

SELECT @BackupFileName = (SELECT ' E:\Backup\AdventureWorks2008_' + 
convert(varchar(500),GetDate(),112) + '.bak') 

--3. Executing the backup command

BACKUP DATABASE AdventureWorks2008 TO DISK=@BackupFileName

--4. Repeat steps 2. and 3. for all other databases

SELECT @BackupFileName = (SELECT ' E:\Backup\AdventureWorks2012_' + 
convert(varchar(500),GetDate(),112) + '.bak') 

BACKUP DATABASE AdventureWorks2012 TO DISK=@BackupFileName

SELECT @BackupFileName = (SELECT ' E:\Backup\AdventureWorks2014_' + 
convert(varchar(500),GetDate(),112) + '.bak') 

BACKUP DATABASE AdventureWorks2014 TO DISK=@BackupFileName