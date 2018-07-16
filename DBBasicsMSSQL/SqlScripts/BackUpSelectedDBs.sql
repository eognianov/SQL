--Script 1: Backup selected databases only

BACKUP DATABASE Database01
TO DISK = 'C:\Database01.BAK'
BACKUP DATABASE Database02
TO DISK = 'C:\Database02.BAK'
BACKUP DATABASE Database03
TO DISK = 'C:\Database03.BAK'