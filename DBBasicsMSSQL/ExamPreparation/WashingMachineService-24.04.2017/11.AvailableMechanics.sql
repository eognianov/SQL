SELECT CONCAT(mec.FirstName, ' ', mec.LastName) AS Available 
FROM dbo.Mechanics mec
WHERE mec.MechanicId NOT IN (SELECT job.MechanicId 
							   FROM dbo.Jobs job 
							  WHERE job.Status <> 'Finished' AND job.MechanicId IS NOT NULL)
ORDER BY mec.MechanicId