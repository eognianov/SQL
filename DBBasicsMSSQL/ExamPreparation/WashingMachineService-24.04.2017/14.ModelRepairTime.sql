SELECT [mod].ModelId, [mod].[Name], CAST(AVG(DATEDIFF(DAY, job.IssueDate,job.FinishDate)) AS VARCHAR) + ' days' AS [Average Service Time]
FROM dbo.Models [mod]
LEFT JOIN dbo.Jobs job ON [mod].ModelId = job.ModelId
GROUP BY [mod].ModelId, [mod].[Name]
ORDER BY [Average Service Time]